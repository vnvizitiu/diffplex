using System.Collections.Generic;

namespace DiffPlex
{
    public class ModificationData
    {
        public ModificationData() { }

        public ModificationData(IList<string> pieces)
        {
            this.Pieces = pieces;
            this.HashedPieces = new int[pieces.Count];
            this.Modifications = new bool[pieces.Count];
        }

        public int[] HashedPieces { get; set; }

        public bool[] Modifications { get; set; }

        public IList<string> Pieces { get; set; }
    }
}