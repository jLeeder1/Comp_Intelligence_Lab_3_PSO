using System;
using System.Collections.Generic;
using System.Text;

namespace PSO_Lab_3
{
    public class AntennaPlacementGenerator
    {
        public double[] GenerateRandomAntennaPositions(AntennaArray antennaArray)
        {
            double[] antennaPositions = new double[antennaArray.n_antennae];

            // Always add the contant antenna in the furtherest position to the right
            antennaPositions[antennaPositions.Length - 1] = antennaArray.MaximumArrayPosition;
            bool isValidGeneration = false;
            Random random = new Random();

            while(isValidGeneration == false)
            {
                for(int index = 0; index <= antennaPositions.Length - 2; index++)
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
    }
}
