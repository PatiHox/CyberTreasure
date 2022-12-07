using System.Windows;
using Caliburn.Micro;
using TerentievCourseWork.ViewModels;

namespace TerentievCourseWork;

public class Bootstrapper : BootstrapperBase
{
    public Bootstrapper()
    {
        Initialize();
    }
    
    protected override async void OnStartup(object sender, StartupEventArgs e)
    {
        await DisplayRootViewForAsync<ShellViewModel>();
    }
}