using ChessModel;
using ChessModel.Pieces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests.ModelTests
{
    [TestClass]
    public class GameStateTest
    {
        [TestMethod]
        public void CheckTest()
        {
            GameBoard gameboard = new GameBoard(8, 8);
            GameLogic gl = new GameLogic(gameboard);
            GameState gs = new GameState(gl);

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

        [TestMethod]
        public void CheckMateDefaultTest()
        {
            GameBoard gameboard = new GameBoard(8, 8);
            GameLogic gl = new GameLogic(gameboard);
            GameState gs = new GameState(gl);

            Assert.AreEqual(false, gs.CheckMate(gameboard, ChessColor.White));
            Assert.AreEqual(false, gs.CheckMate(gameboard, ChessColor.Black));
        }

        [TestMethod]
        public void CheckMateValidTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            GameLogic gl = new GameLogic(gb);
            GameState gs = new GameState(gl);

            gb.ClearBoard();
            IPiece whiteKing = new King(ChessColor.White);
            IPiece whiteQueen = new Queen(ChessColor.White);
            IPiece blackKing = new King(ChessColor.Black);

            gb.PlacePiece(whiteKing, gb.squares[5, 3]);
            gb.PlacePiece(whiteQueen, gb.squares[6, 3]);
            gb.PlacePiece(blackKing, gb.squares[7, 3]);

            Assert.AreEqual(true, gs.CheckMate(gb, ChessColor.Black));
        }

        [TestMethod]
        public void CheckMateFailTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            GameLogic gl = new GameLogic(gb);
            GameState gs = new GameState(gl);

            gb.MovePiece(gb.squares[7, 6], gb.squares[2, 5]);
            Assert.AreEqual(true, gs.Check(gb, ChessColor.White, true));
            Assert.AreEqual(false, gs.CheckMate(gb, ChessColor.White));

            gb.RemovePiece(gb.squares[1, 4]);
            gb.RemovePiece(gb.squares[1, 6]);
            gb.RemovePiece(gb.squares[0, 6]);
            //Assert.AreEqual(true, gs.Check(gb, ChessColor.White, true));
            //Assert.AreEqual(false, gs.CheckMate(gb, ChessColor.White));
        }

        [TestMethod]
        public void FoolsMateTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            GameLogic gl = new GameLogic(gb);
            GameState gs = new GameState(gl);

            gb.MovePiece(gb.squares[1, 5], gb.squares[2, 5]);
            gb.MovePiece(gb.squares[6, 4], gb.squares[4, 4]);
            gb.MovePiece(gb.squares[1, 6], gb.squares[3, 6]);
            gb.MovePiece(gb.squares[7, 3], gb.squares[3, 7]);
            Assert.AreEqual(true, gs.CheckMate(gb, ChessColor.White));
            Assert.AreEqual(false, gs.InPlay(gb, ChessColor.White));
        }

        [TestMethod]
        public void StaleMateDefaultTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            GameLogic gl = new GameLogic(gb);
            GameState gs = new GameState(gl);

            Assert.AreEqual(false, gs.StaleMate(gb, ChessColor.White));
            Assert.AreEqual(false, gs.StaleMate(gb, ChessColor.Black));
        }

        [TestMethod]
        public void StaleMateValidTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            GameLogic gl = new GameLogic(gb);
            GameState gs = new GameState(gl);

            gb.ClearBoard();
            IPiece whiteKing = new King(ChessColor.White);
            IPiece whiteQueen = new Queen(ChessColor.White);
            IPiece blackKing = new King(ChessColor.Black);

            gb.PlacePiece(whiteKing, gb.squares[6, 5]);
            gb.PlacePiece(whiteQueen, gb.squares[5, 6]);
            gb.PlacePiece(blackKing, gb.squares[7, 7]);

            Assert.AreEqual(true, gs.StaleMate(gb, ChessColor.Black));
        }

        [TestMethod]
        public void StaleMateFailTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            GameLogic gl = new GameLogic(gb);
            GameState gs = new GameState(gl);

            gb.ClearBoard();
            IPiece whiteKing = new King(ChessColor.White);
            IPiece whiteQueen = new Queen(ChessColor.White);
            IPiece blackKing = new King(ChessColor.Black);

            gb.PlacePiece(whiteKing, gb.squares[6, 5]);
            gb.PlacePiece(whiteQueen, gb.squares[5, 6]);
            gb.PlacePiece(blackKing, gb.squares[7, 4]);

            Assert.AreEqual(false, gs.StaleMate(gb, ChessColor.Black));
        }

        [TestMethod]
        public void StaleMateSamePositionTest()
        {
            Game game = new Game();
            game.NewGame();
            //Game game2 = new Game();
            //game2.NewGame();


            game.gb.MovePiece(game.gb.squares[1, 4], game.gb.squares[4, 4]);
            game.Player2.Move(game.gl, game.gl.gameBoard.squares[6, 5], game.gl.gameBoard.squares[4, 5]);
            //game2.gb.MovePiece(game2.gb.squares[1, 4], game2.gb.squares[4, 4]);
            //game2.gb.MovePiece(game2.gb.squares[6, 5], game2.gb.squares[4, 5]);
            var stalemate = game.gl.gs.StaleMate(game.gb, ChessColor.White);

            //for(var row = 0; row < 32; row++)
            //{
            //    Assert.AreEqual(game2.gb.pieces[row].Type, game.gb.pieces[row].Type);
            //    Assert.AreEqual(game2.gb.pieces[row].RowID, game.gb.pieces[row].RowID);
            //    Assert.AreEqual(game2.gb.pieces[row].Color, game.gb.pieces[row].Color);
            //    Assert.AreEqual(game2.gb.pieces[row].ColID, game.gb.pieces[row].ColID);
            //}
            game.Player1.Move(game.gl, game.gl.gameBoard.squares[4, 4], game.gl.gameBoard.squares[5, 5]);
            Assert.AreEqual(game.gb.squares[5, 5].Piece.Type, ChessPiece.Pawn);
            Assert.AreEqual(game.gb.squares[5, 5].Piece.Color, ChessColor.White);
        }
    }
}
