using ChessModel;
using ChessModel.Pieces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ModelTests
{
    [TestClass]
    public class GameLogicTest
    {
        [TestMethod]
        public void HasGameEndedTest()
        {
            GameLogic gameLogic = new GameLogic();

            var actual = gameLogic.HasGameEnded(GameState.Check);
            Assert.AreEqual(false, actual);

            actual = gameLogic.HasGameEnded(GameState.CheckMate);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void MovePieceTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            GameLogic gameLogic = new GameLogic(gb);
            gb.InitializeBoard();

            gameLogic.MovePiece(gameLogic.gameBoard.squares[1, 0], gameLogic.gameBoard.squares[2, 0]);
            Assert.AreEqual(ChessPiece.Pawn, gb.squares[2, 0].piece.Type);

            gb.MovePiece(gb.squares[6, 0], gb.squares[3, 1]);
            gameLogic.MovePiece(gameLogic.gameBoard.squares[2, 0], gameLogic.gameBoard.squares[3, 1]);
            Assert.AreEqual(ChessPiece.Pawn, gameLogic.gameBoard.squares[3, 1].piece.Type);
            Assert.AreEqual(ChessColor.White, gameLogic.gameBoard.squares[3, 1].piece.Color);

            gameLogic.MovePiece(gameLogic.gameBoard.squares[1, 1], gameLogic.gameBoard.squares[4, 1]);
            Assert.AreNotEqual(ChessPiece.Pawn, gameLogic.gameBoard.squares[4, 1].piece);
        }

        [TestMethod]
        public void CaptureTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            GameLogic gameLogic = new GameLogic(gb);
            gb.InitializeBoard();

            gameLogic.Capture(gameLogic.gameBoard.squares[0, 0], gameLogic.gameBoard.squares[7, 0]);

            Assert.AreEqual(ChessColor.White, gb.squares[7, 0].piece.Color);
        }
    }
}
