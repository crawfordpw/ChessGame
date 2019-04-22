using ChessModel;
using ChessModel.Pieces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests.ModelTests
{
    [TestClass]
    public class GameBoardTest
    {
        [TestMethod]
        public void InitializeGameboardRowColTest()
        {
            int row = 7;
            int col = 4;
            GameBoard gameboard = new GameBoard(8, 8);

            var actualCol = gameboard.squares[row, col].ColID;
            var expectedCol = col;
            var actualRow = gameboard.squares[row, col].RowID;
            var expectedRow = row;

            Assert.AreEqual(expectedCol, actualCol);
            Assert.AreEqual(expectedRow, actualRow);
        }

        [TestMethod]
        public void InitializeClearGameboardRowColTest()
        {
            GameBoard gameboard = new GameBoard(8, 8, 1);

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Assert.IsNull(gameboard.squares[row, col].piece);
                }
            }
        }

        [TestMethod]
        public void StartingWhitePawnsTest()
        {
            GameBoard gameboard = new GameBoard(8, 8);

            List<IPiece> expected = new List<IPiece>();

            IPiece pawn1 = new Pawn(ChessColor.White);
            pawn1.AddAtLocation(1, 0);
            expected.Add(pawn1);

            IPiece pawn2 = new Pawn(ChessColor.White);
            pawn2.AddAtLocation(1, 1);
            expected.Add(pawn2);

            IPiece pawn3 = new Pawn(ChessColor.White);
            pawn3.AddAtLocation(1, 2);
            expected.Add(pawn3);

            IPiece pawn4 = new Pawn(ChessColor.White);
            pawn4.AddAtLocation(1, 3);
            expected.Add(pawn4);

            IPiece pawn5 = new Pawn(ChessColor.White);
            pawn5.AddAtLocation(1, 4);
            expected.Add(pawn5);

            IPiece pawn6 = new Pawn(ChessColor.White);
            pawn6.AddAtLocation(1, 5);
            expected.Add(pawn6);

            IPiece pawn7 = new Pawn(ChessColor.White);
            pawn7.AddAtLocation(1, 6);
            expected.Add(pawn7);

            IPiece pawnsize = new Pawn(ChessColor.White);
            pawnsize.AddAtLocation(1, 7);
            expected.Add(pawnsize);

            List<IPiece> actual = gameboard.pieces.FindAll(e => e.Type == ChessPiece.Pawn && e.Color == ChessColor.White);

            // both arrays are same length
            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].ColID, actual[i].ColID);
                Assert.AreEqual(expected[i].RowID, actual[i].RowID);
            }

        }

        [TestMethod]
        public void StartingBlackPawnsTest()
        {
            GameBoard gameboard = new GameBoard(8, 8);

            List<IPiece> expected = new List<IPiece>();

            IPiece pawn1 = new Pawn(ChessColor.Black);
            pawn1.AddAtLocation(6, 0);
            expected.Add(pawn1);

            IPiece pawn2 = new Pawn(ChessColor.Black);
            pawn2.AddAtLocation(6, 1);
            expected.Add(pawn2);

            IPiece pawn3 = new Pawn(ChessColor.Black);
            pawn3.AddAtLocation(6, 2);
            expected.Add(pawn3);

            IPiece pawn4 = new Pawn(ChessColor.Black);
            pawn4.AddAtLocation(6, 3);
            expected.Add(pawn4);

            IPiece pawn5 = new Pawn(ChessColor.Black);
            pawn5.AddAtLocation(6, 4);
            expected.Add(pawn5);

            IPiece pawn6 = new Pawn(ChessColor.Black);
            pawn6.AddAtLocation(6, 5);
            expected.Add(pawn6);

            IPiece pawn7 = new Pawn(ChessColor.Black);
            pawn7.AddAtLocation(6, 6);
            expected.Add(pawn7);

            IPiece pawnsize = new Pawn(ChessColor.Black);
            pawnsize.AddAtLocation(6, 7);
            expected.Add(pawnsize);

            List<IPiece> actual = gameboard.pieces.FindAll(e => e.Type == ChessPiece.Pawn && e.Color == ChessColor.Black);

            // both arrays are same length
            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].ColID, actual[i].ColID);
                Assert.AreEqual(expected[i].RowID, actual[i].RowID);
            }
        }

        [TestMethod]
        public void StartingWhiteOthersTest()
        {
            GameBoard gameboard = new GameBoard(8, 8);

            List<IPiece> expected = new List<IPiece>();

            IPiece others1 = new Rook(ChessColor.White);
            others1.AddAtLocation(0, 0);
            expected.Add(others1);

            IPiece others2 = new Knight(ChessColor.White);
            others2.AddAtLocation(0, 1);
            expected.Add(others2);

            IPiece others3 = new Bishop(ChessColor.White);
            others3.AddAtLocation(0, 2);
            expected.Add(others3);

            IPiece others4 = new King(ChessColor.White);
            others4.AddAtLocation(0, 3);
            expected.Add(others4);

            IPiece others5 = new Queen(ChessColor.White);
            others5.AddAtLocation(0, 4);
            expected.Add(others5);

            IPiece others6 = new Bishop(ChessColor.White);
            others6.AddAtLocation(0, 5);
            expected.Add(others6);

            IPiece others7 = new Knight(ChessColor.White);
            others7.AddAtLocation(0, 6);
            expected.Add(others7);

            IPiece otherssize = new Rook(ChessColor.White);
            otherssize.AddAtLocation(0, 7);
            expected.Add(otherssize);

            List<IPiece> actual = gameboard.pieces.FindAll(e => e.Type != ChessPiece.Pawn && e.Color == ChessColor.White);

            // both arrays are same length
            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].ColID, actual[i].ColID);
                Assert.AreEqual(expected[i].RowID, actual[i].RowID);
            }
        }

        [TestMethod]
        public void StartingBlackOthersTest()
        {
            GameBoard gameboard = new GameBoard(8, 8);

            List<IPiece> expected = new List<IPiece>();

            IPiece others1 = new Rook(ChessColor.Black);
            others1.AddAtLocation(7, 0);
            expected.Add(others1);

            IPiece others2 = new Knight(ChessColor.Black);
            others2.AddAtLocation(7, 1);
            expected.Add(others2);

            IPiece others3 = new Bishop(ChessColor.Black);
            others3.AddAtLocation(7, 2);
            expected.Add(others3);

            IPiece others4 = new King(ChessColor.Black);
            others4.AddAtLocation(7, 3);
            expected.Add(others4);

            IPiece others5 = new Queen(ChessColor.Black);
            others5.AddAtLocation(7, 4);
            expected.Add(others5);

            IPiece others6 = new Bishop(ChessColor.Black);
            others6.AddAtLocation(7, 5);
            expected.Add(others6);

            IPiece others7 = new Knight(ChessColor.Black);
            others7.AddAtLocation(7, 6);
            expected.Add(others7);

            IPiece otherssize = new Rook(ChessColor.Black);
            otherssize.AddAtLocation(7, 7);
            expected.Add(otherssize);

            List<IPiece> actual = gameboard.pieces.FindAll(e => e.Type != ChessPiece.Pawn && e.Color == ChessColor.Black);

            // both arrays are same length
            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].ColID, actual[i].ColID);
                Assert.AreEqual(expected[i].RowID, actual[i].RowID);
            }
        }

        [TestMethod]
        public void StartingSquaresTest()
        {
            GameBoard gameboard = new GameBoard(8, 8);

            Square[,] expected = new Square[8, 8];

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    expected[row, col] = new Square(row, col);
                }
            }
            expected[0, 0].HasPiece = true; expected[0, 0].piece = new Rook(ChessColor.White);
            expected[0, 1].HasPiece = true; expected[0, 1].piece = new Knight(ChessColor.White);
            expected[0, 2].HasPiece = true; expected[0, 2].piece = new Bishop(ChessColor.White);
            expected[0, 4].HasPiece = true; expected[0, 3].piece = new Queen(ChessColor.White);
            expected[0, 3].HasPiece = true; expected[0, 4].piece = new King(ChessColor.White);
            expected[0, 5].HasPiece = true; expected[0, 5].piece = new Bishop(ChessColor.White);
            expected[0, 6].HasPiece = true; expected[0, 6].piece = new Knight(ChessColor.White);
            expected[0, 7].HasPiece = true; expected[0, 7].piece = new Rook(ChessColor.White);
            expected[1, 0].HasPiece = true; expected[1, 0].piece = new Pawn(ChessColor.White);
            expected[1, 1].HasPiece = true; expected[1, 1].piece = new Pawn(ChessColor.White);
            expected[1, 2].HasPiece = true; expected[1, 2].piece = new Pawn(ChessColor.White);
            expected[1, 3].HasPiece = true; expected[1, 3].piece = new Pawn(ChessColor.White);
            expected[1, 4].HasPiece = true; expected[1, 4].piece = new Pawn(ChessColor.White);
            expected[1, 5].HasPiece = true; expected[1, 5].piece = new Pawn(ChessColor.White);
            expected[1, 6].HasPiece = true; expected[1, 6].piece = new Pawn(ChessColor.White);
            expected[1, 7].HasPiece = true; expected[1, 7].piece = new Pawn(ChessColor.White);

            expected[7, 0].HasPiece = true; expected[7, 0].piece = new Rook(ChessColor.Black);
            expected[7, 1].HasPiece = true; expected[7, 1].piece = new Knight(ChessColor.Black);
            expected[7, 2].HasPiece = true; expected[7, 2].piece = new Bishop(ChessColor.Black);
            expected[7, 4].HasPiece = true; expected[7, 3].piece = new Queen(ChessColor.Black);
            expected[7, 3].HasPiece = true; expected[7, 4].piece = new King(ChessColor.Black);
            expected[7, 5].HasPiece = true; expected[7, 5].piece = new Bishop(ChessColor.Black);
            expected[7, 6].HasPiece = true; expected[7, 6].piece = new Knight(ChessColor.Black);
            expected[7, 7].HasPiece = true; expected[7, 7].piece = new Rook(ChessColor.Black);
            expected[6, 0].HasPiece = true; expected[6, 0].piece = new Pawn(ChessColor.Black);
            expected[6, 1].HasPiece = true; expected[6, 1].piece = new Pawn(ChessColor.Black);
            expected[6, 2].HasPiece = true; expected[6, 2].piece = new Pawn(ChessColor.Black);
            expected[6, 3].HasPiece = true; expected[6, 3].piece = new Pawn(ChessColor.Black);
            expected[6, 4].HasPiece = true; expected[6, 4].piece = new Pawn(ChessColor.Black);
            expected[6, 5].HasPiece = true; expected[6, 5].piece = new Pawn(ChessColor.Black);
            expected[6, 6].HasPiece = true; expected[6, 6].piece = new Pawn(ChessColor.Black);
            expected[6, 7].HasPiece = true; expected[6, 7].piece = new Pawn(ChessColor.Black);

            // both arrays are same length
            Assert.AreEqual(expected.Length, gameboard.squares.Length);

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (expected[i, j].piece != null && gameboard.squares[i, j].piece != null)
                    {
                        Assert.AreEqual(expected[i, j].piece.Type, gameboard.squares[i, j].piece.Type);
                        Assert.AreEqual(expected[i, j].HasPiece, gameboard.squares[i, j].HasPiece);
                    }
                    else
                    {
                        Assert.IsNull(expected[i, j].piece);
                        Assert.IsNull(gameboard.squares[i, j].piece);
                    }
                }
            }
        }

        [TestMethod]
        public void PlacePieceTest()
        {
            GameBoard gameboard = new GameBoard(8, 8);

            IPiece piece = new Pawn(ChessColor.Black)
            {
                ColID = 3,
                RowID = 3
            };

            gameboard.PlacePiece(piece, gameboard.squares[3, 4]);

            Assert.AreEqual(piece.ColID, gameboard.squares[3, 4].ColID);
            Assert.AreEqual(piece.RowID, gameboard.squares[3, 4].RowID);
            Assert.AreEqual(gameboard.pieces[32].ColID, gameboard.squares[3, 4].ColID);
            Assert.AreEqual(gameboard.pieces[32].RowID, gameboard.squares[3, 4].RowID);
            Assert.AreEqual(true, gameboard.squares[3, 4].HasPiece);
        }

        [TestMethod]
        public void RemovePieceTest()
        {
            GameBoard gameboard = new GameBoard(8, 8);

            IPiece piece = new Pawn(ChessColor.Black)
            {
                ColID = 3,
                RowID = 3
            };
            

            gameboard.PlacePiece(piece, gameboard.squares[3, 4]);
            gameboard.RemovePiece(gameboard.squares[3, 4]);

            Assert.IsNull(gameboard.squares[3, 4].piece);
            Assert.AreEqual(32, gameboard.pieces.Count);
            Assert.AreEqual(false, gameboard.squares[3, 4].HasPiece);
        }

        [TestMethod]
        public void MovePieceTest()
        {
            GameBoard gameboard = new GameBoard(8, 8);

            gameboard.MovePiece(gameboard.squares[1, 0], gameboard.squares[2, 0]);

            Assert.IsNull(gameboard.squares[1, 0].piece);
            Assert.AreEqual(false, gameboard.squares[1, 0].HasPiece);
            Assert.AreEqual(true, gameboard.squares[2, 0].HasPiece);
            Assert.AreEqual(0, gameboard.squares[2, 0].ColID);
            Assert.AreEqual(2, gameboard.squares[2, 0].RowID);
            Assert.AreEqual(gameboard.pieces[8].ColID, gameboard.squares[2, 0].ColID);
            Assert.AreEqual(gameboard.pieces[8].RowID, gameboard.squares[2, 0].RowID);
        }

        [TestMethod]
        public void ClearBoardTest()
        {
            GameBoard gameboard = new GameBoard(8, 8);
            gameboard.ClearBoard();

            Assert.AreEqual(0, gameboard.pieces.Count);

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Assert.IsNull(gameboard.squares[row, col].piece);
                }
            }
        }

    }
}
