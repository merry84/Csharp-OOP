using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHierarchy
{
    public class MyList : AddRemoveCollection
    {
        public ICollection<string>Used {  get; set; }
        public MyList() 
        {
            Used = new List<string>();
        }
        public override string Remove()
        {
            string removeItem = collection[0];
            collection.RemoveAt(0);
            Used.Add(removeItem);
            return removeItem;
        }
    }
}
