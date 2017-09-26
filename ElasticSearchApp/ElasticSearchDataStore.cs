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
        public ElasticSearchDataStore(string elasticSearchUrl, ILogger logger)
        {
            ElasticSearchUrl = elasticSearchUrl;
            _logger = logger ?? new Logger();
        }
        private ILogger _logger;
        public string ElasticSearchUrl { get; }

        public List<Hotel> Search(string index, string query)
        {
            List<Hotel> hotelList = new List<Hotel>();
            var logEntry = new LogEntry
                {
                    RequestTime = DateTime.UtcNow.ToString(),
                    RequestType = "Search",
                };
            try
            {
                var uri = new Uri(ElasticSearchUrl);
                var settings = new ConnectionSettings(uri);
                var elasticSearchClient = new ElasticClient(settings);
                var esResponse = elasticSearchClient.Search<Hotel>(s =>s
                    .Index(index)
                    .Type("newHotel")
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
                logEntry.Status = "Success";
                logEntry.Response = esResponse.ToString();
            }
            catch(Exception e)
            {
                logEntry.Status = "Failure";
                logEntry.Response = e.Message;
            }
            finally
            {
                _logger.WriteLog(logEntry);
            }
            return hotelList;
        }
    }
}
