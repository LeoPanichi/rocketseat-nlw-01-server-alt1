using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Dto;

namespace server.Controllers
{
    [Route("points")]
    [ApiController]
    public class PointsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        private readonly IHttpContextAccessor _accessor;

        public PointsController(DataContext context, IWebHostEnvironment hostingEnvironment, IHttpContextAccessor accessor)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _accessor = accessor;
        }

        [HttpGet]
        public IActionResult GetPoints([FromQuery]PointQueryParam parameters)
        {
            var host = _accessor.HttpContext!.Request.Host.ToString();
            if (!ModelState.IsValid)
                BadRequest(ModelState);
            
            //List<Point> Points = [.. _context.Points];
            var query = _context.Points.AsQueryable();
            if (parameters.City != null)
                query = query.Where(point => point.City == parameters.City);

            if (parameters.Uf != null)
                query = query.Where(point => point.Uf == parameters.Uf);

            if (parameters.Items != null)
            {
                int[] items = [.. Array.ConvertAll(parameters.Items.Split(","), S => Int32.Parse(S.Trim()))];
                // I could not find anywhere how to do this other way
                // In this n-to-n relationship, Point will always have 1 or more Items
                // Whenever I access Item from Point and try to use Contains or some similar solution it throws error
                // This way I select only the Ids from Items, intersect them with my filter array and count this intersection
                // If matches the number of items in the filter array, it means it has been filtered:
                //query = query.Where(point => point.Item.Select(x => x.Id).Intersect(items).Count() == items.Length);
                // Then, I realize that this filter was supposed to be an OR, not an AND. So...
                // If matches ANY number of items in the filter array, it means it has been filtered:
                // (Didnt use the Any(), because everywhere is said it takes a toll on performance)
                query = query.Where(point => point.Item.Select(x => x.Id).Intersect(items).Count() > 0);
            }

            List<Point> Points = [.. query];

            List<PointResponse> response = Points.Select(point => new PointResponse(point, host)).ToList();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetPoint(int id)
        {
            var host = _accessor.HttpContext!.Request.Host.ToString();
            var Point = _context.Points.Find(id);
            if (Point == null)
            {
                NoContent();
            }

            return Ok(new PointResponse(Point!, host));

        }

        [HttpPost]
        public IActionResult CreatePoint([FromForm] PointRequest pointRequest)
        {
            if (!ModelState.IsValid)
                BadRequest(ModelState);

            pointRequest.SetImageName(4);
            string fileName = pointRequest.GetImageName();            
            var Point = pointRequest.ToModel();
            string[] ItemsId = pointRequest.Items.Split(",");
            List<Point2Item> Point2Item = new();
            
            using (var context = _context)
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                         if (pointRequest.Image != null && pointRequest.Image.Length > 0)
                        {
                            var path = _hostingEnvironment.ContentRootPath + "/uploads/user/" + fileName;

                            using (FileStream fs = System.IO.File.Create(path))
                            {
                                pointRequest.Image.CopyTo(fs);
                            }
                        }
                        context.Points.Add(Point);

                        context.SaveChanges();

                        var PointId = Point.Id;

                        foreach (string ItemId in ItemsId)
                        {
                            Point2Item.Add(new Point2Item(PointId, Int32.Parse(ItemId.Trim())));
                        }

                        context.Point2Items.AddRange(Point2Item);

                        context.SaveChanges();

                        dbContextTransaction.Commit();
                    }
                    catch (Exception) 
                    { 
                        dbContextTransaction.Rollback(); //Required according to MSDN article 
                        //throw; //Not in MSDN article, but recommended so the exception still bubbles up
                        return StatusCode(500, "Internal server error");
                    } 
                }
            }
            return CreatedAtAction(nameof(GetPoint), new {id = Point.Id}, Point);
        }
    }
}