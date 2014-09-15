using System;
using System.Collections.Generic;
using System.Linq;

namespace DiffPlex
{
    public class InlineDiffBuilder : IInlineDiffBuilder
    {
        private readonly IDiffer differ;

        public InlineDiffBuilder(IDiffer differ)
        {
            if (differ == null)
                throw new ArgumentNullException("differ");

            this.differ = differ;
        }

        public DiffPaneModel BuildDiffModel(string oldText, string newText)
        {
            if (oldText == null) throw new ArgumentNullException("oldText");
            if (newText == null) throw new ArgumentNullException("newText");

            var diffResult = differ.CreateLineDiffs(oldText, newText, true);
            var model = new DiffPaneModel();
            model.Lines.AddRange(BuildDiffPieces(diffResult));
            return model;
        }

        internal static IEnumerable<DiffPiece> BuildDiffPieces(DiffResult diffResult)
        {
            int bPos = 0;

            foreach (var diffBlock in diffResult.DiffBlocks)
            {
                for (; bPos < diffBlock.InsertStartB; bPos++)
                    yield return new DiffPiece(diffResult.PiecesNew[bPos], ChangeType.Unchanged, bPos + 1);

                int i = 0;
                for (; i < Math.Min(diffBlock.DeleteCountA, diffBlock.InsertCountB); i++)
                    yield return new DiffPiece(diffResult.PiecesOld[i + diffBlock.DeleteStartA], ChangeType.Deleted);

                i = 0;
                for (; i < Math.Min(diffBlock.DeleteCountA, diffBlock.InsertCountB); i++)
                {
                    yield return new DiffPiece(diffResult.PiecesNew[i + diffBlock.InsertStartB], ChangeType.Inserted, bPos + 1);
                    bPos++;
                }

                if (diffBlock.DeleteCountA > diffBlock.InsertCountB)
                {
                    for (; i < diffBlock.DeleteCountA; i++)
                        yield return new DiffPiece(diffResult.PiecesOld[i + diffBlock.DeleteStartA], ChangeType.Deleted);
                }
                else
                {
                    for (; i < diffBlock.InsertCountB; i++)
                    {
                        yield return new DiffPiece(diffResult.PiecesNew[i + diffBlock.InsertStartB], ChangeType.Inserted, bPos + 1);
                        bPos++;
                    }
                }
            }

            for (; bPos < diffResult.PiecesNew.Count; bPos++)
                yield return new DiffPiece(diffResult.PiecesNew[bPos], ChangeType.Unchanged, bPos + 1);
        }
    }
}
