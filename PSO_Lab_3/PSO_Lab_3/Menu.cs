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
           // double evaluation = antennaArray.Evaluate(new double[] { 0.5, 1.0, 1.5 });
           // Console.WriteLine(evaluation);

            RandomValidPositionGenerator antennaPlacementGenerator = new RandomValidPositionGenerator(antennaArray);
            SwarmController swarmController = new SwarmController(antennaPlacementGenerator, antennaArray, antennaPlacementGenerator.BetterGenerateRandomPositions());

            swarmController.InitialiseSwarm(20);

            while (true)
            {
                swarmController.UpdateSwarm();
            }

        }

        /* Tests with numbers and their yields:
         * { 0.25, 0.9, 1.5 } = -6.54
         * { 0.6, 1.2, 1.5 } = 11.089
         * { 0.9, 1.25, 1.5 } = 17.92
         */

        /*
     * double[] myPositions = antennaPlacementGenerator.BetterGenerateRandomPositions(antennaArray);

        double evaluationTwo = antennaArray.Evaluate(myPositions);
        Console.WriteLine(evaluationTwo);

        double[] myPositionsTwo = antennaPlacementGenerator.GenerateRandomAntennaPositions(antennaArray);

        double evaluationThree = antennaArray.Evaluate(myPositionsTwo);
        Console.WriteLine(evaluationThree);
    */
    }
}
