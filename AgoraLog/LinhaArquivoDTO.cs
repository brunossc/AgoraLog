using System;
using System.Collections.Generic;
using System.Text;

namespace CandidateTesting.BrunoSenaeSilvaCorreia.AgoraLog
{
    public class LinhaArquivoDTO
    {
        public string Provider { get; set; }
        public string HttpMethod { get; set; }
        public string StatusCode { get; set; }
        public string Uri { get; set; }
        public string TimeTaken { get; set; }
        public string ResponseSize { get; set; }
        public string CacheStatus { get; set; }
    }
}
