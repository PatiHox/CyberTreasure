using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Caliburn.Micro;

namespace TerentievCourseWork.ViewModels;

public class ShellViewModel : Conductor<object>
{
    private string _appTitle = "Cyber Treasure";

    public string AppTitle
    {
        get => _appTitle;
        set => SetField(ref _appTitle, value);
    }

    protected override async void OnViewLoaded(object view)
    {
        await ActivateItemAsync(new ShopViewModel());
        base.OnViewLoaded(view);
    }
    
    private void SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return;
        }

        field = value;
        OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }
}