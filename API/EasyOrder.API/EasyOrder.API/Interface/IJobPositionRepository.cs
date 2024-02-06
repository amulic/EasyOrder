using EasyOrder.API.Models.Domain;

namespace EasyOrder.API.Interface
{
    public interface IJobPositionRepository
    {
        JobPosition GetJobPosition(int id);
        ICollection<JobPosition> GetJobPositions();
        bool CreateJobPosition(JobPosition jobPosition);
        bool JobPositionExists(int jobPositionId);
        bool Save();
        
    }
}
