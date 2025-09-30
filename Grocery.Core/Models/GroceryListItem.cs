using CommunityToolkit.Mvvm.ComponentModel;
namespace Grocery.Core.Models
{
    public partial class GroceryListItem : Model
    {
        public int GroceryListId { get; set; }
        public int ProductId { get; set; }

        [ObservableProperty]
        public int _Amount;
        public GroceryListItem(int id, int groceryListId, int productId, int amount) : base(id, "")
        {
            GroceryListId = groceryListId;
            ProductId = productId;
        }

        public Product Product { get; set; } = new(0, "None", 0);
    }
}
