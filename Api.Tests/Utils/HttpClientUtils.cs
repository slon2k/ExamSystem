using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Api.Tests.Utils
{
    public static class HttpClientUtils
    {
        public static HttpContent CreateContent(object obj)
        {
            var requestdata = JsonSerializer.Serialize(obj);
            return new StringContent(requestdata, Encoding.UTF8, "application/json");
        }
    }
}
