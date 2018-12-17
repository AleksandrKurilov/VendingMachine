using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine.BL.VendingMachine
{
    public class BeverageRepository : IBeverageRepository
    {
        private readonly List<IRepositoryItem> items;

        public BeverageRepository()
        {
            items = new List<IRepositoryItem>();

            items.Add(new RepositoryItem(new Tea(), 10));
            items.Add(new RepositoryItem(new Coffee(), 20));
            items.Add(new RepositoryItem(new CoffeeWithMilk(), 20));
            items.Add(new RepositoryItem(new Juice(), 15));
        }

        public BeverageRepository(List<IRepositoryItem> items)
        {
            if(items == null || items.Count < 1) 
                throw new ArgumentNullException();

            this.items = items;
        }

        public ICollection<IRepositoryItem> Items
        {
            get { return items.AsReadOnly(); }
        }

        public IRepositoryItem GetBeverage(string beverageName)
        {
            IRepositoryItem item = items.SingleOrDefault(i => i.Beverage.Name == beverageName);

            if(item == null)
                throw new Exception($"Нет такого напитка {beverageName}");

            return item;
        }
    }
}
