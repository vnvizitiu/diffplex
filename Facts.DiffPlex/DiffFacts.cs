using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiffPlex;
using Xunit;

namespace Facts.DiffPlex
{
    public class DiffFacts
    {
        private IList<string> baselineContent;

        public DiffFacts()
        {
            this.baselineContent = new List<string> {
                "abc",
                "def",
                "ghi",
            };
        }

        [Fact]
        public void CompareLines_Unchanged()
        {
            var result = Diff.CompareLines(this.baselineContent, this.baselineContent);
            Assert.NotNull(result);
            Assert.Equal(3, result.Inline.Lines.Count);
            Assert.True(result.Inline.Lines.All(l => l.Type == ChangeType.Unchanged));
            Assert.Equal(this.baselineContent, result.Inline.Lines.Select(l => l.Text));
        }

        [Fact]
        public void CompareLines_AddedTopLine()
        {
            var after = this.baselineContent.ToList();
            after.Insert(0, "foo");
            var result = Diff.CompareLines(this.baselineContent, after);
            Assert.NotNull(result);
            Assert.Equal(4, result.Inline.Lines.Count);
            Assert.Equal(ChangeType.Inserted, result.Inline.Lines[0].Type);
            Assert.Equal(after[0], result.Inline.Lines[0].Text);
            Assert.True(result.Inline.Lines.Skip(1).All(l => l.Type == ChangeType.Unchanged));
            Assert.Equal(this.baselineContent, result.Inline.Lines.Skip(1).Select(l => l.Text));
        }
    }
}
