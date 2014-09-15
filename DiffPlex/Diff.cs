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
            public static DiffResult Compare(string[] before, string[] after, Options options = Options.None)
            {
                // TODO: do something with options.
                // The currently exposed API in other classes makes this difficult or impossible.
                throw new NotImplementedException();
            }

            public class DiffResult
            {
                public DiffPaneModel Inline { get; private set; }

                public SideBySideDiffModel SideBySide { get; private set; }
            }
        }
    }
}
