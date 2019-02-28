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
                { new List<int>() { 4, 6, 2, 0, 0, 2, 6, 4 },
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
                new List<int>() { 10, 10, 10, 10, 10, 10, 10, 10 },
                new List<int>() { 10, 10, 10, 10, 10, 10, 10, 10 }
            },
            [ChessPiece.WhiteKing] = new List<List<int>>()
                { new List<int>() { -6, -8, -8, -10, -10, -8, -8, -6 },
                new List<int>() {-6, -8, -8, -10, -10, -8, -8, -6 },
                new List<int>() {-6, -8, -8, -10, -10, -8, -8, -6 },
                new List<int>() {-6, -8, -8, -10, -10, -8, -8, -6},
                new List<int>() { -4, -6, -6, -8, -8, -6, -6, -4},
                new List<int>() { -2, -4, -4, -4, -4, -4, -4, -2 },
                new List<int>() {4, 4, 0, 0, 0, 0, 4, 4 },
                new List<int>() {4, 6, 2, 0, 0, 2, 6, 4 },
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
                new List<int>() { 10, 10, 10, 10, 10, 10, 10, 10 },
                new List<int>() {10, 10, 10, 10, 10, 10, 10, 10 },
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


        // Static Initialize of Capture Values
        static Dictionary<ChessPiece, int> BlackPieces = new Dictionary<ChessPiece, int>()
        {
            { ChessPiece.BlackQueen, 10 },
            { ChessPiece.BlackBishop, 7 },
            { ChessPiece.BlackRook, 5 },
            { ChessPiece.BlackKnight, 3 },
            { ChessPiece.BlackPawn, 1 }
        };
        static Dictionary<ChessPiece, int> WhitePieces = new Dictionary<ChessPiece, int>()
        {
            { ChessPiece.WhiteQueen, 10 },
            { ChessPiece.WhiteBishop, 7 },
            { ChessPiece.WhiteRook, 5 },
            { ChessPiece.WhiteKnight, 3 },
            { ChessPiece.WhitePawn, 1 }
        };
        public int evaluateBoard(ChessBoard board, ChessBoard pBoard, ChessMove mv, ChessColor myColor)
        {
            // Declare needed variables
            ChessColor oppColor = (myColor == ChessColor.White ? ChessColor.Black : ChessColor.White);
            int mult = (myColor == ChessColor.White ? 1 : -1); // Forces us to always be positive
            int sum = 0;
            sum += positionVals[pBoard[mv.From]][mv.To.X][mv.To.Y];


            // Determine if the player is in check, if so, adjust score accordingly.
            if (mv.Flag == ChessFlag.Checkmate)
            {
                sum += 1000 * mult;
            }
            else if (mv.Flag == ChessFlag.Check)
            {
                sum += 100 * mult;
            }

            // Check to see if the move is going to take a piece
            if (oppColor == ChessColor.Black)
            {
                if (BlackPieces.ContainsKey(pBoard[mv.To]))
                {
                    sum += BlackPieces[pBoard[mv.To]] * mult;
                }
            }
            else
            {
                if (WhitePieces.ContainsKey(pBoard[mv.To]))
                {
                    sum += WhitePieces[pBoard[mv.To]] * mult;
                }
            }

            // Total up all the pieces on the board
            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    switch (board[i, j])//the order we access our board in is col, row, hence the [j][i] below
                    {
                        case ChessPiece.WhitePawn:
                            sum += 1 * mult;
                            break;
                        case ChessPiece.WhiteRook:
                            sum += 5 * mult;
                            break;
                        case ChessPiece.WhiteKnight:
                            sum += 3 * mult;
                            break;
                        case ChessPiece.WhiteBishop:
                            sum += 4 * mult;
                            break;
                        case ChessPiece.WhiteQueen:
                            sum += 9 * mult;
                            break;
                        case ChessPiece.WhiteKing:
                            sum += 4 * mult;
                            break;
                        case ChessPiece.BlackPawn:
                            sum += -1 * mult;
                            break;
                        case ChessPiece.BlackRook:
                            sum += -5 * mult;
                            break;
                        case ChessPiece.BlackKnight:
                            sum += -3 * mult;
                            break;
                        case ChessPiece.BlackBishop:
                            sum += -4 * mult;
                            break;
                        case ChessPiece.BlackQueen:
                            sum += -9 * mult;
                            break;
                        case ChessPiece.BlackKing:
                            sum += -4 * mult;
                            break;
                        default:
                            break;
                    }
                }
            }
            return sum;
        }


        public List<ChessMove> OpponentCheckMoves(ChessBoard board, ChessColor oppColor, ChessLocation kingLocation) 
        {
            // Needed variables
            ChessPiece MyKing = (oppColor == ChessColor.White ? ChessPiece.WhiteKing : ChessPiece.BlackKing);
            ChessColor TheirColor = (oppColor == ChessColor.White ? ChessColor.Black : ChessColor.White);
            ChessPiece TheirKing = (oppColor == ChessColor.White ? ChessPiece.BlackKing : ChessPiece.WhiteKing);

            // Determine how many available moves the king has to get out of check
            List<ChessMove> oppMoves = GetAllMoves(board, oppColor);
            for(int i = 0; i < oppMoves.Count; i++)
            {
                //Clone the board, make the move
                ChessBoard tempBoard = board.Clone();
                tempBoard.MakeMove(oppMoves[i]);

                //Determine if the opponent is still in check
                Tuple<ChessLocation, ChessLocation> kings = FindKings(tempBoard, MyKing, TheirKing);
                if (KingInCheck(tempBoard, TheirColor, kings.Item1))
                {
                    oppMoves.Remove(oppMoves[i]);
                }
            }

            return oppMoves;
        }


        // Used to determine if the given player color is incheck or not
        // Return false: King not in Check
        // Return true: King in Check
        public bool KingInCheck(ChessBoard board, ChessColor oppColor, ChessLocation kingLocation)
        {
            // Determine if the king is in check
            List<ChessMove> oppMoves = GetAllMoves(board, oppColor);
            bool check = false;
            foreach (ChessMove mv in oppMoves)
            {
                if (mv.To == kingLocation)
                {
                    check = true;
                    break;
                }
            }
            return check;
        }

        public Tuple<ChessLocation, ChessLocation> FindKings(ChessBoard board, ChessPiece myKing, ChessPiece theirKing)
        {
            ChessLocation myKingLocation = new ChessLocation(0, 0);
            ChessLocation theirKingLocation = new ChessLocation(0, 0);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] == myKing)
                    {
                        myKingLocation = new ChessLocation(i, j);
                    }
                    else if (board[i, j] == theirKing)
                    {
                        theirKingLocation = new ChessLocation(i, j);
                    }
                }
            }

            return Tuple.Create(myKingLocation, theirKingLocation);
        }


        // Returns a list of moves that will get the player out of check
        public List<ChessMove> GetAllValidMoves(ChessBoard board, ChessColor myColor)
        {
            //Needed Variables
            ChessColor opponentColor = (myColor == ChessColor.White ? ChessColor.Black : ChessColor.White);

            // Find both kings on the board
            // Note: Item1 = MyKing, Item2 = TheirKing
            ChessPiece MyKing = (myColor == ChessColor.White ? ChessPiece.WhiteKing : ChessPiece.BlackKing);
            ChessPiece TheirKing = (myColor == ChessColor.White ? ChessPiece.BlackKing : ChessPiece.WhiteKing);
            Tuple<ChessLocation, ChessLocation> KingLocations = FindKings(board, MyKing, TheirKing);

            // Filter out all moves that don't get us out of check, or move us into check
            List<ChessMove> allMyMoves = GetAllMoves(board, myColor);
            List<ChessMove> filteredMoves = new List<ChessMove>();
            if (KingInCheck(board, opponentColor, KingLocations.Item1))
            {
                for(int i = 0; i < allMyMoves.Count; i++)
                {
                    // Check to see if the king is moving
                    ChessLocation kingLoc = KingLocations.Item1;
                    if (allMyMoves[i].From == kingLoc)
                    {
                        kingLoc = allMyMoves[i].To;
                    }

                    // Remove all moves that keep us in check
                    ChessBoard tempBoard = board.Clone();
                    tempBoard.MakeMove(allMyMoves[i]);
                    if (!KingInCheck(tempBoard, opponentColor, kingLoc))
                    {
                        filteredMoves.Add(allMyMoves[i]);
                    }
                }
            }
            else
            {
                filteredMoves = allMyMoves;
            }


            // Determine which moves place my opponent in check or checkmate
            foreach(ChessMove mv in filteredMoves)
            {
                // Clone the Board and make the move
                ChessBoard tempBoard = board.Clone();
                tempBoard.MakeMove(mv);
                if (KingInCheck(tempBoard, myColor, KingLocations.Item2))
                {
                    // Determine if it is checkmate
                    if (OpponentCheckMoves(tempBoard, opponentColor, KingLocations.Item2).Count == 0)
                    {
                        mv.Flag = ChessFlag.Checkmate;
                    } 
                    else
                    {
                        mv.Flag = ChessFlag.Check;
                    }
                }
            }

            // Return all the valid moves
            return filteredMoves;
        }


        // Static Members for MiniMax
        static int dLimit = 3;
        static Random rnd = new Random();
        static ChessBoard prevBoard = new ChessBoard();
        public ChessMove MiniMax(ChessBoard board, ChessColor color)
        {
            // Variable Declarations
            DecisionTree dt = new DecisionTree(board);
            double alpha = Double.NegativeInfinity;
            double beta = Double.PositiveInfinity;
            double v = Double.NegativeInfinity;
            ChessColor oppColor = (color == ChessColor.White ? ChessColor.Black : ChessColor.White);
            List<ChessMove> moves = GetAllValidMoves(board, color);

            //Loop over list of moves
            ChessMove move = new ChessMove(new ChessLocation(0,0), new ChessLocation(0,1));
            foreach (ChessMove mv in moves)
            {
                // Previous MaxValue
                prevBoard = board;
                ChessBoard tempBoard = board.Clone();

                // Create decision tree and grab the largest value
                tempBoard.MakeMove(mv);
                dt.AddChild(tempBoard, mv);
                v = Math.Max(MinValue(dt, tempBoard, mv, oppColor, alpha, beta, 1), v);

                // Check if previous max is < v
                if (v > alpha)
                {
                    move = mv;
                }

                alpha = Math.Max(alpha, v);

                // Check to see if v >= beta for Alpha-Beta Pruning
                if (beta <= alpha)
                {
                    break;
                }

                v = 0;
            }

            // Return the best move
            dt.BestChildMove = move;
            Debug.WriteLine($"{v}, {move}");
            return move;
        }

        public double MaxValue(DecisionTree dt, ChessBoard board, ChessMove mv, ChessColor color, double alpha, double beta, int depth)
        {
            // Terminating Cases
            if (depth == dLimit) //TODO: Add timer
            {
                // Evaluate the given board
                ChessColor tryColor = (color == ChessColor.White ? ChessColor.Black : ChessColor.White);
                int eval = evaluateBoard(board, prevBoard, mv, tryColor);
                return eval;
            }

            // Generate all moves for the current board
            ChessColor oppColor = (color == ChessColor.White ? ChessColor.Black : ChessColor.White);
            List<ChessMove> moves = GetAllValidMoves(board, color);
            double v = Double.NegativeInfinity;
            double localAlpha = Double.NegativeInfinity;
            foreach (ChessMove nm in moves)
            {
                // Previous MaxValue and oppcolor to pass into MinValue
                prevBoard = board;
                double prevMax = v;
                ChessBoard tempBoard = board.Clone();

                // Create decision tree and grab the largest value
                tempBoard.MakeMove(nm);
                dt.AddChild(tempBoard, nm);
                v = Math.Max(MinValue(dt, tempBoard, nm, oppColor, localAlpha, beta, depth + 1), v);

                // Check if previous max is < v
                if (v > prevMax)
                {
                    dt.BestChildMove = nm;
                }

                localAlpha = Math.Max(alpha, v);

                // Check to see if v >= beta for Alpha-Beta Pruning
                if (beta <= localAlpha)
                {
                    break;
                }
            }
            return v;
        }

        public double MinValue(DecisionTree dt, ChessBoard board, ChessMove mv, ChessColor color, double alpha, double beta, int depth)
        {
            // Terminating Cases
            if (depth == dLimit) //TODO: Add timer
            {
                // Evaluate the given board
                ChessColor tryColor = (color == ChessColor.White ? ChessColor.Black : ChessColor.White);
                int eval = evaluateBoard(board, prevBoard, mv, tryColor);
                return eval;
            }

            // Generate all moves for the current board
            ChessColor oppColor = (color == ChessColor.White ? ChessColor.Black : ChessColor.White);
            List<ChessMove> moves = GetAllValidMoves(board, color);
            double v = Double.PositiveInfinity;
            double localBeta = Double.PositiveInfinity;
            foreach (ChessMove nm in moves)
            {
                // Previous MinValue and oppcolor to pass into MaxValue
                prevBoard = board;
                double prevMin = v;
                ChessBoard tempBoard = board.Clone();

                // Create decision tree and grab the largest value
                tempBoard.MakeMove(nm);
                dt.AddChild(tempBoard, nm);
                v = Math.Min(MaxValue(dt, tempBoard, nm, oppColor, alpha, localBeta, depth + 1), v);

                // Check if previous max is < v
                if (v > prevMin)
                {
                    dt.BestChildMove = nm;
                }

                localBeta = Math.Min(beta, v);

                // Check to see if v >= beta for Alpha-Beta Pruning
                if (localBeta <= alpha)
                {
                    break;
                }
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
            ChessMove chosenMove = MiniMax(board, myColor); // Minimax gets all the moves
            if (chosenMove == null || prevMoves.Contains(chosenMove))
            {
                List<ChessMove> moves = GetAllValidMoves(board, myColor);
                foreach(ChessMove move in moves)
                {
                    if (move.Flag == ChessFlag.Checkmate)
                    {
                        return move;
                    }
                    if (move.Flag == ChessFlag.Check)
                    {
                        return move;
                    }
                    Random rand = new Random();
                    return moves[rand.Next(0, moves.Count)];
                }
            }
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
            List<ChessMove> validMoves = GetAllValidMoves(boardBeforeMove, colorOfPlayerMoving);
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
