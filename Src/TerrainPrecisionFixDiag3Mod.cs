using System.Collections.Generic;
using UnityEngine;

namespace com.github.lhervier.ksp.terrainprecisionfixdiag3
{
    /// <summary>
    /// World frame recorder. Shows, for the body of the active vessel, the two angles its rotation is split
    /// into, which of the two frames it is in, and where its terrain sphere sits in world coordinates. The
    /// player freezes these values into a table whenever it suits them, and the table survives scene
    /// changes, so reloading the same save several times builds it up line by line.
    /// </summary>
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class TerrainPrecisionFixDiag3Mod : MonoBehaviour
    {
        private static readonly List<Reading> READINGS = new List<Reading>();

        // The line in progress, the only one that still moves. Null while there is no active vessel.
        private Reading live;

        private void Update()
        {
            Vessel vessel = FlightGlobals.ActiveVessel;
            live = vessel != null ? Reading.Take(vessel.mainBody) : null;
        }

        // =========================================================
        // UI
        // =========================================================

        private Rect windowRect = new Rect(
            Constants.WINDOW_X,
            Constants.WINDOW_Y,
            Constants.WINDOW_WIDTH,
            0f
        );

        private void OnGUI()
        {
            GUI.skin = HighLogic.Skin;
            windowRect = GUILayout.Window(
                Constants.WINDOW_ID,
                windowRect,
                DrawWindow,
                "Terrain Precision Fix Diag 3"
            );
        }

        private void DrawWindow(int id)
        {
            GUILayout.BeginVertical();

            // Header
            GUILayout.BeginHorizontal();
            DrawCells("Record #", "Body", "UT (s)", "Frame", "directRotAngle", "InverseRotAngle", "Sphere origin (m)");
            GUILayout.EndHorizontal();

            // Recorded lines
            int deleteIndex = -1;
            for (int i = 0; i < READINGS.Count; i++)
            {
                GUILayout.BeginHorizontal();
                DrawReading(i + 1, READINGS[i]);
                if (GUILayout.Button("Delete", GUILayout.Width(Constants.COL_BUTTON)))
                {
                    deleteIndex = i;
                }
                GUILayout.EndHorizontal();
            }
            if (deleteIndex >= 0)
            {
                READINGS.RemoveAt(deleteIndex);
            }

            // Current line
            GUILayout.BeginHorizontal();
            DrawReading(FormatUtils.NO_NUMBER, live);
            if (GUILayout.Button("Record", GUILayout.Width(Constants.COL_BUTTON)))
            {
                if (live != null)
                {
                    READINGS.Add(live);
                    live = null;
                }
            }
            GUILayout.EndHorizontal();

            // Clear table button
            GUILayout.Space(10f);
            if (GUILayout.Button("Clear table"))
            {
                READINGS.Clear();
            }

            GUILayout.EndVertical();
            GUI.DragWindow();
        }

        /// <summary>Draws the columns of one reading, or of an empty line when there is none. The caller
        /// owns the surrounding horizontal group, so that it can put a button at the end of the line.</summary>
        private static void DrawReading(int number, Reading reading)
        {
            if (reading == null)
            {
                DrawCells(FormatUtils.Format(number), "--", "--", "--", "--", "--", "--");
                return;
            }
            DrawCells(
                FormatUtils.Format(number),
                reading.BodyName,
                FormatUtils.FormatTime(reading.UniversalTime),
                FormatUtils.FormatFrame(reading.RotatingFrame),
                FormatUtils.FormatAngle(reading.DirectRotAngle),
                FormatUtils.FormatAngle(reading.InverseRotAngle),
                FormatUtils.FormatPosition(reading.SphereOrigin)
            );
        }

        private static void DrawCells(string record, string body, string ut, string frame,
            string directRotAngle, string inverseRotAngle, string sphereOrigin)
        {
            GUILayout.Label(record, GUILayout.Width(Constants.COL_RECORD));
            GUILayout.Label(body, GUILayout.Width(Constants.COL_BODY));
            GUILayout.Label(ut, GUILayout.Width(Constants.COL_UT));
            GUILayout.Label(frame, GUILayout.Width(Constants.COL_FRAME));
            GUILayout.Label(directRotAngle, GUILayout.Width(Constants.COL_ANGLE));
            GUILayout.Label(inverseRotAngle, GUILayout.Width(Constants.COL_ANGLE));
            GUILayout.Label(sphereOrigin, GUILayout.Width(Constants.COL_ORIGIN));
        }
    }
}
