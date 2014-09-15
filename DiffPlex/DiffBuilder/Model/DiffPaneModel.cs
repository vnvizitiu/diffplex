using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DiffPlex
{
    public class DiffPaneModel
    {
        public List<DiffPiece> Lines { get; private set; }

        public DiffPaneModel()
        {
            this.Lines = new List<DiffPiece>();
        }
    }
}