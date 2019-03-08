using Plugin.Geolocator;
using Plugin.Media;
using Plugin.Media.Abstractions;
using PoI.Model;
using PoI.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Linq;
using Xamarin.Forms.Maps;


namespace PoI.ViewModels
{
    public class NouveauViewModel : ViewModelBase
    {
        private const string BASE_IMAGE_SELECTER = "baseline_add_a_photo_24.xml";
        
        private IPoIService _PoIService;
        private IPageDialogService _dialogService;

        public DelegateCommand DelegateTap { get; private set; }

        public DelegateCommand DelegateSave { get; private set; }

                                                  

        private Position _position;
        public Position position
        {
            get { return _position; }
            set { SetProperty(ref _position, value); }
        }

        private string _adresse;
        public string adresse
        {
            get { return _adresse; }
            set { SetProperty(ref _adresse, value); }
        }

        private string _imageFilePath;
        public string ImageFilePath
        {
            get { return _imageFilePath; }
            set { SetProperty(ref _imageFilePath, value); }
        }

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

        Geocoder geoCoder;

        public NouveauViewModel(INavigationService navigationService, IPoIService PoIService, IPageDialogService dialogService)
            : base(navigationService)
        {

            Title = "Nouveau";
            adresse = "Aucune Adresse";
            CallingGPS();
            ImageFilePath = BASE_IMAGE_SELECTER;

            _PoIService = PoIService;
            _dialogService = dialogService;

            DelegateTap = new DelegateCommand(ChoseMedia);
            DelegateSave = new DelegateCommand(SaveCurrentEntry, CanAddPoI)
                            .ObservesProperty(() => Name)
                            .ObservesProperty(() => Tag)
                            .ObservesProperty(() => Description)
                            .ObservesProperty(() => ImageFilePath);
        }

        private bool CanAddPoI()
        {
            if (string.IsNullOrEmpty(ImageFilePath) || ImageFilePath.Equals(BASE_IMAGE_SELECTER))
                return false; 

            if (string.IsNullOrEmpty(Name))
                return false;

            if (string.IsNullOrEmpty(Tag))
                return false;

            if (string.IsNullOrEmpty(Description))
                return false;

            return true;
        }

        private async void SaveCurrentEntry()
        {
            var answer = await _dialogService.DisplayAlertAsync("Création", "Êtes vous sûr de vouloir créer ce PoI", "Non", "Oui");

            if (!answer)
            {
                CreatePoI();
            }
        }

        private void CreatePoI() {
            var newPoI = new PointOfInterest
            {
                Name = Name,
                MiniName = Name.Length > 5 ? Name.Substring(0, 5) + "..." : Name,
                Description = Description,                                                  
                MiniDesc = Description.Length > 10 ? Description.Substring(0, 10) + "..." : Description,
                Tag = Tag,
                MiniTag = Tag.Length > 5 ? Tag.Substring(0, 5) + "..." : Tag,
                Date = DateTime.Now,
                latitude = position.Latitude,
                longitude = position.Longitude,
                Image = ImageFilePath,
                adresse = adresse
            };

            _PoIService.AddPoI(newPoI);
            NavigationService.NavigateAsync("/AppliMenu/NavigationPage/Enregistrements");
        }

        async void CallingGPS()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 120;

            try
            {
                var ex = await locator.GetPositionAsync(TimeSpan.FromSeconds(120), null, false);
                position = new Position(ex.Latitude, ex.Longitude);
            }
            catch (Exception e)
            {

            }


            var add = await locator.GetAddressesForPositionAsync(new Plugin.Geolocator.Abstractions.Position(position.Latitude, position.Longitude));
            var add2 = add.FirstOrDefault();   

            adresse = add2.Thoroughfare+ " - " + add2.Locality+ " - "  + add2.CountryCode;

        }



        private async void ChoseMedia()
        {
            var answer = await _dialogService.DisplayActionSheetAsync("Ou se trouve votre media ?", "Annuler", null, new string[] { "Gallerie", "Appareil Photo" });
            if(!string.IsNullOrEmpty(answer))
            { 
                if(answer.Equals("Gallerie"))
                {
                    var mediaOption = new PickMediaOptions()
                    {
                        PhotoSize = PhotoSize.Medium,
                    };
                    var stream = await CrossMedia.Current.PickPhotoAsync(mediaOption);

                    if (stream != null)
                        ImageFilePath = stream.Path;
                }
                else if(answer.Equals("Appareil Photo"))
                {
                    if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
                    {
                        var photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                        {
                            CompressionQuality = 50,
                            Directory = "PoI",
                            Name = $"PoI{ DateTime.Now }",
                            PhotoSize = PhotoSize.Medium,
                        });

                        if (photo != null)
                        {
                            ImageFilePath = photo.Path;
                        }
                    }

                    
                }
                else
                {

                }
            }
        }
    }
}
