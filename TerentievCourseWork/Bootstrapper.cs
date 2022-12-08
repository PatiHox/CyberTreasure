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
    
    private SimpleContainer _container = new();

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
        _container.Singleton<IEventAggregator, EventAggregator>();

        _container.PerRequest<ShellViewModel>();
        _container.PerRequest<ShopViewModel>();
        
        _container.Instance<IApiParserService>(new ApiParserService());
        _container.Instance<IWebRequestService>(new WebRequestService(_container.GetInstance<IApiParserService>()));
        _container.Instance<IProductDataProvider>(new ProductDataProvider(_container.GetInstance<IWebRequestService>()));
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