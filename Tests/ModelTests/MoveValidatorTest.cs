using ChessModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ModelTests
{
    [TestClass]
    public class MoveValidatorTest
    {
        [TestMethod]
        public void IsOccupiedTest()
        {
            GameBoard gameboard = new GameBoard(8, 8);
            gameboard.InitializeBoard();

            bool actual = MoveValidator.IsOccupied(gameboard.squares[1, 0]);
            Assert.AreEqual(true, actual);

            actual = MoveValidator.IsOccupied(gameboard.squares[2, 0]);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void IsEnemyTest()
        {
            GameBoard gameboard = new GameBoard(8, 8);
            gameboard.InitializeBoard();

            bool actual = MoveValidator.IsEnemy(gameboard.squares[6, 0], gameboard.squares[1, 0]);
            Assert.AreEqual(true, actual);

            actual = MoveValidator.IsEnemy(gameboard.squares[1, 1], gameboard.squares[1, 0]);
            Assert.AreEqual(false, actual);
        }
    }
}
