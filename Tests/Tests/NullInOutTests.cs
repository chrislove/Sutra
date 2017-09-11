﻿using System.Collections.Generic;
using NUnit.Framework;
using static SharpPipe.Commands;

namespace SharpPipe.Tests {
    public sealed class NullInOutTests : TestBase {
        [TestCase(true, false)]
        [TestCase(false, true)]
        public void Test_Pipe_NullIn_Throws(bool nullInAllowed, bool shouldThrow) {
            void TestDelegate() {
                SharpPipe.Pipe.AllowNullInput = nullInAllowed;
                
                var pipe = start.str.pipe
                           | (string) null;
            }

            ThrowAssert<NullPipeException>(TestDelegate, shouldThrow);
        }
        
        [Test]
        public void Test_Seq_NullIn_Throws() {
            void TestDelegate() {
                var pipe = start.str.pipe
                           | (IEnumerable<string>) null;
            }

            Assert.That(TestDelegate, Throws.TypeOf<NullPipeException>());
        }
        
        [Test]
        public void Test_Seq_NullTransform_Throws() {
            void TestDelegate() {
                SharpPipe.Pipe.AllowNullInput = true;

                var pipe = ABCSeq
                           | transform | (i => (IEnumerable<string>) null);
            }

            Assert.That(TestDelegate, Throws.TypeOf<NullPipeException>());
        }
        
        [Test]
        public void Test_Pipe_NullOut_Throws() {
            void TestDelegate() {
                SharpPipe.Pipe.AllowNullInput = true;
                
                var pipe = TestPipe
                           | (i => null)
                           | ret;
            }

            Assert.That(TestDelegate, Throws.TypeOf<NullPipeException>());
        }
        
        [Test]
        public void Test_Pipe_AllowNullOut_DoesntThrow() {
            void TestDelegate() {
                SharpPipe.Pipe.AllowNullInput = true;
                
                var pipe = TestPipe
                           | (i => null)
                           | allownull
                           | ret;
            }

            Assert.That(TestDelegate, Throws.Nothing);
        }
    }
}