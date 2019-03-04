using PoI.Model;
using Prism.Commands;
using Prism.Navigation;

namespace PoI.ViewModels
{
    public class PoIDetailViewModel : ViewModelBase
	{
        public DelegateCommand DelegateEdit { get; private set; }

        private PointOfInterest PoI;

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

        public PoIDetailViewModel(INavigationService navigationService)
            :base(navigationService)
        {
            Title = "Détails PoI";
            DelegateEdit = new DelegateCommand(EditPoI);
        }

        private void EditPoI()
        {
            var poi = new PointOfInterest()
            {
                Id = PoI.Id,
                Name = Name,
                Description = Description,
                TagList = Tag,
            };

            var param = new NavigationParameters()
            {
                { "poi", poi }
            };

            NavigationService.NavigateAsync("PoIEdit", param);
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
