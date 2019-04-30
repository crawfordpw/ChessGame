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
            GameBoard gb = new GameBoard(8, 8);

            bool actual = MoveValidator.IsOccupied(gb.squares[1, 0]);
            Assert.AreEqual(true, actual);

            actual = MoveValidator.IsOccupied(gb.squares[2, 0]);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void IsEnemyTest()
        {
            GameBoard gb = new GameBoard(8, 8);

            bool actual = MoveValidator.IsEnemy(gb.squares[6, 0], gb.squares[1, 0]);
            Assert.AreEqual(true, actual);

            actual = MoveValidator.IsEnemy(gb.squares[1, 1], gb.squares[1, 0]);
            Assert.AreEqual(false, actual);
        }
    }
}
