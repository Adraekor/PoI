using LiteDB;
using PoI.Model;
using PoI.Services;
using Prism.Commands;
using Prism.Navigation;

namespace PoI.ViewModels
{
    public class PoIEditViewModel : ViewModelBase
	{
        private IPoIService PoIService;
        private PointOfInterest PoI;
        public DelegateCommand DelegateSave { get; private set; }

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

        public PoIEditViewModel(INavigationService navigationService, IPoIService poiService)
            : base(navigationService)
        {
            PoIService = poiService;
            DelegateSave = new DelegateCommand(UpdateAndExit);
        }

        public void UpdateAndExit()
        {
            PoI.Name = Name;
            PoI.TagList = Tag;
            PoI.Description = Description;

            var param = new NavigationParameters()
            {
                {"poi", PoI }
            };

            using (var db = new LiteDatabase(AppConstante.dbPath))
            {
                var PoIs = db.GetCollection<PointOfInterest>("poi");
                PoIs.Update(PoI);
            }

            NavigationService.GoBackAsync(param);
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
