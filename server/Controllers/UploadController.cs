using Microsoft.AspNetCore.Mvc;

namespace server.Controllers
{
    [Route("uploads")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UploadController(DataContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("{imgName}")]
        public IActionResult GetImage(string imgName)
        {
            var image = System.IO.File.OpenRead(_hostingEnvironment.ContentRootPath + "/uploads/" + imgName);
            return File(image, "image/svg+xml");
        }

        [HttpGet("user/{imgName}")]
        public IActionResult GetUserImage(string imgName)
        {
            var image = System.IO.File.OpenRead(_hostingEnvironment.ContentRootPath + "/uploads/user/" + imgName);
            return File(image, "image/jpg");
        }
    }
}