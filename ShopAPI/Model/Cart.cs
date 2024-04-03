namespace ShopAPI.Model
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CartItemId { get; set; }
        public ICollection<CartItem>? Items { get; set; }
    }
}
