using CollectionHierarchy.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHierarchy
{
    public class AddRemoveCollection : AddCollection, IAddRemoveCollection
    {
        
        public override int Add(string item)
        {
            collection.Insert(0,item);
            return 0;
        }
        public virtual string Remove()
        {            
                string removedItem = collection[^1];//елементът на последния индекс
                collection.RemoveAt(collection.Count-1);
                return removedItem;
                      
        }
    }
}

