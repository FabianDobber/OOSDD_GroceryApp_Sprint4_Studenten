using CommunityToolkit.Mvvm.ComponentModel;
namespace Grocery.Core.Models
{
    public partial class Product : Model
    {
        [ObservableProperty]
        public int _Stock;
        public Product(int id, string name, int stock) : base(id, name)
        {
            _Stock = stock;
        }
    }
}
