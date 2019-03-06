using LiteDB;
using PoI.Model;
using System.Collections.Generic;
using System.Linq;

namespace PoI.Services
{
    public class PoIService : IPoIService
    {
        public List<PointOfInterest> PoIs = new List<PointOfInterest>();

        public void AddPoI(PointOfInterest poi)
        {
            PoIs.Add(poi);

            using (var db = new LiteDatabase(AppConstante.dbPath))
            {
                var PoIs = db.GetCollection<PointOfInterest>("poi");
                PoIs.Insert(poi);
            }
        }

        public void DeletePoi(int id)
        {
            using (var db = new LiteDatabase(AppConstante.dbPath))
            {
                var PoIs = db.GetCollection<PointOfInterest>("poi");
                if (PoIs.Exists(Query.EQ("_id", id)))
                {
                    PoIs.Delete(id);
                }
            }
        }

        public List<PointOfInterest> GetAllPoI()
        {
            using (var db = new LiteDatabase(AppConstante.dbPath))
            {
                var PoIs = db.GetCollection<PointOfInterest>("poi");
                return PoIs.FindAll().ToList();
            }
        }

        public PointOfInterest GetPoi(int id)
        {
            using (var db = new LiteDatabase(AppConstante.dbPath))
            {
                var PoIs = db.GetCollection<PointOfInterest>("poi");
                return PoIs.FindById(id);
            }
        }
    }
}
