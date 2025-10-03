using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Grocery.Core.Services
{
    public class BoughtProductsService : IBoughtProductsService
    {
        private readonly IGroceryListItemsRepository _groceryListItemsRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;
        private readonly IGroceryListRepository _groceryListRepository;

        public BoughtProductsService(IGroceryListItemsRepository groceryListItemsRepository, IGroceryListRepository groceryListRepository, IClientRepository clientRepository, IProductRepository productRepository)
        {
            _groceryListItemsRepository = groceryListItemsRepository;
            _groceryListRepository = groceryListRepository;
            _clientRepository = clientRepository;
            _productRepository = productRepository;
        }

        public List<BoughtProducts> Get(int? productId)
        {
            if (productId == null)
            {
                return new List<BoughtProducts>();
            }
            var itemsWithProduct = _groceryListItemsRepository.GetAll().Where(item => item.ProductId == productId);

            var boughtProductsList = itemsWithProduct.Select(item =>
            {
                var groceryList = _groceryListRepository.Get(item.GroceryListId);
                if (groceryList == null) return null;

                var client = _clientRepository.Get(groceryList.ClientId);
                if (client == null) return null;

                var product = _productRepository.Get(item.ProductId);
                if (product == null) return null;

                return new BoughtProducts(client, groceryList, product);

            })
            .Where(boughtProduct => boughtProduct != null)
            .ToList();

            return boughtProductsList;
        }
    }
}