using Prism.Commands;
using Prism.Navigation;

namespace PoI.ViewModels
{
    public class NouveauViewModel : ViewModelBase
    {
        public DelegateCommand DelegateTap { get; private set; }
        public int ClickTotal { get; private set; }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        public NouveauViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Nouveau";
            ClickTotal = 0;
            DelegateTap = new DelegateCommand(ChangeTappedValue);
        }

        void ChangeTappedValue()
        {
            NavigationService.GoBackAsync();
        }
    }
}
