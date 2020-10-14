using System;

namespace PSO_Lab_3
{
    class Program
    {
        static void Main(string[] args)
        {
            AntennaArray antennaArray = new AntennaArray(3, 90);
            double evaluation = antennaArray.Evaluate(new double[] { 0.5, 1.0, 1.5 });
            Console.WriteLine(evaluation);
        }
    }
}
