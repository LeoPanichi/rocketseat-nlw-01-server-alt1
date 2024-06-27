using Microsoft.AspNetCore.Mvc;
using server.Dto;
using server.Models;

namespace server.Controllers
{
    [Route("items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly IHttpContextAccessor _accessor;

        public ItemsController(DataContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        [HttpGet]
        public IActionResult GetItems()
        {
            var host = _accessor.HttpContext!.Request.Host.ToString();
            //var Items = _context.Items.ToList();
            List<Item> Items = [.. _context.Items];
            List<ItemResponse> response = Items.Select(item => new ItemResponse(item, host)).ToList();
            return Ok(response);
        }
    }
}