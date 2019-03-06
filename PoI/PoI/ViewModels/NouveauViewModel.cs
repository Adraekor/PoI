using PoI.Model;
using PoI.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;

namespace PoI.ViewModels
{
    public class NouveauViewModel : ViewModelBase
    {
        private IPoIService _PoIService;
        private IPageDialogService _dialogService;

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

        public NouveauViewModel(INavigationService navigationService, IPoIService PoIService, IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "Nouveau";

            _PoIService = PoIService;
            _dialogService = dialogService;

            DelegateTap = new DelegateCommand(ChangeTappedValue);
            DelegateSave = new DelegateCommand(SaveCurrentEntry, CanAddPoI).ObservesProperty(() => Name).ObservesProperty(() => Tag);
        }

        private bool CanAddPoI()
        {
            if (string.IsNullOrEmpty(Name))
                return false;

            if (string.IsNullOrEmpty(Tag))
                return false;

            return true;
        }

        private async void SaveCurrentEntry()
        {
            var answer = await _dialogService.DisplayAlertAsync("Création", "Êtes vous sûr de vouloir créer ce PoI", "Non", "Oui");

            if (!answer)
            {
                var newPoI = new PointOfInterest
                {
                    Name = Name,
                    MiniName = Name.Length > 5 ? Name.Substring(0, 5) + "..." : Name,
                    Description = Description,
                    Tag = Tag,
                    MiniTag = Tag.Length > 5 ? Tag.Substring(0, 5) + "..." : Tag,
                    Date = DateTime.Now
                };

                if (!string.IsNullOrEmpty(Description) && Description.Length > 10)
                    newPoI.MiniDesc = Description.Substring(0, 10) + "...";
                else
                    newPoI.MiniDesc = Description;

                 _PoIService.AddPoI(newPoI);
                 await NavigationService.NavigateAsync("/AppliMenu/NavigationPage/Enregistrements");
            }
        }

        void ChangeTappedValue()
        {
            NavigationService.GoBackAsync();
        }
    }
}
