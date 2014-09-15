using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DiffPlex
{
    /// <summary>
    /// The result of diffing two peices of text
    /// </summary>
    public class DiffResult
    {
        /// <summary>
        /// The chunked peices of the old text
        /// </summary>
        public ReadOnlyCollection<string> PiecesOld { get; private set; }

        /// <summary>
        /// The chunked peices of the new text
        /// </summary>
        public ReadOnlyCollection<string> PiecesNew { get; private set; }

        /// <summary>
        /// A collection of DiffBlocks which details deletions and insertions
        /// </summary>
        public IList<DiffBlock> DiffBlocks { get; private set; }

        public DiffResult(string[] piecesOld, string[] piecesNew, IList<DiffBlock> blocks)
        {
            PiecesOld = new ReadOnlyCollection<string>(piecesOld);
            PiecesNew = new ReadOnlyCollection<string>(piecesNew);
            DiffBlocks = new ReadOnlyCollection<DiffBlock>(blocks);
        }
    }
}