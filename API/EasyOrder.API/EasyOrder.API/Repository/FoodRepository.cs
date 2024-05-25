using EasyOrder.API.Data;
using EasyOrder.API.Interface;
using EasyOrder.API.Models.Domain;

namespace EasyOrder.API.Repository
{
    public class FoodRepository : IFoodRepository
    {
        private readonly ApplicationDbContext _context;

        public FoodRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Food GetFood(int foodId)
        {
            return _context.Foods.Where(x => x.Id == foodId).FirstOrDefault();
        }
        public ICollection<Food> GetFoods()
        {
            return _context.Foods.ToList();
        }

        public bool CreateFood(Food food)
        {
            _context.Foods.Add(food);
            return Save();
        }

        public bool FoodExists(int foodId)
        {
            return _context.Foods.Any(x => x.Id == foodId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
