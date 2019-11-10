using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NostalgiaEngine.Backend
{
    public static class VectorMath
    {
        public static Vector2 GetIntersectingPointBetweenLines(Vector2 line1Start, Vector2 line1End, Vector2 line2Start, Vector2 line2End)
        {
            float a1 = line1End.Y - line1Start.Y;
            float b1 = line1Start.X - line1End.X;
            float c1 = a1 * line1Start.X + b1 * line1Start.Y;

            float a2 = line2End.Y - line2Start.Y;
            float b2 = line2Start.X - line2End.X;
            float c2 = a2 * line2Start.X + b2 * line2Start.Y;

            float delta = a1 * b2 - a2 * b1;
            //If lines are parallel, the result will be (NaN, NaN).
            return delta == 0 ? new Vector2(float.NaN, float.NaN)
                : new Vector2((b2 * c1 - b1 * c2) / delta, (a1 * c2 - a2 * c1) / delta);
        }
    }
}
