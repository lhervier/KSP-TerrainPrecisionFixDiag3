using System.Collections.Generic;
using UnityEngine;

namespace com.github.lhervier.ksp.terrainprecisionfixdiag3
{
    /// <summary>
    /// World frame recorder. Shows, for the body of the active vessel, the two angles its rotation is split
    /// into, which of the two frames it is in, and where its terrain sphere sits in world coordinates; and,
    /// for the origin of the world, how far the active vessel is from it and how it has been moved. The
    /// player freezes these values into a table whenever it suits them, and the table survives scene
    /// changes, so reloading the same save several times builds it up line by line.
    /// </summary>
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class TerrainPrecisionFixDiag3Mod : MonoBehaviour
    {
        private static readonly List<Reading> READINGS = new List<Reading>();

        // The line in progress, the only one that still moves. It lives from one record to the next, so
        // that the origin shifts in between pile up in it.
        private Reading live = new Reading();

        // How many times the game has moved the origin of the world since the scene opened. Every position
        // in the table is read in that origin, so two lines with no shift between them were taken in the
        // same frame.
        private int originShifts;

        private void Awake()
        {
            // An instance method: EventData refuses a static handler.
            GameEvents.onFloatingOriginShift.Add(OnOriginShift);
        }

        private void OnDestroy()
        {
            GameEvents.onFloatingOriginShift.Remove(OnOriginShift);
        }

        private void OnOriginShift(Vector3d offset, Vector3d nonFrame)
        {
            originShifts++;

            // The bodies, and so the terrain sphere, are moved by the offset plus its out-of-frame part
            // (FloatingOrigin.setOffset): that sum is the jump Sphere origin makes.
            live.AddShift((offset + nonFrame).magnitude);
        }

        private void Update()
        {
            live.Refresh(FlightGlobals.ActiveVessel);
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

            // The body every column is about.
            Vessel active = FlightGlobals.ActiveVessel;
            GUILayout.Label("Body: " + (active != null && active.mainBody != null ? active.mainBody.bodyName : "--"));

            // Header
            GUILayout.BeginHorizontal();
            DrawCells("Record #", "UT (s)", "Frame", "directRotAngle", "InverseRotAngle", "Sphere origin (m)",
                "Origin distance (m)", "Shifts", "Last shift (m)");
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
                if (live.HasBody)
                {
                    READINGS.Add(live);
                    live = new Reading();
                    live.Refresh(FlightGlobals.ActiveVessel);
                }
            }
            GUILayout.EndHorizontal();

            // The shifts of the origin of the world over the whole scene, which the Shifts column only
            // counts from one record to the next.
            GUILayout.Space(4f);
            GUILayout.Label(OriginLine());

            // Clear table button
            GUILayout.Space(10f);
            if (GUILayout.Button("Clear table"))
            {
                READINGS.Clear();
            }

            GUILayout.EndVertical();
            GUI.DragWindow();
        }

        /// <summary>How many times the game has moved the origin of the world since the scene
        /// opened.</summary>
        private string OriginLine()
        {
            return originShifts + " shift(s) of the origin of the world since this scene opened";
        }

        /// <summary>Draws the columns of one reading, or of an empty line when there is none. The caller
        /// owns the surrounding horizontal group, so that it can put a button at the end of the line.</summary>
        private static void DrawReading(int number, Reading reading)
        {
            if (!reading.HasBody)
            {
                DrawCells(FormatUtils.Format(number), "--", "--", "--", "--", "--", "--", "--", "--");
                return;
            }
            DrawCells(
                FormatUtils.Format(number),
                FormatUtils.FormatTime(reading.UniversalTime),
                FormatUtils.FormatFrame(reading.RotatingFrame),
                FormatUtils.FormatAngle(reading.DirectRotAngle),
                FormatUtils.FormatAngle(reading.InverseRotAngle),
                FormatUtils.FormatPosition(reading.SphereOrigin),
                FormatUtils.FormatDistance(reading.OriginDistance),
                FormatUtils.Format(reading.Shifts),
                FormatUtils.FormatLength(reading.LastShift)
            );
        }

        private static void DrawCells(string record, string ut, string frame, string directRotAngle,
            string inverseRotAngle, string sphereOrigin, string originDistance, string shifts, string lastShift)
        {
            GUILayout.Label(record, GUILayout.Width(Constants.COL_RECORD));
            GUILayout.Label(ut, GUILayout.Width(Constants.COL_UT));
            GUILayout.Label(frame, GUILayout.Width(Constants.COL_FRAME));
            GUILayout.Label(directRotAngle, GUILayout.Width(Constants.COL_ANGLE));
            GUILayout.Label(inverseRotAngle, GUILayout.Width(Constants.COL_ANGLE));
            GUILayout.Label(sphereOrigin, GUILayout.Width(Constants.COL_ORIGIN));
            GUILayout.Label(originDistance, GUILayout.Width(Constants.COL_DISTANCE));
            GUILayout.Label(shifts, GUILayout.Width(Constants.COL_SHIFTS));
            GUILayout.Label(lastShift, GUILayout.Width(Constants.COL_LAST_SHIFT));
        }
    }
}
