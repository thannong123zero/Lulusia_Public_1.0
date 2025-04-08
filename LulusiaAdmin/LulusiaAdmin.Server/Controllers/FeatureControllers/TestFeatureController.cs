using Microsoft.AspNetCore.Mvc;

namespace LulusiaAdmin.Server.Controllers.FeatureControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestFeatureController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetImage()
        {
            string imageUrl = "https://t4.ftcdn.net/jpg/07/23/14/93/240_F_723149335_tA0Fo8zefrHzYlSgXRMYHmBQk7CuWrRd.jpg";
            // Validate the URL to prevent misuse (optional, but recommended)
            if (!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
            {
                return BadRequest("Invalid image URL.");
            }

            using (var httpClient = new HttpClient())
            {
                // Fetch the image from the external server
                var response = await httpClient.GetAsync(imageUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return NotFound("Image not found.");
                }

                // Read the image content
                var content = await response.Content.ReadAsByteArrayAsync();
                var contentType = response.Content.Headers.ContentType.ToString();

                // Return the image as a FileResult
                return File(content, contentType);
            }

        }
    }
}
