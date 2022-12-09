using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using TerentievCourseWork.Models;

namespace TerentievCourseWork.ViewModels;

public class GameBannerViewModel : PropertyChangedBase
{
    private GameModel Game { get; }
    
    public long Id => Game.Id;
    public string Title => Game.Title;
    public string Description => Game.Description;
    public ImageSource BannerImage => new BitmapImage(new Uri("pack://application:,,,/../" + (Game.ImagePath == String.Empty ? "Assets/Banners/default_banner.jpg" : Game.ImagePath)));
    public decimal Price => Game.Price;
    public float Rating => Game.Rating;
    
    public GameBannerViewModel(GameModel game)
    {
        Game = game;
    }
}