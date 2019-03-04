using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace PoI.Views
{
    public partial class MyMap : ContentPage
    {
        public MyMap()
        {
            InitializeComponent();
           var map = new Map(MapSpan.FromCenterAndRadius(
new Position(49.094928, 6.229883),
Distance.FromMiles(0.5)))
            {
                IsShowingUser = true,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var position1 = new Position(49.094928, 6.229883);

            var pin1 = new Pin
            {
                Type = PinType.Place,
                Position = position1,
                Label = "MEILLEUR FAC DE LA GALAXY",
                Address = "www.google.fr",
            };



            map.Pins.Add(pin1);

            Content = map;                     
        }
    }
}
