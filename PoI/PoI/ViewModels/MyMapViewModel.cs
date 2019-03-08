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

namespace PoI.ViewModels
{
    
    public class MyMapViewModel : BindableBase
	{
        private IPoIService poiservice;
        public Map Map { get; private set; }
        public MyMapViewModel()
        {
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

             poiservice = new PoIService();
            var liste = poiservice.GetAllPoI();

            var pin1 = new Pin
            {
                Type = PinType.Place,
                Position = position,
            Label = "MEILLEUR FAC DE LA GALAXY",
                Address = "www.google.fr",

            };

            Map.Pins.Add(pin1);                                                       
        }
    }
}
