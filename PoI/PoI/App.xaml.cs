﻿using Prism;
using Prism.Ioc;
using PoI.ViewModels;
using PoI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PoI.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PoI
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("AppliMenu/NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IPoIService, PoIService>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<AppliMenu, AppliMenuViewModel>();
            containerRegistry.RegisterForNavigation<MyMap, MyMapViewModel>();
            containerRegistry.RegisterForNavigation<Enregistrements, EnregistrementsViewModel>();
            containerRegistry.RegisterForNavigation<Nouveau, NouveauViewModel>();
            containerRegistry.RegisterForNavigation<Bonus, BonusViewModel>();
            containerRegistry.RegisterForNavigation<Nouveau, NouveauViewModel>();
            containerRegistry.RegisterForNavigation<PoIDetail, PoIDetailViewModel>();
            containerRegistry.RegisterForNavigation<PoIEdit, PoIEditViewModel>();
        }
    }
}
