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
    }
}
