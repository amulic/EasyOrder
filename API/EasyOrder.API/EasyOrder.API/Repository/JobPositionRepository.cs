using EasyOrder.API.Data;
using EasyOrder.API.Interface;
using EasyOrder.API.Models.Domain;

namespace EasyOrder.API.Repository
{
    public class JobPositionRepository : IJobPositionRepository
    {
        private ApplicationDbContext _context;

        public JobPositionRepository(ApplicationDbContext context) 
        { 
            _context = context;
        }
        public bool CreateJobPosition(JobPosition jobPosition)
        {
            _context.JobPositions.Add(jobPosition);
            return Save();
        }

        public JobPosition GetJobPosition(int id)
        {
            return _context.JobPositions.Where(x => x.Id == id).FirstOrDefault();
        }

        public ICollection<JobPosition> GetJobPositions()
        {
            return _context.JobPositions.ToList();
        }

        public bool JobPositionExists(int jobPositionId)
        {
            return _context.JobPositions.Any(x=>x.Id == jobPositionId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
