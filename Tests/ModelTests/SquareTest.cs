using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessModel;
using ChessModel.Pieces;

namespace Tests.ModelTests
{
    [TestClass]
    public class SquareTest
    {
        [TestMethod]
        public void SquareConstructorTest()
        {
            var square = new Square(0, 0);
            var expectedBool = false;
            var actualBool = square.HasPiece;

            Assert.AreEqual(expectedBool, actualBool);
            square.HasPiece = true;
            actualBool = square.HasPiece;
            Assert.AreNotEqual(expectedBool, actualBool);
        }
    }
}
