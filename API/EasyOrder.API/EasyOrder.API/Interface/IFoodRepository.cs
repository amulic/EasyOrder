using EasyOrder.API.Models.Domain;

namespace EasyOrder.API.Interface
{
    public interface IFoodRepository
    {
        Food GetFood(int foodId);
        ICollection<Food> GetFoods();
        bool CreateFood(Food food);
        bool FoodExists(int foodId);
        bool Save();
        bool DeleteFood(Food foodToDelete);
    }
}
