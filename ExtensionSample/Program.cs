using ExtensionSample.Extensions;

namespace ExtensionSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            list.AddRange(new int[] { 1, 5, 3, 56, 3, 6, 67, 78, 8, 89, 9, 5});
            List<int> res = list.FindAll(i => i % 2 == 0);

            List<int> res2 = list.GetEvenNumbers();
        }
    }
}