﻿using ChessModel;
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

            var actual = gameLogic.HasGameEnded(State.Check);
            Assert.AreEqual(false, actual);

            actual = gameLogic.HasGameEnded(State.CheckMate);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void MovePieceTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            gb.InitializeBoard();
            GameLogic gameLogic = new GameLogic(gb);           

            gameLogic.MovePiece(gameLogic.gameBoard.squares[1, 0], gameLogic.gameBoard.squares[2, 0]);
            Assert.AreEqual(ChessPiece.Pawn, gb.squares[2, 0].piece.Type);

            gb.MovePiece(gb.squares[6, 0], gb.squares[3, 1]);
            gameLogic.MovePiece(gameLogic.gameBoard.squares[2, 0], gameLogic.gameBoard.squares[3, 1]);
            Assert.AreEqual(ChessPiece.Pawn, gameLogic.gameBoard.squares[3, 1].piece.Type);
            Assert.AreEqual(ChessColor.White, gameLogic.gameBoard.squares[3, 1].piece.Color);

            gameLogic.MovePiece(gameLogic.gameBoard.squares[1, 1], gameLogic.gameBoard.squares[4, 1]);
            Assert.IsNull(gameLogic.gameBoard.squares[4, 1].piece);
        }

        [TestMethod]
        public void CaptureTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            GameLogic gameLogic = new GameLogic(gb);

            gameLogic.Capture(gameLogic.gameBoard.squares[0, 0], gameLogic.gameBoard.squares[7, 0]);

            Assert.AreEqual(ChessColor.White, gb.squares[7, 0].piece.Color);
        }

        [TestMethod]
        public void WhiteEnPassantTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            GameLogic gameLogic = new GameLogic(gb);

            gb.MovePiece(gb.squares[6, 1], gb.squares[3, 1]);
            gameLogic.MovePiece(gb.squares[1, 0], gb.squares[3, 0]);
            gameLogic.MovePiece(gb.squares[3, 1], gb.squares[2, 0]);

            Assert.AreEqual(ChessColor.Black, gb.squares[2, 0].piece.Color);
            Assert.IsNull(gb.squares[3, 0].piece);

            gb.MovePiece(gb.squares[6, 2], gb.squares[3, 2]);
            gameLogic.MovePiece(gb.squares[1, 1], gb.squares[3, 1]);
            gameLogic.MovePiece(gb.squares[7, 0], gb.squares[6, 0]);
            gameLogic.MovePiece(gb.squares[0, 0], gb.squares[1, 0]);
            gameLogic.MovePiece(gb.squares[3, 2], gb.squares[2, 1]);

            Assert.AreEqual(ChessColor.Black, gb.squares[3, 2].piece.Color);
            Assert.IsNull(gb.squares[2, 1].piece);       
        }

        [TestMethod]
        public void WhiteFakeEnPassantTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            GameLogic gameLogic = new GameLogic(gb);

            gb.MovePiece(gb.squares[6, 1], gb.squares[3, 1]);
            gb.MovePiece(gb.squares[1, 6], gb.squares[4, 6]);
            gameLogic.MovePiece(gb.squares[1, 0], gb.squares[3, 0]);
            gameLogic.MovePiece(gb.squares[6, 0], gb.squares[4, 0]);
            gameLogic.MovePiece(gb.squares[0, 0], gb.squares[1, 0]);
            gameLogic.MovePiece(gb.squares[3, 1], gb.squares[2, 0]);

            Assert.AreEqual(ChessColor.Black, gb.squares[3, 1].piece.Color);
            Assert.IsNull(gb.squares[2, 0].piece);
            Assert.AreEqual(ChessColor.White, gb.squares[3, 0].piece.Color);
        }

        [TestMethod]
        public void WhiteFakeEnPassantOtherSideTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            GameLogic gameLogic = new GameLogic(gb);

            gb.MovePiece(gb.squares[6, 1], gb.squares[3, 5]);
            gb.MovePiece(gb.squares[1, 6], gb.squares[3, 6]);
            gameLogic.MovePiece(gb.squares[1, 0], gb.squares[3, 0]);
            gameLogic.MovePiece(gb.squares[3, 5], gb.squares[2, 6]);     

            Assert.AreEqual(ChessColor.Black, gb.squares[3, 5].piece.Color);
            Assert.IsNull(gb.squares[2, 6].piece);
            Assert.AreEqual(ChessColor.White, gb.squares[3, 6].piece.Color);
            Assert.AreEqual(ChessColor.White, gb.squares[3, 0].piece.Color);
        }

        [TestMethod]
        public void BlackEnPassantTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            GameLogic gameLogic = new GameLogic(gb);

            gb.MovePiece(gb.squares[1, 1], gb.squares[4, 1]);
            gameLogic.MovePiece(gb.squares[6, 0], gb.squares[4, 0]);
            gameLogic.MovePiece(gb.squares[4, 1], gb.squares[5, 0]);

            Assert.AreEqual(ChessColor.White, gb.squares[5, 0].piece.Color);
            Assert.IsNull(gb.squares[4, 0].piece);

            gb.MovePiece(gb.squares[1, 2], gb.squares[4, 2]);
            gameLogic.MovePiece(gb.squares[6, 1], gb.squares[4, 1]);
            gameLogic.MovePiece(gb.squares[0, 0], gb.squares[1, 0]);
            gameLogic.MovePiece(gb.squares[7, 0], gb.squares[6, 0]);
            gameLogic.MovePiece(gb.squares[4, 2], gb.squares[5, 1]);

            Assert.AreEqual(ChessColor.White, gb.squares[4, 2].piece.Color);
            Assert.IsNull(gb.squares[5, 1].piece);
        }

        [TestMethod]
        public void BlackFakeEnPassantTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            GameLogic gameLogic = new GameLogic(gb);

            gb.MovePiece(gb.squares[6, 1], gb.squares[3, 1]);
            gb.MovePiece(gb.squares[1, 6], gb.squares[4, 6]);
            gameLogic.MovePiece(gb.squares[6, 7], gb.squares[4, 7]);
            gameLogic.MovePiece(gb.squares[1, 0], gb.squares[3, 0]);
            gameLogic.MovePiece(gb.squares[7, 0], gb.squares[6, 0]);
            gameLogic.MovePiece(gb.squares[4, 6], gb.squares[5, 7]);

            Assert.AreEqual(ChessColor.White, gb.squares[4, 6].piece.Color);
            Assert.IsNull(gb.squares[5, 7].piece);
            Assert.AreEqual(ChessColor.Black, gb.squares[4, 7].piece.Color);
        }

        [TestMethod]
        public void BlackFakeEnPassantOtherSideTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            GameLogic gameLogic = new GameLogic(gb);

            gb.MovePiece(gb.squares[1, 1], gb.squares[4, 5]);
            gb.MovePiece(gb.squares[6, 6], gb.squares[4, 6]);
            gameLogic.MovePiece(gb.squares[6, 0], gb.squares[4, 0]);
            gameLogic.MovePiece(gb.squares[4, 5], gb.squares[5, 6]);

            Assert.AreEqual(ChessColor.White, gb.squares[4, 5].piece.Color);
            Assert.IsNull(gb.squares[5, 6].piece);
            Assert.AreEqual(ChessColor.Black, gb.squares[4, 6].piece.Color);
            Assert.AreEqual(ChessColor.Black, gb.squares[4, 0].piece.Color);
        }
    }
}
