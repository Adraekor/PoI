using Plugin.Geolocator;
using PoI.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using PoI.Services;

namespace PoI.Views
{
    public partial class MyMap : ContentPage
    {
        static Position current = new Position(49.094928, 16.229883);
        public MyMap()
        {
            InitializeComponent();
            BindingContext = new MyMapViewModel();
            var map = MyMapViewModel.map; 
            Content = map;
            /*
            var position1 = new Position(49.094928, 6.229883);

            var pin1 = new Pin
            {
                Type = PinType.Place,
                Position = position1,
                Label = "MEILLEUR FAC DE LA GALAXY",
                Address = "www.google.fr",
            };



            map2.Pins.Add(pin1);  */

              
        }
    }
}

