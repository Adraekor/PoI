using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace PoI.Model
{
    public class PointOfInterest
    {
        public PointOfInterest()
        {
                
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string TagList { get; set; }

        public Position pos { get; set; }
    }
}
