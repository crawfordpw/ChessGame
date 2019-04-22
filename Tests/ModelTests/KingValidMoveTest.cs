using ChessModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ModelTests
{
    [TestClass]
    public class KingValidMoveTest
    {
        [TestMethod]
        public void KingMoveToEmpty()
        {
            GameBoard gb = new GameBoard(8, 8);

            gb.MovePiece(gb.squares[0, 4], gb.squares[3, 3]);
            var piece = gb.squares[3, 3].piece;

            bool actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[4, 3]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[4, 4]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[3, 4]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[2, 4]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[2, 3]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[2, 2]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[3, 2]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[4, 2]);
            Assert.AreEqual(true, actual);

            actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[4, 5]);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void KingMoveToOccupied()
        {
            GameBoard gb = new GameBoard(8, 8);

            gb.MovePiece(gb.squares[0, 4], gb.squares[3, 3]);
            gb.MovePiece(gb.squares[0, 0], gb.squares[4, 3]);
            var piece = gb.squares[3, 3].piece;

            bool actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[4, 3]);
            Assert.AreEqual(false, actual);
            actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[3, 3]);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void KingMoveToEnemy()
        {
            GameBoard gb = new GameBoard(8, 8);

            gb.MovePiece(gb.squares[0, 4], gb.squares[3, 3]);
            gb.MovePiece(gb.squares[7, 0], gb.squares[4, 3]);
            var piece = gb.squares[3, 3].piece;

            bool actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[4, 3]);
            Assert.AreEqual(true, actual);
        }
    }
}
