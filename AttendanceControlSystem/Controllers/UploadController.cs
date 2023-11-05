using AttendanceControlSystem.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Net.Http.Headers;

namespace AttendanceControlSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{RoleConstants.Admin}, {RoleConstants.Teacher}")]
    public class UploadController : ControllerBase
    {
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadAsync() 
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var extension = fileName.Split('.')[1];
                    var fullFileName = $"{Guid.NewGuid()}.{extension}";
                    var fullPath = Path.Combine(pathToSave, fullFileName);
                    var dbPath = Path.Combine(folderName, fullFileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    return Ok(new { dbPath });
                }
                else 
                {
                    throw new Exception("The photo was not received");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete("{*imagePath}")]
        public async Task<IActionResult> DeleteAsync(string imagePath)
        {
            if (imagePath.IsNullOrEmpty())
                throw new Exception("Image was not found");

            var urlPart = imagePath.Split("Delete/");

            string decodedPath = WebUtility.UrlDecode(urlPart[1]);

            if (System.IO.File.Exists(decodedPath))
            {
                try
                {
                    System.IO.File.Delete(decodedPath);
                }
                catch
                {
                    throw new Exception("Removing photo failed");
                }
            }
            else
            {
                throw new Exception("Current photo does not exist");
            }

            return NoContent();
        }
    }
}
