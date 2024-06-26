using System.Windows.Input;
using Pokedex_MAUI.Helpers;
using Pokedex_MAUI.Interfaces;
using Pokedex_MAUI.MVVM.Models;
using Pokedex_MAUI.Services;

namespace Pokedex_MAUI.MVVM.ViewModels;

public class VMPokemonDetail : VMBase
{
    private readonly IRestService _service;
    private readonly LiteDbService<PokemonModel> _pokemonDbService;
    private readonly LiteDbService<PokemonSpeciesModel> _pokemonSpeciesDbService;
    private PokemonModel _pokemon;
    private PokemonSpeciesModel _pokemonSpecies;

    public Task Initialization { get; }

    public PokemonModel Pokemon
    {
        get => _pokemon;
        set => SetProperty(ref _pokemon, value);
    }

    public PokemonSpeciesModel PokemonSpecies
    {
        get => _pokemonSpecies;
        set => SetProperty(ref _pokemonSpecies, value);
    }

    public ICommand NavigateToBackCommand { get; }

    public VMPokemonDetail(INavigation navigation, IRestService restService, PokemonModel pokemon) : base(navigation)
    {
        _service = restService;
        _pokemonDbService = new LiteDbService<PokemonModel>();
        _pokemonSpeciesDbService = new LiteDbService<PokemonSpeciesModel>();
        Pokemon = pokemon;
        PokemonSpecies = new PokemonSpeciesModel();

        NavigateToBackCommand = new Command(async () => await ExecuteNavigateToBackCommand());
        Initialization = InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            if (!InternetConnectivity())
            {
                return;
            }

            PokemonSpecies = PokemonHelper.GetMockPokemonSpecies();
            Pokemon.Abilities.ToList().ForEach(i => i.IsBusy = true);
            Pokemon.Weaknesses.ToList().ForEach(i => i.IsBusy = true);
            Pokemon.Stats.ToList().ForEach(i => i.IsBusy = true);
            Pokemon.TypeDefenses.ToList().ForEach(i => i.IsBusy = true);

            var dbSpecies = _pokemonSpeciesDbService.FindById(Pokemon.Id);

            if (dbSpecies == null)
            {
                PokemonSpecies = await GetPokemonSpeciesAsync();
                Pokemon.Abilities.ToList().ForEach(i => i.IsBusy = false);
                Pokemon.Weaknesses.ToList().ForEach(i => i.IsBusy = false);
                Pokemon.Stats.ToList().ForEach(i => i.IsBusy = false);
                Pokemon.TypeDefenses.ToList().ForEach(i => i.IsBusy = false);

                if (PokemonSpecies != null)
                    LiteDbHelper.UpdatePokemonSpeciesDataBase(_pokemonSpeciesDbService, PokemonSpecies);
            }
            else
            {
                await Task.Delay(8000);

                PokemonSpecies = dbSpecies;
                Pokemon.Abilities.ToList().ForEach(i => i.IsBusy = false);
                Pokemon.Weaknesses.ToList().ForEach(i => i.IsBusy = false);
                Pokemon.Stats.ToList().ForEach(i => i.IsBusy = false);
                Pokemon.TypeDefenses.ToList().ForEach(i => i.IsBusy = false);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error en Initialize Async", ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task<PokemonSpeciesModel> GetPokemonSpeciesAsync()
    {
        try
        {
            var species =
                await _service.GetResourceByNameAsync<PokemonSpeciesModel>(Pokemon.Species.ApiEndpoint,
                    Pokemon.Species.Name);

            if (species != null)
            {
                species.EggGroupsDescription = PokemonHelper.EggGroupsToDescription(species.EggGroups);
                species.FlavorText = PokemonHelper.FlavorTextsToDescription(species.FlavorTextEntries);
                species.GenusDescription = PokemonHelper.GeneraToDescription(species.Genera);
                species.GenderDescription = PokemonHelper.GenderRateToDescription(species.GenderRate);
                species.CaptureProbability =
                    PokemonHelper.CalculateCatchRateProbability(Pokemon.Stats, species.CaptureRate);
                species.GrowthRateDescription = PokemonHelper.GrowthRateToDescription(species.GrowthRate);
                species.MaxEggSteps = PokemonHelper.CalculateMaxEggSteps(species.HatchCounter);
                species.MinEggSteps = PokemonHelper.CalculateMinEggSteps(species.MaxEggSteps);
                species.EvYield = PokemonHelper.GetEvYield(Pokemon.Stats);

                species.Locations = await GetPokemonLocationDescriptionAsync(species.PokedexNumbers);
                species.Evolutions = await GetPokemonEvolutionChainAsync(species);

                return species;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error en Obtener Species Pokemon", ex.Message);
        }

        return null;
    }

    private async Task<IEnumerable<LocationModel>> GetPokemonLocationDescriptionAsync(IEnumerable<PokemonSpeciesDexEntryModel> pokedexNumbers)
    {
        List<LocationModel> locations = new List<LocationModel>();

        try
        {
            foreach (var item in pokedexNumbers)
            {
                if (item.Pokedex.Name.ToLower() == "national")
                    continue;

                var pokedex =
                    await _service.GetResourceByNameAsync<PokedexModel>(item.Pokedex.ApiEndpoint,
                        item.Pokedex.Name.ToLower());

                if (pokedex == null || !pokedex.Descriptions.Any())
                    continue;

                var versions = await GetPokemonVersionGroupAsync(pokedex.VersionGroups);

                locations.Add(PokemonHelper.GetLocation(item.EntryNumber, versions, pokedex));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error en obtener la descipcion de Localizacion", ex.Message);
        }

        return locations;
    }

    private async Task<IEnumerable<VersionModel>> GetPokemonVersionGroupAsync(IEnumerable<VersionGroupModel> versionGroups)
    {
        List<VersionModel> versions = new List<VersionModel>();

        try
        {
            foreach (var versionGroup in versionGroups)
            {
                var response =
                    await _service.GetResourceByNameAsync<VersionGroupModel>(versionGroup.ApiEndpoint,
                        versionGroup.Name.ToLower());

                if (response != null)
                    versions.AddRange(response.Versions);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error en obtener Versiones", ex.Message);
        }

        return versions;
    }

    private async Task<IEnumerable<EvolutionModel>> GetPokemonEvolutionChainAsync(PokemonSpeciesModel species)
    {
        try
        {
            var evolutionChain = await _service.GetResourceAsync<EvolutionChainModel>(species.EvolutionChain.Url);

            if (evolutionChain != null)
                return await GetPokemonEvolutions(evolutionChain.Chain, new List<EvolutionModel>());
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error en obtener Cadena Evolutiva", ex.Message);
        }

        return new List<EvolutionModel>();
    }

    private async Task<IEnumerable<EvolutionModel>> GetPokemonEvolutions(ChainLinkModel chain, List<EvolutionModel> evolutions)
    {
        try
        {
            var pokemon = await GetEvolutionPokemon(chain);

            if (pokemon != null)
            {
                EvolutionModel evolution = new EvolutionModel();
                evolution.Id = pokemon.Id;
                evolution.Name = pokemon.NameFirstCharUpper;
                evolution.Image = pokemon.ImageUrl;
                evolution.IsBaby = chain.IsBaby;

                if (chain.EvolvesTo.Any())
                {
                    foreach (var item in chain.EvolvesTo)
                    {
                        evolution.EnvolvesToMinLevel = item.EvolutionDetails.Any()
                            ? item.EvolutionDetails.Select(s => s.MinLevel).FirstOrDefault().HasValue
                                ? item.EvolutionDetails.Select(s => s.MinLevel).FirstOrDefault().Value.ToString()
                                : "???"
                            : "???";

                        evolution.EnvolvesToName = item.Species.NameFirstCharUpper;
                        evolution.HasEvolution = true;

                        var nextEvolution = await GetPokemonEvolutions(item, evolutions);

                        evolution.EnvolvesToId = nextEvolution.FirstOrDefault().Id;
                        evolution.EnvolvesToImage = nextEvolution.FirstOrDefault().Image;
                        evolutions.Add(evolution);

                        var lastEvolution = evolutions.Where(w => !w.HasEvolution).FirstOrDefault();
                        if (lastEvolution != null)
                            evolutions.Remove(lastEvolution);
                    }
                }
                else
                    evolutions.Add(evolution);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error en obtener evoluciones de pokemons", ex.Message);
        }

        return evolutions?.OrderByDescending(o => o.IsBaby).ThenBy(o => o.Id).ToList();
    }
    
    private async Task<PokemonModel> GetEvolutionPokemon(ChainLinkModel evolution)
    {
        PokemonModel pokemon = new PokemonModel();

        try
        {
            pokemon = _pokemonDbService.FindAll().Where(s => s.Name == evolution.Species.Name).FirstOrDefault();

            if (pokemon == null)
                pokemon = await _service.GetResourceByNameAsync<PokemonModel>(Constants.ENDPOINT_POKEMON, evolution.Species.Name);

            return pokemon;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error en Obtener Evolucion de Pokemon", ex.Message);
        }

        return pokemon;
    }
    
    private async Task ExecuteNavigateToBackCommand()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error en cerrar el Modal", ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }
}