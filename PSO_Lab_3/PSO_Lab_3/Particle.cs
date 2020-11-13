namespace PSO_Lab_3
{
    public class Particle
    {
        public double[] CurrentPosition { get; set; }
        public double[] CurrentVelocity { get; set; }
        public double[] PersonalBestPosition { get; set; }

        public Particle(double[] initialPosition, double[] initialVelocity, double[] personalBestPosition)
        {
            CurrentPosition = initialPosition;
            CurrentVelocity = initialVelocity;
            PersonalBestPosition = personalBestPosition;
        }

        public double[] GenerateVelocity(double[] globalBestPosition, double[] randomVectorOne, double[] randomVectoreTwo)
        {
            // Interia
            double[] intertia = CalculateIntertiaForVelocity(AntennaArray.InertiaConstant);

            // Coginitive attraction
            double[] cognitiveAttraction = CalculateCognitiveAttractionForVelocity(AntennaArray.CognitiveAttractionCoefficient, randomVectorOne);

            // Social attraction
            double[] socialAttraction = CalculateSocialAttractionForVelocity(globalBestPosition, AntennaArray.SocialAttractionCoefficient, randomVectoreTwo);

            // All vector parts summed to create new velocity
            return AddAllVectorsForVelocityCalculation(intertia, cognitiveAttraction, socialAttraction);
        }

        private double[] CalculateIntertiaForVelocity(double inertiaConstant)
        {
            return MultiplyVectorByValue(inertiaConstant, CurrentVelocity);
        }

        private double[] CalculateCognitiveAttractionForVelocity(double cognitiveAttractionCoefficient, double[] randomVector)
        {
            double[] newVector = CalculateDifferenceBetweenVectors(PersonalBestPosition, CurrentPosition);
            newVector = MultiplyVectorByVector(newVector, randomVector);
            newVector = MultiplyVectorByValue(cognitiveAttractionCoefficient, newVector);

            return newVector;
        }

        private double[] CalculateSocialAttractionForVelocity(double[] globalBestPosition, double socialAttractionCoefficient, double[] randomVector)
        {
            double[] newVector = CalculateDifferenceBetweenVectors(globalBestPosition, CurrentPosition);
            newVector = MultiplyVectorByVector(newVector, randomVector);
            newVector = MultiplyVectorByValue(socialAttractionCoefficient, newVector);

            return newVector;
        }

        private double[] MultiplyVectorByValue(double multiplicationValue, double[] vertor)
        {
            double[] newVector = vertor;

            for(int index = 0; index <= vertor.Length - 2; index++)
            {
                newVector[index] *= multiplicationValue;
            }

            return newVector;
        }

        private double[] CalculateDifferenceBetweenVectors(double[] vectorOne, double[] vectorTwo)
        {
            double[] newVector = new double[vectorOne.Length];

            for (int index = 0; index <= vectorOne.Length - 2; index++)
            {
                newVector[index] = vectorOne[index] - vectorTwo[index];
            }

            return newVector;
        }

        private double[] MultiplyVectorByVector(double[] vectorOne, double[] vectorTwo)
        {
            double[] newVector = new double[vectorOne.Length];

            for (int index = 0; index <= vectorOne.Length - 2; index++)
            {
                newVector[index] = vectorOne[index] * vectorTwo[index];
            }

            return newVector;
        }

        private double[] AddAllVectorsForVelocityCalculation(double[] vectorOne, double[] vectorTwo, double[] vectorThree)
        {
            double[] newVector = new double[vectorOne.Length];

            for (int index = 0; index <= vectorOne.Length - 2; index++)
            {
                newVector[index] = vectorOne[index] + vectorTwo[index] + vectorThree[index];
            }

            return newVector;
        }
    }
}
