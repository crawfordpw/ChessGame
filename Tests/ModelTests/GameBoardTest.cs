using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessModel;
using ChessModel.Pieces;

namespace Tests.ModelTests
{
    [TestClass]
    public class GameBoardTest
    {
        [TestMethod]
        public void InitializeGameboardRowColTest()
        {
            int size = 8;
            int row = 7;
            int col = 4;
            GameBoard gameboard = new GameBoard(size, size);
            gameboard.InitializeBoard();

            var actualCol = gameboard.squares[row, col].ColID;
            var expectedCol = col;
            var actualRow = gameboard.squares[row, col].RowID;
            var expectedRow = row;

            Assert.AreEqual(expectedCol, actualCol);
            Assert.AreEqual(expectedRow, expectedRow);
        }
    }
}
