using System;
using System.Linq;
using JetBrains.Annotations;
using NUnit.Framework;
using Sutra.CurryLib;
using static System.IO.Path;
using static Sutra.Commands;

namespace Sutra.Tests
{
    public sealed class IfTests : TestBase
    {
        [TestCase("B", "[A][B][C]")]
        [TestCase("D", "ABC")]
        public void Test_Enumerable_IfSelect( string contains, string expected )
        {
            string result = ABCSeq
                            | when        | (e => e.Contains(contains)) | map | (i => $"[{i}]")
                            | when        | (() => true)                | map | (i => i + "")
                            | strf.concat | !get;

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(@"C:\Test\Editor\", @"C:\Test\")]
        [TestCase(@"C:\Test\",        @"C:\Test\")]
        public void Test_Pipe_IfSelect( string inPath, string expected )
        {
            string result = start.pipe
                            | inPath
                            | when | strf.endsWith(@"Editor\") | map | OneDirectoryUp
                            | when | (() => true)              | map | (i => i)
                            | !get;

            Assert.That(result, Is.EqualTo(expected));
        }

        [NotNull]
        private string OneDirectoryUp( [NotNull] string path )
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));

            if (path == @"C:\Test\Editor\")
                return @"C:\Test\";

            throw new InvalidOperationException($"Invalid input: 'path'");
        }
    }
}