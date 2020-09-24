using System;
using System.IO;

namespace CandidateTesting.BrunoSenaeSilvaCorreia.AgoraLog.Integration
{
    public interface ILogger
    {
        Stream GetLogFile(Uri path);
    }
}