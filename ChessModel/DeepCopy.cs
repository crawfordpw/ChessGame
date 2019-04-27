using System;
using System.Collections.Generic;
using System.Text;

namespace ChessModel
{
    public static class DeepCopy
    {
        public static void DeepCopySquareArray(IPiece piece, Square[] source, Square[] target)
        {
            for(int i = 0; i < source.Length; i++)
            {
                DeepCopySquare(piece, source[i], target[i]);
            }
        }

        public static void DeepCopySquare(IPiece piece, Square source, Square target)
        {
            target.ColID = source.ColID;
            target.RowID = source.RowID;
            target.Color = source.Color;
            target.Cord = (string)source.Cord.Clone();

            if (source.Piece == null)
            {
                target.Piece = null;
            }
            else
            {
                if(target.Piece == null)
                {
                    target.Piece = piece;
                }
                target.Piece.Type = source.Piece.Type;
                target.Piece.Color = source.Piece.Color;
                target.Piece.ColID = source.Piece.ColID;
                target.Piece.RowID = source.Piece.RowID;
                target.Piece.MoveCount = source.Piece.MoveCount;
            }

        }
    }
}
