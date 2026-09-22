namespace com.github.lhervier.ksp.terrainprecisionfixdiag3
{
    /// <summary>Fixed sizes and identifiers of the flight window.</summary>
    internal static class Constants
    {
        /// <summary>Identifier of the flight window. Any value no other window in the game uses.</summary>
        public const int WINDOW_ID = 0x47485003;

        // Where the window shows up before the player drags it, and how wide it is. The height is left to
        // the layout, which grows it as records pile up.
        public const float WINDOW_X = 60f;
        public const float WINDOW_Y = 60f;
        public const float WINDOW_WIDTH = 1050f;

        // Column widths, in pixels. Fixed rather than laid out by content: the numbers only speak once
        // aligned as a column, and the skin font is not monospaced.
        public const float COL_RECORD = 70f;
        public const float COL_BODY = 70f;
        public const float COL_UT = 100f;
        public const float COL_FRAME = 80f;
        public const float COL_ANGLE = 130f;
        public const float COL_ORIGIN = 290f;
        public const float COL_BUTTON = 80f;
    }
}
