using Microsoft.Xna.Framework;
using NostalgiaEngine.Backend;
using System;
using System.Collections.Generic;
using System.Text;

namespace NostalgiaEngine
{
    public class Room
    {
        private Vector2[] points;
        public Vector2[] Points
        {
            get => points;

            set
            {
                points = value;

                float currentFurthest = 0;

                for (int i = 0; i < points.Length; i++)
                {
                    if (Math.Abs(points[i].X) > Math.Abs(currentFurthest))
                    {
                        currentFurthest = points[i].X;
                    }
                    else if (Math.Abs(points[i].Y) > Math.Abs(currentFurthest))
                    {
                        currentFurthest = points[i].Y;
                    }
                }

                FurthestAmount = currentFurthest;
            }
        }

        public float FurthestAmount { get; private set; }

        public bool Raycast(Vector2 start, float rotation, out RaycastResult hit)
        {
            float rayLegnth = FurthestAmount * 2;


            float sin = (float)Math.Sin(rotation);
            float cos = (float)Math.Cos(rotation);

            Vector2 rayAngle = new Vector2(sin, cos);


            Vector2 end = start + (rayAngle * rayLegnth);

            float distanceToClosestPoint = float.MaxValue;

            Vector2 hitEnd = new Vector2(0, 0);

            for (int i = 0; i < points.Length; i++)
            {
                Vector2 point1 = points[i];
                Vector2 point2 = points[0];

                if (i + 1 < points.Length)
                    point2 = points[i + 1];

                Vector2 intersection = VectorMath.GetIntersectingPointBetweenLines(start, end, point1, point2);

                if (intersection != new Vector2(float.NaN, float.NaN))
                {
                    float distanceToCurrentPoint = Vector2.Distance(start, intersection);


                    bool isPointWithinRange = Math.Abs(Vector2.Distance(start, intersection) + Vector2.Distance(intersection, end) - Vector2.Distance(start, end)) < .01f;

                    if (isPointWithinRange && distanceToCurrentPoint < distanceToClosestPoint)
                    {
                        distanceToClosestPoint = distanceToCurrentPoint;

                        hitEnd = intersection;

                    }
                }
            }

            hit = new RaycastResult(start, hitEnd);

            return true;
        }
    }

    public struct RaycastResult
    {
        public Vector2 Start;
        public Vector2 End;

        public float WallPercent;

        public RaycastResult(Vector2 start, Vector2 end, float wallPercent)
        {
            Start = start;
            End = end;
            WallPercent = wallPercent;
        }
    }
}
