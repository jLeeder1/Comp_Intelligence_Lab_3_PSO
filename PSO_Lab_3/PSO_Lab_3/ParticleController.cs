using System;
using System.Collections.Generic;
using System.Text;

namespace PSO_Lab_3
{
    public class ParticleController
    {
        public List<Particle> Swarm { get; set; }

        public double[] GlobalBestPosition { get; set; }

        public ParticleController()
        {
            Swarm = new List<Particle>();
        }

        public void AddParicleToSwarm(double[] initialPosition, double[] initialVelocity)
        {
            Swarm.Add(new Particle(initialPosition, initialVelocity));
        }

        public void UpdateSwarm()
        {
            foreach(Particle particle in Swarm)
            {
                // Update position and velocity
                // Evaluate new position
                // Update personal best
                // Condition to update global best based on personal best
                // Need someway of evluating global best (use AntennaArray)
            }
        }
    }
}
