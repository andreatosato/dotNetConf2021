using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IsolatedSample
{
    public static class MultiOutput
    {
        [Function("MultiOutput")]
        public static MyOutputType Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
            FunctionContext context)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteString("Success!");

            string[] myQueueOutput = new[] { "some", "output", "message", "one" };

            return new MyOutputType()
            {
                Name = myQueueOutput,
                HttpResponse = response
            };
        }
    }

    public class MyOutputType
    {
        [QueueOutput("myQueue")]
        public IEnumerable<string> Name { get; set; }

        public HttpResponseData HttpResponse { get; set; }
    }
}
