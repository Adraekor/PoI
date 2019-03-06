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

namespace PoI.ViewModels
{
    
    public class MyMapViewModel : BindableBase
	{
        public Map Map { get; private set; }
        static Position test;
        public MyMapViewModel()
        {
            Map = new Map(
                MapSpan.FromCenterAndRadius(
                new Position(49.094928, 6.229883),
                Distance.FromMiles(0.3)))
            {
                //IsShowingUser = true,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            //MapSpan.FromCenterAndRadius(new Position(49.094928, 6.229883), Distance.FromMiles(0.5))){IsShowingUser = true,VerticalOptions = LayoutOptions.FillAndExpand};


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

            test = new Position(  position.Latitude,position.Longitude);

            var pin1 = new Pin
            {
                Type = PinType.Place,
                //Position = position1,
                //Position = new Position(49.094928, 6.229883),//position.Latitude, position.Longitude),
                Position = position,
            Label = "MEILLEUR FAC DE LA GALAXY",
                Address = "www.google.fr",

            };

            Map.Pins.Add(pin1);                                                       
        }
    }
}
