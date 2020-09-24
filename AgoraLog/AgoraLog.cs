using CandidateTesting.BrunoSenaeSilvaCorreia.AgoraLog.Integration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace CandidateTesting.BrunoSenaeSilvaCorreia.AgoraLog
{
    public class AgoraLog
    {
        private readonly ILogger log;

        public AgoraLog(ILogger cdnIntegration)
        {
            log = cdnIntegration;
        }

        public void WriteFromCDN(Uri pathOrigin, string pathDestiny)
        {
            if (pathOrigin is null || string.IsNullOrWhiteSpace(pathOrigin.OriginalString) || string.IsNullOrWhiteSpace(pathDestiny))
            {
                throw new ArgumentNullException("the paths can't be null");
            }

            var diretory = pathDestiny.Substring(0, pathDestiny.LastIndexOf("/"));
            
            if (!Directory.Exists(diretory))
                Directory.CreateDirectory(diretory);
                 
            using var reader = new StreamReader(log.GetLogFile(pathOrigin));
            using var writer = File.CreateText(pathDestiny);

            WriteHeader(writer);

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var newLine = GetLineValues(line);

                writer.WriteLine(String.Format("{0} {1} {2} {3} {4} {5} {6}",
                    newLine.Provider,
                    newLine.HttpMethod,
                    newLine.StatusCode,
                    newLine.Uri,
                    newLine.TimeTaken,
                    newLine.ResponseSize,
                    newLine.CacheStatus));                
            }
        }

        private static void WriteHeader(StreamWriter writer)
        {
            string[] header = new string[3];
            header[0] = "#Version: " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            header[1] = "#Date: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            header[2] = "#Fields: provider http-method status-code uri-path time-taken response-size cache-status";

            for (int i = 0; i < header.Length; i++)
            {
                writer.WriteLine(header[i]);
            }
        }

        private static LinhaArquivoDTO GetLineValues(string line)
        {
            Regex reg = new Regex(@"(([0-9]{3})|([0-9]{3})|(\b\w+[A-Z]\b)|(\b\s/.+\s\b))");
            var data = reg.Matches(line);

            //provider http-method status-code uri-path time-taken response-size cache-status
            return new LinhaArquivoDTO()
            {
                Provider = "\"MINHA CDN\"",
                HttpMethod = data[3].ToString(),
                StatusCode = data[1].ToString(),
                Uri = data[4].ToString().Trim(),
                TimeTaken = data[6].ToString(),
                ResponseSize = data[0].ToString(),
                CacheStatus = data[2].ToString()
            };
        }
    }
}
