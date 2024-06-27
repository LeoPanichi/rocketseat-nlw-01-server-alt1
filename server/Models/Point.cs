using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    public class Point
    {
        public Point(
            string image, 
            string name, 
            string email, 
            string whatsapp, 
            double latitude, 
            double longitude, 
            string city, 
            string uf
        )
        {
            Image = image;
            Name = name;
            Email = email;
            Whatsapp = whatsapp;
            Latitude = latitude;
            Longitude = longitude;
            City = city;
            Uf = uf;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Whatsapp { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string City { get; set; }
        public string Uf { get; set; }

        public virtual List<Item> Item { get; } = [];
    }
}