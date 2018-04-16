using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace GridHookAPI.Controllers
{
    public class GridController : ApiController
    {
        public static dynamic lastEvent;

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]dynamic value)
        {
            var eventType = value[0].eventType;
            var response = Request.CreateResponse(HttpStatusCode.OK);
            dynamic ret;
            if (eventType == "Microsoft.EventGrid.SubscriptionValidationEvent")
            {
                var validationCode = value[0].data.validationCode.ToString();
                ret = new { ValidationResponse = validationCode };
            }
            else
            {
                lastEvent = value;
                ret = new { Event = eventType };
            }
            string jsonString = JsonConvert.SerializeObject(ret);
            response.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            return response;
        }

        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            string jsonString = lastEvent != null ? JsonConvert.SerializeObject(lastEvent) : "{\"Event\":\"Empty\"}";
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            return response;
        }

        public void Delete()
        {
            lastEvent = null;
        }
    }
}