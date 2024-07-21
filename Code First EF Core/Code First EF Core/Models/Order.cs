namespace Code_First_EF_Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
