using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using Microsoft.Xaml.Behaviors.Core;
using TerentievCourseWork.Models;
using TerentievCourseWork.Services;
using TerentievCourseWork.Services._Impl;

namespace TerentievCourseWork.ViewModels;

public class ShopViewModel : Screen
{
    #region Private fields

    private readonly IProductDataProvider _productDataProvider;
    private int _selectedIndex;

    #endregion
    
    #region Public properties

    public List<GenreButtonViewModel> GenreButtons { get; init; } = new();

    public int SelectedIndex
    {
        get => _selectedIndex;
        set
        { 
            _selectedIndex = value;
            NotifyOfPropertyChange(nameof(SelectedGenreGames));
        }
    }

    public List<GameBannerViewModel> SelectedGenreGames => GenreButtons[SelectedIndex].GenreModel.Games.Select(game => new GameBannerViewModel(game)).ToList();
    public List<GameBannerViewModel> RecommendedGames { get; init; } = new();
    
    public Visibility RecommendedSectionVisibility => RecommendedGames.Count > 0 ? Visibility.Visible : Visibility.Collapsed;

    #endregion

    #region Lifecycle methods

    protected override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        _productDataProvider.Genres.CollectionChanged += GenreButtonsOnCollectionChanged;

        InitializeGenreItems(_productDataProvider.Genres.ToList());
        return Task.FromResult(true);
    }

    private void GenreButtonsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        UpdateGenreButtons(_productDataProvider.Genres.ToList());
        
        NotifyOfPropertyChange(nameof(GenreButtons));
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
        if (obj is GenreButtonViewModel { Parent: ShopViewModel shop } genreButton)
        {
            var index = -1;
            
            for (int i = 0; i < shop.GenreButtons.Count; i++)
            {
                if (shop.GenreButtons[i].GenreModel.Id == genreButton.GenreModel.Id)
                {
                    index = i;
                    break;
                }
            }
            
            if (index != shop.SelectedIndex)
            {
                shop.SetSelectedIndex(index);
            }
        }
    }

    #endregion
    
    #region Private methods

    private void UpdateGenreButtons(List<GenreModel> genreModels)
    {
        long selectedId = -1;
        if(SelectedIndex >= 0)
            selectedId = GenreButtons[SelectedIndex].GenreModel.Id;
        
        GenreButtons.Clear();
        
        foreach (var viewModel in genreModels.Select(item => new GenreButtonViewModel(item, new ActionCommand(GenreButtonAction), this)))
        {
            GenreButtons.Add(viewModel);
        }

        var newIndexOfPreviouslySelected = GenreButtons.IndexOf(GenreButtons.FirstOrDefault(x => x.GenreModel.Id == selectedId, null));
        SetSelectedIndex(newIndexOfPreviouslySelected == -1 ? 0 : newIndexOfPreviouslySelected);
        
        NotifyOfPropertyChange(nameof(GenreButtons));
    }
    
    private void InitializeGenreItems(List<GenreModel> genreModels)
    {
        foreach (var viewModel in genreModels.Select(item => new GenreButtonViewModel(item, new ActionCommand(GenreButtonAction), this)))
        {
            GenreButtons.Add(viewModel);
        }
        
        SetSelectedIndex(0);
        
        NotifyOfPropertyChange(nameof(GenreButtons));
    }

    private void SetSelectedIndex(int index)
    {
        SelectedIndex = index;
        NotifyOfPropertyChange(nameof(SelectedIndex));
    }
    
    #endregion
}