using System;
using System.Collections.Generic;
using System.Text;

namespace ChessModel
{
    public interface IPiece
    {
        int Type { get; set; }
        int Color { get; set; }
        int PosCol { get; set; }
        int PosRow { get; set; }
        bool Alive { get; set; }
        
        void ValidMove();
        void Remove();
    }
}
