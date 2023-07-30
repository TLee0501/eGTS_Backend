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
              return NotFound();
          }
            var request = await _requestService.GetRequest(id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> UpdateRequest(RequestViewModel request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            try
            {
                await _requestService.UpdateRequest(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return Ok();
        }

        // POST: api/Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateRequest(RequestCreateViewModel request)
        {
            if (request == null) return BadRequest();
            try
            {
                var result = await _requestService.CreateRequest(request);
                if (result == 0) return BadRequest("Gửi yêu cầu thất bại!");
                else if(result == 2) return BadRequest("Bạn đã gửi yêu cầu cho người này!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return Ok("Gửi yêu cầu thành công.");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestViewModel>>> GetAllRequestForPTNE(Guid id, bool isPT)
        {
            if (id == null || isPT == null) return BadRequest();

            var result = await _requestService.GetAllRequestForPTNE(id, isPT);
            return Ok(result);
        }

    }
}
