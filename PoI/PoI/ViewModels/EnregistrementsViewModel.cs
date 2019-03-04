using PoI.Model;
using PoI.Services;
using Prism.Navigation;
using System.Collections.ObjectModel;

namespace PoI.ViewModels
{
    public class EnregistrementsViewModel : ViewModelBase
	{
        private IPoIService _PoIService;

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
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            PoIs = new ObservableCollection<PointOfInterest>(_PoIService.GetAllPoI());
        }
    }
}
