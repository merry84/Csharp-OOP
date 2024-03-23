using System.Text;
using CollectionHierarchy.Contracts;
namespace CollectionHierarchy
{
    public class Program
    {
        static void Main(string[] args)
        {
            var elementsCount = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var amountOfRemove = int.Parse(Console.ReadLine());


            var addCollection = new AddCollection();
            var addRemoveCollection = new AddRemoveCollection();
            var myList = new MyList();

            List<IAddCollection> addCollections = new List<IAddCollection>() { addCollection,addRemoveCollection,myList};

            for (int i = 0; i < addCollections.Count; i++)
            {
                for (int j = 0; j < elementsCount.Length; j++)
                {
                    Console.Write(addCollections[i].Add(elementsCount[j]) + " ");   
                }
                Console.WriteLine();
            }

            List<IAddRemoveCollection> addRemoveCollections = new List<IAddRemoveCollection>() {  addRemoveCollection,myList};

            for (int i = 0; i < addRemoveCollections.Count; i++)
            {
                for (int j = 0; j < amountOfRemove; j++)
                {
                    Console.Write(addRemoveCollections[i].Remove()+" ");
                }
                Console.WriteLine();
            }
        }
           
    }
}
