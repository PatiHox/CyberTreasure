using System.Windows.Input;
using Caliburn.Micro;
using TerentievCourseWork.Models;

namespace TerentievCourseWork.ViewModels;

public class GenreButtonViewModel : PropertyChangedBase
{
    private bool _isSelected;
    private GenreModel _genreModel;

    public GenreModel GenreModel
    {
        get => _genreModel;
        set => Set(ref _genreModel, value);
    }
    public ICommand ButtonClickCommand { get; }
    public object Parent { get; }

    public GenreButtonViewModel(GenreModel genreModel, ICommand buttonClickCommand, object parent)
    {
        GenreModel = genreModel;
        ButtonClickCommand = buttonClickCommand;
        Parent = parent;
    }
}