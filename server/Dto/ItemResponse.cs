using server.Models;

namespace server.Dto
{
    public class ItemResponse
    {
        public ItemResponse(Item item, string host)
        {
            Image = "http://" + host + "/uploads/" + item.Image;
            Title = item.Title;

        }

        public string Image { get; }
        public string Title { get; } 
    }

}
