using coffee_kiosk_solution.Data.Responses;
using eGTS.Bussiness.AccountService;
using eGTS.Bussiness.FeedbackService;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eGTS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly EGtsContext _context;
        private readonly ILogger<FeedbackController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(EGtsContext context, ILogger<FeedbackController> logger, IConfiguration configuration, IFeedbackService feedbackService)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _feedbackService = feedbackService;
        }


        /// <summary>
        /// Create a new feedback
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status201Created)]//CREATED
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<bool>> CreateFeedback(FeedbackCreateViewModel request)
        {
            if (request.PackageGymerId == null || request.PackageGymerId.Equals(""))
                return BadRequest(new ErrorResponse(400, "PackageGymerId đang bị bỏ trống."));

            if (request.PtidorNeid == null || request.PtidorNeid.Equals(""))
                return BadRequest(new ErrorResponse(400, "PTID hoặc NEID đang bị bỏ trống."));

            if (request.Rate > 5 || request.Rate < 0)
                return BadRequest(new ErrorResponse(400, "Số sao bị sai."));

            switch (await _feedbackService.CreateFeedback(request))
            {
                case 0:
                    return BadRequest(new ErrorResponse(400, "Không thể tạo đánh giá"));
                    break;
                case 1:
                    return BadRequest(new ErrorResponse(400, "Không tìm thấy PT hay NE"));
                    break;
                case 2:
                    return BadRequest(new ErrorResponse(400, "Không tìm thấy hợp đồng"));
                    break;
                case 3:
                    _logger.LogInformation($"Created Feedback with for expert ID: {request.PtidorNeid}");
                    return Ok(new SuccessResponse<FeedbackCreateViewModel>(200, "Tạo đánh giá thành công", request));
                    break;
                case 4:
                    return BadRequest(new ErrorResponse(400, "Không có PT hay NE trong hợp đồng"));
                    break;
                case 5:
                    return BadRequest(new ErrorResponse(400, "Chưa được gửi đánh giá"));
                    break;
                case 6:
                    return BadRequest(new ErrorResponse(400, "Bạn đã đánh giá hợp đồng này rồi."));
                    break;
                default:
                    return BadRequest(new ErrorResponse(400, "Không thể tạo đánh giá"));
                    break;
            }

        }

        /// <summary>
        /// Change isDelete status of feedback to true
        /// </summary>
        /// <param name="feedbackID"></param>
        /// <returns></returns>
        [HttpDelete("{feedbackID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteFeedback(Guid feedbackID)
        {
            if (await _feedbackService.ChangeisDeleteFeedback(feedbackID, true))
            {
                _logger.LogInformation($"Delete Feedback with ID: {feedbackID}");
                return Ok(new SuccessResponse<FeedbackCreateViewModel>(200, "Xóa feedback thành công.", null));
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPut("{feedbackID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UnDeleteFeedback(Guid feedbackID)
        {
            if (await _feedbackService.ChangeisDeleteFeedback(feedbackID, false))
            {
                _logger.LogInformation($"Delete Feedback with ID: {feedbackID}");
                return Ok(new SuccessResponse<FeedbackCreateViewModel>(200, "khôi phục feedback thành công.", null));
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// REMOVE Feedback (PERMANENT)
        /// </summary>
        /// <param name="feedbackID"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> REMOVEFeedback(Guid feedbackID)
        {
            if (await _feedbackService.REMOVEFeedback(feedbackID))
            {
                _logger.LogInformation($"Delete Feedback with ID: {feedbackID}");
                return Ok(new SuccessResponse<FeedbackCreateViewModel>(200, "Xóa vĩn viễn feedback thành công.", null));
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status204NoContent)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<IActionResult> UpdateFeedback(Guid id, FeedbackUpdateViewModel request)
        {
            if (request.Rate > 5 || request.Rate < 0)
                return BadRequest(new ErrorResponse(400, "Số sao bị sai."));
            switch (await _feedbackService.UpdateFeedback(id, request))
            {
                case 0:
                    return BadRequest(new ErrorResponse(400, "Không thể cập nhập feedback"));
                    break;
                case 1:
                    return BadRequest(new ErrorResponse(400, "Số sao bị sai."));
                    break;
                case 2:
                    _logger.LogInformation($"Update Feedback ID: {id}");
                    return Ok(new SuccessResponse<FeedbackUpdateViewModel>(200, "Update Feedback thành công", null));
                    break;
                default:
                    return BadRequest(new ErrorResponse(400, "Không thể cập nhập feedback"));
                    break;
            }
        }

        /// <summary>
        /// Get ALL Feedback(DEBUG ONLY!)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status204NoContent)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<IEnumerable<FeedbackViewModel>>> DEBUGGetAllFeedback()
        {
            var result = await _feedbackService.DEBUGGetALLFeedback();
            if (result != null)
            {
                return Ok(new SuccessResponse<List<FeedbackViewModel>>(200, "Danh sách các feedback", result));
            }
            else
                return NoContent();
        }

        /// <summary>
        /// Get Feedbacks with ExpertID
        /// </summary>
        /// <param name="expertID"></param>
        /// <param name="isDelete"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status204NoContent)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<IEnumerable<FeedbackViewModel>>> GetFeedbackListByExpertID(Guid expertID, bool? isDelete)
        {
            var result = await _feedbackService.GetFeedbackListByExpertID(expertID, isDelete);
            if (result != null)
            {
                return Ok(new SuccessResponse<List<FeedbackViewModel>>(200, "Danh sách các feedback", result));
            }
            else
                return NoContent();
        }

        /// <summary>
        /// Get Feedbacks with PackageGymerID
        /// </summary>
        /// <param name="packageGymerID"></param>
        /// <param name="isDelete"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status204NoContent)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<IEnumerable<FeedbackViewModel>>> GetFeedbackListByPackageGymerID(Guid packageGymerID, bool? isDelete)
        {
            var result = await _feedbackService.GetFeedbackListByPackageGymerID(packageGymerID, isDelete);
            if (result != null)
            {
                return Ok(new SuccessResponse<List<FeedbackViewModel>>(200, "Danh sách các feedback", result));
            }
            else
                return NoContent();
        }

        /// <summary>
        /// Get the feedback by it's ID
        /// </summary>
        /// <param name="feedbackID"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status204NoContent)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<FeedbackViewModel>> GetFeedbackByID(Guid feedbackID)
        {
            var result = await _feedbackService.GetFeedbackByID(feedbackID);
            if (result != null)
                return Ok(new SuccessResponse<FeedbackViewModel>(200, "Tìm thấy feedback", result));
            else
                return NoContent();
        }


        /// <summary>
        /// get the average feedback rating for expert by their ID(uses diffence viewmodel)
        /// </summary>
        /// <param name="expertID"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status204NoContent)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<FeedbackAverageViewModel>> GetAverageRatingByExpertID(Guid expertID)
        {
            var result = await _feedbackService.GetAverageRatingByExpertID(expertID);
            if (result != null)
                return Ok(new SuccessResponse<FeedbackAverageViewModel>(200, $"Rating trung bình của chuyên gia với ID: {expertID}", result));
            else
                return NoContent();
        }

    }
}
