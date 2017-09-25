using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var esStore = new ElasticSearchDataStore("http://172.16.14.213:9200/");
            var indexManager = new IndexManager();
          
            List<Hotel> list = new List<Hotel>();
            list = Hotel.GetAll();
            foreach(var hotel in list)
            {
                indexManager.CreateIndex("hotels", hotel);
            }
           
            var data=esStore.Search("hotels", "");
           foreach(var x in data)
            {
                Console.WriteLine(x.Name);
            }
           

            Console.ReadLine();
        }
    }
}
