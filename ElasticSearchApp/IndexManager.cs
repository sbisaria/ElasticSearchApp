using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchApp
{
    public class IndexManager
    {
        private ElasticClient elasticSearchClient;

        public IndexManager()
        {
            var uri = new Uri("http://172.16.14.213:9200/");
            var settings = new ConnectionSettings(uri).DefaultIndex("hotel");
            settings.DisableDirectStreaming();
            elasticSearchClient = new ElasticClient(settings);
        }
        public bool CreateIndex(string index,Hotel hotel)
        {
            try
            {
               
                var output = elasticSearchClient.IndexExists(index);
                if (output.Exists == false)
                {
                    elasticSearchClient.Index<Hotel>(hotel, x => x.Index(index).Type("myHotel").Refresh(Elasticsearch.Net.Refresh.True));
                    return true;
                }
                    
                
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        public bool DeleteIndex(string index)
        {
            try
            {
                var a = elasticSearchClient.DeleteIndex(index);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
