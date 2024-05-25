namespace EasyOrder.API.Models.Domain
{
    public class OrderDetailItem<Item>
    {
        public int OrderDetailItemId { get; set; }
        public int OrderDetailId { get; set; }
        public OrderDetail OrderDetail { get; set; }

        public int ItemId { get; set; }
        public Item ItemOfOrder { get; set; }
    }
}
