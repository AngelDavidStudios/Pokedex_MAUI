using System.Collections.ObjectModel;
using System.Diagnostics;
using Pokedex_MAUI.Helpers;
using Pokedex_MAUI.Interfaces;
using Pokedex_MAUI.MVVM.Models;
using Pokedex_MAUI.Services;

namespace Pokedex_MAUI.MVVM.ViewModels;

public class VMHome: VMBase, IInitializeAsync
{
    private readonly IRestService _service;
    private readonly LiteDbService<PokemonModel> _dbServicePokemons;
    private readonly LiteDbService<FiltersModel> _dbServiceFilters;
    private readonly LiteDbService<ResourceListModel> _dbServiceResourceList;
    public ObservableCollection<PokemonModel> Pokemons;
    public ObservableCollection<PokemonModel> PokemonsHelp;
    private IEnumerable<PokemonModel> DbPokemons;
    public FiltersModel Filters;
    public ResourceListModel ResourceList;
    public GenerationModel Generation;
    
    public double[] LockStates;
    public bool FilterGenerationVisible;
    public bool FilterSortVisible;
    public bool FiltersVisible;
    public string SearchParameter;
    public int Offset { get; set; }
    public int Amount
    {
        get => Offset + Constants.PAGE_LIMIT;
    }
    public int ItemTreshold;

    public Task Initialization { get; }
    
    public VMHome(INavigation navigation, IRestService restService) : base(navigation)
    {
        ItemTreshold = 1;
        _service = restService;
        _dbServicePokemons = new LiteDbService<PokemonModel>();
        _dbServiceFilters = new LiteDbService<FiltersModel>();
        _dbServiceResourceList = new LiteDbService<ResourceListModel>();
        ResourceList = new ResourceListModel();
        Generation = new GenerationModel();
        Pokemons = new ObservableCollection<PokemonModel>();
        PokemonsHelp = new ObservableCollection<PokemonModel>();
        DbPokemons = new List<PokemonModel>();
        Initialization = InitializeAsync();
    }
    
    private async Task InitializeAsync()
    {
        var mockPokemonList = PokemonHelper.GetMockPokemonList(Offset,Amount);
        foreach (var pokemon in mockPokemonList)
        {
            Pokemons.Add(pokemon);
        }
        await GetResourceListAsync(string.Format(Constants.BASE_URL_RESOURCE_LIST, 0, Constants.POKEMON_LIMIT));
        await LoadPokemonsAsync();
    }

    private async Task LoadPokemonsAsync()
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
            
            DbPokemons = _dbServicePokemons.FindAll();

            if (DbPokemons.Any())
            {
                // SortPokemonDbList();
            }
            
            if(!DbPokemons.Any() || DbPokemons.Count() < Constants.POKEMON_LIMIT)
            {
                Pokemons = new ObservableCollection<PokemonModel>(await GetPokemonListAsync());
                if (Pokemons.Any())
                    LiteDbHelper.UpdatePokemonListDataBase(_dbServicePokemons, Pokemons);
            }
            else
            {
                await Task.Delay(5000);
                Pokemons = new ObservableCollection<PokemonModel>(DbPokemons.Take(Constants.PAGE_LIMIT));
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine("Error", e.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task GetResourceListAsync(string url)
    {
        try
        {
            if (!InternetConnectivity())
                return;

            var dbResourceList = _dbServiceResourceList.FindAll();
            if (!dbResourceList.Any())
            {
                var resourceList = await _service.GetResourceAsync<ResourceListModel>(url);

                if (resourceList == null)
                    return;

                ResourceList = resourceList;
                LiteDbHelper.UpdateResourceListDataBase(_dbServiceResourceList, ResourceList);
            }
            else
                ResourceList = dbResourceList.FirstOrDefault();

            // SortResourceList();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error", ex.Message);
        }
    }

    private async Task<List<PokemonModel>> GetPokemonListAsync()
    {
        try
        {
            List<PokemonModel> pokemonsList = new List<PokemonModel>();

            foreach (var item in ResourceList.Results.Skip(Offset).Take(Constants.PAGE_LIMIT))
            {
                var pokemon = await _service.GetResourceAsync<PokemonModel>(item.Url);

                if (pokemon != null)
                {
                    pokemon.Stats = PokemonHelper.GetFullStats(pokemon.Stats);
                    pokemon.TotalStat = PokemonHelper.GetTotalStats(pokemon.Stats);
                    pokemon.Meters = PokemonHelper.DecimetresToMeters(pokemon.Height);
                    pokemon.Inches = PokemonHelper.MetersToInches(pokemon.Meters);
                    pokemon.Kilograms = PokemonHelper.HectogramsToKilograms(pokemon.Weight);
                    pokemon.Pounds = PokemonHelper.KilogramsToPounds(pokemon.Kilograms);

                    // pokemon.TypeDefenses = await GetPokemonTypeDefensesAsync(pokemon);
                    pokemon.Weaknesses = PokemonHelper.GetPokemonWeaknesses(pokemon.TypeDefenses);

                    pokemonsList.Add(pokemon);
                }
            }

            return pokemonsList;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error", ex.Message);
        }

        return new List<PokemonModel>();
    }

}