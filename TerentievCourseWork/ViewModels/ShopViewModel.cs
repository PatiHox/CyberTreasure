using System.Collections.Generic;
using Caliburn.Micro;
using TerentievCourseWork.Models;

namespace TerentievCourseWork.ViewModels;

public class ShopViewModel : Screen
{
    #region Public properties
// TODO: Remove mockups
    public List<GenreModel>? Genres { get; set; } = new List<GenreModel>
    {
        new GenreModel() { Name = "Action" },
        new GenreModel() { Name = "Adventure" },
        new GenreModel() { Name = "RPG" },
    };

    public int SelectedGenreIndex { get; set; } = 0;
    public List<GameModel>? SelectedGenreGames => Genres?[SelectedGenreIndex].Games;

    #endregion
}