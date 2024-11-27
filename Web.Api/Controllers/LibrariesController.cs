using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.PixelFormats;
using Web.Models;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrariesController : ControllerBase
    {
        [HttpPost]
        [Route("convert/imagetowebp")]
        public async Task<byte[]> ConvertImageToWebP([FromBody] StringRequest base64)
        {
            var buf = Convert.FromBase64String(base64.Data);
            // Load the image using ImageSharp
            using (Image<Rgba32> image = Image.Load<Rgba32>(buf))
            using (var outputStream = new MemoryStream())
            {
                // Save the image to the output stream as WebP format
                image.Save(outputStream, new WebpEncoder());

                // Return the WebP image as a byte array
                return outputStream.ToArray();
            }
        }
    }
}
