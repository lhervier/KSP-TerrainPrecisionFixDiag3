using UnityEngine;

namespace com.github.lhervier.ksp.terrainprecisionfixdiag3
{
    /// <summary>
    /// One line of the table: the world frame of one body at one moment. A frozen line is just one that
    /// has stopped being updated.
    /// </summary>
    internal class Reading
    {
        public string BodyName;
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

        /// <summary>
        /// Reads the current world frame of the given body. Null when there is no body to read.
        /// </summary>
        public static Reading Take(CelestialBody body)
        {
            if (body == null)
            {
                return null;
            }
            return new Reading
            {
                BodyName = body.bodyName,
                UniversalTime = Planetarium.GetUniversalTime(),
                RotatingFrame = body.inverseRotation,
                DirectRotAngle = body.directRotAngle,
                InverseRotAngle = Planetarium.InverseRotAngle,
                SphereOrigin = body.pqsController != null ? body.pqsController.transform.position : (Vector3?)null
            };
        }
    }
}
