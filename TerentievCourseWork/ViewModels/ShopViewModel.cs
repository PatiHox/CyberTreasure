using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro;
using Microsoft.Xaml.Behaviors.Core;
using TerentievCourseWork.Models;

namespace TerentievCourseWork.ViewModels;

public class ShopViewModel : Screen
{
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
        // TODO: Implement loading genres from service
        var genres = new List<GenreModel>
        {
            new() { Name = "Action" },
            new() { Name = "Adventure" },
            new() { Name = "RPG" },
        };
        InitializeGenreItems(genres);
        return Task.FromResult(true);
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