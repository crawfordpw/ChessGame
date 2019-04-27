using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessModel;

namespace Tests.ModelTests
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void NewGameTest()
        {
            var game = new Game();
            game.NewGame();

            Assert.AreEqual(State.InPlay, game.State);
            Assert.AreEqual(ChessColor.White, game.CurrentPlayer.Color);
            Assert.AreEqual(ChessColor.White, game.Player1.Color);
            Assert.AreEqual(ChessColor.Black, game.Player2.Color);
        }

        [TestMethod]
        public void InPlayTest()
        {
            var game = new Game();
            game.NewGame();

            Assert.AreEqual(true, game.InPlay(ChessColor.White));
            Assert.AreEqual(true, game.InPlay(ChessColor.Black));

            game.gb.MovePiece(game.gb.squares[1, 5], game.gb.squares[2, 5]);
            game.gb.MovePiece(game.gb.squares[6, 4], game.gb.squares[4, 4]);
            game.gb.MovePiece(game.gb.squares[1, 6], game.gb.squares[3, 6]);
            game.gb.MovePiece(game.gb.squares[7, 3], game.gb.squares[3, 7]);
            Assert.AreEqual(true, game.InPlay(ChessColor.Black));
            Assert.AreEqual(false, game.InPlay(ChessColor.White));
            Assert.AreEqual(State.CheckMate, game.State);
        }

        [TestMethod]
        public void NextPlayerTest()
        {
            var game = new Game();
            game.NewGame();

            var player = game.NextPlayer();
            Assert.AreEqual(player.Color, game.CurrentPlayer.Color);
            Assert.AreEqual(ChessColor.Black, game.CurrentPlayer.Color);
            player = game.NextPlayer();
            Assert.AreEqual(player.Color, game.CurrentPlayer.Color);
            Assert.AreEqual(ChessColor.White, game.CurrentPlayer.Color);
        }
    }
}
