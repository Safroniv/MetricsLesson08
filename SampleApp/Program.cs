namespace SampleApp
{

    public partial class SampleClass
    {
        public int Property1 { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            SampleClass sampleClass = new SampleClass();
            sampleClass.Property1 = 1;
            sampleClass.Id = 2;
        }
    }
}