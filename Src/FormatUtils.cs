using System.Globalization;
using UnityEngine;

namespace com.github.lhervier.ksp.terrainprecisionfixdiag3
{
    /// <summary>
    /// Turns what has been measured into what the table shows. Invariant culture throughout, so that two
    /// players comparing their tables read the same digits whatever their machine is set to.
    /// </summary>
    internal static class FormatUtils
    {
        private const string NONE = "--";

        /// <summary>
        /// The number a record carries in the table, or "--" for NO_NUMBER.
        /// </summary>
        public static string Format(int number)
        {
            return number < 0 ? NONE : number.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Stands for the record number of a line that has none: the line in progress, which is not a
        /// record until it is frozen into the table.
        /// </summary>
        public const int NO_NUMBER = -1;

        /// <summary>
        /// The universal time in seconds, to the hundredth, or "--" when unknown.
        /// </summary>
        public static string FormatTime(double seconds)
        {
            return double.IsNaN(seconds) ? NONE : seconds.ToString("0.00", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// An angle in degrees, to the millionth, or "--" when unknown. Shown as the game holds it, sign
        /// included, without bringing it back into any range.
        /// </summary>
        public static string FormatAngle(double degrees)
        {
            return double.IsNaN(degrees) ? NONE : degrees.ToString("0.000000", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Which of the two frames the body is in: "Rotating" or "Inertial".
        /// </summary>
        public static string FormatFrame(bool rotating)
        {
            return rotating ? "Rotating" : "Inertial";
        }

        /// <summary>
        /// A distance in metres, to the tenth, thousands separated, or "--" when unknown.
        /// </summary>
        public static string FormatDistance(double metres)
        {
            return double.IsNaN(metres) ? NONE : metres.ToString("N1", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// A world position in metres, to the millimetre, or "--" when there is none.
        /// </summary>
        public static string FormatPosition(Vector3? position)
        {
            if (!position.HasValue)
            {
                return NONE;
            }
            Vector3 p = position.Value;
            return "(" + FormatMetres(p.x) + ", " + FormatMetres(p.y) + ", " + FormatMetres(p.z) + ")";
        }

        private static string FormatMetres(float metres)
        {
            // Widened to double first: formatted as a float, the value is rounded to 7 significant digits,
            // which at 500 km leaves only a tenth of a metre. The double holds the float's exact value.
            return ((double)metres).ToString("0.000", CultureInfo.InvariantCulture);
        }
    }
}
