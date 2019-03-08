using LiteDB;
using PoI.Model;
using PoI.Services;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace PoI.ViewModels
{
    public class EnregistrementsViewModel : ViewModelBase
	{
        private IPoIService _PoIService;

        private string _search;
        public string Search
        {
            get { return _search; }
            set { SetProperty(ref _search, value); }
        }

        public DelegateCommand<PointOfInterest> DelegateDetailPoI { get; private set; }

        public DelegateCommand DelegateSearch { get; private set; }

        public DelegateCommand DelegateCancel { get; private set; }

        public DelegateCommand DelegateReverse { get; private set; }

        private ObservableCollection<PointOfInterest> _PoIs;
        public ObservableCollection<PointOfInterest> PoIs
        {
            get { return _PoIs; }
            set { SetProperty(ref _PoIs, value); }
        }

        private ObservableCollection<PointOfInterest> BasePoIs;

        public EnregistrementsViewModel(INavigationService navigationService, IPoIService PoIService)
            : base(navigationService)
        {
            _PoIService = PoIService;
            DelegateDetailPoI = new DelegateCommand<PointOfInterest>(EnregistrementDetail);
            DelegateReverse = new DelegateCommand(ReverseEntries);
            DelegateSearch = new DelegateCommand(Research);
            DelegateCancel = new DelegateCommand(Cancel);
        }

        private void Cancel()
        {
            PoIs = BasePoIs;
            Search = string.Empty;
        }

        private void Research()
        {
            if(string.IsNullOrEmpty(Search))
            {
                PoIs = BasePoIs;
            }
            else
            {
                PoIs = new ObservableCollection<PointOfInterest>(BasePoIs.Where(poi => poi.Tag.Contains(Search)));
            }
        }

        private void ReverseEntries()
        {
            PoIs = new ObservableCollection<PointOfInterest>(PoIs.Reverse());
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (string.IsNullOrEmpty(Search))
            { 
                PoIs = new ObservableCollection<PointOfInterest>(_PoIService.GetAllPoI().OrderBy(poi => poi.Date));
                BasePoIs = PoIs;
            }
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
