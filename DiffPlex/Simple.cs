namespace DiffPlex
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class Simple
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

        public static DiffPaneModel CompareInline(string before, string after, Options options = Options.None)
        {
            // TODO: do something with options.
            // The currently exposed API in other classes makes this difficult or impossible.
            return InlineDiffBuilderInstance.BuildDiffModel(before, after);
        }

        public static SideBySideDiffModel CompareSideBySide(string before, string after, Options options = Options.None)
        {
            // TODO: do something with options.
            // The currently exposed API in other classes makes this difficult or impossible.
            return SideBySideDiffBuilderInstance.BuildDiffModel(before, after);
        }
    }
}
