using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Microsoft.Xaml.Behaviors.Core;
using TerentievCourseWork.Models;
using TerentievCourseWork.Services;

namespace TerentievCourseWork.ViewModels;

public class ShopViewModel : Screen
{
    #region Private fields

    private readonly IProductDataProvider _productDataProvider;

    #endregion
    
    #region Public properties

    public ObservableCollection<GenreButtonViewModel> GenreButtons { get; set; } = new();
    public List<GameModel> SelectedGenreGames => GenreButtons
        .Select<GenreButtonViewModel, Tuple<bool, IEnumerable<GameModel>>>(item => new(item.IsSelected, item.GenreModel.Games))
        .FirstOrDefault(tuple => tuple.Item1, new Tuple<bool, IEnumerable<GameModel>>(true, new List<GameModel>()))
        .Item2
        .ToList();

    #endregion

    #region Lifecycle methods

    protected override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        InitializeGenreItems(_productDataProvider.GetGenres().ToList());
        return Task.FromResult(true);
    }

    #endregion

    #region Constructor

    public ShopViewModel(IProductDataProvider productDataProvider)
    {
        _productDataProvider = productDataProvider;
    }

    #endregion

    #region Handlers
    
    private static void GenreButtonAction(object obj)
    {
        if (obj is GenreButtonViewModel { Parent: ShopViewModel shop, IsSelected: false } genreButton)
        {
            SelectGenre(genreButton.IndexInList, shop);
        }
    }

    #endregion
    
    #region Private methods

    private void InitializeGenreItems(List<GenreModel> genreModels)
    {
        foreach (var viewModel in genreModels.Select((item, index) =>
                     new GenreButtonViewModel(item, index == 0, new ActionCommand(GenreButtonAction), index, this)))
        {
            GenreButtons.Add(viewModel);
        }
    }

    private static void SelectGenre(int indexInList, ShopViewModel shopViewModel)
    {
        foreach (var genreButton in shopViewModel.GenreButtons)
        {
            genreButton.IsSelected = genreButton.IndexInList == indexInList;
        }
    }
    
    #endregion
}