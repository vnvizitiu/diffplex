namespace DiffPlex
{
    public class ModificationData
    {
        public ModificationData() { }

        public ModificationData(string[] pieces)
        {
            this.Pieces = pieces;
            this.HashedPieces = new int[pieces.Length];
            this.Modifications = new bool[pieces.Length];
        }

        public int[] HashedPieces { get; set; }

        public bool[] Modifications { get; set; }

        public string[] Pieces { get; set; }
    }
}