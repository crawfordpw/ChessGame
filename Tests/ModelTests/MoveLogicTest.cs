using ChessModel;
using ChessModel.Pieces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ModelTests
{
    [TestClass]
    public class MoveLogicTest
    {
        [TestMethod]
        public void MovePieceTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            gb.InitializeBoard();
            MoveLogic ml = new MoveLogic(gb);           

            ml.MovePiece(ml.gb.squares[1, 0], ml.gb.squares[2, 0]);
            Assert.AreEqual(ChessPiece.Pawn, gb.squares[2, 0].Piece.Type);

            gb.MovePiece(gb.squares[6, 0], gb.squares[3, 1]);
            ml.MovePiece(ml.gb.squares[2, 0], ml.gb.squares[3, 1]);
            Assert.AreEqual(ChessPiece.Pawn, ml.gb.squares[3, 1].Piece.Type);
            Assert.AreEqual(ChessColor.White, ml.gb.squares[3, 1].Piece.Color);

            ml.MovePiece(ml.gb.squares[1, 1], ml.gb.squares[4, 1]);
            Assert.IsNull(ml.gb.squares[4, 1].Piece);
        }

        [TestMethod]
        public void CaptureTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic ml = new MoveLogic(gb);

            ml.Capture(ml.gb.squares[0, 0], ml.gb.squares[7, 0]);

            Assert.AreEqual(ChessColor.White, gb.squares[7, 0].Piece.Color);
        }

        [TestMethod]
        public void WhiteEnPassantTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic ml = new MoveLogic(gb);

            gb.MovePiece(gb.squares[6, 1], gb.squares[3, 1]);
            ml.MovePiece(gb.squares[1, 0], gb.squares[3, 0]);
            ml.MovePiece(gb.squares[3, 1], gb.squares[2, 0]);

            Assert.AreEqual(ChessColor.Black, gb.squares[2, 0].Piece.Color);
            Assert.IsNull(gb.squares[3, 0].Piece);

            gb.MovePiece(gb.squares[6, 2], gb.squares[3, 2]);
            ml.MovePiece(gb.squares[1, 1], gb.squares[3, 1]);
            ml.MovePiece(gb.squares[7, 0], gb.squares[6, 0]);
            ml.MovePiece(gb.squares[0, 0], gb.squares[1, 0]);
            ml.MovePiece(gb.squares[3, 2], gb.squares[2, 1]);

            Assert.AreEqual(ChessColor.Black, gb.squares[3, 2].Piece.Color);
            Assert.IsNull(gb.squares[2, 1].Piece);       
        }

        [TestMethod]
        public void WhiteFakeEnPassantTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic ml = new MoveLogic(gb);

            gb.MovePiece(gb.squares[6, 1], gb.squares[3, 1]);
            gb.MovePiece(gb.squares[1, 6], gb.squares[4, 6]);
            ml.MovePiece(gb.squares[1, 0], gb.squares[3, 0]);
            ml.MovePiece(gb.squares[6, 0], gb.squares[4, 0]);
            ml.MovePiece(gb.squares[0, 0], gb.squares[1, 0]);
            ml.MovePiece(gb.squares[3, 1], gb.squares[2, 0]);

            Assert.AreEqual(ChessColor.Black, gb.squares[3, 1].Piece.Color);
            Assert.IsNull(gb.squares[2, 0].Piece);
            Assert.AreEqual(ChessColor.White, gb.squares[3, 0].Piece.Color);
        }

        [TestMethod]
        public void WhiteFakeEnPassantOtherSideTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic ml = new MoveLogic(gb);

            gb.MovePiece(gb.squares[6, 1], gb.squares[3, 5]);
            gb.MovePiece(gb.squares[1, 6], gb.squares[3, 6]);
            ml.MovePiece(gb.squares[1, 0], gb.squares[3, 0]);
            ml.MovePiece(gb.squares[3, 5], gb.squares[2, 6]);     

            Assert.AreEqual(ChessColor.Black, gb.squares[3, 5].Piece.Color);
            Assert.IsNull(gb.squares[2, 6].Piece);
            Assert.AreEqual(ChessColor.White, gb.squares[3, 6].Piece.Color);
            Assert.AreEqual(ChessColor.White, gb.squares[3, 0].Piece.Color);
        }

        [TestMethod]
        public void BlackEnPassantTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic ml = new MoveLogic(gb);

            gb.MovePiece(gb.squares[1, 1], gb.squares[4, 1]);
            ml.MovePiece(gb.squares[6, 0], gb.squares[4, 0]);
            ml.MovePiece(gb.squares[4, 1], gb.squares[5, 0]);

            Assert.AreEqual(ChessColor.White, gb.squares[5, 0].Piece.Color);
            Assert.IsNull(gb.squares[4, 0].Piece);

            gb.MovePiece(gb.squares[1, 2], gb.squares[4, 2]);
            ml.MovePiece(gb.squares[6, 1], gb.squares[4, 1]);
            ml.MovePiece(gb.squares[0, 0], gb.squares[1, 0]);
            ml.MovePiece(gb.squares[7, 0], gb.squares[6, 0]);
            ml.MovePiece(gb.squares[4, 2], gb.squares[5, 1]);

            Assert.AreEqual(ChessColor.White, gb.squares[4, 2].Piece.Color);
            Assert.IsNull(gb.squares[5, 1].Piece);
        }

        [TestMethod]
        public void BlackFakeEnPassantTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic ml = new MoveLogic(gb);

            gb.MovePiece(gb.squares[6, 1], gb.squares[3, 1]);
            gb.MovePiece(gb.squares[1, 6], gb.squares[4, 6]);
            ml.MovePiece(gb.squares[6, 7], gb.squares[4, 7]);
            ml.MovePiece(gb.squares[1, 0], gb.squares[3, 0]);
            ml.MovePiece(gb.squares[7, 0], gb.squares[6, 0]);
            ml.MovePiece(gb.squares[4, 6], gb.squares[5, 7]);

            Assert.AreEqual(ChessColor.White, gb.squares[4, 6].Piece.Color);
            Assert.IsNull(gb.squares[5, 7].Piece);
            Assert.AreEqual(ChessColor.Black, gb.squares[4, 7].Piece.Color);
        }

        [TestMethod]
        public void BlackFakeEnPassantOtherSideTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic ml = new MoveLogic(gb);

            gb.MovePiece(gb.squares[1, 1], gb.squares[4, 5]);
            gb.MovePiece(gb.squares[6, 6], gb.squares[4, 6]);
            ml.MovePiece(gb.squares[6, 0], gb.squares[4, 0]);
            ml.MovePiece(gb.squares[4, 5], gb.squares[5, 6]);

            Assert.AreEqual(ChessColor.White, gb.squares[4, 5].Piece.Color);
            Assert.IsNull(gb.squares[5, 6].Piece);
            Assert.AreEqual(ChessColor.Black, gb.squares[4, 6].Piece.Color);
            Assert.AreEqual(ChessColor.Black, gb.squares[4, 0].Piece.Color);
        }

        [TestMethod]
        public void WhiteCastleLeftTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic ml = new MoveLogic(gb);

            ml.MovePiece(gb.squares[0, 4], gb.squares[0, 2]);
            Assert.AreEqual(ChessPiece.King, gb.squares[0, 4].Piece.Type);
            Assert.AreEqual(ChessPiece.Bishop, gb.squares[0, 2].Piece.Type);

            gb.RemovePiece(gb.squares[0, 2]);
            ml.MovePiece(gb.squares[0, 4], gb.squares[0, 2]);
            Assert.AreEqual(ChessPiece.King, gb.squares[0, 4].Piece.Type);
            Assert.IsNull(gb.squares[0, 2].Piece);

            gb.RemovePiece(gb.squares[0, 1]);
            ml.MovePiece(gb.squares[0, 4], gb.squares[0, 2]);
            Assert.AreEqual(ChessPiece.King, gb.squares[0, 4].Piece.Type);
            Assert.IsNull(gb.squares[0, 2].Piece);

            gb.RemovePiece(gb.squares[0, 3]);
            ml.MovePiece(gb.squares[0, 4], gb.squares[0, 2]);
            Assert.IsNull(gb.squares[0, 4].Piece);
            Assert.AreEqual(ChessPiece.King, gb.squares[0, 2].Piece.Type);
            Assert.AreEqual(ChessPiece.Rook, gb.squares[0, 3].Piece.Type);
        }

        [TestMethod]
        public void WhiteCastleRightTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic ml = new MoveLogic(gb);

            ml.MovePiece(gb.squares[0, 4], gb.squares[0, 6]);
            Assert.AreEqual(ChessPiece.King, gb.squares[0, 4].Piece.Type);
            Assert.AreEqual(ChessPiece.Knight, gb.squares[0, 6].Piece.Type);

            gb.RemovePiece(gb.squares[0, 6]);
            ml.MovePiece(gb.squares[0, 4], gb.squares[0, 6]);
            Assert.AreEqual(ChessPiece.King, gb.squares[0, 4].Piece.Type);
            Assert.IsNull(gb.squares[0, 6].Piece);

            gb.RemovePiece(gb.squares[0, 5]);
            ml.MovePiece(gb.squares[0, 4], gb.squares[0, 6]);
            Assert.IsNull(gb.squares[0, 4].Piece);
            Assert.AreEqual(ChessPiece.King, gb.squares[0, 6].Piece.Type);
            Assert.AreEqual(ChessPiece.Rook, gb.squares[0, 5].Piece.Type);
        }

        [TestMethod]
        public void BlackCastleLeftTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic ml = new MoveLogic(gb);

            ml.MovePiece(gb.squares[7, 4], gb.squares[7, 2]);
            Assert.AreEqual(ChessPiece.King, gb.squares[7, 4].Piece.Type);
            Assert.AreEqual(ChessPiece.Bishop, gb.squares[7, 2].Piece.Type);

            gb.RemovePiece(gb.squares[7, 2]);
            ml.MovePiece(gb.squares[7, 4], gb.squares[7, 2]);
            Assert.AreEqual(ChessPiece.King, gb.squares[0, 4].Piece.Type);
            Assert.IsNull(gb.squares[7, 2].Piece);

            gb.RemovePiece(gb.squares[7, 1]);
            ml.MovePiece(gb.squares[7, 4], gb.squares[7, 2]);
            Assert.AreEqual(ChessPiece.King, gb.squares[7, 4].Piece.Type);
            Assert.IsNull(gb.squares[7, 2].Piece);

            gb.RemovePiece(gb.squares[7, 3]);
            ml.MovePiece(gb.squares[7, 4], gb.squares[7, 2]);
            Assert.IsNull(gb.squares[7, 4].Piece);
            Assert.AreEqual(ChessPiece.King, gb.squares[7, 2].Piece.Type);
            Assert.AreEqual(ChessPiece.Rook, gb.squares[7, 3].Piece.Type);
        }

        [TestMethod]
        public void BlackCastleRightTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic ml = new MoveLogic(gb);

            ml.MovePiece(gb.squares[7, 4], gb.squares[7, 6]);
            Assert.AreEqual(ChessPiece.King, gb.squares[7, 4].Piece.Type);
            Assert.AreEqual(ChessPiece.Knight, gb.squares[7, 6].Piece.Type);

            gb.RemovePiece(gb.squares[7, 6]);
            ml.MovePiece(gb.squares[7, 4], gb.squares[7, 6]);
            Assert.AreEqual(ChessPiece.King, gb.squares[7, 4].Piece.Type);
            Assert.IsNull(gb.squares[7, 6].Piece);

            gb.RemovePiece(gb.squares[7, 5]);
            ml.MovePiece(gb.squares[7, 4], gb.squares[7, 6]);
            Assert.IsNull(gb.squares[7, 4].Piece);
            Assert.AreEqual(ChessPiece.King, gb.squares[7, 6].Piece.Type);
            Assert.AreEqual(ChessPiece.Rook, gb.squares[7, 5].Piece.Type);
        }

        [TestMethod]
        public void CastleFailTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic ml = new MoveLogic(gb);

            gb.RemovePiece(gb.squares[0, 1]);
            gb.RemovePiece(gb.squares[0, 2]);
            gb.RemovePiece(gb.squares[0, 3]);
            gb.RemovePiece(gb.squares[0, 5]);
            gb.RemovePiece(gb.squares[0, 6]);
            gb.RemovePiece(gb.squares[7, 1]);
            gb.RemovePiece(gb.squares[7, 2]);
            gb.RemovePiece(gb.squares[7, 3]);
            gb.RemovePiece(gb.squares[7, 5]);
            gb.RemovePiece(gb.squares[7, 6]);

            ml.MovePiece(gb.squares[7, 0], gb.squares[7, 1]);
            ml.MovePiece(gb.squares[7, 1], gb.squares[7, 0]);
            ml.MovePiece(gb.squares[7, 4], gb.squares[7, 2]);
            Assert.IsNull(gb.squares[7, 2].Piece);
            Assert.AreEqual(ChessPiece.King, gb.squares[7, 4].Piece.Type);
            Assert.AreEqual(ChessPiece.Rook, gb.squares[7, 0].Piece.Type);

            ml.MovePiece(gb.squares[7, 4], gb.squares[7, 5]);
            ml.MovePiece(gb.squares[7, 5], gb.squares[7, 4]);
            ml.MovePiece(gb.squares[7, 4], gb.squares[7, 2]);
            Assert.IsNull(gb.squares[7, 2].Piece);
            Assert.AreEqual(ChessPiece.King, gb.squares[7, 4].Piece.Type);
            Assert.AreEqual(ChessPiece.Rook, gb.squares[7, 0].Piece.Type);

            ml.MovePiece(gb.squares[0, 4], gb.squares[7, 2]);
            Assert.IsNull(gb.squares[7, 2].Piece);
            Assert.AreEqual(ChessPiece.King, gb.squares[0, 4].Piece.Type);
            Assert.AreEqual(ChessPiece.Rook, gb.squares[7, 0].Piece.Type);
            Assert.AreEqual(ChessPiece.Rook, gb.squares[0, 0].Piece.Type);

            ml.MovePiece(gb.squares[0, 4], gb.squares[0, 2]);
            Assert.IsNull(gb.squares[0, 4].Piece);
            Assert.AreEqual(ChessPiece.King, gb.squares[0, 2].Piece.Type);
            Assert.AreEqual(ChessPiece.Rook, gb.squares[0, 3].Piece.Type);
        }

        [TestMethod]
        public void UndoNormalMoveTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic ml = new MoveLogic(gb);

            ml.MovePiece(gb.squares[1, 0], gb.squares[2, 0]);    
            Assert.AreEqual(ChessPiece.Pawn, gb.squares[2, 0].Piece.Type);
            Assert.IsNull(gb.squares[1, 0].Piece);
            Assert.AreEqual(1, gb.squares[2, 0].Piece.MoveCount);

            ml.Undo();
            Assert.AreEqual(ChessPiece.Pawn, gb.squares[1, 0].Piece.Type);
            Assert.IsNull(gb.squares[2, 0].Piece);
            Assert.AreEqual(0, gb.squares[1, 0].Piece.MoveCount);
        }

        [TestMethod]
        public void UndoCaptureTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic ml = new MoveLogic(gb);

            gb.MovePiece(gb.squares[6, 0], gb.squares[2, 1]);
            ml.MovePiece(gb.squares[1, 0], gb.squares[2, 1], true);
            Assert.AreEqual(ChessPiece.Pawn, gb.squares[2, 1].Piece.Type);
            Assert.AreEqual(ChessColor.White, gb.squares[2, 1].Piece.Color);
            Assert.IsNull(gb.squares[1, 0].Piece);
            Assert.AreEqual(1, gb.squares[2, 1].Piece.MoveCount);

            ml.Undo();
            Assert.AreEqual(ChessPiece.Pawn, gb.squares[1, 0].Piece.Type);
            Assert.AreEqual(ChessColor.White, gb.squares[1, 0].Piece.Color);
            Assert.AreEqual(ChessPiece.Pawn, gb.squares[2, 1].Piece.Type);
            Assert.AreEqual(ChessColor.Black, gb.squares[2, 1].Piece.Color);
            Assert.AreEqual(0, gb.squares[1, 0].Piece.MoveCount);
            Assert.AreEqual(1, gb.squares[2, 1].Piece.MoveCount);
        }

        [TestMethod]
        public void UndoEnPassantTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic ml = new MoveLogic(gb);

            gb.MovePiece(gb.squares[6, 1], gb.squares[3, 1]);
            ml.MovePiece(gb.squares[1, 0], gb.squares[3, 0]);
            ml.MovePiece(gb.squares[3, 1], gb.squares[2, 0]);
            Assert.AreEqual(ChessPiece.Pawn, gb.squares[2, 0].Piece.Type);
            Assert.AreEqual(ChessColor.Black, gb.squares[2, 0].Piece.Color);
            Assert.IsNull(gb.squares[3, 0].Piece);
            Assert.IsNull(gb.squares[3, 1].Piece);
            Assert.AreEqual(2, gb.squares[2, 0].Piece.MoveCount);

            ml.Undo();
            Assert.AreEqual(ChessPiece.Pawn, gb.squares[3, 1].Piece.Type);
            Assert.AreEqual(ChessColor.Black, gb.squares[3, 1].Piece.Color);
            Assert.AreEqual(ChessPiece.Pawn, gb.squares[3, 0].Piece.Type);
            Assert.AreEqual(ChessColor.White, gb.squares[3, 0].Piece.Color);
            Assert.AreEqual(1, gb.squares[3, 0].Piece.MoveCount);
            Assert.AreEqual(1, gb.squares[3, 1].Piece.MoveCount);
            Assert.IsNull(gb.squares[2, 0].Piece);
        }

        [TestMethod]
        public void UndoCastleTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic ml = new MoveLogic(gb);

            gb.RemovePiece(gb.squares[0, 1]);
            gb.RemovePiece(gb.squares[0, 2]);
            gb.RemovePiece(gb.squares[0, 3]);
            gb.RemovePiece(gb.squares[7, 5]);
            gb.RemovePiece(gb.squares[7, 6]);

            ml.MovePiece(gb.squares[0, 4], gb.squares[0, 2]);
            Assert.AreEqual(ChessPiece.King, gb.squares[0, 2].Piece.Type);
            Assert.AreEqual(ChessColor.White, gb.squares[0, 2].Piece.Color);
            Assert.AreEqual(ChessPiece.Rook, gb.squares[0, 3].Piece.Type);
            Assert.AreEqual(ChessColor.White, gb.squares[0, 3].Piece.Color);
            Assert.IsNull(gb.squares[0, 0].Piece);
            Assert.IsNull(gb.squares[0, 4].Piece);
            Assert.AreEqual(1, gb.squares[0, 2].Piece.MoveCount);
            Assert.AreEqual(1, gb.squares[0, 3].Piece.MoveCount);

            ml.Undo();
            Assert.AreEqual(ChessPiece.King, gb.squares[0, 4].Piece.Type);
            Assert.AreEqual(ChessColor.White, gb.squares[0, 4].Piece.Color);
            Assert.AreEqual(ChessPiece.Rook, gb.squares[0, 0].Piece.Type);
            Assert.AreEqual(ChessColor.White, gb.squares[0, 0].Piece.Color);
            Assert.IsNull(gb.squares[0, 2].Piece);
            Assert.IsNull(gb.squares[0, 3].Piece);
            Assert.AreEqual(0, gb.squares[0, 0].Piece.MoveCount);
            Assert.AreEqual(0, gb.squares[0, 4].Piece.MoveCount);

            ml.MovePiece(gb.squares[7, 4], gb.squares[7, 6]);
            Assert.AreEqual(ChessPiece.King, gb.squares[7, 6].Piece.Type);
            Assert.AreEqual(ChessColor.Black, gb.squares[7, 6].Piece.Color);
            Assert.AreEqual(ChessPiece.Rook, gb.squares[7, 5].Piece.Type);
            Assert.AreEqual(ChessColor.Black, gb.squares[7, 5].Piece.Color);
            Assert.IsNull(gb.squares[7, 4].Piece);
            Assert.IsNull(gb.squares[7, 7].Piece);
            Assert.AreEqual(1, gb.squares[7, 5].Piece.MoveCount);
            Assert.AreEqual(1, gb.squares[7, 6].Piece.MoveCount);

            ml.Undo();
            Assert.AreEqual(ChessPiece.King, gb.squares[7, 4].Piece.Type);
            Assert.AreEqual(ChessColor.Black, gb.squares[7, 4].Piece.Color);
            Assert.AreEqual(ChessPiece.Rook, gb.squares[7, 7].Piece.Type);
            Assert.AreEqual(ChessColor.Black, gb.squares[7, 7].Piece.Color);
            Assert.IsNull(gb.squares[7, 5].Piece);
            Assert.IsNull(gb.squares[7, 6].Piece);
            Assert.AreEqual(0, gb.squares[7, 4].Piece.MoveCount);
            Assert.AreEqual(0, gb.squares[7, 7].Piece.MoveCount);
        }

        [TestMethod]
        public void MoveIntoCheckTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic ml = new MoveLogic(gb);

            gb.MovePiece(gb.squares[0, 4], gb.squares[5, 4]);
            Assert.AreEqual(false, ml.MovePiece(gb.squares[5, 4], gb.squares[5, 5]));
            Assert.AreEqual(ChessPiece.King, gb.squares[5, 4].Piece.Type);
            Assert.AreEqual(ChessColor.White, gb.squares[5, 4].Piece.Color);
            Assert.IsNull(gb.squares[5, 5].Piece);

            gb.MovePiece(gb.squares[5, 4], gb.squares[3, 4]);
            gb.MovePiece(gb.squares[0, 0], gb.squares[4, 4]);
            gb.MovePiece(gb.squares[7, 7], gb.squares[5, 4]);
            Assert.AreEqual(false, ml.MovePiece(gb.squares[4, 4], gb.squares[4, 6]));
            Assert.AreEqual(ChessPiece.King, gb.squares[3, 4].Piece.Type);
            Assert.AreEqual(ChessColor.White, gb.squares[3, 4].Piece.Color);
            Assert.AreEqual(ChessPiece.Rook, gb.squares[4, 4].Piece.Type);
            Assert.AreEqual(ChessColor.White, gb.squares[4, 4].Piece.Color);
            Assert.IsNull(gb.squares[4, 6].Piece);
        }

        [TestMethod]
        public void MoveIntoCheckEnPassantTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic ml = new MoveLogic(gb);
            gb.ClearBoard();

            IPiece whitePawn = new Pawn(ChessColor.White);
            IPiece whiteBishop = new Bishop(ChessColor.White);
            IPiece whiteKing = new King(ChessColor.White);
            IPiece blackPawn = new Pawn(ChessColor.Black);
            IPiece blackKing = new King(ChessColor.Black);

            gb.PlacePiece(whitePawn, gb.squares[1, 3]);
            gb.PlacePiece(whiteBishop, gb.squares[0, 2]);
            gb.PlacePiece(whiteKing, gb.squares[0, 4]);
            gb.PlacePiece(blackPawn, gb.squares[3, 2]);
            gb.PlacePiece(blackKing, gb.squares[5, 7]);

            Assert.AreEqual(true, ml.MovePiece(gb.squares[1, 3], gb.squares[3, 3]));
            Assert.AreEqual(false, ml.MovePiece(gb.squares[3, 2], gb.squares[2, 3]));
            Assert.AreEqual(ChessPiece.Pawn, gb.squares[3, 3].Piece.Type);
            Assert.AreEqual(ChessColor.White, gb.squares[3, 3].Piece.Color);
            Assert.AreEqual(ChessPiece.Pawn, gb.squares[3, 2].Piece.Type);
            Assert.AreEqual(ChessColor.Black, gb.squares[3, 2].Piece.Color);
            Assert.IsNull(gb.squares[2, 3].Piece);

            gb.MovePiece(gb.squares[3, 3], gb.squares[1, 3]);
            gb.squares[1, 3].Piece.MoveCount = 0;
            gb.MovePiece(gb.squares[0, 2], gb.squares[1, 0]);
            gb.MovePiece(gb.squares[5, 7], gb.squares[7, 6]);
            Assert.AreEqual(true, ml.MovePiece(gb.squares[1, 3], gb.squares[3, 3]));
            Assert.AreEqual(false, ml.MovePiece(gb.squares[3, 2], gb.squares[2, 3]));
            Assert.AreEqual(ChessPiece.Pawn, gb.squares[3, 3].Piece.Type);
            Assert.AreEqual(ChessColor.White, gb.squares[3, 3].Piece.Color);
            Assert.AreEqual(ChessPiece.Pawn, gb.squares[3, 2].Piece.Type);
            Assert.AreEqual(ChessColor.Black, gb.squares[3, 2].Piece.Color);
            Assert.IsNull(gb.squares[2, 3].Piece);
        }

        [TestMethod]
        public void MoveIntoCheckCastleTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic ml = new MoveLogic(gb);

            gb.RemovePiece(gb.squares[0, 5]);
            gb.RemovePiece(gb.squares[0, 6]);
            gb.MovePiece(gb.squares[7, 6], gb.squares[2, 5]);
            Assert.AreEqual(false, ml.MovePiece(gb.squares[0, 4], gb.squares[0, 6]));
            Assert.AreEqual(ChessPiece.King, gb.squares[0, 4].Piece.Type);
            Assert.AreEqual(ChessColor.White, gb.squares[0, 4].Piece.Color);
            Assert.AreEqual(ChessPiece.Rook, gb.squares[0, 7].Piece.Type);
            Assert.AreEqual(ChessColor.White, gb.squares[0, 7].Piece.Color);

            gb.MovePiece(gb.squares[2, 5], gb.squares[2, 6]);
            Assert.AreEqual(false, ml.MovePiece(gb.squares[0, 4], gb.squares[0, 6]));
            Assert.AreEqual(ChessPiece.King, gb.squares[0, 4].Piece.Type);
            Assert.AreEqual(ChessColor.White, gb.squares[0, 4].Piece.Color);
            Assert.AreEqual(ChessPiece.Rook, gb.squares[0, 7].Piece.Type);
            Assert.AreEqual(ChessColor.White, gb.squares[0, 7].Piece.Color);
        }

        [TestMethod]
        public void MoveAnotherPieceCheckTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic ml = new MoveLogic(gb);

            gb.MovePiece(gb.squares[0, 4], gb.squares[5, 4]);
            Assert.AreEqual(true, ml.MovePiece(gb.squares[7, 6], gb.squares[5, 5]));
            Assert.AreEqual(ChessPiece.Knight, gb.squares[5, 5].Piece.Type);
            Assert.AreEqual(ChessColor.Black, gb.squares[5, 5].Piece.Color);
            Assert.IsNull(gb.squares[7, 6].Piece);
        }

        [TestMethod]
        public void MoveTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic ml = new MoveLogic(gb);

            Assert.AreEqual(true, ml.MovePiece(gb.squares[1, 0], gb.squares[2, 0]));
            Assert.AreEqual(true, ml.MovePiece(gb.squares[1, 1], gb.squares[3, 1]));
            Assert.AreEqual(false, ml.MovePiece(gb.squares[2, 0], gb.squares[4, 0]));
        }

        [TestMethod]
        public void IsPromotionTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic ml = new MoveLogic(gb);

            Assert.AreEqual(false, ml.IsPromotion(gb.squares[1, 0]));
            Assert.AreEqual(false, ml.IsPromotion(gb.squares[6, 0]));

            gb.RemovePiece(gb.squares[7, 0]);
            gb.MovePiece(gb.squares[1, 0], gb.squares[7, 0]);
            Assert.AreEqual(true, ml.IsPromotion(gb.squares[7, 0]));

            gb.RemovePiece(gb.squares[0, 0]);
            gb.MovePiece(gb.squares[6, 0], gb.squares[0, 0]);
            Assert.AreEqual(true, ml.IsPromotion(gb.squares[0, 0]));
        }

        [TestMethod]
        public void PromoteTest()
        {
            GameBoard gb = new GameBoard(8, 8);
            MoveLogic ml = new MoveLogic(gb);

            ml.Promote(gb.squares[0, 0], Promotion.Queen);
            Assert.AreEqual(ChessPiece.Queen, gb.squares[0, 0].Piece.Type);

            ml.Promote(gb.squares[0, 0], Promotion.Knight);
            Assert.AreEqual(ChessPiece.Knight, gb.squares[0, 0].Piece.Type);

            ml.Promote(gb.squares[0, 0], Promotion.Rook);
            Assert.AreEqual(ChessPiece.Rook, gb.squares[0, 0].Piece.Type);

            ml.Promote(gb.squares[0, 0], Promotion.Bishop);
            Assert.AreEqual(ChessPiece.Bishop, gb.squares[0, 0].Piece.Type);

            ml.Promote(gb.squares[0, 0], Promotion.Bishop);
            Assert.AreNotEqual(ChessPiece.Queen, gb.squares[0, 0].Piece.Type);
        }
    }
}
