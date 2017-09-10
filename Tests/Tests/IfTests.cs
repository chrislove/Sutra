using System.Linq;
using NUnit.Framework;
using static System.IO.Path;
using static SharpPipe.Commands;
using static SharpPipe.Curried.str;

namespace SharpPipe.Tests {
    public sealed class IfTests : TestBase {
        [TestCase("B", "[A][B][C]")]
        [TestCase("D", "ABC")]
        public void Test_Enumerable_IfSelect(string contains, string expected) {
            string result = abcseq
                            | when | (e => e.Contains(contains)) | select | (i => $"[{i}]")
                            | when | ( () => true ) | select | (i => i)
                            | concat | ret;
            
            Assert.That(result, Is.EqualTo(expected));
        }
        
        [TestCase(@"C:\Test\Editor", @"C:\Test")]
        [TestCase(@"C:\Test", @"C:\Test")]
        public void Test_Pipe_IfSelect(string inPath, string expected) {
            string OneDirectoryUp(string path) => GetDirectoryName( GetFullPath(Combine(path, @"..\") ) );
            string result = start.str.pipe
                            | inPath
                            | when | EndsWith("Editor") | select | OneDirectoryUp
                            | when | ( () => true )     | select | (i => i)
                            | ret;
            
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}