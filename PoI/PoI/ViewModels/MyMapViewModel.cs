using Plugin.Geolocator;
using PoI.Services;
using Prism.Navigation;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace PoI.ViewModels
{
    public class MyMapViewModel : ViewModelBase
	{
        private IPoIService poiservice;
        public Map Map { get; private set; }
        public MyMapViewModel(INavigationService nav, IPoIService poi)
            :base(nav)
        {
            poiservice = poi;
            Map = new Map(
             MapSpan.FromCenterAndRadius(
              new Position(),
              Distance.FromMiles(0.3)))
            {
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            CallingGPS();          
        }

        async void CallingGPS()
        {
            var locator = CrossGeolocator.Current;
            Position position;
            locator.DesiredAccuracy = 120;
            
            if (!CrossGeolocator.IsSupported 
             || !CrossGeolocator.Current.IsGeolocationEnabled  
             || !CrossGeolocator.Current.IsGeolocationAvailable)
                position = new Position();

            try
            {
                var ex = await locator.GetPositionAsync(TimeSpan.FromSeconds(120), null, false);
                position = new Position(ex.Latitude, ex.Longitude);
            }
            catch (Exception e)
            {
                position = new Position();
            }

            Map.MoveToRegion(MapSpan.FromCenterAndRadius(position,Distance.FromMiles(1)));

            var liste = poiservice.GetAllPoI();

            for(int i=0; i< liste.Count; i++)
            {
                 //if (liste[i].pos != new Position())
                {
                    Pin pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = new Position(liste[i].latitude, liste[i].longitude),
                        Label = liste[i].Name,
                        Address = "#" + liste[i].Tag,
                        BindingContext = liste[i],
                    };

                    pin.Clicked += (sender, args) =>
                    {
                        var param = new NavigationParameters()
                        {
                            {"poi", pin.BindingContext },
                            {"source", "map" }
                        };

                        NavigationService.NavigateAsync("PoIDetail", param);
                    };

                    Map.Pins.Add(pin);
                }
            }
                                                      
        }
    }
}
