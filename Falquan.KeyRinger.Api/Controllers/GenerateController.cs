using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Falquan.KeyRinger.Services;

namespace Falquan.KeyRinger.Api.Controllers
{
    public class GenerateController : ApiController
    {

        /// <summary>
        /// Retrieve a random string
        /// GET /api/generate
        /// </summary>
        /// <returns>Cryptographically securely generated string of 32 characters</returns>
        public HttpResponseMessage Get()
        {
            return Get(32);
        }

        /// <summary>
        /// Retrieve a random string of any number of characters
        /// GET /api/generate/12
        /// </summary>
        /// <param name="length">Number of characters to generate</param>
        /// <returns>Cryptographically securely generated string of characters of provided length</returns>
        public HttpResponseMessage Get(int length)
        {
            return Request.CreateResponse(HttpStatusCode.OK, StringGenerator.GetRandomString(length));
        }

        public HttpResponseMessage Get(int length, string allow)
        {
            return Request.CreateResponse(HttpStatusCode.OK, StringGenerator.GetRandomString(length, allow));
        }
    }
}
