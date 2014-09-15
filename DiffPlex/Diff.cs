namespace DiffPlex
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class Diff
    {
        private static readonly char[] LineEndingCharacters = new char[] { '\r', '\n' };
        private static readonly Differ DifferInstance = new Differ();
        private static readonly InlineDiffBuilder InlineDiffBuilderInstance = new InlineDiffBuilder(DifferInstance);
        private static readonly SideBySideDiffBuilder SideBySideDiffBuilderInstance = new SideBySideDiffBuilder(DifferInstance);

        /// <summary>
        /// Customizations applicable to comparing two strings.
        /// </summary>
        [Flags]
        public enum Options
        {
            /// <summary>
            /// Any differences between two lines are considered significant.
            /// </summary>
            None,
            
            /// <summary>
            /// Differences in capitalization between two lines are not considered significant.
            /// </summary>
            IgnoreCase,
            
            /// <summary>
            /// Differences in leading or trailing whitespace between two lines are not considered significant.
            /// </summary>
            IgnoreWhitespace,
        }

        public static CompareResult CompareLines(string before, string after, Options options = Options.None)
        {
            return CompareLines(SplitIntoLines(before), SplitIntoLines(after), options);
        }

        public static CompareResult CompareLines(IList<string> before, IList<string> after, Options options = Options.None)
        {
            var beforeData = new ModificationData(before);
            var afterData = new ModificationData(after);

            DiffResult diffResult = Differ.CreateCustomDiffs(beforeData, afterData, options.HasFlag(Options.IgnoreWhitespace), options.HasFlag(Options.IgnoreCase));
            return new CompareResult(diffResult);
        }

        /// <summary>
        /// Splits a large text blob into a list of lines.
        /// Whitespace (including the line endings) is preserved.
        /// Line endings may be \r, \n, \r\n, or \n\r
        /// </summary>
        /// <param name="text">The text to split up.</param>
        /// <returns>The array of lines.</returns>
        private static IList<string> SplitIntoLines(string text)
        {
            var lines = new List<string>();
            int start = 0;
            while (start < text.Length)
            {
                // Find the next line ending.
                int next = text.IndexOfAny(LineEndingCharacters, start);
                if (next < 0)
                {
                    lines.Add(text.Substring(start));
                    break;
                }

                int lineEndingSequenceLength = 1;

                // Check to see if this is a two character line ending.
                int peek = next + 1;
                if (text.Length > peek)
                {
                    char nextCharacter = text[peek];
                    int nextLineEndingCharacter = Array.IndexOf(LineEndingCharacters, nextCharacter);
                    if (nextLineEndingCharacter >= 0 && nextCharacter != text[next])
                    {
                        lineEndingSequenceLength++;
                    }
                }

                int lineLength = next + lineEndingSequenceLength - start;
                string line = text.Substring(start, lineLength);
                lines.Add(line);
                start += lineLength;
            }

            return lines;
        }

        public class CompareResult
        {
            private readonly DiffResult result;
            private DiffPaneModel inline;
            private SideBySideDiffModel sideBySide;

            public CompareResult(DiffResult result)
            {
                if (result == null) throw new ArgumentNullException("result");
                this.result = result;
            }

            /// <summary>
            /// Gets a list of all lines (before and after) with diff metadata.
            /// </summary>
            public IList<DiffPiece> Inline
            {
                get
                {
                    if (this.inline == null)
                    {
                        var inline = new DiffPaneModel();
                        inline.Lines.AddRange(InlineDiffBuilder.BuildDiffPieces(this.result));
                        this.inline = inline;
                    }

                    return this.inline.Lines;
                }
            }

            /// <summary>
            /// Gets the lines that appear on the left pane of a side-by-side view.
            /// </summary>
            public IList<DiffPiece> LeftSide
            {
                get { return this.SideBySide.OldText.Lines; }
            }

            /// <summary>
            /// Gets the lines that appear on the right pane of a side-by-side view.
            /// </summary>
            public IList<DiffPiece> RightSide
            {
                get { return this.SideBySide.NewText.Lines; }
            }

            /// <summary>
            /// Gets an object that describes the left and right sides of a comparison window.
            /// </summary>
            private SideBySideDiffModel SideBySide
            {
                get
                {
                    if (this.sideBySide == null)
                    {
                        this.sideBySide = SideBySideDiffBuilderInstance.BuildLineDiff(this.result);
                    }

                    return this.sideBySide;
                }
            }
        }
    }
}
