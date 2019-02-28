using System;
using System.Collections.Generic;
using UvsChess;
using System.Diagnostics;

namespace StudentAI
{
    public class StudentAIMinimax : IChessAI
    {
        #region IChessAI Members that are implemented by the Student

        /// <summary>
        /// The name of your AI
        /// </summary>
        public string Name
        {
#if DEBUG
            get { return "Group 3 JEK Minimax"; }
#else
            get { return "JEK"; }
#endif
        }

        static Dictionary<ChessPiece, List<List<int>>> positionVals = new Dictionary<ChessPiece, List<List<int>>>()
        {   // the order as listed is in the shape of the board as we look at it
            [ChessPiece.BlackKing] = new List<List<int>>()
                { new List<int>() { 4, 6, 2, 2,2, 2, 6, 4 },
                new List<int>() { 4, 4, 0, 0, 0, 0, 4, 4 },
                new List<int>() {-2, -4, -4, -4, -4, -4, -4, -2 },
                new List<int>() {-4, -6, -6, -8, -8, -6, -6, -4 },
                new List<int>() {-6, -8, -8, -10, -10, -8, -8, -6 },
                new List<int>() {-6, -8, -8, -10, -10, -8, -8, -6 },
                new List<int>() {-6, -8, -8, -10, -10, -8, -8, -6 },
                new List<int>() {-6, -8, -8, -10, -10, -8, -8, -6 }
                },
            [ChessPiece.BlackQueen] = new List<List<int>>()
            {
                new List<int>() { -4, -2, -2, -1, -1, -2, -2, -4 },
                new List<int>() { -2, 0, 0, 0, 0, 1, 0, -2 },
                new List<int>() { -2, 0, 1, 1, 1, 1, 1, -2 },
                new List<int>() { -1, 0, 1, 1, 1, 1, 0, 0 },
                new List<int>() { -1, 0, 1, 1, 1, 1, 0, -1 },
                new List<int>() { -2, 0, 1, 1, 1, 1, 0, -2},
                new List<int>() { -2, 0, 0, 0, 0, 0, 0, -2 },
                new List<int>() { -4, -2, -2, -1, -1, -2, -2, -4 }
            },
            [ChessPiece.BlackRook] = new List<List<int>>()
                { new List<int>() { 0, 0, 0, 1, 1, 0, 0, 0 },
                new List<int>() {-1, 0, 0, 0, 0, 0, 0, -1 },
                new List<int>() {-1, 0, 0, 0, 0, 0, 0, -1 },
                new List<int>() {-1, 0, 0, 0, 0, 0, 0, -1 },
                new List<int>() {-1, 0, 0, 0, 0, 0, 0, -1 },
                new List<int>() {-1, 0, 0, 0, 0, 0, 0, -1 },
                new List<int>() {1, 2, 2, 2, 2, 2, 2, 1 },
                new List<int>() {0, 0, 0, 0, 0, 0, 0, 0}
                },
            [ChessPiece.BlackBishop] = new List<List<int>>()
            {
                new List<int>() { -4, -2, -2, -2, -2, -2, -2, -4 },
                new List<int>() { -2, 1, 0, 0, 0, 0, 1, -2 },
                new List<int>() {-2, 2, 2, 2, 2, 2, 2, -2},
                new List<int>() { -2, 0, 2, 2, 2, 2, 0, -2 },
                new List<int>() { -2, 1, 1, 2, 2, 1, 1, -2 },
                new List<int>() { -2, 0, 1, 2, 2, 1, 0, -2 },
                new List<int>() { -2, 0, 0, 0, 0, 0, 0, -2 },
                new List<int>() { -4, -2, -2, -2, -2, -2, -2, -4 }
            },
            [ChessPiece.BlackKnight] = new List<List<int>>()
                { new List<int>() {-10, -8, -6, -6, -6, -6, -8, -10 },
                new List<int>() { -8, -4, 0, 1, 1, 0, -4, -8},
                new List<int>() {-6, 1, 2, 3, 3, 2, 1, -6},
                new List<int>() {-6, 0, 3, 4, 4, 3, 0, -6},
                new List<int>() {-6, 1, 3, 4, 4, 3, 1, -6},
                new List<int>() {-6, 0, 2, 3, 3, 2, 0, -6},
                new List<int>() {-8, -4, 0, 0, 0, 0, -4, -8},
                new List<int>() {-1, -8, -6, -6, -6, -6, -8, -10}
                },
            [ChessPiece.BlackPawn] = new List<List<int>>()
            {
                new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 },
                new List<int>() { 1, 2, 2, -4, -4, 2, 2, 1 },
                new List<int>() { 1, -1, -2, 0, 0, -2, -1, 1 },
                new List<int>() { 0, 0, 0, 4, 4, 0, 0, 0 },
                new List<int>() { 1, 1, 2, 5, 5, 2, 1, 1 },
                new List<int>() {2, 2, 4, 6, 6, 4, 2, 2 },
                new List<int>() { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new List<int>() { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5}
            },
            [ChessPiece.WhiteKing] = new List<List<int>>()
                { new List<int>() { -6, -8, -8, -10, -10, -8, -8, -6 },
                new List<int>() {-6, -8, -8, -10, -10, -8, -8, -6 },
                new List<int>() {-6, -8, -8, -10, -10, -8, -8, -6 },
                new List<int>() {-6, -8, -8, -10, -10, -8, -8, -6},
                new List<int>() { -4, -6, -6, -8, -8, -6, -6, -4},
                new List<int>() { -2, -4, -4, -4, -4, -4, -4, -2 },
                new List<int>() {4, 4, 0, 0, 0, 0, 4, 4 },
                new List<int>() {4, 6, 2, 2,2, 2, 6, 4 },
                },
            [ChessPiece.WhiteQueen] = new List<List<int>>()
            {
                new List<int>() { -4, -2, -2, -1, -1, -2, -2, -4 },
                new List<int>() { -2, 0, 0, 0, 0, 0, 0, -2 },
                new List<int>() { -2, 0, 1, 1, 1, 1, 0, -2 },
                new List<int>() { -1, 0, 1, 1, 1, 1, 0, -1},
                new List<int>() { 0, 0, 1, 1, 1, 1, 0, -1},
                new List<int>() { -2, 1, 1, 1, 1, 1, 0, -2},
                new List<int>() { -2, 0, 1, 0, 0, 0, 0, -2 },
                new List<int>() { -4, -2, -2, -1, -1, -2, -2, -4 }
            },
            [ChessPiece.WhiteRook] = new List<List<int>>()
                { new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0},
                new List<int>() { 1, 2, 2, 2, 2, 2, 2, 1 },
                new List<int>() {-1, 0, 0, 0, 0, 0, 0, -1 },
                new List<int>() {-1, 0, 0, 0, 0, 0, 0, -1 },
                new List<int>() {-1, 0, 0, 0, 0, 0, 0, -1},
                new List<int>() {-1, 0, 0, 0, 0, 0, 0, -1},
                new List<int>() {-1, 0, 0, 0, 0, 0, 0, -1},
                new List<int>() {0, 0, 0, 1, 1, 0, 0, 0}
                },
            [ChessPiece.WhiteBishop] = new List<List<int>>()
            {
                new List<int>() { -4, -2, -2, -2, -2, -2, -2, -4 },
                new List<int>() { -2, 0, 0, 0, 0, 0, 0, -2 },
                new List<int>() { -2, 0, 1, 2, 2, 1, 0, -2},
                new List<int>() { -2, 1, 1, 2, 2, 1, 1, -2 },
                new List<int>() { -2, 0, 2, 2, 2, 2, 0, -2 },
                new List<int>() { -2, 2, 2, 2, 2, 2, 2, -2 },
                new List<int>() { -2, 1, 0, 0, 0, 0, 1, -2 },
                new List<int>() { -4, -2, -2, -2, -2, -2, -2, -4 }
            },
            [ChessPiece.WhiteKnight] = new List<List<int>>()
                { new List<int>() { -10, -8, -6, -6, -6, -6, -8, -10 },
                new List<int>() { -8, -4, 0, 0, 0, 0, -4, -8 },
                new List<int>() {-6, 0, 2, 3, 3, 2, 0, -6 },
                new List<int>() {-6, 1, 3, 4, 4, 3, 1, -6},
                new List<int>() { -6, 0, 3, 4, 4, 3, 0, -6 },
                new List<int>() {-6, 1, 2, 3, 3, 2, 1, -6 },
                new List<int>() {-8, -4, 0, 1, 1, 0, -4, -8 },
                new List<int>() {-10, -8, -6, -6, -6, -6, -8, -10}
                },
            [ChessPiece.WhitePawn] = new List<List<int>>()
            {
                new List<int>() { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
                new List<int>() {1, 1, 1, 1, 1, 1, 1, 1 },
                new List<int>() { 2, 2, 4, 6, 6, 4, 2, 2 },
                new List<int>() { 1, 1, 2, 3, 3, 2, 1, 1},
                new List<int>() { 0, 0, 0, 4, 4, 0, 0, 0 },
                new List<int>() { 1, -1, -2, 0, 0, -2, -1, 1 },
                new List<int>() { 1, 2, 2, -4, -4, 2, 2, 1 },
                new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 }
            },
        };

        private static readonly Random random = new Random();

        public List<ChessMove> GetAllMoves(ChessBoard board, ChessColor color)
        {
            List<ChessMove> allMoves = new List<ChessMove>();
            for (int i = 0; i <= 7; i++) //columns
            {
                for (int j = 0; j <= 7; j++) //rows
                {
                    ChessLocation from = new ChessLocation(i, j);
                    if (board[i, j] == ChessPiece.Empty)
                    {//do nothing
                        continue;
                    }
                    if (color == ChessColor.Black)
                    {
                        switch (board[i, j])
                        {
                            case ChessPiece.BlackKing:
                                //black King down 1 row
                                if ((j + 1 <= 7) && board[i, j + 1] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i, j + 1));
                                    allMoves.Add(move);
                                }
                                //black King up 1 row
                                if ((j - 1 >= 0) && board[i, j - 1] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i, j - 1));
                                    allMoves.Add(move);
                                }
                                //black King left 1 col
                                if ((i - 1 >= 0) && board[i - 1, j] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i - 1, j));
                                    allMoves.Add(move);
                                }
                                //black King right 1 col
                                if ((i + 1 <= 7) && board[i + 1, j] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i + 1, j));
                                    allMoves.Add(move);
                                }
                                //black King up (row) and right (col)
                                if ((j - 1 >= 0) && (i + 1 <= 7) && board[i + 1, j - 1] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i + 1, j - 1));
                                    allMoves.Add(move);
                                }
                                //black king down (row) and right (col)
                                if ((j + 1 <= 7) && (i + 1 <= 7) && board[i + 1, j + 1] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i + 1, j + 1));
                                    allMoves.Add(move);
                                }
                                //black king up (row) and left (col)
                                if ((j - 1 >= 0) && (i - 1 >= 0) && board[i - 1, j - 1] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i - 1, j - 1));
                                    allMoves.Add(move);
                                }
                                //black King down (row) and left (col)
                                if ((j + 1 <= 7) && (i - 1 >= 0) && board[i - 1, j + 1] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i - 1, j + 1));
                                    allMoves.Add(move);
                                }
                                break;
                            case ChessPiece.BlackQueen:

                                //black queen down (row)
                                int rj = j;
                                while ((rj + 1 <= 7) && board[i, rj + 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i, rj + 1));
                                    allMoves.Add(move);
                                    rj++;
                                }
                                if ((rj + 1 <= 7) && board[i, rj + 1] > ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i, rj + 1));
                                    allMoves.Add(move);
                                }
                                //black queen right (col)
                                int ri = i;
                                while ((ri + 1 <= 7) && board[ri + 1, j] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(ri + 1, j));
                                    allMoves.Add(move);
                                    ri++;
                                }
                                if (ri + 1 <= 7 && board[ri + 1, j] > ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(ri + 1, j));
                                    allMoves.Add(move);
                                }
                                //black queen up (row)
                                rj = j;
                                while ((rj - 1 >= 0) && board[i, rj - 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i, rj - 1));
                                    allMoves.Add(move);
                                    rj--;
                                }
                                if (rj - 1 >= 0 && board[i, rj - 1] > ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i, rj - 1));
                                    allMoves.Add(move);
                                }
                                //black queen left (col)
                                ri = i;
                                while ((ri - 1 >= 0) && board[ri - 1, j] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(ri - 1, j));
                                    allMoves.Add(move);
                                    ri--;
                                }
                                if (ri - 1 >= 0 && board[ri - 1, j] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(ri - 1, j));
                                    allMoves.Add(move);
                                }
                                //black queen down and right (+row)(+col)
                                int bi = i;
                                int bj = j;
                                while ((bi + 1 <= 7) && (bj + 1 <= 7) && board[bi + 1, bj + 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi + 1, bj + 1));
                                    allMoves.Add(move);
                                    bi++;
                                    bj++;
                                }
                                if ((bi + 1 <= 7) && (bj + 1 <= 7) && board[bi + 1, bj + 1] > ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi + 1, bj + 1));
                                    allMoves.Add(move);
                                }
                                //black queen up and left (-row)(-col)
                                bi = i;
                                bj = j;
                                while ((bi - 1 >= 0) && (bj - 1 >= 0) && board[bi - 1, bj - 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi - 1, bj - 1));
                                    allMoves.Add(move);
                                    bi--;
                                    bj--;
                                }
                                if ((bi - 1 >= 0) && (bj - 1 >= 0) && board[bi - 1, bj - 1] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi - 1, bj - 1));
                                    allMoves.Add(move);
                                }
                                //black queen down and left (+row)(-col)
                                bi = i;
                                bj = j;
                                while ((bi - 1 >= 0) && (bj + 1 <= 7) && board[bi - 1, bj + 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi - 1, bj + 1));
                                    allMoves.Add(move);
                                    bi--;
                                    bj++;
                                }
                                if ((bi - 1 >= 0) && (bj + 1 <= 7) && board[bi - 1, bj + 1] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi - 1, bj + 1));
                                    allMoves.Add(move);
                                }
                                //black queen up and right (-row)(+col)
                                bi = i;
                                bj = j;
                                while ((bi + 1 <= 7) && (bj - 1 >= 0) && board[bi + 1, bj - 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi + 1, bj - 1));
                                    allMoves.Add(move);
                                    bi++;
                                    bj--;
                                }
                                if ((bi + 1 <= 7) && (bj - 1 >= 0) && board[bi + 1, bj - 1] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi + 1, bj - 1));
                                    allMoves.Add(move);
                                }

                                break;
                            case ChessPiece.BlackKnight:

                                //black knight right 2 down 1
                                if ((i + 2 <= 7) && (j + 1 <= 7) && board[i + 2, j + 1] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i + 2, j + 1));
                                    allMoves.Add(move);
                                }
                                //black knight left 2 up 1
                                if ((i - 2 >= 0) && (j - 1 >= 0) && board[i - 2, j - 1] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i - 2, j - 1));
                                    allMoves.Add(move);
                                }
                                //black knight left 2 down 1
                                if ((i - 2 >= 0) && (j + 1 <= 7) && board[i - 2, j + 1] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i - 2, j + 1));
                                    allMoves.Add(move);
                                }
                                //black knight right 2 up 1
                                if ((i + 2 <= 7) && (j - 1 >= 0) && board[i + 2, j - 1] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i + 2, j - 1));
                                    allMoves.Add(move);
                                }
                                //black knight down 2 right 1
                                if ((i + 1 <= 7) && (j + 2 <= 7) && board[i + 1, j + 2] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i + 1, j + 2));
                                    allMoves.Add(move);
                                }
                                //black knight down 2 left 1
                                if ((i - 1 >= 0) && (j + 2 <= 7) && board[i - 1, j + 2] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i - 1, j + 2));
                                    allMoves.Add(move);
                                }
                                //black knight up 2 right 1
                                if ((i + 1 <= 7) && (j - 2 >= 0) && board[i + 1, j - 2] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i + 1, j - 2));
                                    allMoves.Add(move);
                                }
                                //black knight up 2 left 1
                                if ((i - 1 >= 0) && (j - 2 >= 0) && board[i - 1, j - 2] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i - 1, j - 2));
                                    allMoves.Add(move);
                                }
                                break;
                            case ChessPiece.BlackBishop:

                                //black Bishop down and right
                                bi = i;
                                bj = j;
                                while ((bi + 1 <= 7) && (bj + 1 <= 7) && board[bi + 1, bj + 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi + 1, bj + 1));
                                    allMoves.Add(move);
                                    bi++;
                                    bj++;
                                }
                                if ((bi + 1 <= 7) && (bj + 1 <= 7) && board[bi + 1, bj + 1] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi + 1, bj + 1));
                                    allMoves.Add(move);
                                }
                                //black Bishop up and left
                                bi = i;
                                bj = j;
                                while ((bi - 1 >= 0) && (bj - 1 >= 0) && board[bi - 1, bj - 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi - 1, bj - 1));
                                    allMoves.Add(move);
                                    bi--;
                                    bj--;
                                }
                                if ((bi - 1 >= 0) && (bj - 1 >= 0) && board[bi - 1, bj - 1] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi - 1, bj - 1));
                                    allMoves.Add(move);
                                }
                                //black Bishop down and left
                                bi = i;
                                bj = j;
                                while ((bi - 1 >= 0) && (bj + 1 <= 7) && board[bi - 1, bj + 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi - 1, bj + 1));
                                    allMoves.Add(move);
                                    bi--;
                                    bj++;
                                }
                                if ((bi - 1 >= 0) && (bj + 1 <= 7) && board[bi - 1, bj + 1] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi - 1, bj + 1));
                                    allMoves.Add(move);
                                }
                                //black Bishop up and right
                                bi = i;
                                bj = j;
                                while ((bi + 1 <= 7) && (bj - 1 >= 0) && board[bi + 1, bj - 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi + 1, bj - 1));
                                    allMoves.Add(move);
                                    bi++;
                                    bj--;
                                }
                                if ((bi + 1 <= 7) && (bj - 1 >= 0) && board[bi + 1, bj - 1] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi + 1, bj - 1));
                                    allMoves.Add(move);
                                }
                                break;
                            case ChessPiece.BlackRook:
                                //black rook down
                                rj = j;
                                while ((rj + 1 <= 7) && board[i, rj + 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i, rj + 1));
                                    allMoves.Add(move);
                                    rj++;
                                }
                                if ((rj + 1 <= 7) && board[i, rj + 1] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i, rj + 1));
                                    allMoves.Add(move);
                                }
                                //black rook right
                                ri = i;
                                while ((ri + 1 <= 7) && board[ri + 1, j] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(ri + 1, j));
                                    allMoves.Add(move);
                                    ri++;
                                }
                                if (ri + 1 <= 7 && board[ri + 1, j] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(ri + 1, j));
                                    allMoves.Add(move);
                                }
                                //black rook up
                                rj = j;
                                while ((rj - 1 >= 0) && board[i, rj - 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i, rj - 1));
                                    allMoves.Add(move);
                                    rj--;
                                }
                                if (rj - 1 >= 0 && board[i, rj - 1] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i, rj - 1));
                                    allMoves.Add(move);
                                }
                                //black rook left
                                ri = i;
                                while ((ri - 1 >= 0) && board[ri - 1, j] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(ri - 1, j));
                                    allMoves.Add(move);
                                    ri--;
                                }
                                if (ri - 1 >= 0 && board[ri - 1, j] >= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(ri - 1, j));
                                    allMoves.Add(move);
                                }
                                break;
                            case ChessPiece.BlackPawn:
                                if (j == 1)
                                {
                                    //black pawn down 2
                                    if (board[i, j + 1] == ChessPiece.Empty && board[i, j + 2] == ChessPiece.Empty)
                                    {
                                        ChessMove move = new ChessMove(from, new ChessLocation(i, j + 2));
                                        allMoves.Add(move);
                                    }
                                }
                                //black pawn down 1
                                if (j + 1 <= 7 && board[i, j + 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i, j + 1));
                                    allMoves.Add(move);
                                }
                                //black pawn diagonal attack down/left (+row)(-col)
                                if ((j + 1 <= 7) && (i - 1 >= 0) && board[i - 1, j + 1] > ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i - 1, j + 1));
                                    allMoves.Add(move);
                                }
                                //black pawn diagonal attack down/right
                                if ((j + 1 <= 7) && (i + 1 <= 7) && board[i + 1, j + 1] > ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i + 1, j + 1));
                                    allMoves.Add(move);
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (board[i, j])
                        {
                            case ChessPiece.WhitePawn:
                                //white pawn up 2
                                if (j == 6)
                                {
                                    if (board[i, j - 2] == ChessPiece.Empty && board[i, j - 1] == ChessPiece.Empty)
                                    {
                                        ChessMove move = new ChessMove(from, new ChessLocation(i, j - 2));
                                        allMoves.Add(move);
                                    }
                                }
                                //white pawn up 1
                                if ((j - 1 >= 0) && board[i, j - 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i, j - 1));
                                    allMoves.Add(move);
                                }
                                //white pawn diagonal attack up/left
                                if ((j - 1 >= 0) && (i - 1 >= 0) && board[i - 1, j - 1] < ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i - 1, j - 1));
                                    allMoves.Add(move);
                                }
                                //white pawn diagonal attack up/right
                                if ((j - 1 >= 0) && (i + 1 <= 7) && board[i + 1, j - 1] < ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i + 1, j - 1));
                                    allMoves.Add(move);
                                }
                                break;
                            case ChessPiece.WhiteBishop:

                                //white Bishop down and right
                                int bi = i;
                                int bj = j;
                                while ((bi + 1 <= 7) && (bj + 1 <= 7) && board[bi + 1, bj + 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi + 1, bj + 1));
                                    allMoves.Add(move);
                                    bi++;
                                    bj++;
                                }
                                if ((bi + 1 <= 7) && (bj + 1 <= 7) && board[bi + 1, bj + 1] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi + 1, bj + 1));
                                    allMoves.Add(move);
                                }
                                //white Bishop up and left
                                bi = i;
                                bj = j;
                                while ((bi - 1 >= 0) && (bj - 1 >= 0) && board[bi - 1, bj - 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi - 1, bj - 1));
                                    allMoves.Add(move);

                                    bi--;
                                    bj--;
                                }
                                if ((bi - 1 >= 0) && (bj - 1 >= 0) && board[bi - 1, bj - 1] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi - 1, bj - 1));
                                    allMoves.Add(move);
                                }
                                //white Bishop down and left
                                bi = i;
                                bj = j;
                                while ((bi - 1 >= 0) && (bj + 1 <= 7) && board[bi - 1, bj + 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi - 1, bj + 1));
                                    allMoves.Add(move);
                                    bi--;
                                    bj++;
                                }
                                if ((bi - 1 >= 0) && (bj + 1 <= 7) && board[bi - 1, bj + 1] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi - 1, bj + 1));
                                    allMoves.Add(move);
                                }
                                //white Bishop up and right
                                bi = i;
                                bj = j;
                                while ((bi + 1 <= 7) && (bj - 1 >= 0) && board[bi + 1, bj - 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi + 1, bj - 1));
                                    allMoves.Add(move);
                                    bi++;
                                    bj--;
                                }
                                if ((bi + 1 <= 7) && (bj - 1 >= 0) && board[bi + 1, bj - 1] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi + 1, bj - 1));
                                    allMoves.Add(move);
                                }
                                break;
                            case ChessPiece.WhiteKnight:

                                //white knight right 2 down 1
                                if ((i + 2 <= 7) && (j + 1 <= 7) && board[i + 2, j + 1] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i + 2, j + 1));
                                    allMoves.Add(move);
                                }
                                //white knight left 2 up 1
                                if ((i - 2 >= 0) && (j - 1 >= 0) && board[i - 2, j - 1] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i - 2, j - 1));
                                    allMoves.Add(move);
                                }
                                //white knight left 2 down 1
                                if ((i - 2 >= 0) && (j + 1 <= 7) && board[i - 2, j + 1] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i - 2, j + 1));
                                    allMoves.Add(move);
                                }
                                //white knight right 2 up 1
                                if ((i + 2 <= 7) && (j - 1 >= 0) && board[i + 2, j - 1] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i + 2, j - 1));
                                    allMoves.Add(move);
                                }
                                //white knight down 2 right 1
                                if ((i + 1 <= 7) && (j + 2 <= 7) && board[i + 1, j + 2] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i + 1, j + 2));
                                    allMoves.Add(move);
                                }
                                //white knight down 2 left 1
                                if ((i - 1 >= 0) && (j + 2 <= 7) && board[i - 1, j + 2] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i - 1, j + 2));
                                    allMoves.Add(move);
                                }
                                //white knight up 2 right 1
                                if ((i + 1 <= 7) && (j - 2 >= 0) && board[i + 1, j - 2] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i + 1, j - 2));
                                    allMoves.Add(move);
                                }
                                //white knight up 2 left 1
                                if ((i - 1 >= 0) && (j - 2 >= 0) && board[i - 1, j - 2] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i - 1, j - 2));
                                    allMoves.Add(move);
                                }
                                break;
                            case ChessPiece.WhiteQueen:

                                //white queen down (row)
                                int rj = j;
                                while ((rj + 1 <= 7) && board[i, rj + 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i, rj + 1));
                                    allMoves.Add(move);
                                    rj++;
                                }
                                if ((rj + 1 <= 7) && board[i, rj + 1] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i, rj + 1));
                                    allMoves.Add(move);
                                }
                                //white queen right (col)
                                int ri = i;
                                while ((ri + 1 <= 7) && board[ri + 1, j] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(ri + 1, j));
                                    allMoves.Add(move);
                                    ri++;
                                }
                                if (ri + 1 <= 7 && board[ri + 1, j] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(ri + 1, j));
                                    allMoves.Add(move);
                                }
                                //white queen up (row)
                                rj = j;
                                while ((rj - 1 >= 0) && board[i, rj - 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i, rj - 1));
                                    allMoves.Add(move);
                                    rj--;
                                }
                                if (rj - 1 >= 0 && board[i, rj - 1] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i, rj - 1));
                                    allMoves.Add(move);
                                }
                                //white queen left (col)
                                ri = i;
                                while ((ri - 1 >= 0) && board[ri - 1, j] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(ri - 1, j));
                                    allMoves.Add(move);
                                    ri--;
                                }
                                if (ri - 1 >= 0 && board[ri - 1, j] < ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(ri - 1, j));
                                    allMoves.Add(move);
                                }
                                //white queen down and right (+row)(+col)
                                bi = i;
                                bj = j;
                                while ((bi + 1 <= 7) && (bj + 1 <= 7) && board[bi + 1, bj + 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi + 1, bj + 1));
                                    allMoves.Add(move);
                                    bi++;
                                    bj++;
                                }
                                if ((bi + 1 <= 7) && (bj + 1 <= 7) && board[bi + 1, bj + 1] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi + 1, bj + 1));
                                    allMoves.Add(move);
                                }
                                //white queen up and left (-row)(-col)
                                bi = i;
                                bj = j;
                                while ((bi - 1 >= 0) && (bj - 1 >= 0) && board[bi - 1, bj - 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi - 1, bj - 1));
                                    allMoves.Add(move);
                                    bi--;
                                    bj--;
                                }
                                if ((bi - 1 >= 0) && (bj - 1 >= 0) && board[bi - 1, bj - 1] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi - 1, bj - 1));
                                    allMoves.Add(move);
                                }
                                //white queen down and left (+row)(-col)
                                bi = i;
                                bj = j;
                                while ((bi - 1 >= 0) && (bj + 1 <= 7) && board[bi - 1, bj + 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi - 1, bj + 1));
                                    allMoves.Add(move);
                                    bi--;
                                    bj++;
                                }
                                if ((bi - 1 >= 0) && (bj + 1 <= 7) && board[bi - 1, bj + 1] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi - 1, bj + 1));
                                    allMoves.Add(move);
                                }
                                //white queen up and right (-row)(+col)
                                bi = i;
                                bj = j;
                                while ((bi + 1 <= 7) && (bj - 1 >= 0) && board[bi + 1, bj - 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi + 1, bj - 1));
                                    allMoves.Add(move);
                                    bi++;
                                    bj--;
                                }
                                if ((bi + 1 <= 7) && (bj - 1 >= 0) && board[bi + 1, bj - 1] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(bi + 1, bj - 1));
                                    allMoves.Add(move);
                                }
                                break;
                            case ChessPiece.WhiteRook:

                                //white rook down
                                rj = j;
                                while ((rj + 1 <= 7) && board[i, rj + 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i, rj + 1));
                                    allMoves.Add(move);
                                    rj++;
                                }
                                if ((rj + 1 <= 7) && board[i, rj + 1] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i, rj + 1));
                                    allMoves.Add(move);
                                }
                                //white rook right
                                ri = i;
                                while ((ri + 1 <= 7) && board[ri + 1, j] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(ri + 1, j));
                                    allMoves.Add(move);
                                    ri++;
                                }
                                if (ri + 1 <= 7 && board[ri + 1, j] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(ri + 1, j));
                                    allMoves.Add(move);
                                }
                                //white rook up
                                rj = j;
                                while ((rj - 1 >= 0) && board[i, rj - 1] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i, rj - 1));
                                    allMoves.Add(move);
                                    rj--;
                                }
                                if (rj - 1 >= 0 && board[i, rj - 1] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i, rj - 1));
                                    allMoves.Add(move);
                                }
                                //white rook left
                                ri = i;
                                while ((ri - 1 >= 0) && board[ri - 1, j] == ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(ri - 1, j));
                                    allMoves.Add(move);
                                    ri--;
                                }
                                if (ri - 1 >= 0 && board[ri - 1, j] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(ri - 1, j));
                                    allMoves.Add(move);
                                }
                                break;
                            case ChessPiece.WhiteKing:

                                //White King down 1 row
                                if ((j + 1 <= 7) && board[i, j + 1] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i, j + 1));
                                    allMoves.Add(move);
                                }
                                //White King up 1 row
                                if ((j - 1 >= 0) && board[i, j - 1] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i, j - 1));
                                    allMoves.Add(move);
                                }
                                //White King left 1 col
                                if ((i - 1 >= 0) && board[i - 1, j] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i - 1, j));
                                    allMoves.Add(move);
                                }
                                //White King right 1 col
                                if ((i + 1 <= 7) && board[i + 1, j] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i + 1, j));
                                    allMoves.Add(move);
                                }
                                //White King up (row) and right (col)
                                if ((j - 1 >= 0) && (i + 1 <= 7) && board[i + 1, j - 1] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i + 1, j - 1));
                                    allMoves.Add(move);
                                }
                                //White king down (row) and right (col)
                                if ((j + 1 <= 7) && (i + 1 <= 7) && board[i + 1, j + 1] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i + 1, j + 1));
                                    allMoves.Add(move);
                                }
                                //White king up (row) and left (col)
                                if ((j - 1 >= 0) && (i - 1 >= 0) && board[i - 1, j - 1] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i - 1, j - 1));
                                    allMoves.Add(move);
                                }
                                //White King down (row) and left (col)
                                if ((j + 1 <= 7) && (i - 1 >= 0) && board[i - 1, j + 1] <= ChessPiece.Empty)
                                {
                                    ChessMove move = new ChessMove(from, new ChessLocation(i - 1, j + 1));
                                    allMoves.Add(move);
                                }
                                break;
                        }

                    }
                }
            }
            return allMoves;
        }


        //static List<ChessMove> outOfChecks = new List<ChessMove>();

        ChessFlag Capturable = new ChessFlag();

        public List<ChessMove> setFlags(List<ChessMove> allMoves, ChessBoard board, ChessColor color)
        {
            List<ChessMove> validMoves = new List<ChessMove>();
            ChessColor oppColor = (color == ChessColor.White ? ChessColor.Black : ChessColor.White);
            foreach (ChessMove move in allMoves)
            {
                if (color == ChessColor.White)
                {
                    if (board[move.To] < ChessPiece.Empty)
                    {
                        move.Flag = Capturable;
                    }
                }
                else
                {
                    if (board[move.To] > ChessPiece.Empty)
                    {
                        move.Flag = Capturable;
                    }
                }

                int inCheck = InCheck(move, board, color, 0);
                if (inCheck == 0)//don't move into check or mate
                {
                    int checkTest = InCheck(move, board, oppColor, 0);
                    if (checkTest == 1) //set flag if I put opp in check
                    {
                        move.Flag = ChessFlag.Check;
                    }
                    else if (checkTest == 2) //set flag if I put opp in checkmate
                    {
                        move.Flag = ChessFlag.Checkmate;
                    }
                    validMoves.Add(move.Clone());
                }
            }
            return validMoves;
        }

        public int InCheck(ChessMove move, ChessBoard board, ChessColor testColor, int depth)
        {
            ChessBoard tempBoard = board.Clone();
            tempBoard.MakeMove(move);
            ChessPiece myKing = (testColor == ChessColor.White ? ChessPiece.WhiteKing : ChessPiece.BlackKing);
            ChessColor oppColor = (testColor == ChessColor.White ? ChessColor.Black : ChessColor.White);
            List<ChessMove> allOppMoves = GetAllMoves(tempBoard, oppColor);
            foreach (ChessMove tempMove in allOppMoves)  //what moves can opposition make?
            {
                if (tempBoard[tempMove.To] == myKing)
                {
                    if (depth > 1)
                    {
                        return 1;
                    }
                    List<ChessMove> myNextMoves = GetAllMoves(tempBoard, testColor);
                    foreach (ChessMove kMove in myNextMoves)// can you make a move that will get you out of check
                    {
                        if (InCheck(kMove, tempBoard, testColor, depth + 1) == 0)
                        {
                            Debug.WriteLine("Interior incheck called {0}", kMove);
                            return 1; //check, not mate
                        }
                    }
                    return 2;
                }
            }
            return 0;
        }

        public int evaluateBoard(ChessMove m, ChessBoard board, ChessColor myColor)
        {
            ChessColor oppColor = (myColor == ChessColor.White ? ChessColor.Black : ChessColor.White);
            int mult = (myColor == ChessColor.White ? 1 : -1); //sets negative or positive for values
            int sum = 0;
            if (m.Flag == ChessFlag.Checkmate)
            {
                sum += 2000 * mult;
                Debug.WriteLine("m.Flag is Checkmate {0}", sum);
            }
            else if (m.Flag == ChessFlag.Check)
            {
                sum += 100 * mult;
                Debug.WriteLine("m.Flag is Check {0}", sum);

            }
            if (board[m.To] != ChessPiece.Empty)
            {
                sum += 100;
                Debug.WriteLine("m.To is not empty {0}", mult);
            }
            if (m.Flag == Capturable)
            {
                sum -= 150;
            }
            board.MakeMove(m);

            for (int i=0; i<= 7; i++)
            {
                for (int j=0; j<=7; j++)
                {
                    switch (board[i,j])//the order we access our board in is col, row, hence the [j][i] below
                    {
                        case ChessPiece.WhitePawn:
                            sum += (1 + positionVals[ChessPiece.WhitePawn][j][i]) * mult;
                            //sum += 1 * mult;
                            Debug.WriteLine("WhitePawn {0}", sum);
                            break;
                        case ChessPiece.WhiteRook:
                            sum += (5 + positionVals[ChessPiece.WhiteRook][j][i]) * mult;
                            //sum += 5 * mult;
                            Debug.WriteLine("WhiteRook {0}", sum);
                            break;
                        case ChessPiece.WhiteKnight:
                            sum += (3 + positionVals[ChessPiece.WhiteKnight][j][i]) *mult;
                            //sum += 3 * mult;
                            Debug.WriteLine("WhiteKnight {0}", sum);
                            break;
                        case ChessPiece.WhiteBishop:
                            sum += (3 + positionVals[ChessPiece.WhiteBishop][j][i]) * mult;
                            //sum += 3 * mult;
                            Debug.WriteLine("WhiteBishop {0}", sum);
                            break;
                        case ChessPiece.WhiteQueen:
                            sum += (15 + positionVals[ChessPiece.WhiteQueen][j][i]) * mult;
                            //sum += 9 * mult;
                            Debug.WriteLine("WhiteQueen {0}", sum);
                            break;
                        case ChessPiece.WhiteKing:
                            sum += (10 + positionVals[ChessPiece.WhiteKing][j][i]) * mult;
                            //sum += 4 * mult;
                            Debug.WriteLine("WhiteKing {0}", sum);
                            break;
                        case ChessPiece.BlackPawn:
                            sum += (-1 + positionVals[ChessPiece.BlackPawn][j][i]) * mult;
                            //sum += -1 * mult;
                            Debug.WriteLine("BlackPawn {0}", sum);
                            break;
                        case ChessPiece.BlackRook:
                            sum += (-5 + positionVals[ChessPiece.BlackRook][j][i]) * mult;
                            //sum += -5 * mult;
                            Debug.WriteLine("BlackRook {0}", sum);
                            break;
                        case ChessPiece.BlackKnight:
                            sum += (-3 + positionVals[ChessPiece.BlackKnight][j][i]) * mult;
                            //sum += -3 * mult;
                            Debug.WriteLine("BlackKnight {0}", sum);
                            break;
                        case ChessPiece.BlackBishop:
                            sum += (-3 + positionVals[ChessPiece.BlackBishop][j][i]) * mult;
                            //sum += -3 * mult;
                            Debug.WriteLine("BlackBishop {0}", sum);
                            break;
                        case ChessPiece.BlackQueen:
                            sum += (-15 + positionVals[ChessPiece.BlackQueen][j][i]) * mult;
                            //sum += -9 * mult;
                            Debug.WriteLine("BlackQueen {0}", sum);
                            break;
                        case ChessPiece.BlackKing:
                            sum += (-10 + positionVals[ChessPiece.BlackKing][j][i]) * mult;
                            //sum += -4 * mult;
                            Debug.WriteLine("BlackKing {0}", sum);
                            break;
                        case ChessPiece.Empty:
                            sum += 0;
                            Debug.WriteLine("Empty {0}", sum);
                            break;
                    }
                }
            }
            Debug.WriteLine(sum);
            return sum;
        }

        public List<ChessMove> SortedMoves(List<ChessMove> moves, ChessBoard board, ChessColor myColor)
        {
            foreach (ChessMove m in moves)
            {
                m.ValueOfMove = evaluateBoard(m, board, myColor);
            }
            moves.Sort();
            moves.Reverse();
            return moves;
        }

        const int depthLimit = 4;
        const int MOVE_TIME = 5000;
        const int CHECK_TIME = 700;
        int returnTime;
        const int ALPHA = -99999;
        const int BETA = 99999;
        public ChessMove MiniMaxAB(int depthLimit, ChessBoard board, ChessColor color)
        {
            int alpha = ALPHA;
            int beta = BETA;
            ChessMove bestMove = new ChessMove(null, null);
            bestMove.ValueOfMove = ALPHA;
            ChessColor oppColor = (color == ChessColor.White ? ChessColor.Black : ChessColor.White);
            List<ChessMove> allOppMoves = GetAllMoves(board, color);
            List<ChessMove> moves = setFlags(allOppMoves, board, color);
            List<ChessMove> sortedMoves = SortedMoves(moves, board, color);
            foreach (ChessMove mv in sortedMoves)
            {
                mv.ValueOfMove = maxMoveAB(mv, depthLimit, 0, alpha, beta, board, color);
                Debug.WriteLine("{0}  {1}", mv, mv.ValueOfMove);
                if (mv.ValueOfMove > bestMove.ValueOfMove)
                {
                    bestMove = mv;
                }
            }
            if(bestMove.ValueOfMove == ALPHA)
            {
                bestMove = sortedMoves[random.Next(sortedMoves.Count)];
            }
            return bestMove;
        }


        public int maxMoveAB(ChessMove m, int depthLimit, int currDepth, int alpha, int beta, ChessBoard board, ChessColor color)
        {//looking for the highest minimum value of opposite color
            int v = alpha;
            if (currDepth == depthLimit || timer.ElapsedMilliseconds > returnTime)
            {
                Debug.WriteLine("Evaluating {0}", color);
                v = evaluateBoard(m, board, color);
                return v;
            }
            ChessColor oppColor = (color == ChessColor.White ? ChessColor.Black : ChessColor.White);
            List<ChessMove> allOppMoves = GetAllMoves(board, oppColor);
            List<ChessMove> moves = setFlags(allOppMoves, board, oppColor);
            List<ChessMove> sortedMoves = SortedMoves(moves, board, color);
            foreach (ChessMove mv in sortedMoves)
            {
                v = minMoveAB(mv, depthLimit, currDepth + 1, alpha, beta, board, oppColor);
                if (alpha < v)
                {
                    Debug.WriteLine("New alpha: {0}\n", v);
                    alpha = v;
                }
                return v;
            }
            return v;
        }

        public int minMoveAB(ChessMove m, int depthLimit, int currDepth, int alpha, int beta, ChessBoard board, ChessColor color)
        {//looking for the minimum value of the max values of opposing player
            int v = beta;
            if (currDepth == depthLimit || timer.ElapsedMilliseconds > returnTime)
            {
                Debug.WriteLine("Evaluating {0}", color);
                v = evaluateBoard(m, board, color);
                return v;
            }
            ChessColor oppColor = (color == ChessColor.White ? ChessColor.Black : ChessColor.White);
            List<ChessMove> allOppMoves = GetAllMoves(board, oppColor);
            List<ChessMove> moves = setFlags(allOppMoves, board, oppColor);
            List<ChessMove> sortedMoves = SortedMoves(moves, board, oppColor);
            foreach (ChessMove mv in sortedMoves)
            {
                v = maxMoveAB(mv, depthLimit, currDepth + 1, alpha, beta, board, oppColor);
                if (v < beta)
                {
                    Debug.WriteLine("New beta: {0}\n", v);
                    beta = v;
                }
                return v;
            }
            return v;
        }


        /// <summary>
        /// Evaluates the chess board and decided which move to make. This is the main method of the AI.
        /// The framework will call this method when it's your turn.
        /// </summary>
        /// <param name="board">Current chess board</param>
        /// <param name="yourColor">Your color</param>
        /// <returns> Returns the best chess move the player has for the given chess board</returns>
        public static Stopwatch timer;
        public static HashSet<ChessMove> prevMoves = new HashSet<ChessMove> { };
        public static HashSet<ChessBoard> transposition = new HashSet<ChessBoard>();

        public ChessMove GetNextMove(ChessBoard board, ChessColor myColor)
        {
            ChessMove fake = new ChessMove(new ChessLocation(0, 0), new ChessLocation(0, 0));
            List<ChessMove> moves = GetAllMoves(board, myColor);
            List<ChessMove> validMoves = setFlags(moves, board, myColor);
            timer = Stopwatch.StartNew();
            returnTime = InCheck(fake, board, myColor, 0) > 0 ? CHECK_TIME : MOVE_TIME;
            ChessMove chosenMove = MiniMaxAB(depthLimit, board, myColor);
            Debug.WriteLine("Chosen Move: {0}", chosenMove);
            prevMoves.Add(chosenMove);
            return chosenMove;
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
            List<ChessMove> allMoves = GetAllMoves(boardBeforeMove, colorOfPlayerMoving);
            List<ChessMove> validMoves = setFlags(allMoves, boardBeforeMove, colorOfPlayerMoving);
            if (validMoves.Contains(moveToCheck))
            {
                return true;
            }
            return false;
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
