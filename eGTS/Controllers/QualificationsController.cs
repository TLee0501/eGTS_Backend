using Microsoft.AspNetCore.Mvc;
using eGTS_Backend.Data.Models;
using Microsoft.EntityFrameworkCore;
using eGTS_Backend.Data.ViewModel;
using eGTS.Bussiness.QualitificationService;

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

        /*[HttpPost("upload")]
        public async Task<IActionResult> UploadAvatar(IFormFile imageFile, Guid Id)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("No image file provided");
            }

            try
            {
                var imageUrl = await _iFrirebaseService.UploadAvatarImage(imageFile, Id);

                return Ok();
            }
            catch (Exception ex)
            {
                // Handle the exception accordingly
                return StatusCode(500, "Failed to upload image: " + ex.Message);
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadQualification(IFormFile imageFile, Guid Id)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("No image file provided");
            }

            try
            {
                var imageUrl = await _iFrirebaseService.UploadCertificateImage(imageFile, Id);

                return Ok();
            }
            catch (Exception ex)
            {
                // Handle the exception accordingly
                return StatusCode(500, "Failed to upload image: " + ex.Message);
            }
        }


        [HttpGet("{imageName}")]
        public async Task<IActionResult> GetImageForTest(string imageName)
        {
            try
            {
                string projectId = "egts-2023";
                string bucketName = "egts-2023.appspot.com";
                string imagePath = imageName;

                var credential = GoogleCredential.FromFile("egts-2023-firebase.json");
                var storageClient = StorageClient.Create(credential);

                var imageObject = storageClient.GetObject(bucketName, imagePath);
                var imageStream = new MemoryStream();
                await storageClient.DownloadObjectAsync(bucketName, imagePath, imageStream);
                //await storageClient.DeleteObjectAsync(bucketName, imagePath);

                // Reset the memory stream position
                imageStream.Position = 0;

                // Return the image stream
                return new FileStreamResult(imageStream, "image/jpeg"); // or the appropriate content type
            }
            catch (Exception ex)
            {
                // Handle the exception and return an appropriate response
                return StatusCode(500, ex.Message);
            }
        }*/



        /////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
                return NotFound();
            }
            return result;
        }

        /*// PUT: api/Qualifications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQualification(Guid id, Qualification qualification)
        {
            if (id != qualification.ExpertId)
            {
                return BadRequest();
            }

            _context.Entry(qualification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QualificationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/Qualifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<bool>> CreateQualification(QualitificationCreateViewModel request)
        {
            if (request == null)
            {
                return Problem("'Qualifications' is null.");
            }
            bool result;
            result = await _qualitificationService.CreateQualitification(request);
            if (result)
            {
                return Ok();
            }
            return StatusCode(500, "Failed to Create Qualitification");
        }

        /*// DELETE: api/Qualifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQualification(Guid id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var qualification = await _iFrirebaseService.DeleleQualification(id);
            if (qualification == false)
            {
                return BadRequest();
            }

            return Ok();
        }*/
    }
}
