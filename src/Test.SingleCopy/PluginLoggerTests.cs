using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingleCopy.Plugin;

namespace Test.SingleCopy
{
    [TestClass]
    public class PluginLoggerInfo
    {
        [TestMethod]
        public void InfoException()
        {
            PluginLogger.Info(new Exception("TestException"));
        }
        [TestMethod]
        public void InfoExceptionMessage()
        {
            PluginLogger.Info(new Exception("TestException"), "TestMessage");
        }

        [TestMethod]
        public void InfoExceptionMessageArgs()
        {
            PluginLogger.Info(new Exception("TestException"), "TestMessage {0} {1} {2}", new string[]{ "A","B","C"});
        }

        [TestMethod]
        public void InfoMessage()
        {
            PluginLogger.Info("TestMessage");
        }

        [TestMethod]
        public void InfoMessageArgs()
        {
            PluginLogger.Info("TestMessage {0} {1} {2}", new string[] { "A", "B", "C" });
        }
    }
}
