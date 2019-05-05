# ChessGame
A Chess application written in C# using the .Net framework. This project was made in order to develop and learn more about object-oriented programming and practices as well as the framework itself. It uses an MVVM software architectural pattern to seperate the game logic from the UI logic (or atleast try to follow this structure as close as possible, as this is my first experience with programming in this manner, and may not be entirely accurate). Windows Presentation Foundation (WPF) is used for rendering the UI.

These features have been implemented thus far:
- A UI game board which updates automatically when a player makes a move
- Show all possible valid moves of a selected piece on the UI
- Select a piece for a pawn to promote to when it is able to promote via a window dialog box
- A window popup to end the application or start a new game when checkmate or a stalemate is detected
- Player clocks that show time remaining, and logic to end game when time runs out
- Ability to play a full game of chess with a second player. This involves:
	- A move validator that checks if a piece can make a move or not
	- A game state that detects if moves can still be played, if there is a checkmate, or if there is a stalemate
	- En passant movement
	- Castling movement
	- Pawn Promotion

TODO:
- Ability to Undo a move
- Starting window for a user to select various options. eg. time limit, player color, number of players etc. 
- Save and Load a game
- PGN and/or FEN translation
- Board editor and start a game from a given position
- Detect if a game has seen a position N number of times (three-fold repetition)

Longterm Goals:
- Create an AI from scratch with varying difficulties using:
	- minimax
	- alpha-beta pruning
	- monte carlo search trees
	- machine learning?
- Online play?
