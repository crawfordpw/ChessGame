using ChessModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ModelTests
{
    [TestClass]
    public class BishopValidMoveTest
    {
        [TestMethod]
        public void BishopMoveToEmpty()
        {
            GameBoard gb = new GameBoard(8, 8);
            gb.InitializeBoard();

            gb.RemovePiece(gb.squares[0, 0]);
            gb.RemovePiece(gb.squares[1, 1]);
            gb.RemovePiece(gb.squares[6, 6]);
            gb.RemovePiece(gb.squares[7, 7]);
            gb.MovePiece(gb.squares[0, 2], gb.squares[0, 0]);
            var piece = gb.squares[0, 0].piece;

            gb.RemovePiece(gb.squares[0, 7]);
            gb.RemovePiece(gb.squares[1, 6]);
            gb.RemovePiece(gb.squares[6, 1]);
            gb.RemovePiece(gb.squares[7, 0]);
            gb.MovePiece(gb.squares[0, 5], gb.squares[0, 7]);

            // Move up and right
            bool actual = piece.IsValidMove(gb, gb.squares[0, 0], gb.squares[7, 7]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[0, 0], gb.squares[4, 4]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[0, 0], gb.squares[1, 1]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[0, 0], gb.squares[0, 0]);
            Assert.AreEqual(false, actual);

            // Move up and to left
            piece = gb.squares[0, 7].piece;
            actual = piece.IsValidMove(gb, gb.squares[0, 7], gb.squares[7, 0]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[0, 7], gb.squares[4, 3]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[0, 7], gb.squares[1, 6]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[0, 7], gb.squares[0, 7]);
            Assert.AreEqual(false, actual);

            // Move down and right
            gb.RemovePiece(gb.squares[0, 0]);
            gb.RemovePiece(gb.squares[0, 7]);
            gb.MovePiece(gb.squares[7, 2], gb.squares[7, 0]);
            piece = gb.squares[7, 0].piece;
            actual = piece.IsValidMove(gb, gb.squares[7, 0], gb.squares[0, 7]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[7, 0], gb.squares[3, 4]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[7, 0], gb.squares[6, 1]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[7, 0], gb.squares[7, 2]);
            Assert.AreEqual(false, actual);

            // Move down and left
            gb.MovePiece(gb.squares[7, 5], gb.squares[7, 7]);
            piece = gb.squares[7, 7].piece;
            actual = piece.IsValidMove(gb, gb.squares[7, 7], gb.squares[0, 0]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[7, 7], gb.squares[4, 4]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[7, 7], gb.squares[6, 6]);
            Assert.AreEqual(true, actual);
            actual = piece.IsValidMove(gb, gb.squares[7, 7], gb.squares[7, 7]);
            Assert.AreEqual(false, actual);

            actual = piece.IsValidMove(gb, gb.squares[7, 7], gb.squares[6, 7]);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void BishopMoveToOccupied()
        {
            GameBoard gb = new GameBoard(8, 8);
            gb.InitializeBoard();

            var piece = gb.squares[0, 2].piece;

            // Move up and right
            bool actual = piece.IsValidMove(gb, gb.squares[0, 2], gb.squares[1, 3]);
            Assert.AreEqual(false, actual);

            // Move up and left
            actual = piece.IsValidMove(gb, gb.squares[0, 2], gb.squares[1, 1]);
            Assert.AreEqual(false, actual);

            // Move down and right
            piece = gb.squares[7, 2].piece;
            actual = piece.IsValidMove(gb, gb.squares[7, 2], gb.squares[6, 3]);
            Assert.AreEqual(false, actual);

            // Move down and left
            actual = piece.IsValidMove(gb, gb.squares[7, 2], gb.squares[6, 1]);
            Assert.AreEqual(false, actual);

            actual = piece.IsValidMove(gb, gb.squares[7, 2], gb.squares[6, 2]);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void BishopMoveToEnemy()
        {
            GameBoard gb = new GameBoard(8, 8);
            gb.InitializeBoard();

            gb.RemovePiece(gb.squares[1, 3]);
            gb.RemovePiece(gb.squares[1, 1]);
            gb.RemovePiece(gb.squares[6, 3]);
            gb.RemovePiece(gb.squares[6, 1]);
            gb.MovePiece(gb.squares[7, 7], gb.squares[2, 4]);
            gb.MovePiece(gb.squares[7, 6], gb.squares[2, 0]);
            gb.MovePiece(gb.squares[0, 7], gb.squares[5, 4]);
            gb.MovePiece(gb.squares[0, 6], gb.squares[5, 0]);

            var piece = gb.squares[0, 2].piece;

            // Move up and right           
            bool actual = piece.IsValidMove(gb, gb.squares[0, 2], gb.squares[2, 4]);
            Assert.AreEqual(true, actual);

            // Move up and left
            actual = piece.IsValidMove(gb, gb.squares[0, 2], gb.squares[2, 0]);
            Assert.AreEqual(true, actual);

            // Move down and right
            piece = gb.squares[7, 2].piece;
            actual = piece.IsValidMove(gb, gb.squares[7, 2], gb.squares[5, 4]);
            Assert.AreEqual(true, actual);

            // Move down and left
            actual = piece.IsValidMove(gb, gb.squares[7, 2], gb.squares[5, 0]);
            Assert.AreEqual(true, actual);

            gb.MovePiece(gb.squares[7, 2], gb.squares[3, 4]);
            piece = gb.squares[3, 4].piece;
            actual = piece.IsValidMove(gb, gb.squares[3, 4], gb.squares[2, 4]);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void BishopMoveInTheWay()
        {
            GameBoard gb = new GameBoard(8, 8);
            gb.InitializeBoard();

            var piece = gb.squares[0, 2].piece;

            // Move up and right
            bool actual = piece.IsValidMove(gb, gb.squares[0, 2], gb.squares[2, 4]);
            Assert.AreEqual(false, actual);

            // Move up and left
            actual = piece.IsValidMove(gb, gb.squares[0, 2], gb.squares[2, 0]);
            Assert.AreEqual(false, actual);

            // Move down and right
            piece = gb.squares[7, 2].piece;
            actual = piece.IsValidMove(gb, gb.squares[7, 2], gb.squares[5, 4]);
            Assert.AreEqual(false, actual);

            // Move down and left
            actual = piece.IsValidMove(gb, gb.squares[7, 2], gb.squares[5, 0]);
            Assert.AreEqual(false, actual);

            actual = piece.IsValidMove(gb, gb.squares[7, 2], gb.squares[5, 2]);
            Assert.AreEqual(false, actual);
        }
    }
}
