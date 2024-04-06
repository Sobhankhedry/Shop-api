namespace ShopAPI.Model
{
    public class Cart
    {
        public int Id { get; set; }
        public ICollection<CartItem>? Items { get; set; }
    }
}
