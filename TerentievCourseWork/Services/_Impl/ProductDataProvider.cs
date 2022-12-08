using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using TerentievCourseWork.Models;
using Timer = System.Timers.Timer;

namespace TerentievCourseWork.Services._Impl;

public class ProductDataProvider : IProductDataProvider
{
    private IWebRequestService _webRequestService;
    private Timer _updateTimer;
    private readonly SemaphoreSlim _updateSemaphore = new(1, 1);

    private DateTime _lastUpdate;
    public ObservableCollection<GenreModel> Genres { get; } = new ObservableCollection<GenreModel>();

    // next code is calling UpdateGenres() method every minute
    public ProductDataProvider(IWebRequestService webRequestService)
    {
        _webRequestService = webRequestService;
        _updateTimer = new Timer(10000);

        InitUpdateTimer();
        UpdateGenres();
    }

    private void InitUpdateTimer()
    {
        _updateTimer.Elapsed += TimerElapsed;
        _updateTimer.Start();
    }

    private async void TimerElapsed(object? sender, ElapsedEventArgs e)
    {
        await _updateSemaphore.WaitAsync();
        _updateTimer.Stop();
        await UpdateGenres();
        _updateTimer.Start();
        _updateSemaphore.Release();
    }

    private async Task UpdateGenres()
    {
        var oldGenres = Genres;
        var newGenres = await _webRequestService.GetGenres();
        // remove genres no longer present
        foreach (var removedGenre in oldGenres.Where(oldGenre => newGenres.All(newGenre => newGenre.Id != oldGenre.Id)).ToList())
        {
            Genres.Remove(removedGenre);
        }
        // add new genres
        foreach (var addedGenre in newGenres.Where(newGenre => oldGenres.All(oldGenre => oldGenre.Id != newGenre.Id)).ToList())
        {
            Genres.Add(addedGenre);
        }
        // update genres
        foreach (var updatedGenre in newGenres.Where(newGenre =>
                     oldGenres.Any(oldGenre => oldGenre.Id == newGenre.Id 
                                               && !oldGenre.Equals(newGenre))).ToList())
        {
            var oldGenre = oldGenres.First(oldGenre => oldGenre.Id == updatedGenre.Id);
            Genres[Genres.IndexOf(oldGenre)] = updatedGenre;
        }
    }
}