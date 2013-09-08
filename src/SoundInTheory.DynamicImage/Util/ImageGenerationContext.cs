using System.Web;

namespace SoundInTheory.DynamicImage.Util
{
    public class ImageGenerationContext
    {
        public HttpContext HttpContext { get; private set; }

        public ImageGenerationContext(HttpContext httpContext)
        {
            HttpContext = httpContext;
        }
    }
}