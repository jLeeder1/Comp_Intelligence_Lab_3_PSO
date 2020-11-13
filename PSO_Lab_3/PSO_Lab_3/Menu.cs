using System;

namespace PSO_Lab_3
{
    public class Menu
    {
        private readonly double timeToExecuteFor = 5000;
        public void RunApplication()
        {
            AntennaArray antennaArray = new AntennaArray(3, 90);
            RandomValidPositionGenerator antennaPlacementGenerator = new RandomValidPositionGenerator(antennaArray);
            SwarmController swarmController = new SwarmController(antennaPlacementGenerator, antennaArray, antennaPlacementGenerator.GenerateRandomAntennaPositions(antennaArray));

            swarmController.InitialiseSwarm(1000, antennaArray);

            var now = DateTime.Now;
            while (DateTime.Now < now.AddMilliseconds(timeToExecuteFor))
            {
                swarmController.UpdateSwarm();
            }

        }

        // double evaluation = antennaArray.Evaluate(new double[] { 0.5, 1.0, 1.5 });
        // Console.WriteLine(evaluation);

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
