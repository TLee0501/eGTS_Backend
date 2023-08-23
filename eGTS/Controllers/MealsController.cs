using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using System.Web.WebPages;
using eGTS.Bussiness.MealService;

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

        // GET: api/Meals/5
        /*[HttpGet("{id}")]
        public async Task<ActionResult<Meal>> GetMeal(Guid id)
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

            return meal;
        }*/

        // PUT: api/Meals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> UpdateMeal(MealCreateViewModel request)
        {
            if (request.ToDatetime < request.FromDatetime) return BadRequest("Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu!");
            if (request.MonAnSang == null && request.MonAnTrua == null && request.MonAnToi == null && request.MonAnTruocTap == null)
                return BadRequest("Vui lòng nhập món ăn để cập nhật  thực đơn!");

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

        private bool MealExists(Guid id)
        {
            return (_context.Meals?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
