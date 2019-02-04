using Prism.Commands;
using Prism.Navigation;

namespace PoI.ViewModels
{
    public class NouveauViewModel : ViewModelBase
    {
        public DelegateCommand DelegateTap;

        private string _tapped;
        public string Tapped
        {
            get { return _tapped; }
            set { SetProperty(ref _tapped, value); }
        }

        public NouveauViewModel(INavigationService navigationService)
            :base(navigationService)
        {
            Title = "Nouveau";
            Tapped = "Untapped";
            DelegateTap = new DelegateCommand(ChangeTappedValue);
        }

        void ChangeTappedValue()
        {
            NavigationService.NavigateAsync("NavigationPage/MainPage");
        }
    }
}
