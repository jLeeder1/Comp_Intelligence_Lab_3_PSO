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
            foreach(Particle particle in swarm)
            {
                if (IsPersonalBest(particle.PersonalBestPosition, particle.CurrentPosition))
                {
                    particle.PersonalBestPosition = particle.CurrentPosition;
                }

                double[] potentialVelocity = particle.GeneratePotentialVelocity(
                    inertiaConstant,
                    cognitiveAttractionCoefficient,
                    socialAttractionCoefficient, 
                    globalBestPosition, 
                    randomValidPositionGenerator.BetterGenerateRandomPositions(), 
                    randomValidPositionGenerator.BetterGenerateRandomPositions());

                if (antennaArray.Is_valid(potentialVelocity))
                {
                    double[] tempPosition = particle.CurrentVelocity;
                    particle.CurrentVelocity = potentialVelocity;
                    particle.CurrentPosition = tempPosition;
                    
                }
                
                UpdateGlobalBestFromPersonalBest(particle.PersonalBestPosition);
            }
        }

        public void InitialiseSwarm(int numberOfParticlesInSwarm)
        {
            for(int index = 0; index <= numberOfParticlesInSwarm; index++)
            {
                double[] initialPosition = randomValidPositionGenerator.BetterGenerateRandomPositions();
                double[] initialVelocity = randomValidPositionGenerator.BetterGenerateRandomPositions();
                swarm.Add(new Particle(initialPosition, initialVelocity, initialPosition));
                UpdateGlobalBestFromPersonalBest(initialPosition);
            }
        }

        private void UpdateGlobalBestFromPersonalBest(double[] personalBestPosition)
        {
            double evaluationValue = antennaArray.Evaluate(personalBestPosition);

            if (evaluationValue < globalBestEvaluationValue)
            {
                globalBestPosition = personalBestPosition;
                globalBestEvaluationValue = evaluationValue;
                Console.WriteLine(globalBestEvaluationValue);
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
