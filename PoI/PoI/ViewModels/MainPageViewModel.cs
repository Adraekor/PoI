using Prism.Navigation;

namespace PoI.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string _projectTitle;
        public string ProjectTitle
        {
            get { return _projectTitle; }
            set { SetProperty(ref _projectTitle, value); }
        }

        private string _projectYear;
        public string ProjectYear
        {
            get { return _projectYear; }
            set { SetProperty(ref _projectYear, value); }
        }

        private string _group;
        public string GroupName
        {
            get { return _group; }
            set { SetProperty(ref _group, value); }
        }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Accueil";
            ProjectTitle = "Projet développement mobile";
            ProjectYear = "2018 - 2019";
            GroupName = "Klein / Tazen";


        }
    }
}
