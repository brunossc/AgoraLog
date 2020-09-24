using CandidateTesting.BrunoSenaeSilvaCorreia.AgoraLog;
using CandidateTesting.BrunoSenaeSilvaCorreia.AgoraLog.Integration;
using Moq;
using NUnit.Framework;
using System;
using System.IO;

namespace TestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            var uri = new Uri("http://teste.com");
            var mockRepo = new Mock<ILogger>();
            mockRepo.Setup(repo => repo.GetLogFile(uri))
                .Returns(File.OpenRead("./TextFile1.txt"));

            var agoraLog = new AgoraLog(mockRepo.Object);
            agoraLog.WriteFromCDN(uri, "./outputTest/testeLog.txt");

            Assert.IsTrue(File.Exists("./outputTest/testeLog.txt"));
        }
    }
}