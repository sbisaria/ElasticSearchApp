using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace ElasticSearchApp
{
    public class ElasticSearchDataStore
    {
        public ElasticSearchDataStore(string elasticSearchUrl)
        {
            ElasticSearchUrl = elasticSearchUrl;
        }

        public string ElasticSearchUrl { get; }

        public List<Hotel> Search(string index, string query)
        {
            List<Hotel> hotelList = new List<Hotel>();
            try
            {
                var uri = new Uri(ElasticSearchUrl);
                var settings = new ConnectionSettings(uri);
                var elasticSearchClient = new ElasticClient(settings);
                var esResponse = elasticSearchClient.Search<Hotel>(s =>s
                    .Index(index)
                    .Type("myHotel")
                    .Size(100)
                    .Query(
                        q =>
                            q.Match(x=>x.Field("name").Query(query))
                    ));
                
                foreach(var hit in esResponse.Hits)
                {
                    var hotel = new Hotel();
                    hotel.Id = hit.Source.Id;
                    hotel.Name = hit.Source.Name;
                    hotel.Type = hit.Source.Type;
                    hotel.Description = hit.Source.Description;
                    hotelList.Add(hotel);
                }
               
            }
            catch(Exception e)
            {
                //handel exception
            }
            return hotelList;
        }
    }
}
