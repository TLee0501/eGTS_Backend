using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eGTS_Backend.Data.Models;
using eGTS.Bussiness.SuspendService;
using coffee_kiosk_solution.Data.Responses;
using eGTS_Backend.Data.ViewModel;

namespace eGTS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SuspendsController : ControllerBase
    {
        private readonly EGtsContext _context;
        private readonly ISuspendService _suspendService;

        public SuspendsController(EGtsContext context, ISuspendService suspendService)
        {
            _context = context;
            _suspendService = suspendService;
        }


        // GET: api/Suspends
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Suspend>>> GetSuspendsForTest()
        {
          if (_context.Suspends == null)
          {
              return NotFound();
          }
            return await _context.Suspends.ToListAsync();
        }

        // GET: api/Suspends/5
        [HttpGet("{SuspendId}")]
        public async Task<ActionResult<Suspend>> GetSuspend(Guid SuspendId)
        {
            var result = await _suspendService.GetSuspend(SuspendId);
            if (result == null) return NotFound(new ErrorResponse(404, "Không tìm thấy!"));
            else return Ok(new SuccessResponse<SuspendViewModel>(200, "Thành công!", result));
        }

        [HttpGet("{PackageGymerId}")]
        public async Task<ActionResult<Suspend>> GetSuspends(Guid PackageGymerId)
        {
            var result = await _suspendService.GetSuspends(PackageGymerId);
            if (result == null) return NotFound(new ErrorResponse(404, "Không tìm thấy!"));
            else return Ok(new SuccessResponse<List<SuspendViewModel>>(200, "Thành công!", result));
        }

        // POST: api/Suspends
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Suspend>> CreateSuspend(SuspendCreateViewModel suspend)
        {
            if (suspend.From.Date >= suspend.To.Date)
                return BadRequest(new ErrorResponse(400, "Sai dữ liệu ngày bắt đầu/kết thúc!"));
            if (suspend.From.Date <= DateTime.Now.Date)
                return BadRequest(new ErrorResponse(400, "Sai dữ liệu ngày bắt đầu!"));
            var result = await _suspendService.CreateSuspend(suspend);
            if (result == 1) return NotFound(new ErrorResponse(404, "Không tìm thấy!"));
            else if (result == 2) return BadRequest(new ErrorResponse(400, "Sai dữ liệu ngày bắt đầu!"));
            else if (result == 3) return BadRequest(new ErrorResponse(400, "Thời gian tạm ngưng tối đa là 90 ngày!"));
            else if (result == 5) return BadRequest(new ErrorResponse(400, "Chỉ có thể tạm ngưng gói đang hoạt động!"));
            else if (result == 4) return Ok(new ErrorResponse(200, "Thành công!"));
            else return BadRequest(new ErrorResponse(400, "Thất bại!"));
        }
    }
}
