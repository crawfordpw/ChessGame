﻿using System;

namespace ChessModel
{
    /*
     * A class used for setting up the logic needed for a game
     */
    public class Game
    {
        public GameBoard gb;
        public MoveLogic ml;
        public GameLogic gl;
        public Player CurrentPlayer { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public State State { get { return ml.gs.State; } }

        /*
         * Creates a new game with two players.
         */
        public void NewGame(PlayerClock player1Clock, PlayerClock player2Clock)
        {
            gb = new GameBoard();
            ml = new MoveLogic(gb);           
            Player1 = new Human(ChessColor.White, player1Clock);
            Player2 = new Human(ChessColor.Black, player2Clock);
            CurrentPlayer = Player1;
            gl = new GameLogic(this);
        }

        /*
         * Wrapper for the InPlay method in the Game State class
         */
        public bool InPlay(ChessColor color)
        {
            color = color == ChessColor.White ? ChessColor.Black : ChessColor.White;
            return ml.gs.InPlay(gb, color);
        }

        /*
         * Calls logic to start/end a Player's turn
         */
        public Player NextPlayer()
        {
            return CurrentPlayer = CurrentPlayer.Color == ChessColor.White ? Player2 : Player1;
        }

        /*
         * Ends the application. Although, this will not be used in most cases since the
         * UI should handle how this is done
         */
        public void EndGame()
        {
            System.Environment.Exit(1);
        }
    }
}
