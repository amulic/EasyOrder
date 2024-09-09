using B2Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyOrder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly B2Client _b2Client;

        public FileUploadController(B2Client b2Client)
        {
            _b2Client = b2Client;
        }

        [HttpPost("upload")] 
        public async Task<IActionResult> Upload([FromForm]IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("No file selected for upload");
            }

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();

                    var response = await _b2Client.Files.Upload(fileBytes, image.FileName, "EasyOrder");
                    var fileUrl = $"https://s3.us-east-005.backblazeb2.com/easyorder/{response.FileName}";
                    return Ok(new { FileUrl = fileUrl });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
    }
}
