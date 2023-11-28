namespace EasyOrderAPI.Models
{
    public class Tables
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] QRCode { get; set; }
        public ICollection<Orders> Orders { get; set; }
        public ICollection<Ratings> Ratings { get; set; }
    }
}
