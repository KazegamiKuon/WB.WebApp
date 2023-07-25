namespace WB.WebApp.Models
{
    public class BillViewModel
    {
        public string Id { get; set; }
        public CustomerViewModel Customer { get; set; }
        public ProductViewModel Product { get; set; }
        public ShopViewModel Shop { get; set; }
    }
}
