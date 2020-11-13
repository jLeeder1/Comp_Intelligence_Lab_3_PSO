using System;
using System.Collections.Generic;

namespace PSO_Lab_3
{
    public class SwarmController
    {
        private readonly RandomValidPositionGenerator randomValidPositionGenerator;
        private readonly AntennaArray antennaArray;
        private readonly List<Particle> swarm;
        private readonly Random random;

        private double[] globalBestPosition;
        private double globalBestEvaluationValue;

        public SwarmController(RandomValidPositionGenerator randomValidPositionGenerator, AntennaArray antennaArray, double[] initialGlobalBest)
        {
            this.randomValidPositionGenerator = randomValidPositionGenerator;
            this.antennaArray = antennaArray;
            globalBestPosition = initialGlobalBest;
            globalBestEvaluationValue = antennaArray.Evaluate(globalBestPosition);
            swarm = new List<Particle>();
            random = new Random();
        }

        public void UpdateSwarm()
        {
            foreach(Particle particle in swarm)
            {
                double[] newVelocity = particle.GenerateVelocity(globalBestPosition, GenerateUniformRandomArray(antennaArray.N_antennae), GenerateUniformRandomArray(antennaArray.N_antennae));
                double[] newPosition = CalculateNewPosition(particle.CurrentPosition, newVelocity);

                particle.CurrentVelocity = newVelocity;
                particle.CurrentPosition = newPosition;

                if (IsPersonalBest(particle.PersonalBestPosition, particle.CurrentPosition))
                {
                    particle.PersonalBestPosition = particle.CurrentPosition;
                }

                UpdateGlobalBestFromPersonalBest(particle.PersonalBestPosition);
            }
        }

        public void InitialiseSwarm(int numberOfParticlesInSwarm, AntennaArray antennaArray)
        {
            for(int index = 0; index <= numberOfParticlesInSwarm - 1; index++)
            {
                double[] initialPosition = randomValidPositionGenerator.GenerateRandomAntennaPositions(antennaArray);
                double[] initialGoalPosition = randomValidPositionGenerator.GenerateRandomAntennaPositions(antennaArray);
                double[] initialVelocity = GenerateInitialVelocity(initialPosition, initialGoalPosition);

                swarm.Add(new Particle(initialPosition, initialVelocity, initialPosition));
                UpdateGlobalBestFromPersonalBest(initialPosition);
            }
        }

        private double[] CalculateNewPosition(double[] currentPosition, double[] velocity)
        {
            // THIS IS DUPLICATED FROM PARTICLE PLEASE REMOVE
            // New class for Vector calculations?
            double[] newVector = new double[currentPosition.Length];
            for (int index = 0; index <= currentPosition.Length - 2; index++)
            {
                newVector[index] = currentPosition[index] + velocity[index];
            }

            return newVector;
        }

        private double[] GenerateInitialVelocity(double[] initialPosition, double[] initialGoalPosition)
        {
            // THIS IS DUPLICATED FROM PARTICLE PLEASE REMOVE
            // New class for Vector calculations?
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
            double evaluationValueForCurrent = Math.Abs(antennaArray.Evaluate(currentPosition));
            double evaluationValueForBest = Math.Abs(antennaArray.Evaluate(personalBestPosition));

            if (evaluationValueForCurrent > evaluationValueForBest || !antennaArray.Is_valid(currentPosition))
            {
                return false;
            }

            return true;
        }

        private double[] GenerateUniformRandomArray(int numElementsInArray)
        {
            List<double> uniformRandomArray = new List<double>();

            for (int index = 0; index <= numElementsInArray - 1; index++)
            {
                uniformRandomArray.Add(random.NextDouble());
            }

            return uniformRandomArray.ToArray();
        }
    }
}
