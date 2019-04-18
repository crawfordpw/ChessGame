using System;
using System.Collections.Generic;
using System.Text;

namespace ChessModel.Pieces
{
    class Knight
    {
        public int Type { get; set; }
        public int Color { get; set; }
        public int PosCol { get; set; }
        public int PosRow { get; set; }
        public bool Alive { get; set; }

        public void Remove()
        {
            throw new NotImplementedException();
        }

        public void ValidMove()
        {
            throw new NotImplementedException();
        }
    }
}
