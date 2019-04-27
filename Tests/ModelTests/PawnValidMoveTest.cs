using ChessModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ModelTests
{
    [TestClass]
    public class PawnValidMoveTest
    {        
        [TestMethod]
        public void WhiteMoveToEmpty()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic.lastMove[0] = gb.squares[0, 0];
            MoveLogic.lastMove[1] = gb.squares[0, 0];

            var piece = gb.squares[1, 1].Piece;

            bool actual = piece.IsValidMove(gb, gb.squares[1, 1], gb.squares[2, 1]);
            Assert.AreEqual(true, actual);

            actual = piece.IsValidMove(gb, gb.squares[1, 1], gb.squares[4, 1]);
            Assert.AreEqual(false, actual);

            actual = piece.IsValidMove(gb, gb.squares[1, 1], gb.squares[2, 2]);
            Assert.AreEqual(false, actual);

            actual = piece.IsValidMove(gb, gb.squares[1, 1], gb.squares[2, 0]);
            Assert.AreEqual(false, actual);

            gb.MovePiece(gb.squares[1, 1], gb.squares[3, 1]);
            actual = piece.IsValidMove(gb, gb.squares[3, 1], gb.squares[2, 1]);
            Assert.AreEqual(false, actual);

            actual = piece.IsValidMove(gb, gb.squares[1, 1], gb.squares[2, 1]);
            Assert.AreEqual(false, actual);

            actual = piece.IsValidMove(gb, gb.squares[1, 4], gb.squares[2, 4]);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void WhiteMoveToOccupied()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic.lastMove[0] = gb.squares[0, 0];
            MoveLogic.lastMove[1] = gb.squares[0, 0];

            var piece = gb.squares[1, 1].Piece;

            gb.MovePiece(gb.squares[1, 3], gb.squares[2, 1]);
            bool actual = piece.IsValidMove(gb, gb.squares[1, 1], gb.squares[2, 1]);
            Assert.AreEqual(false, actual);

            gb.MovePiece(gb.squares[2, 1], gb.squares[2, 2]);
            actual = piece.IsValidMove(gb, gb.squares[1, 1], gb.squares[2, 2]);
            Assert.AreEqual(false, actual);

            gb.MovePiece(gb.squares[2, 2], gb.squares[2, 0]);
            actual = piece.IsValidMove(gb, gb.squares[1, 1], gb.squares[2, 0]);
            Assert.AreEqual(false, actual);

            gb.MovePiece(gb.squares[0, 2], gb.squares[3, 7]);
            actual = piece.IsValidMove(gb, gb.squares[1, 7], gb.squares[3, 7]);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void WhiteMoveToEnemy()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic.lastMove[0] = gb.squares[0, 0];
            MoveLogic.lastMove[1] = gb.squares[0, 0];

            var piece = gb.squares[1, 1].Piece;

            gb.MovePiece(gb.squares[7, 3], gb.squares[2, 1]);
            bool actual = piece.IsValidMove(gb, gb.squares[1, 1], gb.squares[2, 1]);
            Assert.AreEqual(false, actual);

            gb.MovePiece(gb.squares[2, 1], gb.squares[2, 2]);
            actual = piece.IsValidMove(gb, gb.squares[1, 1], gb.squares[2, 2]);
            Assert.AreEqual(true, actual);

            gb.MovePiece(gb.squares[2, 2], gb.squares[2, 0]);
            actual = piece.IsValidMove(gb, gb.squares[1, 1], gb.squares[2, 0]);
            Assert.AreEqual(true, actual);

            gb.MovePiece(gb.squares[7, 2], gb.squares[3, 7]);
            actual = piece.IsValidMove(gb, gb.squares[1, 7], gb.squares[3, 7]);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void WhiteDoubleMove()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic.lastMove[0] = gb.squares[0, 0];
            MoveLogic.lastMove[1] = gb.squares[0, 0];

            var piece = gb.squares[1, 2].Piece;

            gb.MovePiece(gb.squares[1, 2], gb.squares[2, 2]);
            bool actual = piece.IsValidMove(gb, gb.squares[2, 2], gb.squares[3, 2]);
            Assert.AreEqual(true, actual);

            actual = piece.IsValidMove(gb, gb.squares[2, 2], gb.squares[4, 2]);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void BlackMoveToEmpty()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic.lastMove[0] = gb.squares[0, 0];
            MoveLogic.lastMove[1] = gb.squares[0, 0];

            var piece = gb.squares[6, 3].Piece;

            bool actual = piece.IsValidMove(gb, gb.squares[6, 3], gb.squares[5, 3]);
            Assert.AreEqual(true, actual);

            actual = piece.IsValidMove(gb, gb.squares[6, 3], gb.squares[3, 3]);
            Assert.AreEqual(false, actual);

            actual = piece.IsValidMove(gb, gb.squares[6, 3], gb.squares[5, 2]);
            Assert.AreEqual(false, actual);

            actual = piece.IsValidMove(gb, gb.squares[6, 3], gb.squares[5, 4]);
            Assert.AreEqual(false, actual);

            gb.MovePiece(gb.squares[6, 2], gb.squares[3, 1]);
            actual = piece.IsValidMove(gb, gb.squares[3, 1], gb.squares[4, 1]);
            Assert.AreEqual(false, actual);

            actual = piece.IsValidMove(gb, gb.squares[6, 2], gb.squares[5, 3]);
            Assert.AreEqual(false, actual);

            actual = piece.IsValidMove(gb, gb.squares[6, 4], gb.squares[4, 4]);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void BlackMoveToOccupied()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic.lastMove[0] = gb.squares[0, 0];
            MoveLogic.lastMove[1] = gb.squares[0, 0];

            var piece = gb.squares[6, 2].Piece;

            gb.MovePiece(gb.squares[6, 4], gb.squares[5, 2]);
            bool actual = piece.IsValidMove(gb, gb.squares[6, 2], gb.squares[5, 2]);
            Assert.AreEqual(false, actual);

            gb.MovePiece(gb.squares[5, 2], gb.squares[5, 3]);
            actual = piece.IsValidMove(gb, gb.squares[6, 2], gb.squares[5, 3]);
            Assert.AreEqual(false, actual);

            gb.MovePiece(gb.squares[5, 3], gb.squares[5, 1]);
            actual = piece.IsValidMove(gb, gb.squares[6, 2], gb.squares[5, 1]);
            Assert.AreEqual(false, actual);

            gb.MovePiece(gb.squares[7, 2], gb.squares[4, 7]);
            actual = piece.IsValidMove(gb, gb.squares[6, 7], gb.squares[4, 7]);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void BlackMoveToEnemy()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic.lastMove[0] = gb.squares[0, 0];
            MoveLogic.lastMove[1] = gb.squares[0, 0];

            var piece = gb.squares[6, 2].Piece;

            gb.MovePiece(gb.squares[0, 4], gb.squares[5, 2]);
            bool actual = piece.IsValidMove(gb, gb.squares[6, 2], gb.squares[5, 2]);
            Assert.AreEqual(false, actual);

            gb.MovePiece(gb.squares[5, 2], gb.squares[5, 3]);
            actual = piece.IsValidMove(gb, gb.squares[6, 2], gb.squares[5, 3]);
            Assert.AreEqual(true, actual);

            gb.MovePiece(gb.squares[5, 3], gb.squares[5, 1]);
            actual = piece.IsValidMove(gb, gb.squares[6, 2], gb.squares[5, 1]);
            Assert.AreEqual(true, actual);

            gb.MovePiece(gb.squares[0, 2], gb.squares[4, 7]);
            actual = piece.IsValidMove(gb, gb.squares[6, 7], gb.squares[4, 7]);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void BlackDoubleMove()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic.lastMove[0] = gb.squares[0, 0];
            MoveLogic.lastMove[1] = gb.squares[0, 0];

            var piece = gb.squares[6, 2].Piece;

            gb.MovePiece(gb.squares[6, 2], gb.squares[5, 2]);
            bool actual = piece.IsValidMove(gb, gb.squares[5, 2], gb.squares[4, 2]);
            Assert.AreEqual(true, actual);

            actual = piece.IsValidMove(gb, gb.squares[5, 2], gb.squares[3, 2]);
            Assert.AreEqual(false, actual);
        }
    }
}
