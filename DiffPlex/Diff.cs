namespace DiffPlex
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class Diff
    {
        private static readonly Differ DifferInstance = new Differ();
        private static readonly InlineDiffBuilder InlineDiffBuilderInstance = new InlineDiffBuilder(DifferInstance);
        private static readonly SideBySideDiffBuilder SideBySideDiffBuilderInstance = new SideBySideDiffBuilder(DifferInstance);

        [Flags]
        public enum Options
        {
            None,
            IgnoreCase,
            IgnoreWhitespace,
        }

        public static class Lines
        {
            public static CompareResult Compare(string[] before, string[] after, Options options = Options.None)
            {
                var beforeData = new ModificationData(before);
                var afterData = new ModificationData(after);

                DiffResult diffResult = Differ.CreateCustomDiffs(beforeData, afterData, options.HasFlag(Options.IgnoreWhitespace), options.HasFlag(Options.IgnoreCase));
                return new CompareResult(diffResult);
            }

            public class CompareResult
            {
                private readonly DiffResult result;
                private DiffPaneModel inline;
                private SideBySideDiffModel sideBySide;

                public CompareResult(DiffResult result)
                {
                    this.result = result;
                }

                public DiffPaneModel Inline
                {
                    get
                    {
                        if (this.inline == null)
                        {
                            var inline = new DiffPaneModel();
                            inline.Lines.AddRange(InlineDiffBuilder.BuildDiffPieces(this.result));
                            this.inline = inline;
                        }

                        return this.inline;
                    }
                }

                public SideBySideDiffModel SideBySide
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
}
