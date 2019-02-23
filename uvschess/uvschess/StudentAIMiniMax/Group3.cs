using System;
using System.Collections.Generic;
using System.Text;
using UvsChess;

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
                                if (ri - 1 >= 0 && board[ri - 1, j] > ChessPiece.Empty)
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
                                if ((bi - 1 >= 0) && (bj - 1 >= 0) && board[bi - 1, bj - 1] > ChessPiece.Empty)
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
                                if ((bi - 1 >= 0) && (bj + 1 <= 7) && board[bi - 1, bj + 1] > ChessPiece.Empty)
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
                                if ((bi + 1 <= 7) && (bj - 1 >= 0) && board[bi + 1, bj - 1] > ChessPiece.Empty)
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
                                if ((bi + 1 <= 7) && (bj + 1 <= 7) && board[bi + 1, bj + 1] > ChessPiece.Empty)
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
                                if ((bi - 1 >= 0) && (bj - 1 >= 0) && board[bi - 1, bj - 1] > ChessPiece.Empty)
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
                                if ((bi - 1 >= 0) && (bj + 1 <= 7) && board[bi - 1, bj + 1] > ChessPiece.Empty)
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
                                if ((bi + 1 <= 7) && (bj - 1 >= 0) && board[bi + 1, bj - 1] > ChessPiece.Empty)
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
                                if ((rj + 1 <= 7) && board[i, rj + 1] > ChessPiece.Empty)
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
                                if (ri + 1 <= 7 && board[ri + 1, j] > ChessPiece.Empty)
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
                                if (rj - 1 >= 0 && board[i, rj - 1] > ChessPiece.Empty)
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
                                if (ri - 1 >= 0 && board[ri - 1, j] > ChessPiece.Empty)
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
                                if ((bi + 1 <= 7) && (bj + 1 <= 7) && board[bi + 1, bj + 1] < ChessPiece.Empty)
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
                                if ((bi - 1 >= 0) && (bj - 1 >= 0) && board[bi - 1, bj - 1] < ChessPiece.Empty)
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
                                if ((bi - 1 >= 0) && (bj + 1 <= 7) && board[bi - 1, bj + 1] < ChessPiece.Empty)
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
                                if ((bi + 1 <= 7) && (bj - 1 >= 0) && board[bi + 1, bj - 1] < ChessPiece.Empty)
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
                                if ((rj + 1 <= 7) && board[i, rj + 1] < ChessPiece.Empty)
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
                                if (ri + 1 <= 7 && board[ri + 1, j] < ChessPiece.Empty)
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
                                if (rj - 1 >= 0 && board[i, rj - 1] < ChessPiece.Empty)
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
                                if ((bi + 1 <= 7) && (bj + 1 <= 7) && board[bi + 1, bj + 1] < ChessPiece.Empty)
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
                                if ((bi - 1 >= 0) && (bj - 1 >= 0) && board[bi - 1, bj - 1] < ChessPiece.Empty)
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
                                if ((bi - 1 >= 0) && (bj + 1 <= 7) && board[bi - 1, bj + 1] < ChessPiece.Empty)
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
                                if ((bi + 1 <= 7) && (bj - 1 >= 0) && board[bi + 1, bj - 1] < ChessPiece.Empty)
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
                                if ((rj + 1 <= 7) && board[i, rj + 1] < ChessPiece.Empty)
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
                                if (ri + 1 <= 7 && board[ri + 1, j] < ChessPiece.Empty)
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
                                if (rj - 1 >= 0 && board[i, rj - 1] < ChessPiece.Empty)
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
                                if (ri - 1 >= 0 && board[ri - 1, j] < ChessPiece.Empty)
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

        public List<ChessMove> setFlags(List<ChessMove> allMoves, ChessBoard board, ChessColor color)
        {
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
                if (InCheck(move, board, color, false) == 0)//make sure I'm not in check or mate
                {
                    int checkTest = InCheck(move, board, oppColor, false);
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

        public void AddAllPossibleMovesToDecisionTree(List<ChessMove> allMyMoves, ChessMove myChosenMove,
                                                      ChessBoard currentBoard, ChessColor myColor)
        {
            // Create the decision tree object
            DecisionTree dt = new DecisionTree(currentBoard);

            // Tell UvsChess about the decision tree object
            SetDecisionTree(dt);
            dt.BestChildMove = myChosenMove;

            // Go through all of my moves, add them to the decision tree
            // Then go through each of these moves and generate all of my
            // opponents moves and add those to the decision tree as well.
            for (int ix = 0; ix < allMyMoves.Count; ix++)
            {
                ChessMove myCurMove = allMyMoves[ix];
                ChessBoard boardAfterMyCurMove = currentBoard.Clone();
                boardAfterMyCurMove.MakeMove(myCurMove);

                // Add the new move and board to the decision tree
                dt.AddChild(boardAfterMyCurMove, myCurMove);

                // Descend the decision tree to the last child added so we can 
                // add all of the opponents response moves to our move.
                dt = dt.LastChild;

                // Get all of the opponents response moves to my move
                ChessColor oppColor = (myColor == ChessColor.White ? ChessColor.Black : ChessColor.White);
                List<ChessMove> allOppMoves = GetAllMoves(boardAfterMyCurMove, oppColor);

                // Go through all of my opponent moves and add them to the decision tree
                foreach (ChessMove oppCurMove in allOppMoves)
                {
                    ChessBoard boardAfterOppCurMove = boardAfterMyCurMove.Clone();
                    boardAfterOppCurMove.MakeMove(oppCurMove);
                    dt.AddChild(boardAfterOppCurMove, oppCurMove);

                    // Setting all of the opponents eventual move values to 0 (see below).
                    dt.LastChild.EventualMoveValue = "0";
                }

                if (allOppMoves.Count > 0)
                {
                    // Tell the decision tree which move we think our opponent will choose.
                    int chosenOppMoveNumber = random.Next(allOppMoves.Count);
                    dt.BestChildMove = allOppMoves[chosenOppMoveNumber];
                }

                // Tell the decision tree what this moves eventual value will be.
                // Since this AI can't evaulate anything, I'm just going to set this
                // value to 0.
                dt.EventualMoveValue = evaluateBoard(myChosenMove, boardAfterMyCurMove, myColor).ToString();

                // All of the opponents response moves have been added to this childs move, 
                // so return to the parent so we can do the loop again for our next move.
                dt = dt.Parent;
            }
        }
        public int InCheck(ChessMove move, ChessBoard board, ChessColor testColor, bool ckMate)
        {
            ChessBoard tempBoard = board.Clone();
            tempBoard.MakeMove(move);
            ChessPiece myKing = (testColor == ChessColor.White ? ChessPiece.WhiteKing : ChessPiece.BlackKing);
            ChessColor oppColor = (testColor == ChessColor.White ? ChessColor.Black : ChessColor.White);

            foreach (ChessMove tempMove in GetAllMoves(tempBoard, oppColor))  //what moves can opposition make?
            {
                if (tempBoard[tempMove.To] == myKing) //but we don't want to move right next to the king. How to fix?
                {
                    if (ckMate)
                    {
                        return 1;
                    }
                    foreach (ChessMove kMove in GetAllMoves(tempBoard, testColor))// can you make a move that will get you out of check
                    {
                        if (InCheck(kMove, tempBoard, testColor, true) == 0)
                        {
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
            if(m == null)
            {
                return 0;
            }
            ChessColor oppColor = (myColor == ChessColor.White ? ChessColor.Black : ChessColor.White);
            int mult = (myColor == ChessColor.White ? 1 : -1); //sets negative or positive for values
            ChessBoard newBoard = board.Clone();
            newBoard.MakeMove(m);
            int sum = 0;
            int blackCheck = InCheck(m, board, ChessColor.White, false);
            int whiteCheck = InCheck(m, board, ChessColor.Black, false);
            if (whiteCheck == 1)
            {
                sum += 7 * mult;
            }
            if (whiteCheck == 2)
            {
                sum += 1000 * mult;
            }
            if (blackCheck == 1)
            {
                sum += -7 * mult;
            }
            if (blackCheck == 2)
            {
                sum += -1000 * mult;
            }
            for (int i=0; i<= 7; i++)
            {
                for (int j=0; j<=7; j++)
                {
                    switch (newBoard[i,j])//the order we access our board in is col, row, hence the [j][i] below
                    {
                        case ChessPiece.WhitePawn:
                            sum += 1 * mult * positionVals[ChessPiece.WhitePawn][j][i];
                            break;
                        case ChessPiece.WhiteRook:
                            sum += 5 * mult * positionVals[ChessPiece.WhiteRook][j][i];
                            break;
                        case ChessPiece.WhiteKnight:
                            sum += 3 * mult * positionVals[ChessPiece.WhiteKnight][j][i];
                            break;
                        case ChessPiece.WhiteBishop:
                            sum += 3 * mult * positionVals[ChessPiece.WhiteBishop][j][i];
                            break;
                        case ChessPiece.WhiteQueen:
                            sum += 9 * mult * positionVals[ChessPiece.WhiteQueen][j][i];
                            break;
                        case ChessPiece.WhiteKing:
                            sum += 4 * mult * positionVals[ChessPiece.WhiteKing][j][i];
                            break;
                        case ChessPiece.BlackPawn:
                            sum += -1 * mult * positionVals[ChessPiece.BlackPawn][j][i];
                            break;
                        case ChessPiece.BlackRook:
                            sum += -5 * mult * positionVals[ChessPiece.BlackRook][j][i];
                            break;
                        case ChessPiece.BlackKnight:
                            sum += -3 * mult * positionVals[ChessPiece.BlackKnight][j][i];
                            break;
                        case ChessPiece.BlackBishop:
                            sum += -3 * mult * positionVals[ChessPiece.BlackBishop][j][i];
                            break;
                        case ChessPiece.BlackQueen:
                            sum += -9 * mult * positionVals[ChessPiece.BlackQueen][j][i];
                            break;
                        case ChessPiece.BlackKing:
                            sum += -4 * mult * positionVals[ChessPiece.BlackKing][j][i];
                            break;
                        case ChessPiece.Empty:
                            sum += 0;
                            break;
                    }
                }
            }
            return sum;
        }
        
        public ChessMove minimaxAB(List<ChessMove> moves, int depthLimit, ChessBoard board, ChessColor color)
        {
            ChessMove move = null;
            ChessMove begin = moves[random.Next(moves.Count)];
            for (int dl = 1; dl <= depthLimit; dl++)
            {
                move = maxMoveAB(begin, dl, 0, -999999, 999999, board, color);
            }
            if (move == null)
            {
                return begin;
            }
            else
            {
                return move;
            }
        }

        public ChessMove minMoveAB(ChessMove m, int depthLimit, int currDepth, int alpha, int beta, ChessBoard board, ChessColor color)
        {
            ChessMove bestMove = null;
            //ChessMove goodMove = null;
            ChessMove move = null;
            TimeSpan sofar = DateTime.Now - start;
            Console.WriteLine(sofar);
            if (currDepth >= depthLimit)
            {
                return m;
            }
            else if (DateTime.Now - start > timeLimit)
            {
                return bestMove;
            }
            else
            {
                List<ChessMove> moves = GetAllMoves(board, color);
                List<ChessMove> validMoves = setFlags(moves, board, color);
                int choiceInd = random.Next(validMoves.Count);
                ChessMove choice = validMoves[choiceInd];
                AddAllPossibleMovesToDecisionTree(validMoves, choice, board, color);
                for (int i = choiceInd; i < validMoves.Count; i++)
                {
                    validMoves[i].ValueOfMove = evaluateBoard(validMoves[i], board, color == ChessColor.White ? ChessColor.Black : ChessColor.White);
                    int bestVal;
                    if (bestMove == null)
                    {
                        bestVal = 999999;
                    }
                    else
                    {
                        bestVal = bestMove.ValueOfMove;
                    }
                    beta = Math.Min(beta, bestVal);
                    if (beta <= alpha)
                    {
                        continue;
                    }
                    if (prevMoves.Contains(validMoves[i]))
                    {
                        continue;
                    }
                    move = maxMoveAB(validMoves[i], depthLimit, currDepth + 1, alpha, beta, board, color);

                    if (bestMove == null || evaluateBoard(move, board, color == ChessColor.White ? ChessColor.Black : ChessColor.White)
                    < bestVal)
                    {
                        //goodMove = move;
                        bestMove = validMoves[i];
                    }
                }
                if (bestMove == null)
                { return m; }
                else
                { return bestMove; }
            }
        }

        public ChessMove maxMoveAB(ChessMove m, int depthLimit, int currDepth, int alpha, int beta, ChessBoard board, ChessColor color)
        {
            ChessMove bestMove = null;
            //ChessMove goodMove = null;
            ChessMove move = null;
            TimeSpan sofar = DateTime.Now - start;
            if (currDepth >= depthLimit)
            {
                return m;
            }
            else if (DateTime.Now - start > timeLimit)
            { return bestMove; }
            else
            {
                List<ChessMove> moves = GetAllMoves(board, color);
                List<ChessMove> validMoves = setFlags(moves, board, color);
                int choiceInd = random.Next(validMoves.Count);
                ChessMove choice = validMoves[choiceInd];
                AddAllPossibleMovesToDecisionTree(validMoves, choice, board, color);
                for (int i = choiceInd; i<validMoves.Count; i++)
                {
                    validMoves[i].ValueOfMove = evaluateBoard(validMoves[i], board, color == ChessColor.White ? ChessColor.Black : ChessColor.White);
                    int bestVal;
                    if (bestMove == null)
                    {
                        bestVal = -999999;
                    }
                    else
                    {
                        bestVal = bestMove.ValueOfMove;
                    }
                    alpha = Math.Max(alpha, bestVal);
                    if (beta <= alpha)
                    {
                        continue;
                    }
                    if (prevMoves.Contains(validMoves[i]))
                    {
                        continue;
                    }
                    move = minMoveAB(validMoves[i], depthLimit, currDepth + 1, alpha, beta, board, color);
                    
                    if (bestMove == null || move.ValueOfMove
                    > bestVal)
                    {
                        bestMove = validMoves[i];
                    } 
                }
                if (bestMove == null)
                { return m; }
                else
                { return bestMove; }
            }
        }

        public static DateTime start;
        public static TimeSpan timeLimit = TimeSpan.FromSeconds(40);
        /// <summary>
        /// Evaluates the chess board and decided which move to make. This is the main method of the AI.
        /// The framework will call this method when it's your turn.
        /// </summary>
        /// <param name="board">Current chess board</param>
        /// <param name="yourColor">Your color</param>
        /// <returns> Returns the best chess move the player has for the given chess board</returns>
        public ChessMove GetNextMove(ChessBoard board, ChessColor myColor)
        {
            start = DateTime.Now;
            List<ChessMove> moves = GetAllMoves(board, myColor);
            List<ChessMove> validMoves = setFlags(moves, board, myColor);
            ChessMove chosenMove = minimaxAB(validMoves, 4, board, myColor);
            prevMoves.Add(chosenMove);

            return chosenMove;
        }

        public static HashSet<ChessMove> prevMoves = new HashSet<ChessMove> { };

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
