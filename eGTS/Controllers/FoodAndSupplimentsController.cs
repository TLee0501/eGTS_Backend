﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eGTS_Backend.Data.Models;
using eGTS.Bussiness.FoodAndSupplimentService;
using coffee_kiosk_solution.Data.Responses;
using eGTS_Backend.Data.ViewModel;

namespace eGTS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FoodAndSupplimentsController : ControllerBase
    {
        private readonly EGtsContext _context;
        private readonly IFoodAndSupplimentService _foodAndSupplimentService;

        public FoodAndSupplimentsController(EGtsContext context, IFoodAndSupplimentService foodAndSupplimentService)
        {
            _context = context;
            _foodAndSupplimentService = foodAndSupplimentService;
        }

        // GET: api/FoodAndSuppliments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodAndSuppliment>>> GetFoodAndSuppliments()
        {
            var result = await _foodAndSupplimentService.GetFoodAndSuppliments();
            if (result != null)
            {
                return Ok(new SuccessResponse<List<FoodAndSupplimentViewModel>>(200, "Danh sách thực phẩm", result));
            }
            else
                return NotFound(new ErrorResponse(404, "Không tìm thấy thực phẩm!"));
        }

        // GET: api/FoodAndSupplimentsByNE
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<FoodAndSuppliment>>> GetFoodAndSupplimentsBYNE(Guid id)
        {
            var result = await _foodAndSupplimentService.GetFoodAndSupplimentsBYNE(id);
            if (result != null)
            {
                return Ok(new SuccessResponse<List<FoodAndSupplimentViewModel>>(200, "List of FoodAndSuppliment found", result));
            }
            else
                return NotFound(new ErrorResponse(204, "No FoodAndSuppliment Found"));
        }

        // GET: api/FoodAndSuppliments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodAndSuppliment>> GetFoodAndSuppliment(Guid id)
        {
            var result = await _foodAndSupplimentService.GetFoodAndSuppliment(id);
            if (result != null)
            {
                return Ok(new SuccessResponse<FoodAndSupplimentViewModel>(200, "FoodAndSuppliment found", result));
            }
            else
                return NoContent();
        }

        // PUT: api/FoodAndSuppliments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> UpdateFoodAndSuppliment(FoodAndSupplimentUpdateViewModel foodAndSuppliment)
        {
            if (foodAndSuppliment == null) return BadRequest(new ErrorResponse(400, "Cập nhật thất bại!"));
            try
            {
                var result = await _foodAndSupplimentService.UpdateFoodAndSuppliment(foodAndSuppliment);
                return Ok(new SuccessResponse<FoodAndSupplimentUpdateViewModel>(200, "Cập nhật thành công!", foodAndSuppliment));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(400, "Cập nhật thất bại!"));
            }
        }

        // POST: api/FoodAndSuppliments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FoodAndSuppliment>> CreateFoodAndSuppliment(FoodAndSupplimentCreateViewModel foodAndSuppliment)
        {
            if (foodAndSuppliment == null) return BadRequest(new ErrorResponse(400, "Tạo mới thất bại!"));
            try
            {
                var result = await _foodAndSupplimentService.CreateFoodAndSuppliment(foodAndSuppliment);
                return Ok(new SuccessResponse<FoodAndSupplimentCreateViewModel>(200, "Tạo mới thành công!", foodAndSuppliment));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(400, "Tạo mới thất bại!"));
            }
        }

        // DELETE: api/FoodAndSuppliments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodAndSuppliment(Guid id)
        {

            if (id == null) return BadRequest(new ErrorResponse(400, "Xóa thất bại!"));
            var result = await _foodAndSupplimentService.DeleteFoodAndSuppliment(id);
            if (result == true) return Ok(new SuccessResponse<FoodAndSupplimentCreateViewModel>(200, "Xóa thành công!", null));
            else
            {
                return BadRequest(new ErrorResponse(400, "Xóa thất bại!"));
            }
        }

        private bool FoodAndSupplimentExists(Guid id)
        {
            return (_context.FoodAndSuppliments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
