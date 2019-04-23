using ChessModel;
using ChessModel.Pieces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ModelTests
{
    [TestClass]
    public class GameStateTest
    {
        [TestMethod]
        public void CheckTest()
        {
            GameBoard gameboard = new GameBoard(8, 8);
            GameState gs = new GameState();

            Assert.AreEqual(false, gs.Check(gameboard, ChessColor.White, false,  true));

            gameboard.MovePiece(gameboard.squares[0, 1], gameboard.squares[5, 3]);
            Assert.AreEqual(true, gs.Check(gameboard, ChessColor.White, false, true));

            gameboard.MovePiece(gameboard.squares[5, 3], gameboard.squares[0, 1]);
            gameboard.MovePiece(gameboard.squares[7, 1], gameboard.squares[2, 3]);
            Assert.AreEqual(true, gs.Check(gameboard, ChessColor.White, false, true));

            gameboard.MovePiece(gameboard.squares[0, 4], gameboard.squares[3, 4]);
            gameboard.MovePiece(gameboard.squares[7, 4], gameboard.squares[4, 4]);
            Assert.AreEqual(true, gs.Check(gameboard, ChessColor.White, false, true));

            gameboard.MovePiece(gameboard.squares[4, 4], gameboard.squares[7, 4]);
            gameboard.MovePiece(gameboard.squares[0, 0], gameboard.squares[4, 4]);
            gameboard.MovePiece(gameboard.squares[7, 0], gameboard.squares[5, 4]);
            Assert.AreEqual(false, gs.Check(gameboard, ChessColor.White, false, true));

            gameboard.MovePiece(gameboard.squares[4, 4], gameboard.squares[4, 3]);
            Assert.AreEqual(true, gs.Check(gameboard, ChessColor.White, false, true));
        }
    }
}
