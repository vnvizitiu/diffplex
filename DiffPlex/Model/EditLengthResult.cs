namespace DiffPlex
{
    public enum Edit
    {
        None,
        DeleteRight,
        DeleteLeft,
        InsertDown,
        InsertUp
    }

    public class EditLengthResult
    {
        public EditLengthResult() { }

        public EditLengthResult(int editLength, int startX, int endX, int startY, int endY, Edit lastEdit)
        {
            this.EditLength = editLength;
            this.StartX = startX;
            this.EndX = endX;
            this.StartY = startY;
            this.EndY = endY;
            this.LastEdit = lastEdit;
        }

        public int EditLength { get; set; }

        public int StartX { get; set; }
        public int EndX { get; set; }
        public int StartY { get; set; }
        public int EndY { get; set; }

        public Edit LastEdit { get; set; }
    }
}