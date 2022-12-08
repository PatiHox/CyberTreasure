using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TerentievCourseWork.Models;

namespace TerentievCourseWork.ViewModels;

public class GenreButtonViewModel : INotifyPropertyChanged
{
    private bool _isSelected;
    private GenreModel _genreModel;

    public GenreModel GenreModel
    {
        get => _genreModel;
        set => SetField(ref _genreModel, value);
    }
    public int IndexInList { get; }
    public bool IsSelected
    {
        get => _isSelected;
        set => SetField(ref _isSelected, value);
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

    #region INotifyPropertyChanged Implementation

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    #endregion
}