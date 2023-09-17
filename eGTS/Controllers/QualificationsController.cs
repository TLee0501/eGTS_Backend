using Microsoft.AspNetCore.Mvc;
using eGTS_Backend.Data.Models;
using Microsoft.EntityFrameworkCore;
using eGTS_Backend.Data.ViewModel;
using eGTS.Bussiness.QualitificationService;
using Azure.Core;

namespace eGTS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QualificationsController : ControllerBase
    {
        private readonly EGtsContext _context;
        private readonly IQualitificationService _qualitificationService;

        public QualificationsController(EGtsContext context, IQualitificationService qualitificationService)
        {
            _context = context;
            _qualitificationService = qualitificationService;
        }

        // GET: api/Qualifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Qualification>>> GetQualificationsForTest()
        {
            if (_context.Qualifications == null)
            {
                return NotFound();
            }
            return await _context.Qualifications.ToListAsync();
        }

        // GET: api/Qualifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QualitificationViewModel>> GetQualificationByAccountId(Guid id)
        {
            var result = await _qualitificationService.GetQualitificationByAccountId(id);
            if (result == null)
            {
                return NotFound("Không tìm thấy chứng chỉ!");
            }
            return result;
        }

        // PUT: api/Qualifications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> UpdateQualification(QualitificationViewModel qualification)
        {
            if (qualification == null) return Problem("'Qualifications' is null.");
            if (qualification.Experience < 0) return BadRequest("Experience không hợp lệ!");
            bool result = await _qualitificationService.UpdateQualitification(qualification);
            if (result)
            {
                return Ok("Cập nhật chứng chỉ thành công!");
            }
            return StatusCode(500, "Cập nhật chứng chỉ thất bại!");
        }

        // POST: api/Qualifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<bool>> CreateQualification(QualitificationCreateViewModel request)
        {
            if (request == null) return Problem("'Qualifications' is null.");
            if (request.Experience < 0) return BadRequest("Experience không hợp lệ!");

            bool result;
            result = await _qualitificationService.CreateQualitification(request);
            if (result)
            {
                return Ok();
            }
            return StatusCode(500, "Tạo chứng chỉ mới thất bại!");
        }

        // DELETE: api/Qualifications/5
        [HttpDelete("{ExpertId}")]
        public async Task<IActionResult> DeleteQualificationByAccountId(Guid ExpertId)
        {
            if (ExpertId == Guid.Empty)
            {
                return BadRequest();
            }
            var result = await _qualitificationService.DeleteQualitification(ExpertId);
            if (result)
            {
                return Ok();
            }
            return StatusCode(500, "Xóa chứng chỉ thất bại!");
        }
    }
}
