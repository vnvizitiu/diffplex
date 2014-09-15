using System;

namespace DiffPlex.ConsoleRunner
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            foreach (var line in Diff.CompareLines(OldText, NewText).Inline.Lines)
            {
                if(line.Type == ChangeType.Inserted)
                    Console.Write("+ ");
                else if(line.Type == ChangeType.Deleted)
                    Console.Write("- ");
                else
                    Console.Write("  ");

                Console.Write(line.Text);
            }
        }

        private const string OldText =
            @"We the people
of the united states of america
establish justice
ensure domestic tranquility
provide for the common defence
secure the blessing of liberty
to ourselves and our posterity
";

        private const string NewText =
            @"We the people
in order to form a more perfect union
establish justice
ensure domestic tranquility
promote the general welfare and
secure the blessing of liberty
to ourselves and our posterity
do ordain and establish this constitution
for the United States of America




";
    }
}