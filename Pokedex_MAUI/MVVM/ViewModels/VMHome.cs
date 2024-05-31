using System.Collections.ObjectModel;
using System.Diagnostics;
using Pokedex_MAUI.Helpers;
using Pokedex_MAUI.Interfaces;
using Pokedex_MAUI.MVVM.Models;
using Pokedex_MAUI.Services;
using PropertyChanged;

namespace Pokedex_MAUI.MVVM.ViewModels;

[AddINotifyPropertyChangedInterface]
public class VMHome
{
    private INavigation _Navigation;
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
    
    public VMHome(INavigation navigation, IRestService restService)
    {
        _Navigation = navigation;
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
        Task.Run(() => InitializeAsync());
    }
    
    private async Task InitializeAsync()
    {
        try
        {
            var mockPokemonList = PokemonHelper.GetMockPokemonList(Offset,Amount);
            foreach (var pokemon in mockPokemonList)
            {
                Pokemons.Add(pokemon);
            }
            await GetResourceListAsync(string.Format(Constants.BASE_URL_RESOURCE_LIST, 0, Constants.POKEMON_LIMIT));
            await LoadPokemonsAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in InitializeAsync: {ex.Message}");
        }
    }

    private async Task LoadPokemonsAsync()
    {

        try
        {
            
            DbPokemons = _dbServicePokemons.FindAll();

            if (DbPokemons.Any())
            {
                SortPokemonDbList();
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
    }

    private async Task GetResourceListAsync(string url)
    {
        try
        {
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

            SortResourceList();
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

    private void SortPokemonDbList()
    {
        if (!Filters.Orders.Any(a => a.Selected == true))
            return;

        var sort = Filters.Orders.Where(w => w.Selected == true).FirstOrDefault().Sort;

        switch (sort)
        {
            case Enums.SortEnum.Ascending:
                DbPokemons = DbPokemons.OrderBy(o => o.Id);
                break;
            case Enums.SortEnum.Descending:
                DbPokemons = DbPokemons.OrderByDescending(o => o.Id);
                break;
            case Enums.SortEnum.AlphabeticalAscending:
                DbPokemons = DbPokemons.OrderBy(o => o.Name);
                break;
            case Enums.SortEnum.AlphabeticalDescending:
                DbPokemons = DbPokemons.OrderByDescending(o => o.Name);
                break;
            default:
                DbPokemons = DbPokemons.OrderBy(o => o.Id);
                break;
        }
    }

    private void SortResourceList()
    {
        if (!Filters.Orders.Any(a => a.Selected == true))
            return;

        var sort = Filters.Orders.Where(w => w.Selected == true).FirstOrDefault().Sort;

        switch (sort)
        {
            case Enums.SortEnum.Ascending:
                ResourceList.Results = ResourceList.Results.OrderBy(o => o.Id);
                break;
            case Enums.SortEnum.Descending:
                ResourceList.Results = ResourceList.Results.OrderByDescending(o => o.Id);
                break;
            case Enums.SortEnum.AlphabeticalAscending:
                ResourceList.Results = ResourceList.Results.OrderBy(o => o.Name);
                break;
            case Enums.SortEnum.AlphabeticalDescending:
                ResourceList.Results = ResourceList.Results.OrderByDescending(o => o.Name);
                break;
            default:
                ResourceList.Results = ResourceList.Results.OrderBy(o => o.Id);
                break;
        }

        LiteDbHelper.UpdateResourceListDataBase(_dbServiceResourceList, ResourceList);
    }

}