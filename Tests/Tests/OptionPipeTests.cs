using System.Collections.Generic;
using NUnit.Framework;
using static SharpPipe.Commands;

namespace SharpPipe.Tests {
    public sealed class OptionPipeTests : TestBase {
        [Test]
        public void Test_EmptyPipe_DoesntThrow_OnGet() {
            void TestDelegate() {
                Option<string> option = start.str.pipe
                                        | (string) null
                                        | get;
            }
            
            Assert.That(TestDelegate, Throws.Nothing);
        }

        [Test]
        public void Test_EmptyPipe_Throws_OnGetValue() {
            void TestDelegate() {
                string str = start.str.pipe
                             | (string) null
                             | !get;
            }
            
            Assert.That(TestDelegate, Throws.TypeOf<EmptyOptionException>());
        }

        [Test]
        public void Test_EmptySeq_DoesntThrow_OnGet() {
            void TestDelegate() {
                SeqOption<string> seq = start.str.seq
                                        | (IEnumerable<string>) null
                                        | get;
            }
            
            Assert.That(TestDelegate, Throws.Nothing);
        }

        [Test]
        public void Test_EmptySeq_Throws_OnGetValue() {
            void TestDelegate() {
                IEnumerable<Option<string>> seq = start.str.seq
                                                  | (IEnumerable<string>) null
                                                  | !get;
            }
            
            Assert.That(TestDelegate, Throws.TypeOf<EmptyOptionException>());
        }
    }
}