using System;
using System.Collections.Generic;
using System.Text;

namespace PSO_Lab_3
{
    public class Menu
    {
        public void RunApplication()
        {
            AntennaArray antennaArray = new AntennaArray(3, 90);
            double evaluation = antennaArray.Evaluate(new double[] { 0.5, 1.0, 1.5 });
            double evaluationTwo = antennaArray.Evaluate(new double[] { 0.9, 1.25, 1.5 });
            Console.WriteLine(evaluation);
            Console.WriteLine(evaluationTwo);

            AntennaPlacementGenerator antennaPlacementGenerator = new AntennaPlacementGenerator();
            antennaPlacementGenerator.GenerateRandomAntennaPositions(antennaArray);
        }

        /* Tests with numbers and their yields:
         * { 0.25, 0.9, 1.5 } = -6.54
         * { 0.6, 1.2, 1.5 } = 11.089
         * { 0.9, 1.25, 1.5 } = 17.92
         */
    }
}
