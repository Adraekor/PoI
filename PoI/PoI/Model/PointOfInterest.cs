using System;
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

        public string MiniName { get; set; }

        public string Description { get; set; }

        public string MiniDesc { get; set; }

        public string Image { get; set; }

        public string Tag { get; set; }

        public string MiniTag { get; set; }

        public DateTime Date { get; set; }

        public  Position pos { get; set; }
    }
}
