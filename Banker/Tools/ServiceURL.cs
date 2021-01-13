using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.Tools
{
    public static class ServiceURL
    {
        public static string GetURL(IConfiguration config)
        {
            return config["Service:Url"];
        }
    }
}
