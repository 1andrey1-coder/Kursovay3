using ApiDB;
using ApiDB.DB;

namespace Kursovay2Api2._0.Service
{
    public class RoflCRUD
    {
        private readonly MemContext _memContext;

        public RoflCRUD(MemContext userService)
        {
           
            _memContext = userService;
           
        }
        public void Add(Rofl rofl)
        {
            _memContext.Rofls.Add(rofl);
            _memContext.SaveChanges();
        }
        public void Edit(Rofl rofl)
        {
            _memContext.Rofls.FirstOrDefault(s => s.RoflId == rofl.RoflId).RoflName = rofl.RoflName;
            _memContext.Rofls.FirstOrDefault(s => s.RoflOpisanie == rofl.RoflOpisanie).RoflOpisanie = rofl.RoflOpisanie;
            _memContext.Rofls.FirstOrDefault(s => s.RoflStatusId == rofl.RoflStatusId).RoflStatusId = rofl.RoflStatusId;
            _memContext.Rofls.FirstOrDefault(s => s.RoflStartId == rofl.RoflStartId).RoflStartId = rofl.RoflStartId;
            _memContext.Rofls.FirstOrDefault(s => s.RoflGenreId == rofl.RoflGenreId).RoflGenreId = rofl.RoflGenreId;
            _memContext.Rofls.FirstOrDefault(s => s.RoflEndId == rofl.RoflEndId).RoflEndId = rofl.RoflEndId;
            _memContext.Rofls.FirstOrDefault(s => s.RoflDateTime == rofl.RoflDateTime).RoflDateTime = rofl.RoflDateTime;

            _memContext.SaveChanges();
        }
     
    }
}
