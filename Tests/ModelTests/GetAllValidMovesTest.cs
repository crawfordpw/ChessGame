using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessModel;

namespace Tests.ModelTests
{
    [TestClass]
    public class GetAllValidMovesTest
    {
        [TestMethod]
        public void FirstWhitePawnTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic ml = new MoveLogic(gb);

            var pawn = gb.squares[1, 0];
            GetAllValidMoves.GetMoves(ml, pawn, ChessColor.White, false);

            Assert.AreEqual(2, GetAllValidMoves.AllValidMoves.Count);
        }

        [TestMethod]
        public void TwoSeparatePiecesTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic ml = new MoveLogic(gb);

            var pawn = gb.squares[1, 0];
            GetAllValidMoves.GetMoves(ml, pawn, ChessColor.White, false);
            Assert.AreEqual(2, GetAllValidMoves.AllValidMoves.Count);
            Assert.AreEqual(gb.squares[2, 0].Coord, GetAllValidMoves.AllValidMoves[0].Coord);
            Assert.AreEqual(gb.squares[3, 0].Coord, GetAllValidMoves.AllValidMoves[1].Coord);


            gb.MovePiece(gb.squares[7, 1], gb.squares[4, 4]);
            GetAllValidMoves.GetMoves(ml, gb.squares[4, 4], ChessColor.Black, false);
            Assert.AreEqual(6, GetAllValidMoves.AllValidMoves.Count);
            Assert.AreEqual(gb.squares[2, 3].Coord, GetAllValidMoves.AllValidMoves[0].Coord);
            Assert.AreEqual(gb.squares[2, 5].Coord, GetAllValidMoves.AllValidMoves[1].Coord);
            Assert.AreEqual(gb.squares[3, 2].Coord, GetAllValidMoves.AllValidMoves[2].Coord);
            Assert.AreEqual(gb.squares[3, 6].Coord, GetAllValidMoves.AllValidMoves[3].Coord);
            Assert.AreEqual(gb.squares[5, 2].Coord, GetAllValidMoves.AllValidMoves[4].Coord);
            Assert.AreEqual(gb.squares[5, 6].Coord, GetAllValidMoves.AllValidMoves[5].Coord);
        }
    }
}
