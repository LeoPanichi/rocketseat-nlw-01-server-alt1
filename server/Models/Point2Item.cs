using Microsoft.EntityFrameworkCore;


namespace server.Models
{
    [PrimaryKey(nameof(PointId), nameof(ItemId))]
    public class Point2Item
    {
        public Point2Item(int pointId, int itemId)
        {
            PointId = pointId;
            ItemId = itemId;
        }

        public int PointId { get; set; }
        public int ItemId { get; set; }

    }
}