using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using vshed.IO;
using System.Threading.Tasks;

namespace Test.SingleCopy
{
    [TestClass]
    public class ExtendedFileInfo
    {
        [TestMethod]
        public void ExtendedFileInfo_md5sum()
        {
            FileInfo file = new FileInfo(@"Test File\md5sum Test document.txt");
            Assert.AreEqual("2AAAD2B38E77F4F0E2045CD118116F80", file.md5sum());
        }

        [TestMethod]
        public void ExtendedFileInfo_md5sum_x2Run()
        {
            FileInfo file = new FileInfo(@"Test File\md5sum Test document.txt");
            FileInfo file2 = new FileInfo(@"Test File\md5sum Test document2.txt");

            Task<string>[] array = new Task<string>[2];
            array[0] =  file.md5sumAsync();
            array[1] = file2.md5sumAsync();
            
            Task.WaitAll(array);

            //You can either pull the value from the Task or call the non async function.
            Assert.AreEqual("2AAAD2B38E77F4F0E2045CD118116F80", array[0].Result);//file.md5sum());
            Assert.AreEqual("2AAAD2B38E77F4F0E2045CD118116F80", file2.md5sum());
        }

        [TestMethod]
        public void FileInfoCollection_Add_md5sum()
        {
            FileInfoCollection files = new FileInfoCollection();
            files.Add(new FileInfo(@"Test File\md5sum Test document.txt"));
            files.Add(new FileInfo(@"Test File\md5sum Test document2.txt"));
            files.WaitMd5();

            Assert.AreEqual("2AAAD2B38E77F4F0E2045CD118116F80", files[0].md5sum());
            Assert.AreEqual("2AAAD2B38E77F4F0E2045CD118116F80", files[1].md5sum());
        }

        [TestMethod]
        public void FileInfoCollection_AddRange_md5sum()
        {
            FileInfoCollection files = new FileInfoCollection();
            files.AddRange((new DirectoryInfo(@"Test File\")).GetFiles());
            files.WaitMd5();

            Assert.AreEqual("2AAAD2B38E77F4F0E2045CD118116F80", files[0].md5sum());
        }
    }
}
