using PoI.Model;
using PoI.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace PoI.ViewModels
{
    public class PoIDetailViewModel : ViewModelBase
	{
        private string source { get; set; }

        public DelegateCommand DelegateEdit { get; private set; }

        public DelegateCommand DelegateDelete { get; private set; }

        private PointOfInterest PoI;

        private IPageDialogService _dialogService;
        private IPoIService _poiService;

                private string _date;
        public string Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }

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

        private string _imageFilePath;
        public string ImageFilePath
        {
            get { return _imageFilePath; }
            set { SetProperty(ref _imageFilePath, value); }
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
            var answer = await _dialogService.DisplayAlertAsync("Suppression", "Êtes vous sûr de vouloir supprimer ce PoI", "Oui", "Non");

            if(answer)
            {
                _poiService.DeletePoi(PoI.Id);
                if(!string.IsNullOrEmpty(source) && source.Equals("map"))
                    await NavigationService.NavigateAsync("/AppliMenu/NavigationPage/MyMap");
                else 
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
            source = parameters.GetValue<string>("source");
            PoI = poi;
            Name = poi.Name;
            Description = poi.Description;
            Tag = poi.Tag;

            ImageFilePath = poi.Image;
            Date = "Photo prise le : " + poi.Date.ToString(AppConstante.BasicDateFormat);
        }
    }
}
