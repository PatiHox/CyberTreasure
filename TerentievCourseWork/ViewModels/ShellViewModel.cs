using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Caliburn.Micro;

namespace TerentievCourseWork.ViewModels;

public class ShellViewModel : Conductor<object>.Collection.OneActive
{
    private string _appTitle = "Cyber Treasure";

    public string AppTitle
    {
        get => _appTitle;
        set => Set(ref _appTitle, value);
    }

    protected override async void OnViewLoaded(object view)
    {
        await ActivateItemAsync(IoC.Get<ShopViewModel>());
        base.OnViewLoaded(view);
    }
}