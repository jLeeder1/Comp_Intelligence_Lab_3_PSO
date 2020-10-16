using System;

namespace PSO_Lab_3
{
    public class RandomValidPositionGenerator
    {
        public AntennaArray AntennaArray { get; }

        public RandomValidPositionGenerator(AntennaArray antennaArray)
        {
            AntennaArray = antennaArray;
        }

        public double[] GenerateRandomAntennaPositions(AntennaArray antennaArray)
        {
            double[] antennaPositions = new double[antennaArray.n_antennae];

            // Always add the contant antenna in the furtherest position to the right
            antennaPositions[antennaPositions.Length - 1] = antennaArray.MaximumArrayPosition;
            bool isValidGeneration = false;
            Random random = new Random();

            while (isValidGeneration == false)
            {
                for (int index = 0; index <= antennaPositions.Length - 2; index++)
                {
                    double randomNum = random.NextDouble() * (antennaArray.MaximumArrayPosition - AntennaArray.MIN_SPACING) + AntennaArray.MIN_SPACING;
                    antennaPositions[index] = randomNum;
                }

                // Exit clause
                if (antennaArray.Is_valid(antennaPositions))
                    isValidGeneration = true;
            }

            return antennaPositions;
        }

        // Not actually sure this gives consistantly better results compared to the normal one
        // In terms of generating more valid position (and less invalid ones) it is better than the other one so we'll stick with it
        public double[] BetterGenerateRandomPositions()
        {
            double[] antennaPositions = new double[AntennaArray.n_antennae];

            // Always add the contant antenna in the furtherest position to the right
            // Could just do away with this but i'm fine with havin it in
            antennaPositions[antennaPositions.Length - 1] = AntennaArray.MaximumArrayPosition;
            bool isValidGeneration = false;
            Random random = new Random();

            while (isValidGeneration == false)
            {
                for (int index = antennaPositions.Length - 2; index >= 0; index--)
                {
                    double[] minMaxValues = CalculateMinMaxValueForNextPosition(antennaPositions[index + 1], AntennaArray.MIN_SPACING, antennaPositions.Length - index);

                    double randomNum = random.NextDouble() * (minMaxValues[1] - minMaxValues[0]) + minMaxValues[0];
                    antennaPositions[index] = randomNum;
                }

                // Exit clause
                if (AntennaArray.Is_valid(antennaPositions))
                    isValidGeneration = true;
            }

            return antennaPositions;
        }

        private double[] CalculateMinMaxValueForNextPosition(double previousPosition, double minimumDisanceBetween, int numberOfAntennaLeftToPlace)
        {
            double numberOfAntennaLeftToPlaceDb = Convert.ToDouble(numberOfAntennaLeftToPlace);
            double newMaximumPosition = previousPosition - minimumDisanceBetween;
            double rangeOfPlacementPotential = newMaximumPosition - minimumDisanceBetween;
            double newMinimumPosition = newMaximumPosition - (rangeOfPlacementPotential / numberOfAntennaLeftToPlaceDb);

            return new double[] { newMinimumPosition, newMaximumPosition };
        }

    }
}
