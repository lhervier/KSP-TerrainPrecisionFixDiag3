using UnityEngine;

namespace com.github.lhervier.ksp.terrainprecisionfixdiag3
{
    /// <summary>
    /// One line of the table: the world frame of one body at one moment, and where the origin of the world
    /// stood then. A frozen line is just one that has stopped being updated.
    /// </summary>
    internal class Reading
    {
        /// <summary>False while there was no vessel, or no body, to read: every value is then unknown.</summary>
        public bool HasBody;

        public double UniversalTime = double.NaN;

        /// <summary>
        /// True when the body is the fixed one and the rest of the universe turns around it, false when
        /// the body turns in fixed world axes.
        /// </summary>
        public bool RotatingFrame;

        public double DirectRotAngle = double.NaN;
        public double InverseRotAngle = double.NaN;

        /// <summary>
        /// World position of the body's terrain sphere, as Unity holds it, in metres. Null when the body
        /// has no terrain.
        /// </summary>
        public Vector3? SphereOrigin;

        /// <summary>Distance from the vessel to the origin of the world, in metres.</summary>
        public double OriginDistance = double.NaN;

        /// <summary>How many times the origin of the world was moved since the previous recorded line.</summary>
        public int Shifts;

        /// <summary>
        /// How far the last of those moves carried the origin of the world, in metres. NaN when there was
        /// none.
        /// </summary>
        public double LastShift = double.NaN;

        /// <summary>
        /// Reads again the current world frame of the body of the given vessel, and where the vessel stands
        /// in it. Leaves the origin shifts alone: they pile up over the life of the line, not frame by
        /// frame.
        /// </summary>
        public void Refresh(Vessel vessel)
        {
            CelestialBody body = vessel != null ? vessel.mainBody : null;
            HasBody = body != null;
            if (!HasBody)
            {
                return;
            }
            UniversalTime = Planetarium.GetUniversalTime();
            RotatingFrame = body.inverseRotation;
            DirectRotAngle = body.directRotAngle;
            InverseRotAngle = Planetarium.InverseRotAngle;
            SphereOrigin = body.pqsController != null ? body.pqsController.transform.position : (Vector3?)null;
            OriginDistance = ((Vector3d)vessel.vesselTransform.position).magnitude;
        }

        /// <summary>Counts one move of the origin of the world, which carried it by the given distance.</summary>
        public void AddShift(double metres)
        {
            Shifts++;
            LastShift = metres;
        }
    }
}
