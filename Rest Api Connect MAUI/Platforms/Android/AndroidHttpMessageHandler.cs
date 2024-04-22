﻿using Java.Security.Cert;
using MAUI.WebApi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Android.Net;

namespace Rest_Api_Connect_MAUI.Platforms.Android
{
    public class AndroidHttpMessageHandler : IPlatforumHttpMessageHandler
    {
        public HttpMessageHandler GetHttpMessageHandler() =>
            new AndroidMessageHandler
            {
                ServerCertificateCustomValidationCallback = (httpRequestMessage,
                certificate, chain, sslPolicyErrors) => certificate?.Issuer == "CN=localhost" ||
                sslPolicyErrors == SslPolicyErrors.None
            };

    }
}
