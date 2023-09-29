using coffee_kiosk_solution.Data.Responses;
using eGTS.Bussiness.MealService;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eGTS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly EGtsContext _context;
        private readonly IMealService _mealService;

        public MealsController(EGtsContext context, IMealService mealService)
        {
            _context = context;
            _mealService = mealService;
        }

        // GET: api/Meals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meal>>> GetMealsForTest()
        {
            if (_context.Meals == null)
            {
                return NotFound();
            }
            return await _context.Meals.ToListAsync();
        }

        // PUT: api/Meals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> UpdateMeal(MealCreateViewModel request)
        {
            if (request.ToDatetime < request.FromDatetime) return BadRequest("Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu!");
            if (request.MonAnSang == null && request.MonAnTrua == null && request.MonAnToi == null && request.MonAnTruocTap == null)
                return BadRequest("Vui lòng nhập món ăn để cập nhật thực đơn!");

            var result = await _mealService.UpdateMeal(request);
            if (result == false) return BadRequest("Cập nhật thực đơn thất bại!");
            return Ok("Cập nhật  thực đơn thành công!");
        }

        // POST: api/Meals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Meal>> CreateMeal(MealCreateViewModel request)
        {
            if (request.ToDatetime < request.FromDatetime) return BadRequest("Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu!");
            if (request.MonAnSang == null && request.MonAnTrua == null && request.MonAnToi == null && request.MonAnTruocTap == null)
                return BadRequest("Vui lòng nhập món ăn để tạo thực đơn!");

            var check = await _mealService.CheckvalidDate(request);
            if (check == false) return BadRequest("Vui lòng kiểm tra lại thời gian bữa ăn!");

            var result = await _mealService.CreateMeal(request);
            if (result == false) return BadRequest("Tạo thực đơn thất bại!");
            return Ok("Tạo thực đơn thành công!");
        }

        // DELETE: api/Meals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeal(Guid id)
        {
            if (_context.Meals == null)
            {
                return NotFound();
            }
            var meal = await _context.Meals.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }

            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<Meal>> UpdateMealFood(MealCreateViewModel request)
        {
            if (request.ToDatetime < request.FromDatetime) return BadRequest(new ErrorResponse(400, "Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu!"));
            /*if (request.MonAnSang == null && request.MonAnTrua == null && request.MonAnToi == null && request.MonAnTruocTap == null)
                return BadRequest("Vui lòng nhập món ăn để tạo thực đơn!");*/

            /*var check = await _mealService.CheckvalidDate(request);
            if (check == false) return BadRequest("Vui lòng kiểm tra lại thời gian bữa ăn!");*/

            var result = await _mealService.UpdateMealFood(request);
            if (result == false) return BadRequest(new ErrorResponse(400, "Tạo thực đơn thất bại!"));
            return Ok(new ErrorResponse(200, "Tạo thực đơn thành công!"));
        }
    }
}
