namespace ShopAPI.Model
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Status { get; set; }
        public ICollection<CartItem>? Items { get; set; }
    }
}
