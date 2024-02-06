namespace EasyOrder.API.Models.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsFinished { get; set; }
    }
}
