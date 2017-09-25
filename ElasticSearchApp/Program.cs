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
            foreach (var hotel in list)
            {
                indexManager.CreateIndex("hotel", hotel);
            }
            var data=esStore.Search("hotel", "Hayat");
            foreach(var x in data)
            {
                Console.WriteLine($"Hotel Id:\t"+x.Id+"\nName:\t"+x.Name+"\nType:\t"+x.Type+"\nDescription:\t"+x.Description);
            }
            Console.ReadLine();
        }
    }
}
