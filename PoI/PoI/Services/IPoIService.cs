using PoI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoI.Services
{
    public interface IPoIService
    {
        List<PointOfInterest> GetAllPoI();

        void AddPoI(PointOfInterest poi);

        PointOfInterest GetPoi(int id);

        void DeletePoi(int id);
    }
}
