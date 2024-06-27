using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    public class Item
    {
        public Item(string image, string title)
        {
            Image = image;
            Title = title;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }

        public virtual List<Point> Point { get; } = [];
    }
}