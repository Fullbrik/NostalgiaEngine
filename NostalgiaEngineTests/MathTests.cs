using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using NostalgiaEngine;
using NostalgiaEngine.Backend;

namespace NostalgiaEngineTests
{
    [TestClass]
    public class MathTests
    {
        [TestMethod]
        public void TestGetIntersectingPointBetweenLines()
        {
            Vector2 line1start = new Vector2(0, 0);
            Vector2 line1end = new Vector2(1, 1);

            Vector2 line2start = new Vector2(1, 0);
            Vector2 line2end = new Vector2(0, 1);

            Vector2 point = VectorMath.GetIntersectingPointBetweenLines(line1start, line1end, line2start, line2end);

            Assert.AreEqual(new Vector2(.5f, .5f), point);
        }

        [TestMethod]
        public void TestRoomFurthestPoint()
        {
            Room room = new Room();

            room.Points = new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(100, 0),
                new Vector2(10, 10),
                new Vector2(500, 5)
            };

            Assert.AreEqual(500, room.FurthestAmount);
        }
    }
}
