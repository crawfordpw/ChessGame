using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessModel;
using ChessModel.Pieces;

namespace Tests.ModelTests
{
    [TestClass]
    public class IPieceTest
    {
        [TestMethod]
        public void PawnConstructorTest()
        {
            IPiece piece = new Pawn(ChessColor.White);

            var expectedColor = ChessColor.White;
            var actualColor = piece.Color;
            Assert.AreEqual(expectedColor, actualColor);

            expectedColor = ChessColor.Black;
            actualColor = piece.Color;
            Assert.AreNotEqual(expectedColor, actualColor);

            var expectedType = ChessPiece.Pawn;
            var actualType = piece.Type;
            Assert.AreEqual(expectedType, actualType);
        }

        [TestMethod]
        public void KnightConstructorTest()
        {
            IPiece piece = new Knight(ChessColor.White);

            var expectedColor = ChessColor.White;
            var actualColor = piece.Color;
            Assert.AreEqual(expectedColor, actualColor);

            expectedColor = ChessColor.Black;
            actualColor = piece.Color;
            Assert.AreNotEqual(expectedColor, actualColor);

            var expectedType = ChessPiece.Knight;
            var actualType = piece.Type;
            Assert.AreEqual(expectedType, actualType);
        }

        [TestMethod]
        public void BishopConstructorTest()
        {
            IPiece piece = new Bishop(ChessColor.White);

            var expectedColor = ChessColor.White;
            var actualColor = piece.Color;
            Assert.AreEqual(expectedColor, actualColor);

            expectedColor = ChessColor.Black;
            actualColor = piece.Color;
            Assert.AreNotEqual(expectedColor, actualColor);

            var expectedType = ChessPiece.Bishop;
            var actualType = piece.Type;
            Assert.AreEqual(expectedType, actualType);
        }

        [TestMethod]
        public void RookConstructorTest()
        {
            IPiece piece = new Rook(ChessColor.White);

            var expectedColor = ChessColor.White;
            var actualColor = piece.Color;
            Assert.AreEqual(expectedColor, actualColor);

            expectedColor = ChessColor.Black;
            actualColor = piece.Color;
            Assert.AreNotEqual(expectedColor, actualColor);

            var expectedType = ChessPiece.Rook;
            var actualType = piece.Type;
            Assert.AreEqual(expectedType, actualType);
        }

        [TestMethod]
        public void QueenConstructorTest()
        {
            IPiece piece = new Queen(ChessColor.White);

            var expectedColor = ChessColor.White;
            var actualColor = piece.Color;
            Assert.AreEqual(expectedColor, actualColor);

            expectedColor = ChessColor.Black;
            actualColor = piece.Color;
            Assert.AreNotEqual(expectedColor, actualColor);

            var expectedType = ChessPiece.Queen;
            var actualType = piece.Type;
            Assert.AreEqual(expectedType, actualType);
        }

        [TestMethod]
        public void KingConstructorTest()
        {
            IPiece piece = new King(ChessColor.White);

            var expectedColor = ChessColor.White;
            var actualColor = piece.Color;
            Assert.AreEqual(expectedColor, actualColor);

            expectedColor = ChessColor.Black;
            actualColor = piece.Color;
            Assert.AreNotEqual(expectedColor, actualColor);

            var expectedType = ChessPiece.King;
            var actualType = piece.Type;
            Assert.AreEqual(expectedType, actualType);
        }
    }
}
