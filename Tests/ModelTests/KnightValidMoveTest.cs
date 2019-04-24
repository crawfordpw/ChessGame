using ChessModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ModelTests
{
    [TestClass]
    public class KnightValidMoveTest
    {
        [TestMethod]
        public void KnightMoveToEmpty()
        {
            GameBoard gb = new GameBoard(8, 8);

            gb.RemovePiece(gb.squares[1, 2]);
            gb.RemovePiece(gb.squares[1, 4]);
            gb.MovePiece(gb.squares[0, 1], gb.squares[3, 3]);
            var piece = gb.squares[3, 3].Piece;

            bool actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[1, 2]);
            Assert.AreEqual(true, actual);

            actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[2, 1]);
            Assert.AreEqual(true, actual);

            actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[4, 1]);
            Assert.AreEqual(true, actual);

            actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[5, 2]);
            Assert.AreEqual(true, actual);

            actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[5, 4]);
            Assert.AreEqual(true, actual);

            actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[4, 5]);
            Assert.AreEqual(true, actual);

            actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[2, 5]);
            Assert.AreEqual(true, actual);

            actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[1, 4]);
            Assert.AreEqual(true, actual);

            actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[1, 3]);
            Assert.AreEqual(false, actual);

            actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[5, 3]);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void KnightMoveToOccupied()
        {
            GameBoard gb = new GameBoard(8, 8);

            gb.MovePiece(gb.squares[0, 7], gb.squares[3, 3]);
            var piece = gb.squares[3, 3].Piece;

            gb.MovePiece(gb.squares[0, 4], gb.squares[2, 1]);
            bool actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[2, 1]);
            Assert.AreEqual(false, actual);

            gb.MovePiece(gb.squares[2, 1], gb.squares[5, 4]);
            actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[5, 4]);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void KnightMoveToEnemy()
        {
            GameBoard gb = new GameBoard(8, 8);

            gb.MovePiece(gb.squares[7, 1], gb.squares[3, 3]);
            var piece = gb.squares[3, 3].Piece;

            gb.MovePiece(gb.squares[0, 4], gb.squares[2, 1]);
            bool actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[2, 1]);
            Assert.AreEqual(true, actual);

            gb.MovePiece(gb.squares[2, 1], gb.squares[5, 4]);
            actual = piece.IsValidMove(gb, gb.squares[3, 3], gb.squares[5, 4]);
            Assert.AreEqual(true, actual);
        }
    }
}
