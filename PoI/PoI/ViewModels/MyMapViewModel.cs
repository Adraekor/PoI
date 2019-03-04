using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace PoI.ViewModels
{
    
    public class MyMapViewModel : BindableBase
	{
      public static Map map = new Map(MapSpan.FromCenterAndRadius(new Position(49.094928, 6.229883), Distance.FromMiles(0.5))){IsShowingUser = true,VerticalOptions = LayoutOptions.FillAndExpand};

        public MyMapViewModel()
        {

            var position1 = new Position(49.094928, 6.229883);

            var pin1 = new Pin
            {
                Type = PinType.Place,
                Position = position1,
                Label = "MEILLEUR FAC DE LA GALAXY",
                Address = "www.google.fr",
            };



            map.Pins.Add(pin1); 
        }


	}
}
