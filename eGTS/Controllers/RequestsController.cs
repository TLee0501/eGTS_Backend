using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using eGTS.Bussiness.RequestService;
using Microsoft.AspNetCore.Http.HttpResults;
using coffee_kiosk_solution.Data.Responses;

namespace eGTS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly EGtsContext _context;
        private readonly IRequestService _requestService;

        public RequestsController(EGtsContext context, IRequestService requestService)
        {
            _context = context;
            _requestService = requestService;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequestsForTest()
        {
          if (_context.Requests == null)
          {
              return NotFound();
          }
            return await _context.Requests.ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet]
        public async Task<ActionResult<Request>> GetRequest(Guid id)
        {
          if (_context.Requests == null)
          {
                return BadRequest(new ErrorResponse(400, "Gửi yêu cầu thất bại!"));
            }
            var request = await _requestService.GetRequest(id);

            if (request == null)
            {
                return BadRequest(new ErrorResponse(400, "Gửi yêu cầu thất bại!"));
            }

            return request;
        }

        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> UpdateRequest(RequestUpdateViewModel request)
        {
            if (request == null)
            {
                return BadRequest(new ErrorResponse(400, "Gửi yêu cầu thất bại!"));
            }

            try
            {
                var result = await _requestService.UpdateRequest(request);
                if(result == false) return BadRequest(new ErrorResponse(400, "Thất bại!"));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return Ok(new SuccessResponse<RequestViewModel>(200, "Thành công!", null));
        }

        // POST: api/Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateRequest(RequestCreateViewModel request)
        {
            if (request == null) return BadRequest(new ErrorResponse(400, "Gửi yêu cầu thất bại!"));
            try
            {
                var result = await _requestService.CreateRequest(request);
                if (result == 0) return BadRequest(new ErrorResponse(400, "Gửi yêu cầu thất bại!"));
                else if (result == 2) return BadRequest(new ErrorResponse(400, "Bạn đã gửi yêu cầu cho người này!"));
                else if (result == 3) return BadRequest(new ErrorResponse(400, "Gói không phù hợp với người được gửi yêu cầu!"));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return Ok(new SuccessResponse<RequestCreateViewModel>(200, "Gửi yêu cầu thành công!", request));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestViewModel>>> GetAllRequestForPTNE(Guid ExpertId)
        {
            if (ExpertId == null) return BadRequest(new ErrorResponse(400, "Vui lòng kiểm tra là thông tin yêu cầu!"));

            var result = await _requestService.GetAllRequestForPTNE(ExpertId);
            if (result == null) return BadRequest(new ErrorResponse(400, "Không tìm thấy yêu cầu!"));
            return Ok(new SuccessResponse<List<RequestViewModel>>(200, "Thành công!", result));
        }

    }
}
