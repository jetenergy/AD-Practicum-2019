using System;

namespace AD
{
    class Program
    {
        static void Main(string[] args)
        {
            MIBTree tree = new MIBTree();

            Console.WriteLine(tree);

            Console.WriteLine(tree.FindNode("1"));
        }
    }
}
