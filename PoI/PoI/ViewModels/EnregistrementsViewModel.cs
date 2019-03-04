using PoI.Model;
using PoI.Services;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;

namespace PoI.ViewModels
{
    public class EnregistrementsViewModel : ViewModelBase
	{
        private IPoIService _PoIService;

        public DelegateCommand<PointOfInterest> DelegateDetailPoI { get; private set; }

        private ObservableCollection<PointOfInterest> _PoIs;
        public ObservableCollection<PointOfInterest> PoIs
        {
            get { return _PoIs; }
            set { SetProperty(ref _PoIs, value); }
        }

        public EnregistrementsViewModel(INavigationService navigationService, IPoIService PoIService)
            : base(navigationService)
        {
            _PoIService = PoIService;
            DelegateDetailPoI = new DelegateCommand<PointOfInterest>(EnregistrementDetail);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            PoIs = new ObservableCollection<PointOfInterest>(_PoIService.GetAllPoI());
        }

        private void EnregistrementDetail(PointOfInterest poi)
        {
            var param = new NavigationParameters
            {
                { "poi", poi }
            };

            NavigationService.NavigateAsync("PoIDetail", param);
        }
    }
}
