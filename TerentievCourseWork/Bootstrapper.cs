using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using TerentievCourseWork.Services;
using TerentievCourseWork.Services._Impl;
using TerentievCourseWork.ViewModels;

namespace TerentievCourseWork;

public class Bootstrapper : BootstrapperBase
{
    #region Private fields
    
    private SimpleContainer _container;

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
        _container = new SimpleContainer();
        
        _container.Singleton<IWindowManager, WindowManager>();
        _container.Singleton<IEventAggregator, EventAggregator>();

        _container.PerRequest<ShellViewModel>();
        _container.PerRequest<ShopViewModel>();
        
        _container.Instance<IProductDataProvider>(new ProductDataProvider());
    }

    protected override async void OnStartup(object sender, StartupEventArgs e)
    {
        await DisplayRootViewForAsync<ShellViewModel>();
    }

    protected override object GetInstance(Type serviceType, string key) 
    {
        return _container.GetInstance(serviceType, key);
    }
    
    protected override IEnumerable<object> GetAllInstances(Type serviceType) 
    {
        return _container.GetAllInstances(serviceType);
    }
    
    protected override void BuildUp(object instance) 
    {
        _container.BuildUp(instance);
    }

    protected override IEnumerable<Assembly> SelectAssemblies()
    {
        return new[] { Assembly.GetExecutingAssembly() };
    }

    #endregion
}