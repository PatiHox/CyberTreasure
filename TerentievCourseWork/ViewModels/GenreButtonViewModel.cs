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
    public int IndexInList { get; }
    public bool IsSelected
    {
        get => _isSelected;
        set => Set(ref _isSelected, value);
    }
    public ICommand ButtonClickCommand { get; }
    public object Parent { get; }

    public GenreButtonViewModel(GenreModel genreModel, bool isSelected, ICommand buttonClickCommand, int index, object parent)
    {
        GenreModel = genreModel;
        IsSelected = isSelected;
        ButtonClickCommand = buttonClickCommand;
        IndexInList = index;
        Parent = parent;
    }
}