using System;
using System.Collections.Generic;
using System.Text;
using UvsChess;

namespace StudentAI
{
    public class StudentAI : IChessAI
    {
        #region IChessAI Members that are implemented by the Student

        /// <summary>
        /// The name of your AI
        /// </summary>
        public string Name
        {
#if DEBUG
            get { return "JEK (Debug)"; }
#else
            get { return "JEK"; }
#endif
        }
        public Dictionary<int, HashSet<Tuple<int,int>>> GenMoves(ChessBoard board)
        {
            Dictionary<int, HashSet<Tuple<int,int>>> moves;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; i < 7; i++)
                {
                    int curLocation = (i * 10) + j;
                    Tuple<int, int> move;
                    if (board[i,j] == ChessPiece.Empty){//do nothing
                    } 
                    else if (board[i, j] == ChessPiece.BlackPawn)
                    {
                        if (i == 1)
                        {
                            //black pawn forward 2
                            if (board[i + 2, j] == ChessPiece.Empty && board[i + 1, j] == ChessPiece.Empty)
                            {
                                move = Tuple.Create(curLocation + 10, 1);
                                moves[curLocation].Add(move);
                            }

                        }
                        //black pawn forward 1
                        if (board[i + 1, j] == ChessPiece.Empty)
                        {
                            move = Tuple.Create(curLocation + 10, 1);
                            moves[curLocation].Add(move);
                        }
                        //black pawn diaganal attack right
                        if (j + 1 <= 7 && board[i + 1, j + 1] != ChessPiece.Empty)
                        {
                            move = Tuple.Create(curLocation + 11, 2);
                            moves[curLocation].Add(move);
                        }
                        //black pawn diaganal attack left
                        if (j - 1 >= 0 && board[i + 1, j - 1] != ChessPiece.Empty)
                        {
                            int moveLocation = ((i + 1) * 10) + j - 1;
                            move = Tuple.Create(moveLocation, 2);
                            moves[curLocation].Add(move);
                        }
                    }
                }
            }

            return moves;
        }
        /// <summary>
        /// Evaluates the chess board and decided which move to make. This is the main method of the AI.
        /// The framework will call this method when it's your turn.
        /// </summary>
        /// <param name="board">Current chess board</param>
        /// <param name="yourColor">Your color</param>
        /// <returns> Returns the best chess move the player has for the given chess board</returns>
        public ChessMove GetNextMove(ChessBoard board, ChessColor myColor)
        {
            return ChessMove((1,1),(1,2));
        }

        /// <summary>
        /// Validates a move. The framework uses this to validate the opponents move.
        /// </summary>
        /// <param name="boardBeforeMove">The board as it currently is _before_ the move.</param>
        /// <param name="moveToCheck">This is the move that needs to be checked to see if it's valid.</param>
        /// <param name="colorOfPlayerMoving">This is the color of the player who's making the move.</param>
        /// <returns>Returns true if the move was valid</returns>
        public bool IsValidMove(ChessBoard boardBeforeMove, ChessMove moveToCheck, ChessColor colorOfPlayerMoving)
        {
            throw (new NotImplementedException());
        }

        #endregion
















        #region IChessAI Members that should be implemented as automatic properties and should NEVER be touched by students.
        /// <summary>
        /// This will return false when the framework starts running your AI. When the AI's time has run out,
        /// then this method will return true. Once this method returns true, your AI should return a 
        /// move immediately.
        /// 
        /// You should NEVER EVER set this property!
        /// This property should be defined as an Automatic Property.
        /// This property SHOULD NOT CONTAIN ANY CODE!!!
        /// </summary>
        public AIIsMyTurnOverCallback IsMyTurnOver { get; set; }

        /// <summary>
        /// Call this method to print out debug information. The framework subscribes to this event
        /// and will provide a log window for your debug messages.
        /// 
        /// You should NEVER EVER set this property!
        /// This property should be defined as an Automatic Property.
        /// This property SHOULD NOT CONTAIN ANY CODE!!!
        /// </summary>
        /// <param name="message"></param>
        public AILoggerCallback Log { get; set; }

        /// <summary>
        /// Call this method to catch profiling information. The framework subscribes to this event
        /// and will print out the profiling stats in your log window.
        /// 
        /// You should NEVER EVER set this property!
        /// This property should be defined as an Automatic Property.
        /// This property SHOULD NOT CONTAIN ANY CODE!!!
        /// </summary>
        /// <param name="key"></param>
        public AIProfiler Profiler { get; set; }

        /// <summary>
        /// Call this method to tell the framework what decision print out debug information. The framework subscribes to this event
        /// and will provide a debug window for your decision tree.
        /// 
        /// You should NEVER EVER set this property!
        /// This property should be defined as an Automatic Property.
        /// This property SHOULD NOT CONTAIN ANY CODE!!!
        /// </summary>
        /// <param name="message"></param>
        public AISetDecisionTreeCallback SetDecisionTree { get; set; }
        #endregion
    }
}
