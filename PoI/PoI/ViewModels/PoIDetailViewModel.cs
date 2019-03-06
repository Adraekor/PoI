using PoI.Model;
using PoI.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace PoI.ViewModels
{
    public class PoIDetailViewModel : ViewModelBase
	{
        public DelegateCommand DelegateEdit { get; private set; }

        public DelegateCommand DelegateDelete { get; private set; }

        private PointOfInterest PoI;

        private IPageDialogService _dialogService;
        private IPoIService _poiService;

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _tag;
        public string Tag
        {
            get { return _tag; }
            set { SetProperty(ref _tag, value); }
        }

        public PoIDetailViewModel(INavigationService navigationService, IPageDialogService dialogService, IPoIService poiService)
            :base(navigationService)
        {
            Title = "Détails PoI";

            _dialogService = dialogService;
            _poiService = poiService;

            DelegateEdit = new DelegateCommand(EditPoI);
            DelegateDelete = new DelegateCommand(DeletePoI);

        }

        private async void DeletePoI()
        {
            var answer = await _dialogService.DisplayAlertAsync("Suppression", "Êtes vous sûr de vouloir supprimer ce PoI", "Non", "Oui");

            if(!answer)
            {
                _poiService.DeletePoi(PoI.Id);
                await NavigationService.GoBackAsync();
            }
        }

        private async void EditPoI()
        {
            var param = new NavigationParameters()
            {
                { "poi", PoI }
            };

            await NavigationService.NavigateAsync("PoIEdit", param);

           
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            var poi = parameters.GetValue<PointOfInterest>("poi");
            PoI = poi;
            Name = poi.Name;
            Description = poi.Description;
            Tag = poi.TagList;
        }
    }
}
