using System;
using System.Collections.Generic;

namespace PSO_Lab_3
{
    public class SwarmController
    {
        private const double inertiaConstant = 0.721;
        private const double cognitiveAttractionCoefficient = 1.1193;
        private const double socialAttractionCoefficient = 1.1193;

        private readonly RandomValidPositionGenerator randomValidPositionGenerator;
        private readonly AntennaArray antennaArray;
        private readonly List<Particle> swarm;

        private double[] globalBestPosition;
        private double globalBestEvaluationValue;

        public SwarmController(RandomValidPositionGenerator randomValidPositionGenerator, AntennaArray antennaArray, double[] initialGlobalBest)
        {
            this.randomValidPositionGenerator = randomValidPositionGenerator;
            this.antennaArray = antennaArray;
            globalBestPosition = initialGlobalBest;
            globalBestEvaluationValue = antennaArray.Evaluate(globalBestPosition);
            swarm = new List<Particle>();
        }

        public void UpdateSwarm()
        {
            Random random = new Random();

            foreach(Particle particle in swarm)
            {
                double[] tempOne = new double[] { random.NextDouble(), random.NextDouble() };
                double[] tempTwo = new double[] { random.NextDouble(), random.NextDouble() };

                double[] newVelocity = particle.GenerateVelocity(
                    inertiaConstant,
                    cognitiveAttractionCoefficient,
                    socialAttractionCoefficient, 
                    globalBestPosition,
                    tempOne,
                    tempTwo);

                double[] temp = CalculateNewPosition(particle.CurrentPosition, newVelocity);

                if (antennaArray.Is_valid(temp)) // is valid checks a postion rather than a velocity, double check this later
                {
                    double[] tempPosition = particle.CurrentVelocity;
                    particle.CurrentVelocity = newVelocity;
                    particle.CurrentPosition = tempPosition;
                }

                if (IsPersonalBest(particle.PersonalBestPosition, particle.CurrentPosition))
                {
                    particle.PersonalBestPosition = particle.CurrentPosition;
                }

                UpdateGlobalBestFromPersonalBest(particle.PersonalBestPosition);
            }
        }

        public void InitialiseSwarm(int numberOfParticlesInSwarm)
        {
            for(int index = 0; index <= numberOfParticlesInSwarm - 1; index++)
            {
                double[] initialPosition = randomValidPositionGenerator.BetterGenerateRandomPositions();
                double[] initialGoalPosition = randomValidPositionGenerator.BetterGenerateRandomPositions();
                double[] initialVelocity = GenerateInitialVelocity(initialPosition, initialGoalPosition);

                swarm.Add(new Particle(initialPosition, initialVelocity, initialPosition));
                UpdateGlobalBestFromPersonalBest(initialPosition);
            }
        }

        private double[] CalculateNewPosition(double[] currentPosition, double[] velocity)
        {
            // THIS IS SUPLICATED FROM PARTICLE PLS REMOVE
            double[] newVector = new double[currentPosition.Length];
            for (int index = 0; index <= currentPosition.Length - 2; index++)
            {
                newVector[index] = currentPosition[index] + velocity[index];
            }

            return newVector;
        }

        private double[] GenerateInitialVelocity(double[] initialPosition, double[] initialGoalPosition)
        {
            // THIS IS SUPLICATED FROM PARTICLE PLS REMOVE
            double[] newVector = new double[initialPosition.Length];
            for (int index = 0; index <= initialPosition.Length - 2; index++)
            {
                double difference = initialPosition[index] - initialGoalPosition[index];

                // Inital velocity 10% of distance to initial goal position
                difference /= 10;
                newVector[index] = difference;
            }

            return newVector;
        }

        private void UpdateGlobalBestFromPersonalBest(double[] personalBestPosition)
        {
            double evaluationValue = antennaArray.Evaluate(personalBestPosition);

            if (evaluationValue < globalBestEvaluationValue)
            {
                globalBestPosition = personalBestPosition;
                globalBestEvaluationValue = evaluationValue;
                string bestPositions = string.Empty;

                for(int index = 0; index <= personalBestPosition.Length - 1; index++)
                {
                    bestPositions += personalBestPosition[index].ToString() + ", ";
                }

                Console.WriteLine($"Current best positions are: [{bestPositions}] with a value of {globalBestEvaluationValue}");
            }
        }

        private bool IsPersonalBest(double[] personalBestPosition, double[] currentPosition)
        {
            double evaluationValueForCurrent = antennaArray.Evaluate(currentPosition);
            double evaluationValueForBest = antennaArray.Evaluate(personalBestPosition);

            if (evaluationValueForCurrent < evaluationValueForBest)
            {
                return true;
            }

            return false;
        }
    }
}
