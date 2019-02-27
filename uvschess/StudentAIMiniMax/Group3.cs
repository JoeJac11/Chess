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
                new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 }
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
                new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 },
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


        static List<ChessMove> outOfChecks = new List<ChessMove>();
        
        public List<ChessMove> setFlags(List<ChessMove> allMoves, ChessBoard board, ChessColor color)
        {
            ChessMove fake = new ChessMove(new ChessLocation(0, 0), new ChessLocation(0, 0));
            int alreadyInCheck = InCheck(fake, board, color, 0);
            if (alreadyInCheck != 0)
            {
                allMoves = outOfChecks;
            }
            List<ChessMove> validMoves = new List<ChessMove>();
            ChessColor oppColor;
            if (color == ChessColor.White)
            {
                oppColor = ChessColor.Black;
            }
            else
            {
                oppColor = ChessColor.White;
            }
            //check to see if any of these possible moves are invalid because they put me in check
            foreach (ChessMove move in allMoves)
            {
                int inCheck = InCheck(move, board, color, 0);
                Debug.WriteLine("Move is {0}  inCheck returned {1}", move, inCheck);
                if (inCheck == 0)//make sure I'm not in check or mate
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
            outOfChecks.Clear();
            return validMoves;
        }


        public int InCheck(ChessMove move, ChessBoard board, ChessColor testColor, int depth)
        {
            ChessMove saveMoves = new ChessMove(new ChessLocation(0, 0), new ChessLocation(0, 0));
            ChessBoard tempBoard = board.Clone();
            tempBoard.MakeMove(move);
            ChessPiece myKing = (testColor == ChessColor.White ? ChessPiece.WhiteKing : ChessPiece.BlackKing);
            ChessColor oppColor = (testColor == ChessColor.White ? ChessColor.Black : ChessColor.White);

            foreach (ChessMove tempMove in GetAllMoves(tempBoard, oppColor))  //what moves can opposition make?
            {
                if (tempBoard[tempMove.To] == myKing)
                {
                    if (depth > 1)
                    {
                        Debug.WriteLine("Depth > 1 incheck called {0}", tempMove);
                        return 1;
                    }
                    foreach (ChessMove kMove in GetAllMoves(tempBoard, testColor))// can you make a move that will get you out of check
                    {
                        if (InCheck(kMove, tempBoard, testColor, depth + 1) == 0)
                        {
                            Debug.WriteLine("Interior incheck called {0}", kMove);
                            if (move == saveMoves)
                            {
                                outOfChecks.Add(kMove);
                            }
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
            int inCheck = InCheck(m, board, myColor, 0);
            if (inCheck == 1)
            {
                sum += -100 * mult;
            }
            if (inCheck == 2)
            {
                sum += -1000 * mult;
            }
            board.MakeMove(m);
            for (int i=0; i<= 7; i++)
            {
                for (int j=0; j<=7; j++)
                {
                    switch (board[i,j])//the order we access our board in is col, row, hence the [j][i] below
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
                            sum += 3 * mult;
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
                            sum += -3 * mult;
                            break;
                        case ChessPiece.BlackQueen:
                            sum += -9 * mult;
                            break;
                        case ChessPiece.BlackKing:
                            sum += -4 * mult;
                            break;
                        case ChessPiece.Empty:
                            sum += 0;
                            break;
                    }
                }
            }
            return sum;
        }


        public List<ChessMove> SortedMoves(List<ChessMove> moves, ChessBoard board, ChessColor myColor)
        {
            foreach (ChessMove m in moves)
            {
                m.ValueOfMove = evaluateBoard(m, board, myColor);
            }
            moves.Sort();
            return moves;
        }
        const int depthLimit = 5;
        const int RETURN_TIME = 5000;
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
            foreach (ChessMove mv in SortedMoves(moves, board, color))
            {
                mv.ValueOfMove = maxMoveAB(mv, depthLimit, 0, alpha, beta, board, color);
                Debug.WriteLine("{0}  {1}", mv, mv.ValueOfMove);
                if (mv.ValueOfMove > bestMove.ValueOfMove)
                {
                    bestMove = mv;
                }
            }
            return bestMove;
        }


        public int maxMoveAB(ChessMove m, int depthLimit, int currDepth, int alpha, int beta, ChessBoard board, ChessColor color)
        {//looking for the highest minimum value of opposite color
            int v = alpha;
            if (currDepth == depthLimit || timer.ElapsedMilliseconds > RETURN_TIME)
            {
                v = evaluateBoard(m, board, color);
                return v;
            }
            ChessColor oppColor = (color == ChessColor.White ? ChessColor.Black : ChessColor.White);
            List<ChessMove> allOppMoves = GetAllMoves(board, oppColor);
            List<ChessMove> moves = setFlags(allOppMoves, board, oppColor);
            foreach (ChessMove mv in SortedMoves(moves, board, color))
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
            if (currDepth == depthLimit || timer.ElapsedMilliseconds > RETURN_TIME)
            {
                v = evaluateBoard(m, board, color);
                return v;
            }
            ChessColor oppColor = (color == ChessColor.White ? ChessColor.Black : ChessColor.White);
            List<ChessMove> allOppMoves = GetAllMoves(board, oppColor);
            List<ChessMove> moves = setFlags(allOppMoves, board, oppColor);
            foreach (ChessMove mv in SortedMoves(moves, board, oppColor))
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

        // Static Members for MiniMax
        static int dLimit = 20;
        static int rTime = 5000;
        public ChessMove MiniMax(List<ChessMove> moves, ChessBoard board, ChessColor color)
        {
            // Variable Declarations
            DecisionTree dt = new DecisionTree(board);
            double alpha = Double.NegativeInfinity;
            double beta = Double.PositiveInfinity;
            double v = Double.NegativeInfinity;
            ChessColor oppColor = (color == ChessColor.White ? ChessColor.Black : ChessColor.White);
            ChessMove move = null;

            // Get the first MaxValue()
            List<ChessMove> sMoves = SortedMoves(moves, board, color);
            foreach (ChessMove mv in sMoves)
            {
                // Previous MaxValue
                double prevMax = v;
                ChessBoard tempBoard = board.Clone();

                // Create decision tree and grab the largest value
                tempBoard.MakeMove(mv);
                dt.AddChild(tempBoard, mv);
                v = Math.Max(MinValue(dt, board, mv, oppColor, alpha, beta, 1), v);

                // Check if previous max is < v
                if (prevMax < v)
                {
                    move = mv;
                }

                // Check to see if v >= beta for Alpha-Beta Pruning
                if (v >= beta)
                {
                    break;
                }

                alpha = Math.Max(alpha, v);
            }

            // Return the best move
            dt.BestChildMove = move;
            return move;
        }

        public double MaxValue(DecisionTree dt, ChessBoard board, ChessMove mv, ChessColor color, double alpha, double beta, int depth)
        {
            // Terminating Cases
            if (depth == dLimit || timer.ElapsedMilliseconds > rTime) //TODO: Add timer
            {
                Debug.WriteLine(timer.ElapsedMilliseconds);
                Debug.WriteLine(depth);
                return evaluateBoard(mv, board, color);
            }

            // Generate all moves for the current board
            ChessColor oppColor = (color == ChessColor.White ? ChessColor.Black : ChessColor.White);
            List<ChessMove> allMoves = GetAllMoves(board, oppColor);
            List<ChessMove> moves = setFlags(allMoves, board, oppColor);
            List<ChessMove> sMoves = SortedMoves(moves, board, oppColor);
            double v = mv.ValueOfMove;
            foreach (ChessMove nm in moves)
            {
                // Previous MaxValue and oppcolor to pass into MinValue
                double prevMax = v;
                ChessBoard tempBoard = board.Clone();

                // Create decision tree and grab the largest value
                tempBoard.MakeMove(nm);
                dt.AddChild(tempBoard, nm);
                v = Math.Max(MinValue(dt, board, nm, oppColor, alpha, beta, depth + 1), v);

                // Check if previous max is < v
                if (prevMax < v)
                {
                    dt.BestChildMove = nm;
                }

                // Check to see if v >= beta for Alpha-Beta Pruning
                if (v >= beta)
                {
                    break;
                }

                alpha = Math.Max(alpha, v);
            }
            return v;
        }

        public double MinValue(DecisionTree dt, ChessBoard board, ChessMove mv, ChessColor color, double alpha, double beta, int depth)
        {
            // Terminating Cases
            if (depth == dLimit || timer.ElapsedMilliseconds > rTime) //TODO: Add timer
            {
                Debug.WriteLine(timer.ElapsedMilliseconds);
                Debug.WriteLine(depth);
                return evaluateBoard(mv, board, color);
            }

            // Generate all moves for the current board
            ChessColor oppColor = (color == ChessColor.White ? ChessColor.Black : ChessColor.White);
            List<ChessMove> allMoves = GetAllMoves(board, oppColor);
            List<ChessMove> moves = setFlags(allMoves, board, oppColor);
            List<ChessMove> sMoves = SortedMoves(moves, board, oppColor);
            double v = mv.ValueOfMove;
            foreach (ChessMove nm in moves)
            {
                // Previous MinValue and oppcolor to pass into MaxValue
                double prevMin = v;
                ChessBoard tempBoard = board.Clone();

                // Create decision tree and grab the largest value
                tempBoard.MakeMove(nm);
                dt.AddChild(tempBoard, nm);
                v = Math.Min(MaxValue(dt, board, nm, oppColor, alpha, beta, depth + 1), v);

                // Check if previous max is < v
                if (prevMin > v)
                {
                    dt.BestChildMove = nm;
                }

                // Check to see if v >= beta for Alpha-Beta Pruning
                if (v <= beta)
                {
                    break;
                }

                beta = Math.Min(beta, v);
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
            List<ChessMove> moves = GetAllMoves(board, myColor);
            List<ChessMove> validMoves = setFlags(moves, board, myColor);
            timer = Stopwatch.StartNew();
            ChessMove chosenMove = MiniMaxAB(depthLimit, board, myColor);
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
