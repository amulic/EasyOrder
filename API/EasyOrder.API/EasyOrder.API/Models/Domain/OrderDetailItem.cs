namespace EasyOrder.API.Models.Domain
{
    public class OrderDetailItem
    {
        public int OrderDetailItemId { get; set; }
        public int OrderDetailId { get; set; }
        public virtual OrderDetail OrderDetail { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; } //represents food or drink
    }
}
