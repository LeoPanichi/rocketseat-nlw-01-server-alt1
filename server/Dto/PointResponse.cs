using server.Models;

namespace server.Dto
{
    public class PointResponse
    {
        public PointResponse(Point point, string host)
        {
            Image = "http://" + host + "/uploads/user/" + point.Image;
            Name = point.Name;
            Email = point.Email;
            Whatsapp = point.Whatsapp;
            Latitude = point.Latitude;
            Longitude = point.Longitude;
            City = point.City;
            Uf = point.Uf;

        }

        public string Image { get; }
        public string Name { get; }
        public string Email { get; }
        public string Whatsapp { get; }
        public double Latitude { get; }
        public double Longitude { get; }
        public string City { get; }
        public string Uf { get; }
    }

}
