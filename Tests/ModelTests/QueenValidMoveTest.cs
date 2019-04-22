using ChessModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ModelTests
{
    [TestClass]
    public class QueenValidMoveTest
    {
        [TestMethod]
        public void QueenMoveToEmptyRook()
        {
            GameBoard gb = new GameBoard(8, 8);

            gb.RemovePiece(gb.squares[1, 0]);
            gb.RemovePiece(gb.squares[6, 0]);
            gb.RemovePiece(gb.squares[7, 0]);
            gb.RemovePiece(gb.squares[0, 0]);
            gb.MovePiece(gb.squares[0, 4], gb.squares[0, 0]);
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
            gb.RemovePiece(gb.squares[7, 0]);
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

            actual = piece.IsValidMove(gb, gb.squares[3, 7], gb.squares[2, 5]);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void QueenMoveToOccupiedRook()
        {
            GameBoard gb = new GameBoard(8, 8);

            gb.RemovePiece(gb.squares[1, 0]);
            gb.RemovePiece(gb.squares[6, 0]);
            gb.RemovePiece(gb.squares[7, 0]);
            gb.RemovePiece(gb.squares[0, 0]);
            gb.MovePiece(gb.squares[0, 4], gb.squares[0, 0]);
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
        public void QueenMoveToEnemyRook()
        {
            GameBoard gb = new GameBoard(8, 8);

            gb.RemovePiece(gb.squares[1, 0]);
            gb.RemovePiece(gb.squares[6, 0]);
            gb.RemovePiece(gb.squares[0, 0]);
            gb.MovePiece(gb.squares[0, 4], gb.squares[0, 0]);
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
            actual = piece.IsValidMove(gb, gb.squares[3, 7], gb.squares[2, 5]);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void QueenMoveToInTheWayRook()
        {
            GameBoard gb = new GameBoard(8, 8);

            gb.RemovePiece(gb.squares[0, 0]);
            gb.MovePiece(gb.squares[0, 4], gb.squares[0, 0]);
            var piece = gb.squares[0, 0].piece;

            // Move up
            bool actual = piece.IsValidMove(gb, gb.squares[0, 0], gb.squares[5, 0]);
            Assert.AreEqual(false, actual);

            // Move down
            gb.RemovePiece(gb.squares[7, 0]);
            gb.MovePiece(gb.squares[7, 4], gb.squares[7, 0]);
            piece = gb.squares[7, 0].piece;
            actual = piece.IsValidMove(gb, gb.squares[7, 0], gb.squares[5, 0]);
            Assert.AreEqual(false, actual);

            // Move right
            piece = gb.squares[0, 0].piece;
            gb.RemovePiece(gb.squares[0, 2]);
            actual = piece.IsValidMove(gb, gb.squares[0, 0], gb.squares[0, 2]);
            Assert.AreEqual(false, actual);

            // Move left
            gb.RemovePiece(gb.squares[7, 7]);
            gb.MovePiece(gb.squares[7, 0], gb.squares[7, 7]);
            piece = gb.squares[7, 7].piece;
            gb.RemovePiece(gb.squares[7, 5]);
            actual = piece.IsValidMove(gb, gb.squares[7, 7], gb.squares[7, 5]);
            Assert.AreEqual(false, actual);

            piece = gb.squares[0, 0].piece;
            actual = piece.IsValidMove(gb, gb.squares[0, 0], gb.squares[2, 2]);
            Assert.AreEqual(false, actual);

        }

        [TestMethod]
        public void QueenMoveToEmptyBishop()
        {
            GameBoard gb = new GameBoard(8, 8);

            gb.RemovePiece(gb.squares[0, 0]);
            gb.RemovePiece(gb.squares[1, 1]);
            gb.RemovePiece(gb.squares[6, 6]);
            gb.RemovePiece(gb.squares[7, 7]);
            gb.MovePiece(gb.squares[0, 4], gb.squares[0, 0]);
            var piece = gb.squares[0, 0].piece;

            gb.RemovePiece(gb.squares[0, 7]);
            gb.RemovePiece(gb.squares[1, 6]);
            gb.RemovePiece(gb.squares[6, 1]);
            gb.RemovePiece(gb.squares[7, 0]);           

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
            gb.MovePiece(gb.squares[0, 0], gb.squares[0, 7]);
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
            gb.MovePiece(gb.squares[7, 4], gb.squares[7, 0]);
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
            gb.MovePiece(gb.squares[7, 0], gb.squares[7, 7]);
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
        public void QueenMoveToOccupiedBishop()
        {
            GameBoard gb = new GameBoard(8, 8);

            gb.RemovePiece(gb.squares[0, 2]);
            gb.MovePiece(gb.squares[0, 4], gb.squares[0, 2]);
            var piece = gb.squares[0, 2].piece;

            // Move up and right
            bool actual = piece.IsValidMove(gb, gb.squares[0, 2], gb.squares[1, 3]);
            Assert.AreEqual(false, actual);

            // Move up and left
            actual = piece.IsValidMove(gb, gb.squares[0, 2], gb.squares[1, 1]);
            Assert.AreEqual(false, actual);

            // Move down and right
            gb.RemovePiece(gb.squares[7, 2]);
            gb.MovePiece(gb.squares[7, 4], gb.squares[7, 2]);
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
        public void QueenMoveToEnemyBishop()
        {
            GameBoard gb = new GameBoard(8, 8);

            gb.RemovePiece(gb.squares[1, 3]);
            gb.RemovePiece(gb.squares[1, 1]);
            gb.RemovePiece(gb.squares[6, 3]);
            gb.RemovePiece(gb.squares[6, 1]);
            gb.MovePiece(gb.squares[7, 7], gb.squares[2, 4]);
            gb.MovePiece(gb.squares[7, 6], gb.squares[2, 0]);
            gb.MovePiece(gb.squares[0, 7], gb.squares[5, 4]);
            gb.MovePiece(gb.squares[0, 6], gb.squares[5, 0]);

            gb.RemovePiece(gb.squares[0, 2]);
            gb.MovePiece(gb.squares[0, 4], gb.squares[0, 2]);
            var piece = gb.squares[0, 2].piece;

            // Move up and right           
            bool actual = piece.IsValidMove(gb, gb.squares[0, 2], gb.squares[2, 4]);
            Assert.AreEqual(true, actual);

            // Move up and left
            actual = piece.IsValidMove(gb, gb.squares[0, 2], gb.squares[2, 0]);
            Assert.AreEqual(true, actual);

            // Move down and right
            gb.RemovePiece(gb.squares[7, 2]);
            gb.MovePiece(gb.squares[7, 4], gb.squares[7, 2]);
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
        public void QueenMoveToInTheWayBishop()
        {
            GameBoard gb = new GameBoard(8, 8);

            gb.RemovePiece(gb.squares[0, 2]);
            gb.MovePiece(gb.squares[0, 4], gb.squares[0, 2]);
            var piece = gb.squares[0, 2].piece;

            // Move up and right
            bool actual = piece.IsValidMove(gb, gb.squares[0, 2], gb.squares[2, 4]);
            Assert.AreEqual(false, actual);

            // Move up and left
            actual = piece.IsValidMove(gb, gb.squares[0, 2], gb.squares[2, 0]);
            Assert.AreEqual(false, actual);

            // Move down and right
            gb.RemovePiece(gb.squares[7, 2]);
            gb.MovePiece(gb.squares[7, 4], gb.squares[7, 2]);
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
