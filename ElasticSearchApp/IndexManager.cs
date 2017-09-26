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
        private ILogger _logger;

        public IndexManager(string elasticSearchUrl, ILogger logger)
        {
            var uri = new Uri(elasticSearchUrl);
            var settings = new ConnectionSettings(uri).DefaultIndex("hotel");
            settings.DisableDirectStreaming();
            elasticSearchClient = new ElasticClient(settings);

            _logger = logger ?? new Logger();
        }
        public bool CreateIndex(string index,Hotel hotel)
        {
            var logEntry = new LogEntry
            {
                RequestTime = DateTime.UtcNow.ToString(),
                RequestType = "Create Index",
            };
            try
            {
                var output = elasticSearchClient.IndexExists(index);
                if (output.Exists == false)
                {
                    var response = elasticSearchClient.Index<Hotel>(hotel, x => x.Index(index).Type("newHotel").Refresh(Elasticsearch.Net.Refresh.True));
                    logEntry.Status = "Success";
                    logEntry.Response = response.ToString();
                    return true;
                }
                else
                {
                    logEntry.Status = "Failure";
                    logEntry.Response = "Index already exists.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                logEntry.Status = "Failure";
                logEntry.Response = ex.Message;
                return false;
            }
            finally
            {
                _logger.WriteLog(logEntry);
            }
        }

        public bool DeleteIndex(string index)
        {
            var logEntry = new LogEntry
            {
                RequestTime = DateTime.UtcNow.ToString(),
                RequestType = "Delete Index",
            };
            try
            {
                var response = elasticSearchClient.DeleteIndex(index);
                logEntry.Status = "Success";
                logEntry.Response = response.ToString();

                return true;
            }
            catch (Exception ex)
            {
                logEntry.Status = "Failure";
                logEntry.Response = ex.Message;
                return false;
            }
            finally
            {
                _logger.WriteLog(logEntry);
            }
        }
    }
}
