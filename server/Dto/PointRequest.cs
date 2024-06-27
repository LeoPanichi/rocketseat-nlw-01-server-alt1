using System.Security.Cryptography;
using server.Models;

namespace server.Dto
{
    public class PointRequest
    {
        public required IFormFile Image { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Whatsapp { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public required string City { get; set; }
        public required string Uf { get; set; }
        public required string Items { get; set; }
        private string imageName { get; set; } = Path.GetRandomFileName(); 

        public Point ToModel()
        {
            return new Point(
                imageName,
                Name,
                Email,
                Whatsapp,
                Latitude,
                Longitude,
                City,
                Uf
            );
        }

        public string GetImageName()
        {
            return imageName;
        }

        public void SetImageName(int length)
        {
            // Generate four random bytes
            byte[] rndByte = new byte[length];
            RandomNumberGenerator.Create().GetBytes(rndByte);

            // Convert the bytes to a UInt32
            uint randomNumber = BitConverter.ToUInt32(rndByte, 0);
            imageName = randomNumber + Image.FileName;
        }

    }

}
