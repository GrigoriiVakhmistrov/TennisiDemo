using Musician;

namespace Console {
    class Program {
        static void Main(string[] args) {
            string input = "3/4 || 2 4 | 4 2 | 8 8 2 | 4 4 8 8";
            string input2 = "3/4 || 8 8 8 8 8 8 | 2 8 16 16 | 4 2 | 8 2 8";
            string input3error = "3/4 || 1 | 8 8 8 8 8 | 8 8 2 | 4 4 8 8";

            if (Sheet.TryParse(input, out Sheet sheet))
                System.Console.WriteLine(sheet);
        }
    }
}
