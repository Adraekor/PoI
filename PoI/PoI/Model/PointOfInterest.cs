﻿using System.Collections.Generic;

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

        public List<string> TagList { get; set; }
    }
}
