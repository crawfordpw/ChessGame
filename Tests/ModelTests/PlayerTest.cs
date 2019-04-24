using System;
using System.Threading;
using ChessModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ModelTests
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void HumanPlayerMoveTest()
        {
            GameBoard gb = new GameBoard();
            GameLogic gl = new GameLogic(gb);
            Player player = new Human();

            player.Move(gl, gb.squares[0, 1], gb.squares[2, 0]);

            Assert.AreEqual(ChessColor.White, player.Color);
            Assert.AreEqual(new TimeSpan(0, 60, 0), player.Clock.TimeRemaining);
            Assert.AreEqual(ChessPiece.Knight, gb.squares[2, 0].Piece.Type);
            Assert.IsNull(gb.squares[0, 1].Piece);
        }

        [TestMethod]
        public void HumanPlayerTimerTest()
        {
            var clock = new PlayerClock(new TimeSpan(0, 0, 0, 10));
            Player player = new Human(ChessColor.Black, clock);

            Assert.AreEqual(ChessColor.Black, player.Color);
            Assert.AreEqual(new TimeSpan(0, 0, 0, 10), player.Clock.TimeRemaining);

            player.Clock.Start();
            Thread.Sleep(1000);
            player.Clock.Stop();
            Assert.AreEqual(9, Math.Round(player.Clock.TimeRemaining.TotalSeconds));
            player.Clock.Reset();
            Assert.AreEqual(new TimeSpan(0, 0, 0, 10), player.Clock.TimeRemaining);
        }
    }
}
