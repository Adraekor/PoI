using LiteDB;
using PoI.Model;
using PoI.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;

namespace PoI.ViewModels
{
    public class PoIEditViewModel : ViewModelBase
	{
        private IPoIService PoIService;
        private PointOfInterest PoI;
        public DelegateCommand DelegateSave { get; private set; }

        private IPageDialogService _dialogService;

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

        public PoIEditViewModel(INavigationService navigationService, IPoIService poiService, IPageDialogService pageDialog)
            : base(navigationService)
        {
            PoIService = poiService;
            _dialogService = pageDialog;
            DelegateSave = new DelegateCommand(UpdateAndExit, CanEditPoI)
                .ObservesProperty(() => Name)
                .ObservesProperty(() => Tag)
                .ObservesProperty(() => Description);
        }

        public async void UpdateAndExit()
        {
            var answer = await _dialogService.DisplayAlertAsync("Modification", "Êtes vous sûr de vouloir modifier ce PoI", "Oui", "Non");

            if (answer)
            {
                PoI.Name = Name;
                PoI.MiniName = Name.Length > 5 ? Name.Substring(0, 5) + "..." : Name;
                PoI.Description = Description;
                if(string.IsNullOrEmpty(Description))
                    PoI.MiniDesc = Description.Length > 10 ? Description.Substring(0, 10) + "..." : Description;
                PoI.Tag = Tag;
                PoI.MiniTag = Tag.Length > 5 ? Tag.Substring(0, 5) + "..." : Tag;

                var param = new NavigationParameters()
                {
                    {"poi", PoI }
                };

                using (var db = new LiteDatabase(AppConstante.dbPath))
                {
                    var PoIs = db.GetCollection<PointOfInterest>("poi");
                    PoIs.Update(PoI);
                }

                await NavigationService.GoBackAsync(param);
            }
        }

        private bool CanEditPoI()
        {
            if (string.IsNullOrEmpty(Name))
                return false;

            if (string.IsNullOrEmpty(Tag))
                return false;

            if (string.IsNullOrEmpty(Description))
                return false;

            return true;
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            var poi = parameters.GetValue<PointOfInterest>("poi");
            PoI = poi;
            Name = poi.Name;
            Description = poi.Description;
            Tag = poi.Tag;
            ImageFilePath = poi.Image;
            Date = "Photo prise le : " + poi.Date.ToString(AppConstante.BasicDateFormat);
        }
    }
}
