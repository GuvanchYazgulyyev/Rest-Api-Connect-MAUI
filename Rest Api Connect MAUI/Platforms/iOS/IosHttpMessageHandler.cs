using MAUI.WebApi.Interface;
using Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest_Api_Connect_MAUI.Platforms.iOS
{
    public class IosHttpMessageHandler : IPlatforumHttpMessageHandler
    {
        public HttpMessageHandler GetHttpMessageHandler() =>
            new NSUrlSessionHandler
            {
                TrustOverrideForUrl = (NSUrlSessionHandler sender, string url, SecTrust trust) =>
                url.StartsWith("https://localhost")
            };
    }
}
