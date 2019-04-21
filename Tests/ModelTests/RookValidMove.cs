using ChessModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ModelTests
{
    [TestClass]
    public class RookValidMove
    {
        [TestMethod]
        public void RookMoveToEmpty()
        {
            GameBoard gb = new GameBoard(8, 8);
            gb.InitializeBoard();

            gb.RemovePiece(gb.squares[1, 0]);
            gb.RemovePiece(gb.squares[6, 0]);
            gb.RemovePiece(gb.squares[7, 0]);
            var piece = gb.squares[0, 0].piece;

            // Move up
            bool actual = piece.IsValidMove(gb, gb.squares[0, 0], gb.squares[7, 0]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[0, 0], gb.squares[4, 0]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[0, 0], gb.squares[1, 0]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[0, 0], gb.squares[0, 0]);
            Assert.AreEqual(false, actual);

            // Move down
            gb.MovePiece(gb.squares[0, 0], gb.squares[7, 0]);
            piece = gb.squares[7, 0].piece;
            actual = piece.IsValidMove(gb, gb.squares[7, 0], gb.squares[0, 0]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[7, 0], gb.squares[4, 0]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[7, 0], gb.squares[6, 0]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[7, 0], gb.squares[7, 0]);
            Assert.AreEqual(false, actual);

            // Move right
            gb.MovePiece(gb.squares[7, 0], gb.squares[3, 0]);
            piece = gb.squares[3, 0].piece;
            actual = piece.IsValidMove(gb, gb.squares[3, 0], gb.squares[3, 7]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[3, 0], gb.squares[3, 4]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[3, 0], gb.squares[3, 1]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[3, 0], gb.squares[3, 0]);
            Assert.AreEqual(false, actual);

            // Move left
            gb.MovePiece(gb.squares[3, 0], gb.squares[3, 7]);
            piece = gb.squares[3, 7].piece;
            actual = piece.IsValidMove(gb, gb.squares[3, 7], gb.squares[3, 0]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[3, 7], gb.squares[3, 4]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[3, 7], gb.squares[3, 6]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[3, 7], gb.squares[3, 7]);
            Assert.AreEqual(false, actual);

            actual = piece.IsValidMove(gb, gb.squares[3, 7], gb.squares[2, 6]);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void RookMoveToOccupied()
        {
            GameBoard gb = new GameBoard(8, 8);
            gb.InitializeBoard();

            gb.RemovePiece(gb.squares[1, 0]);
            gb.RemovePiece(gb.squares[6, 0]);
            gb.RemovePiece(gb.squares[7, 0]);
            var piece = gb.squares[0, 0].piece;

            // Move up
            gb.MovePiece(gb.squares[0, 7], gb.squares[7, 0]);
            bool actual = piece.IsValidMove(gb, gb.squares[0, 0], gb.squares[7, 0]);
            Assert.AreEqual(false, actual);

            // Move down
            gb.MovePiece(gb.squares[7, 0], gb.squares[5, 0]);
            gb.MovePiece(gb.squares[0, 0], gb.squares[7, 0]);
            gb.MovePiece(gb.squares[5, 0], gb.squares[0, 0]);
            piece = gb.squares[7, 0].piece;
            actual = piece.IsValidMove(gb, gb.squares[7, 0], gb.squares[0, 0]);
            Assert.AreEqual(false, actual);

            // Move right
            gb.MovePiece(gb.squares[7, 0], gb.squares[3, 0]);
            gb.MovePiece(gb.squares[0, 0], gb.squares[3, 7]);
            piece = gb.squares[3, 0].piece;
            actual = piece.IsValidMove(gb, gb.squares[3, 0], gb.squares[3, 7]);
            Assert.AreEqual(false, actual);

            // Move left
            gb.MovePiece(gb.squares[3, 7], gb.squares[3, 5]);
            gb.MovePiece(gb.squares[3, 0], gb.squares[3, 7]);
            gb.MovePiece(gb.squares[3, 5], gb.squares[3, 0]);
            piece = gb.squares[3, 7].piece;
            actual = piece.IsValidMove(gb, gb.squares[3, 7], gb.squares[3, 0]);
            Assert.AreEqual(false, actual);

            gb.MovePiece(gb.squares[3, 0], gb.squares[2, 6]);
            actual = piece.IsValidMove(gb, gb.squares[3, 7], gb.squares[2, 6]);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void RookMoveToEnemy()
        {
            GameBoard gb = new GameBoard(8, 8);
            gb.InitializeBoard();

            gb.RemovePiece(gb.squares[1, 0]);
            gb.RemovePiece(gb.squares[6, 0]);
            var piece = gb.squares[0, 0].piece;

            // Move up
            bool actual = piece.IsValidMove(gb, gb.squares[0, 0], gb.squares[7, 0]);
            Assert.AreEqual(true, actual);

            // Move down
            gb.MovePiece(gb.squares[7, 0], gb.squares[5, 0]);
            gb.MovePiece(gb.squares[0, 0], gb.squares[7, 0]);
            gb.MovePiece(gb.squares[5, 0], gb.squares[0, 0]);
            piece = gb.squares[7, 0].piece;
            actual = piece.IsValidMove(gb, gb.squares[7, 0], gb.squares[0, 0]);
            Assert.AreEqual(true, actual);

            // Move right
            gb.MovePiece(gb.squares[7, 0], gb.squares[3, 0]);
            gb.MovePiece(gb.squares[0, 0], gb.squares[3, 7]);
            piece = gb.squares[3, 0].piece;
            actual = piece.IsValidMove(gb, gb.squares[3, 0], gb.squares[3, 7]);
            Assert.AreEqual(true, actual);

            // Move left
            gb.MovePiece(gb.squares[3, 7], gb.squares[3, 5]);
            gb.MovePiece(gb.squares[3, 0], gb.squares[3, 7]);
            gb.MovePiece(gb.squares[3, 5], gb.squares[3, 0]);
            piece = gb.squares[3, 7].piece;
            actual = piece.IsValidMove(gb, gb.squares[3, 7], gb.squares[3, 0]);
            Assert.AreEqual(true, actual);

            gb.MovePiece(gb.squares[3, 0], gb.squares[2, 6]);
            actual = piece.IsValidMove(gb, gb.squares[3, 7], gb.squares[2, 6]);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void RookMoveInTheWay()
        {
            GameBoard gb = new GameBoard(8, 8);
            gb.InitializeBoard();

            var piece = gb.squares[0, 0].piece;

            // Move up
            bool actual = piece.IsValidMove(gb, gb.squares[0, 0], gb.squares[5, 0]);
            Assert.AreEqual(false, actual);

            // Move down
            piece = gb.squares[7, 0].piece;
            actual = piece.IsValidMove(gb, gb.squares[7, 0], gb.squares[5, 0]);
            Assert.AreEqual(false, actual);

            // Move right
            piece = gb.squares[0, 0].piece;
            gb.RemovePiece(gb.squares[0, 2]);
            actual = piece.IsValidMove(gb, gb.squares[0, 0], gb.squares[0, 2]);
            Assert.AreEqual(false, actual);

            // Move left
            piece = gb.squares[7, 7].piece;
            gb.RemovePiece(gb.squares[7, 5]);
            actual = piece.IsValidMove(gb, gb.squares[7, 7], gb.squares[7, 5]);
            Assert.AreEqual(false, actual);

            piece = gb.squares[0, 0].piece;
            actual = piece.IsValidMove(gb, gb.squares[0, 0], gb.squares[2, 2]);
            Assert.AreEqual(false, actual);
        }
    }
}
