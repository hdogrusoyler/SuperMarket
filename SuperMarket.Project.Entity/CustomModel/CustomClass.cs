using System;
using System.Collections.Generic;
using System.Text;

namespace SuperMarket.Project.Entity.CustomModel
{
    public class EntityEntryList
    {
        public EntityEntryList()
        {
            CartProductList = new List<CartProduct>();
            AddedCartProductList = new List<CartProduct>();
            ModifiedCartProductList = new List<CartProduct>();
            DeletedCartProductList = new List<CartProduct>();
        }
        public EntityEntryState entityState { get; set; }
        public List<CartProduct> CartProductList { get; set; }
        public bool AddedEntityState { get; set; }
        public bool ModifiedEntityState { get; set; }
        public bool DeletedEntityState { get; set; }
        public List<CartProduct> AddedCartProductList { get; set; }
        public List<CartProduct> ModifiedCartProductList { get; set; }
        public List<CartProduct> DeletedCartProductList { get; set; }
    }

    public enum EntityEntryState
    {
        Added = 1,
        Deleted = 2,
        Modified = 3
    }
}
