using System;
using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;
using TerentievCourseWork.ViewModels;
using TerentievCourseWork.Views;

namespace TerentievCourseWork;

public class Bootstrapper : BootstrapperBase
{
    #region Private fields
    
    private SimpleContainer _container = new SimpleContainer();

    #endregion

    #region Constructor

    public Bootstrapper()
    {
        Initialize();
    }
    
    #endregion

    #region Protected methods

    protected override void Configure()
    {
        _container.Singleton<IWindowManager, WindowManager>();
        base.Configure();
    }

    protected override async void OnStartup(object sender, StartupEventArgs e)
    {
        await DisplayRootViewForAsync<ShellViewModel>();
    }

    // protected override object GetInstance(Type serviceType, string key) {
    //     return _container.GetInstance(serviceType, key);
    // }
    //
    // protected override IEnumerable<object> GetAllInstances(Type serviceType) {
    //     return _container.GetAllInstances(serviceType);
    // }
    //
    // protected override void BuildUp(object instance) {
    //     _container.BuildUp(instance);
    // }

    #endregion
}