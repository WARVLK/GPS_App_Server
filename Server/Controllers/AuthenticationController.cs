using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IHttpClientFactory clientFactory;

        public AuthenticationController(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        [HttpPut]
        [Route("/api/[controller]/code")]
        [Consumes("text/plain")]
        public async Task<IActionResult> PutCode()
        {
            //using nahradza garbage collector, netreba potom reader.Dispose();
            using var reader = new StreamReader(Request.Body, Encoding.UTF8);
            var code = await reader.ReadToEndAsync();
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest("Error reading code");
            }
            var request = new HttpRequestMessage(HttpMethod.Post, "https://www.strava.com/oauth/token")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"client_id", "50927"},
                    {"client_secret", "85dbb8610c45d88b3dcdd55b4864c8d57fa87243"},
                    {"code", code},
                    {"grant_type", "authorization_code"}
                })
            };
            var client = clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            return Ok(await response.Content.ReadAsStringAsync());
        }
    }
}
