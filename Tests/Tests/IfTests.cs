using System.Linq;
using NUnit.Framework;
using static System.IO.Path;
using static SharpPipe.Commands;

namespace SharpPipe.Tests {
    public sealed class IfTests : TestBase {
        [TestCase("B", "[A][B][C]")]
        [TestCase("D", "ABC")]
        public void Test_Enum_IfSelect(string contains, string expected) {
            string result = ABCPipe
                            | IF | (e => e.Contains(contains)) | SELECT | (i => $"[{i}]")
                            | IF | ( () => true ) | SELECT | (i => i)
                            | CONCAT("") | OUT;
            
            Assert.That(result, Is.EqualTo(expected));
        }
        
        [TestCase(@"C:\Test\Editor", @"C:\Test")]
        [TestCase(@"C:\Test", @"C:\Test")]
        public void Test_Pipe_IfSelect(string inPath, string expected) {
            string OneDirectoryUp(string path) => GetDirectoryName( GetFullPath(Combine(path, @"..\") ) );

            string result = NEW.STRING.PIPE
                            | inPath
                            | IF | (p => p.EndsWith("Editor")) | SELECT | OneDirectoryUp
                            | IF | ( () => true ) | SELECT | (i => i)
                            | OUT;
            
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}