using CollectionHierarchy.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHierarchy
{
    public class AddCollection : IAddCollection
    {
        protected List<string> collection;

        public AddCollection()
        {
             collection = new List<string>();
        }

        public virtual int Add(string item)
        {
            collection.Add(item);
            return collection.Count-1;
        }
    }
}
