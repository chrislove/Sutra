using System.Collections.Generic;
using NUnit.Framework;
using static Sutra.Commands;

namespace Sutra.Tests {
    public sealed class OptionPipeTests : TestBase {
        [Test]
        public void Test_EmptyPipe_DoesntThrow_OnGet() {
            void TestDelegate() {
                str str = start.pipe
                          | (string) null
                          | get;
            }
            
            Assert.That(TestDelegate, Throws.Nothing);
        }

        [Test]
        public void Test_EmptyPipe_Throws_OnGetValue() {
            void TestDelegate() {
                string str = start.pipe
                             | (string) null
                             | !get;
            }
            
            Assert.That(TestDelegate, Throws.TypeOf<EmptyPipeException>());
        }

        [Test]
        public void Test_EmptySeq_DoesntThrow_OnGet() {
            void TestDelegate() {
                SeqOption<string> seq = start.seq
                                        | (IEnumerable<string>) null
                                        | get;
            }
            
            Assert.That(TestDelegate, Throws.Nothing);
        }

        [Test]
        public void Test_EmptySeq_Throws_OnGetValue() {
            void TestDelegate() {
                IEnumerable<Option<string>> seq = start.seq
                                                  | (IEnumerable<string>) null
                                                  | !!get;
            }
            
            Assert.That(TestDelegate, Throws.TypeOf<EmptySequenceException>());
        }
    }
}