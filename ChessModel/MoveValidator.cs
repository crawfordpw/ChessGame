namespace ChessModel
{
   public class MoveValidator
    {
        public static bool IsOccupied(Square toSquare)
        {
            if (toSquare.piece != null)
                return true;
            else
                return false;
        }

        public static bool IsEnemy(Square fromSquare, Square toSquare)
        {
            if (fromSquare.piece.Color == toSquare.piece.Color)
                return false;
            else
                return true;
        }
    }
}
