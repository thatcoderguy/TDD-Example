using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTTPConnection;

namespace TDD_Example
{
    public class StatusCheck
    {
        private IClient _client;

        public StatusCheck(IClient client)
        {
            _client = client;
        }

        public bool WebsiteIsRunning(string url)
        {
            HttpResponseMessage response = null;
            
            try
            {
                response = _client.GetAsync(url);
            }
            catch(System.AggregateException)
            {
                return false;
            }
            catch(InvalidOperationException)
            {
                return false;
            }

            if (response is null)
                return false;

            if(response.IsSuccessStatusCode)
                return true;

            return false;
        }

    }
}
