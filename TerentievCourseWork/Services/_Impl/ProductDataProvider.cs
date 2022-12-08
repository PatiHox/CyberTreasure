using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using TerentievCourseWork.Models;

namespace TerentievCourseWork.Services._Impl;

public class ProductDataProvider : IProductDataProvider
{
    private IWebRequestService _webRequestService;
    // public delegate void GenresListChangedEventHandler(object sender, GenresListChangedEventArgs e);
    // public event GenresListChangedEventHandler GenresListChangedEvent;

    private DateTime _lastUpdate;
    public ObservableCollection<GenreModel> Genres { get; set; } = new ObservableCollection<GenreModel>();
    
    // next code is calling UpdateGenres() method every minute
    public ProductDataProvider(IWebRequestService webRequestService)
    {
        _webRequestService = webRequestService;
        UpdateGenres();
        Timer timer = new Timer(60000);
        timer.Elapsed += (sender, args) => UpdateGenres();
    }

    private async void UpdateGenres()
    {
        var oldGenres = Genres;
        var newGenres = await _webRequestService.GetGenres();
        foreach (var removedGenre in oldGenres.Where(oldGenre => newGenres.All(newGenre => newGenre.Id != oldGenre.Id)))
        {
            Genres.Remove(removedGenre);
        }
        foreach (var addedGenre in newGenres.Where(newGenre => oldGenres.All(oldGenre => oldGenre.Id != newGenre.Id)))
        {
            Genres.Add(addedGenre);
        }
        foreach (var updatedGenre in newGenres.Where(newGenre => oldGenres.Any(oldGenre => oldGenre.Id == newGenre.Id && !oldGenre.Equals(newGenre))))
        {
            var oldGenre = oldGenres.First(oldGenre => oldGenre.Id == updatedGenre.Id);
            Genres[Genres.IndexOf(oldGenre)] = updatedGenre;
        }
        
        // var addedGenres = Genres.FindAll(genreModel => oldGenres.All(oldGenreModel => oldGenreModel.Id != genreModel.Id));
        // var removedGenres = oldGenres.FindAll(genreModel => Genres.All(oldGenreModel => oldGenreModel.Id != genreModel.Id));
        // var updatedGenres = Genres.FindAll(genreModel => oldGenres.Any(oldGenreModel => oldGenreModel.Id == genreModel.Id && !oldGenreModel.Equals(genreModel))).Select(genreModel => (oldGenres.First(oldGenreModel => oldGenreModel.Id == genreModel.Id), genreModel)).ToList();
        //
        // RaiseGenresListChangedEvent(oldGenres, Genres, addedGenres, removedGenres, updatedGenres);
    }
    
    // protected virtual void RaiseGenresListChangedEvent(List<GenreModel> oldGenresList, List<GenreModel> newGenresList, List<GenreModel> addedGenres, List<GenreModel> removedGenres, List<(GenreModel, GenreModel)> updatedGenres)
    // {
    //     GenresListChangedEvent?.Invoke(this, new GenresListChangedEventArgs(oldGenresList, newGenresList, addedGenres, removedGenres, updatedGenres));    
    // }
}

// public class GenresListChangedEventArgs
// {
//     public GenresListChangedEventArgs(List<GenreModel> oldGenresList, List<GenreModel> newGenresList, List<GenreModel> addedGenres, List<GenreModel> removedGenres, List<(GenreModel, GenreModel)> updatedGenres)
//     {
//         OldGenresList = oldGenresList;
//         NewGenresList = newGenresList;
//         AddedGenres = addedGenres;
//         RemovedGenres = removedGenres;
//         UpdatedGenres = updatedGenres;
//     }
//
//     public List<GenreModel> OldGenresList { get; }
//     public List<GenreModel> NewGenresList { get; }
//     public List<GenreModel> AddedGenres { get; }
//     public List<GenreModel> RemovedGenres { get; }
//     public List<(GenreModel old, GenreModel updated)> UpdatedGenres { get; }
// }