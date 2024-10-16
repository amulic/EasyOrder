
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyOrder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {


        public FileUploadController()
        {
        }

        [HttpPost("upload")] 
        public async Task<IActionResult> Upload(IFormFile file)
        {
            //max 5mb 
            if (file.Length > 5 * 1024 * 1024) { 
                return BadRequest();
            }
            var generator = new BackblazePresignedUrlGenerator();

            var id = Guid.NewGuid();
            var url = generator.GeneratePresignedUrl($"food/{id}", file.ContentType, TimeSpan.FromMinutes(15));

            return Ok(url);


        //    if (image == null || image.Length == 0)
        //    {
        //        return BadRequest("No file selected for upload");
        //    }

            //    try
            //    {
            //        using (var memoryStream = new MemoryStream())
            //        {
            //            await image.CopyToAsync(memoryStream);
            //            var fileBytes = memoryStream.ToArray();

            //            var response = await _b2Client.Files.Upload(fileBytes, image.FileName, "EasyOrder");
            //            var fileUrl = $"https://s3.us-east-005.backblazeb2.com/easyorder/{response.FileName}";
            //            return Ok(new { FileUrl = fileUrl });
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        return StatusCode(500, $"Internal server error: {ex.Message}");
            //    }

        }
    }
}
