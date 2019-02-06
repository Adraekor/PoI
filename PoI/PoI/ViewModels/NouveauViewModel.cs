using Prism.Commands;
using Prism.Navigation;

namespace PoI.ViewModels
{
    public class NouveauViewModel : ViewModelBase
    {
        public DelegateCommand DelegateTap { get; private set; }
        public int ClickTotal { get; private set; }

        private string labelContent;
        public string LabelContent
        {
            get { return labelContent; }
            set { SetProperty(ref labelContent, value); }
        }

        public NouveauViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Nouveau";
            ClickTotal = 0;
            DelegateTap = new DelegateCommand(ChangeTappedValue);
            LabelContent = "0 clicks";
        }

        void ChangeTappedValue()
        {
            NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        void OnImageButtonClicked()
        {
            ClickTotal += 1;
            LabelContent = $"{ClickTotal} ImageButton click{(ClickTotal == 1 ? "" : "s")}";
        }
    }
}
