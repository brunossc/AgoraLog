using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.BrunoSenaeSilvaCorreia.AgoraLog.Integration
{
    public class CDN : ILogger
    {
        public Stream GetLogFile(Uri path)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(path);
            httpRequest.Method = WebRequestMethods.Http.Get;

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            var httpResponseStream = httpResponse.GetResponseStream();

            return httpResponseStream;
        }
    }
}
