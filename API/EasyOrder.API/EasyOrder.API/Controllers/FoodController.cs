using AutoMapper;
using EasyOrder.API.Interface;
using EasyOrder.API.Models.Domain;
using EasyOrder.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyOrder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private IFoodRepository _foodRepository;
        private IMapper _mapper;

        public FoodController(IFoodRepository foodRepository, IMapper mapper)
        {
            _foodRepository = foodRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Food[]>> GetFoods()
        {
            var foods = _mapper.Map<List<FoodDto>>(_foodRepository.GetFoods());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(foods);

        }

        [HttpGet("{foodId}")]
        [ProducesResponseType(200, Type = typeof(Food))]
        [ProducesResponseType(400)]
        public IActionResult GetFood(int foodId)
        {
            if (!_foodRepository.FoodExists(foodId))
                return NotFound();

            var food = _mapper.Map<FoodDto>(_foodRepository.GetFood(foodId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(food);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Food>> CreateFood([FromBody] FoodDto foodCreate)
        {
            if (foodCreate == null)
                return BadRequest();

            var food = _foodRepository.GetFoods().Where(c => c.Name.Trim().ToUpper() == foodCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (food != null)
            {
                ModelState.AddModelError("", "Food already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest();

            var foodMap = _mapper.Map<Food>(foodCreate);


            if (!_foodRepository.CreateFood(foodMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(new { Message = "Successfully created" });
        }

        [HttpDelete("{foodId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFood(int foodId)
        {
            if (!_foodRepository.FoodExists(foodId))
                return NotFound("Food item not found!");

            var foodToDelete = _foodRepository.GetFood(foodId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_foodRepository.DeleteFood(foodToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting food item!");
            }

            return NoContent();
        }

    }
}
