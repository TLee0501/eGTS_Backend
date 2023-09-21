using coffee_kiosk_solution.Data.Responses;
using eGTS.Bussiness.ReportService;
using eGTS_Backend.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace eGTS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GymerPackageActiveViewModel>>> GetActivePackages()
        {
            var result = await _reportService.GetActivePackages();
            if (result.Count == 0) return NotFound(new ErrorResponse(404, "Không tìm thấy!"));

            return Ok(new SuccessResponse<List<GymerPackageActiveViewModel>>(200, "Tìm thành công!", result));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GymerPackageActiveViewModel>>> GetDonePackages()
        {
            var result = await _reportService.GetDonePackages();
            if (result.Count == 0) return NotFound(new ErrorResponse(404, "Không tìm thấy!"));

            return Ok(new SuccessResponse<List<GymerPackageActiveViewModel>>(200, "Tìm thành công!", result));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GymerPackageActiveViewModel>>> GetPausePackages()
        {
            var result = await _reportService.GetPausePackages();
            if (result.Count == 0) return NotFound(new ErrorResponse(404, "Không tìm thấy!"));

            return Ok(new SuccessResponse<List<GymerPackageActiveViewModel>>(200, "Tìm thành công!", result));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportInComeViewModel>>> getReportInCome()
        {
            var result = await _reportService.getReportInCome();

            return Ok(new SuccessResponse<ReportInComeViewModel>(200, "Thành công!", result));
        }
    }
}
