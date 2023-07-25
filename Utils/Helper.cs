using WB.WebApp.Data;
using WB.WebApp.Models;

namespace WB.WebApp.Utils
{
    public static class Helper
    {
        public static CustomerViewModel ToViewModel(this Customer customer)
        {
            return new CustomerViewModel()
            {
                Id = customer.Id.ToString(),
                DateOfBirth = customer.Dob?.ToString(),
                Email = customer.Email,
                Name = customer.Name
            };
        }
        public static ShopViewModel ToViewModel(this Shop shop)
        {
            return new ShopViewModel()
            {
                Id = shop.Id.ToString(),
                Location = shop.Location,
                Name = shop.Name,
            };
        }
        public static ProductViewModel ToViewModel(this Product product)
        {
            return new ProductViewModel()
            {
                Id = product.Id.ToString(),
                Name = product.Name,
                Price = product.Price
            };
        }
        public static BillViewModel ToViewModel(this Bill data)
        {
            return new BillViewModel()
            {
                Id = data.Id.ToString(),
                Customer = data.Customer?.ToViewModel(),
                Product = data.Product?.ToViewModel(),
                Shop = data.Shop?.ToViewModel()
            };
        }
        public static List<CustomerViewModel> ToListViewModel(this List<Customer> data)
        {
            return data.Select(x => x.ToViewModel()).ToList();
        }
        public static List<ShopViewModel> ToListViewModel(this List<Shop> data)
        {
            return data.Select(x => x.ToViewModel()).ToList();
        }
        public static List<ProductViewModel> ToListViewModel(this List<Product> data)
        {
            return data.Select(x => x.ToViewModel()).ToList();
        }
        public static List<BillViewModel> ToListViewModel(this List<Bill> data)
        {
            return data.Select(x => x.ToViewModel()).ToList();
        }

        public static Customer ToModel(this CustomerViewModel data) {
            return new Customer()
            {
                Id = 0,
                Dob = DateTime.TryParse(data.DateOfBirth, out DateTime dateOfBirth) ? dateOfBirth : null,
                Name = data.Name,
                Email = data.Email,
            };
        }
        public static Shop ToModel(this ShopViewModel data)
        {
            return new Shop()
            {
                Id = 0,
                Name = data.Name,
                Location = data.Location,
            };
        }
        public static Product ToModel(this ProductViewModel data)
        {
            return new Product()
            {
                Id = 0,
                Name = data.Name,
                Price = data.Price,
            };
        }
        public static List<Customer> ToListModel(this List<CustomerViewModel> data) {
            return data.Select(d => d.ToModel()).ToList();
        }
        public static List<Shop> ToListModel(this List<ShopViewModel> data)
        {
            return data.Select(d => d.ToModel()).ToList();
        }
        public static List<Product> ToListModel(this List<ProductViewModel> data)
        {
            return data.Select(d => d.ToModel()).ToList();
        }
    }
}
