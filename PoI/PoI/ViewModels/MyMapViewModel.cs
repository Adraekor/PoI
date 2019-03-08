using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Plugin.Geolocator;
using System.Threading.Tasks;
using System.Diagnostics;
using PoI.Services;
using Prism.Navigation;

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
            
            if (!CrossGeolocator.IsSupported)
                position = new Position();
            if (!CrossGeolocator.Current.IsGeolocationEnabled)
                position = new Position();
            if (!CrossGeolocator.Current.IsGeolocationAvailable)
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
                        Address = liste[i].Description,
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
