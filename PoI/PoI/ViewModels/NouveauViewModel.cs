using LiteDB;
using PoI.Model;
using PoI.Services;
using Prism.Commands;
using Prism.Navigation;

namespace PoI.ViewModels
{
    public class NouveauViewModel : ViewModelBase
    {
        private IPoIService _PoIService;

        public DelegateCommand DelegateTap { get; private set; }

        public DelegateCommand DelegateSave { get; private set; }

        private string _tag;
        public string Tag
        {
            get { return _tag; }
            set { SetProperty(ref _tag, value); }
        }

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

        public NouveauViewModel(INavigationService navigationService, IPoIService PoIService)
            : base(navigationService)
        {
            Title = "Nouveau";
            DelegateTap = new DelegateCommand(ChangeTappedValue);
            DelegateSave = new DelegateCommand(SaveCurrentEntry);
            _PoIService = PoIService;
        }

        void SaveCurrentEntry()
        {
            var newPoI = new PointOfInterest
            {
                Name = Name,
                Description = Description,
                TagList = Tag
            };

            _PoIService.AddPoI(newPoI);

            NavigationService.NavigateAsync("Enregistrements");
        }

        void ChangeTappedValue()
        {
            NavigationService.GoBackAsync();
        }
    }
}
