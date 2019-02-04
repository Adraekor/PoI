using PoI.ViewModels;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EXPLOSION.ViewModels
{
	public class AppliMenuViewModel : ViewModelBase
	{
        public DelegateCommand DelegateHomepage { get; private set; }
        public DelegateCommand DelegateMap { get; private set; }
        public DelegateCommand DelegateRecord { get; private set; }
        public DelegateCommand DelegateNewPoI { get; private set; }
        public DelegateCommand DelegateBonus { get; private set; }

        public AppliMenuViewModel(INavigationService navigationService) : base(navigationService)
        {
            DelegateHomepage = new DelegateCommand(NavToHomepage);
            DelegateMap = new DelegateCommand(NavToMap);
            DelegateRecord = new DelegateCommand(NavToRecord);
            DelegateNewPoI = new DelegateCommand(NavToNewPoI);
            DelegateBonus = new DelegateCommand(NavToBonus);
        }

        private void NavToHomepage()
        {
            NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        private void NavToMap()
        {
            NavigationService.NavigateAsync("NavigationPage/Map");
        }

        private void NavToRecord()
        {
            NavigationService.NavigateAsync("NavigationPage/Enregistrements");
        }

        private void NavToNewPoI()
        {
            NavigationService.NavigateAsync("NavigationPage/Nouveau");
        }

        private void NavToBonus()
        {
            NavigationService.NavigateAsync("NavigationPage/Bonus");
        }
    }
}
