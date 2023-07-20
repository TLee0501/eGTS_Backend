using Microsoft.AspNetCore.Mvc;
using eGTS_Backend.Data.Models;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace eGTS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QualificationsController : ControllerBase
    {
        private readonly EGtsContext _context;

        public QualificationsController(EGtsContext context)
        {
            _context = context;
        }


        private async Task<string> UploadImageToFirebaseStorage(Stream imageStream, string imageName)
        {
            var credential = GoogleCredential.FromFile("egts-2023-firebase.json");
            var storage = await StorageClient.CreateAsync(credential);

            var bucketName = "egts-2023.appspot.com"; // Replace with your actual bucket name

            using (var stream = imageStream)
            {
                var imageObject = await storage.UploadObjectAsync(bucketName, imageName, null, stream);
                var url = $"https://storage.googleapis.com/{bucketName}/{imageName}";
                return url;
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("No image file provided");
            }

            using var imageStream = imageFile.OpenReadStream();

            try
            {
                var imageUrl = await UploadImageToFirebaseStorage(imageStream, imageFile.FileName);

                // Return the image URL in the API response
                return Ok(new { imageUrl });
            }
            catch (Exception ex)
            {
                // Handle the exception accordingly
                return StatusCode(500, "Failed to upload image: " + ex.Message);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /*// GET: api/Qualifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Qualification>>> GetQualifications()
        {
          if (_context.Qualifications == null)
          {
              return NotFound();
          }
            return await _context.Qualifications.ToListAsync();
        }*/

        /*// GET: api/Qualifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Qualification>> GetQualification(Guid id)
        {
          if (_context.Qualifications == null)
          {
              return NotFound();
          }
            var qualification = await _context.Qualifications.FindAsync(id);

            if (qualification == null)
            {
                return NotFound();
            }

            return qualification;
        }*/

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

        /*// POST: api/Qualifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Qualification>> CreateQualification(Qualification qualification)
        {
          if (_context.Qualifications == null)
          {
              return Problem("Entity set 'EGtsContext.Qualifications'  is null.");
          }
            _context.Qualifications.Add(qualification);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (QualificationExists(qualification.ExpertId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetQualification", new { id = qualification.ExpertId }, qualification);
        }*/

        /*// DELETE: api/Qualifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQualification(Guid id)
        {
            if (_context.Qualifications == null)
            {
                return NotFound();
            }
            var qualification = await _context.Qualifications.FindAsync(id);
            if (qualification == null)
            {
                return NotFound();
            }

            _context.Qualifications.Remove(qualification);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        private bool QualificationExists(Guid id)
        {
            return (_context.Qualifications?.Any(e => e.ExpertId == id)).GetValueOrDefault();
        }
    }
}
