using System.Linq;
using NUnit.Framework;
using static System.IO.Path;
using static SharpPipe.Commands;
using static SharpPipe.CurryLib.strf;

namespace SharpPipe.Tests {
    public sealed class IfTests : TestBase {
        [TestCase("B", "[A][B][C]")]
        [TestCase("D", "ABC")]
        public void Test_Enumerable_IfSelect(string contains, string expected) {
            string result = ABCSeq
                            | when | (e => e.Contains(contains)) | map | (i => $"[{i}]")
                            | when | ( () => true ) | map | (i => i + "")
                            | concat | !get;
            
            Assert.That(result, Is.EqualTo(expected));
        }
        
        [TestCase(@"C:\Test\Editor", @"C:\Test")]
        [TestCase(@"C:\Test", @"C:\Test")]
        public void Test_Pipe_IfSelect(string inPath, string expected) {
            string OneDirectoryUp(string path) => GetDirectoryName( GetFullPath(Combine(path, @"..\") ) );
            string result = start.pipe
                            | inPath
                            | when | endsWith("Editor") | map | OneDirectoryUp
                            | when | ( () => true )     | map | (i => i)
                            | !get;
            
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}