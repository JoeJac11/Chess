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
            get { return "JEK (Debug) Kris"; }
#else
            get { return "JEK"; }
#endif
            }
        public List<ChessMove> GenMoves(ChessBoard board, ChessColor color)
            {
            List<ChessMove> validMoves = new List<ChessMove>();
            for (int i = 0; i < 7; i++) //columns
                {
                for (int j = 0; j < 7; j++) //rows
                    {
                    //int curLocation = (i * 10) + j;
                    if (board[i, j] == ChessPiece.Empty)
                        {//do nothing
                        }
                    if (color == ChessColor.Black)
                        {
                    if (board[i, j] == ChessPiece.BlackKing)
                        {
                        //black King down 1 row
                        if ((j + 1 <= 7) && board[i, j + 1] >= ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(i, j + 1);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                        //black King up 1 row
                        if ((j - 1 >= 0) && board[i, j - 1] >= ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(i, j - 1);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                        //black King left 1 col
                        if ((i - 1 >= 0) && board[i - 1, j] >= ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(i - 1, j);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                        //black King right 1 col
                        if ((i + 1 <= 7) && board[i + 1, j] >= ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(i + 1, j);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                        //black King up (row) and right (col)
                        if ((j - 1 >= 0) && (i + 1 <= 7) && board[i + 1, j - 1] >= ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(i + 1, j - 1);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                        //black king down (row) and right (col)
                        if ((j + 1 <= 7) && (i + 1 <= 7) && board[i + 1, j + 1] >= ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(i + 1, j + 1);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                        //black king up (row) and left (col)
                        if ((j - 1 >= 0) && (i - 1 >= 0) && board[i - 1, j - 1] >= ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(i - 1, j - 1);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                        //black King down (row) and left (col)
                        if ((j + 1 <= 7) && (i - 1 >= 0) && board[i - 1, j + 1] >= ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(i - 1, j + 1);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                    }
                    else if (board[i, j] == ChessPiece.BlackQueen)
                    {
                        //black queen down (row)
                        int rj = j;
                        while ((rj + 1 <= 7) && board[i, rj + 1] == ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(i, rj + 1);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                            rj++;
                        }
                        if ((rj + 1 <= 7) && board[i, rj + 1] > ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(i, rj + 1);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                        //black queen right (col)
                        int ri = i;
                        while ((ri + 1 <= 7) && board[ri + 1, j] == ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(ri + 1, j);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                            ri++;
                        }
                        if (ri + 1 <= 7 && board[ri + 1, j] > ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(ri + 1, j);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                        //black queen up (row)
                        rj = j;
                        while ((rj - 1 >= 0) && board[i, rj - 1] == ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(i, rj - 1);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                            rj--;
                        }
                        if (rj - 1 >= 0 && board[i, rj - 1] > ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(i, rj - 1);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                        //black queen left (col)
                        ri = i;
                        while ((ri - 1 >= 0) && board[ri - 1, j] == ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(ri - 1, j);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                            ri--;
                        }
                        if (rj - 1 >= 0 && board[ri - 1, j] > ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(ri - 1, j);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                        //black queen down and right (+row)(+col)
                        int bi = i;
                        int bj = j;
                        while ((bi + 1 <= 7) && (bj + 1 <= 7) && board[bi + 1, bj + 1] == ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(bi + 1, bj + 1);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                            bi++;
                            bj++;
                        }
                        if ((bi + 1 <= 7) && (bj + 1 <= 7) && board[bi + 1, bj + 1] > ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(bi + 1, bj + 1);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                        //black queen up and left (-row)(-col)
                        bi = i;
                        bj = j;
                        while ((bi - 1 >= 0) && (bj - 1 >= 0) && board[bi - 1, bj - 1] == ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(bi - 1, bj - 1);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                            bi--;
                            bj--;
                        }
                        if ((bi - 1 >= 0) && (bj - 1 >= 0) && board[bi - 1, bj - 1] > ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(bi - 1, bj - 1);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                        //black queen down and left (+row)(-col)
                        bi = i;
                        bj = j;
                        while ((bi - 1 >= 0) && (bj + 1 <= 7) && board[bi - 1, bj + 1] == ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(bi - 1, bj + 1);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                            bi--;
                            bj++;
                        }
                        if ((bi - 1 >= 0) && (bj + 1 <= 7) && board[bi - 1, bj + 1] > ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(bi - 1, bj + 1);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                        //black queen up and right (-row)(+col)
                        bi = i;
                        bj = j;
                        while ((bi + 1 >= 0) && (bj - 1 <= 7) && board[bi + 1, bj - 1] == ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(bi + 1, bj - 1);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                            bi++;
                            bj--;
                        }
                        if ((bi + 1 >= 0) && (bj - 1 <= 7) && board[bi + 1, bj - 1] > ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(bi + 1, bj - 1);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                    }
                    else if (board[i, j] == ChessPiece.BlackKnight)
                    {
                        int moveLocation;
                        //black knight right 2 down 1
                        if ((i + 2 <= 7) && (j + 1 <= 7) && board[i + 2, j + 1] >= ChessPiece.Empty)
                        {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i + 2, j + 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                        }
                        //black knight left 2 up 1
                        if ((i - 2 >= 0) && (j - 1 >= 0) && board[i - 2, j - 1] >= ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(i - 2, j - 1);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                        //black knight left 2 down 1
                        if ((i - 2 >= 0) && (j + 1 <= 7) && board[i - 2, j + 1] >= ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(i - 2, j + 1);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                        //black knight right 2 up 1
                        if ((i + 2 <= 7) && (j - 1 >= 0) && board[i + 2, j - 1] >= ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(i + 2, j - 1);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                        //black knight down 2 right 1
                        if ((i + 1 <= 7) && (j + 2 <= 7) && board[i + 1, j + 2] >= ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(i + 1, j + 2);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                        //black knight down 2 left 1
                        if ((i - 1 >= 0) && (j + 2 <= 7) && board[i - 1, j + 2] >= ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(i - 1, j + 2);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                        //black knight up 2 right 1
                        if ((i + 1 <= 7) && (j - 2 >= 0) && board[i + 1, j - 2] >= ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(i + 1, j - 2);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                        //black knight up 2 left 1
                        if ((i - 1 <= 0) && (j - 2 <= 7) && board[i - 1, j - 2] >= ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(i - 1, j - 2);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                    }
                    else if (board[i, j] == ChessPiece.BlackBishop)
                        {
                            //black Bishop down and right
                            int bi = i;
                            int bj = j;
                            while ((bi + 1 <= 7) && (bj + 1 <= 7) && board[bi + 1, bj + 1] == ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(bi + 1, bj + 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                                bi++;
                                bj++;
                            }
                            if ((bi + 1 <= 7) && (bj + 1 <= 7) && board[bi + 1, bj + 1] > ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(bi + 1, bj + 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //black Bishop up and left
                            bi = i;
                            bj = j;
                            while ((bi - 1 >= 0) && (bj - 1 >= 0) && board[bi - 1, bj - 1] == ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(bi - 1, bj - 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                                bi--;
                                bj--;
                            }
                            if ((bi - 1 >= 0) && (bj - 1 >= 0) && board[bi - 1, bj - 1] > ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(bi - 1, bj - 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //black Bishop down and left
                            bi = i;
                            bj = j;
                            while ((bi - 1 >= 0) && (bj + 1 <= 7) && board[bi - 1, bj + 1] == ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(bi - 1, bj + 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                                bi--;
                                bj++;
                            }
                            if ((bi - 1 >= 0) && (bj + 1 <= 7) && board[bi - 1, bj + 1] > ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(bi - 1, bj + 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //black Bishop up and right
                            bi = i;
                            bj = j;
                            while ((bi + 1 >= 0) && (bj - 1 <= 7) && board[bi + 1, bj - 1] == ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(bi + 1, bj - 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                                bi++;
                                bj--;
                            }
                            if ((bi + 1 >= 0) && (bj - 1 <= 7) && board[bi + 1, bj - 1] > ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(bi + 1, bj - 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                        }
                    else if (board[i, j] == ChessPiece.BlackRook)
                    {
                        //black rook down
                        int rj = j;
                        while ((rj + 1 <= 7) && board[i, rj + 1] == ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(i, rj + 1);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                            rj++;
                        }
                        if ((rj + 1 <= 7) && board[i, rj + 1] > ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(i, rj + 1);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                        //black rook right
                        int ri = i;
                        while ((ri + 1 <= 7) && board[ri + 1, j] == ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(ri + 1, j);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                            ri++;
                        }
                        if (ri + 1 <= 7 && board[ri + 1, j] > ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(ri + 1, j);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                        //black rook up
                        rj = j;
                        while ((rj - 1 >= 0) && board[i, rj - 1] == ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(i, rj - 1);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                            rj--;
                        }
                        if (rj - 1 >= 0 && board[i, rj - 1] > ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(i, rj - 1);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                        //black rook left
                        ri = i;
                        while ((ri - 1 >= 0) && board[ri - 1, j] == ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(ri - 1, j);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                            ri--;
                        }
                        if (rj - 1 >= 0 && board[ri - 1, j] > ChessPiece.Empty)
                        {
                            ChessLocation from = new ChessLocation(i, j);
                            ChessLocation to = new ChessLocation(ri - 1, j);
                            ChessMove move = new ChessMove(from, to);
                            validMoves.Add(move);
                        }
                    }
                    else if (board[i, j] == ChessPiece.BlackPawn)
                            {
                            if (j == 1)
                                {
                                //black pawn down 2
                                if (board[i, j + 1] == ChessPiece.Empty && board[i, j + 2] == ChessPiece.Empty)
                                    {
                                    ChessLocation from = new ChessLocation(i, j);
                                    ChessLocation to = new ChessLocation(i, j + 2);
                                    ChessMove move = new ChessMove(from, to);
                                    validMoves.Add(move);
                                    }
                                }
                            //black pawn down 1
                            if (j + 1 <= 7 && board[i, j + 1] == ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i, j + 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //black pawn diagonal attack down/left (+row)(-col)
                            if ((j + 1 <= 7) && (i - 1 >= 0) && board[i - 1, j + 1] > ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i - 1, j + 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                                                            }
                            //black pawn diagonal attack down/right
                            if ((j + 1 <= 7) && (i + 1 <= 7) && board[i + 1, j + 1] > ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i + 1, j + 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                        }
                        }
                    else
                    {
                        if (board[i, j] == ChessPiece.WhitePawn)
                        {
                            //white pawn up 2
                            if (j == 6)
                            {
                                if (board[i, j - 2] == ChessPiece.Empty && board[i, j - 1] == ChessPiece.Empty)
                                {
                                    ChessLocation from = new ChessLocation(i, j);
                                    ChessLocation to = new ChessLocation(i, j - 2);
                                    ChessMove move = new ChessMove(from, to);
                                    validMoves.Add(move);
                                }
                            }
                            //white pawn up 1
                            if ((j - 1 >= 0) && board[i, j - 1] == ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i, j - 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //white pawn diagonal attack up/left
                            if ((j - 1 >= 0) && (i - 1 >= 0) && board[i - 1, j - 1] < ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i - 1, j - 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //white pawn diagonal attack up/right
                            if ((j - 1 >= 0) && (i + 1 <= 7) && board[i + 1, j - 1] < ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i + 1, j - 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                        }
                        else if (board[i, j] == ChessPiece.WhiteBishop)
                        {
                            //white Bishop down and right
                            int bi = i;
                            int bj = j;
                            while ((bi + 1 <= 7) && (bj + 1 <= 7) && board[bi + 1, bj + 1] == ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(bi + 1, bj + 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                                bi++;
                                bj++;
                            }
                            if ((bi + 1 <= 7) && (bj + 1 <= 7) && board[bi + 1, bj + 1] > ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(bi + 1, bj + 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //white Bishop up and left
                            bi = i;
                            bj = j;
                            while ((bi - 1 >= 0) && (bj - 1 >= 0) && board[bi - 1, bj - 1] == ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(bi - 1, bj - 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                                bi--;
                                bj--;
                            }
                            if ((bi - 1 >= 0) && (bj - 1 >= 0) && board[bi - 1, bj - 1] > ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(bi - 1, bj - 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //white Bishop down and left
                            bi = i;
                            bj = j;
                            while ((bi - 1 >= 0) && (bj + 1 <= 7) && board[bi - 1, bj + 1] == ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(bi - 1, bj + 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                                bi--;
                                bj++;
                            }
                            if ((bi - 1 >= 0) && (bj + 1 <= 7) && board[bi - 1, bj + 1] > ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(bi - 1, bj + 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //white Bishop up and right
                            bi = i;
                            bj = j;
                            while ((bi + 1 >= 0) && (bj - 1 <= 7) && board[bi + 1, bj - 1] == ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(bi + 1, bj - 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                                bi++;
                                bj--;
                            }
                            if ((bi + 1 >= 0) && (bj - 1 <= 7) && board[bi + 1, bj - 1] > ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(bi + 1, bj - 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                        }
                        else if (board[i, j] == ChessPiece.WhiteKnight)
                        {
                            //white knight right 2 down 1
                            if ((i + 2 <= 7) && (j + 1 <= 7) && board[i + 2, j + 1] >= ChessPiece.Empty)
                            {
                                    ChessLocation from = new ChessLocation(i, j);
                                    ChessLocation to = new ChessLocation(i + 2, j + 1);
                                    ChessMove move = new ChessMove(from, to);
                                    validMoves.Add(move);
                            }
                            //white knight left 2 up 1
                            if ((i - 2 >= 0) && (j - 1 >= 0) && board[i - 2, j - 1] >= ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i - 2, j - 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //white knight left 2 down 1
                            if ((i - 2 >= 0) && (j + 1 <= 7) && board[i - 2, j + 1] >= ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i - 2, j - 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //white knight right 2 up 1
                            if ((i + 2 <= 7) && (j - 1 >= 0) && board[i + 2, j - 1] >= ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i + 2, j - 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //white knight down 2 right 1
                            if ((i + 1 <= 7) && (j + 2 <= 7) && board[i + 1, j + 2] >= ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i + 1, j + 2);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //white knight down 2 left 1
                            if ((i - 1 >= 0) && (j + 2 <= 7) && board[i - 1, j + 2] >= ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i - 1, j + 2);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //white knight up 2 right 1
                            if ((i + 1 <= 7) && (j - 2 >= 0) && board[i + 1, j - 2] >= ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i + 1, j - 2);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //white knight up 2 left 1
                            if ((i - 1 <= 0) && (j - 2 <= 7) && board[i - 1, j - 2] >= ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i - 1, j - 2);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                        }
                        else if (board[i, j] == ChessPiece.WhiteQueen)
                        {
                            //white queen down (row)
                            int rj = j;
                            while ((rj + 1 <= 7) && board[i, rj + 1] == ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i, rj + 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                                rj++;
                            }
                            if ((rj + 1 <= 7) && board[i, rj + 1] > ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i, rj + 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //white queen right (col)
                            int ri = i;
                            while ((ri + 1 <= 7) && board[ri + 1, j] == ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(ri + 1, j);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                                ri++;
                            }
                            if (ri + 1 <= 7 && board[ri + 1, j] > ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(ri + 1, j);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //white queen up (row)
                            rj = j;
                            while ((rj - 1 >= 0) && board[i, rj - 1] == ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i, rj - 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                                rj--;
                            }
                            if (rj - 1 >= 0 && board[i, rj - 1] > ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i, rj - 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //white queen left (col)
                            ri = i;
                            while ((ri - 1 >= 0) && board[ri - 1, j] == ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(ri - 1, j);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                                ri--;
                            }
                            if (rj - 1 >= 0 && board[ri - 1, j] > ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(ri - 1, j);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //white queen down and right (+row)(+col)
                            int bi = i;
                            int bj = j;
                            while ((bi + 1 <= 7) && (bj + 1 <= 7) && board[bi + 1, bj + 1] == ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(bi + 1, bj + 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                                bi++;
                                bj++;
                            }
                            if ((bi + 1 <= 7) && (bj + 1 <= 7) && board[bi + 1, bj + 1] > ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(bi + 1, bj + 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //white queen up and left (-row)(-col)
                            bi = i;
                            bj = j;
                            while ((bi - 1 >= 0) && (bj - 1 >= 0) && board[bi - 1, bj - 1] == ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(bi - 1, bj - 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                                bi--;
                                bj--;
                            }
                            if ((bi - 1 >= 0) && (bj - 1 >= 0) && board[bi - 1, bj - 1] > ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(bi - 1, bj - 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //white queen down and left (+row)(-col)
                            bi = i;
                            bj = j;
                            while ((bi - 1 >= 0) && (bj + 1 <= 7) && board[bi - 1, bj + 1] == ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(bi - 1, bj + 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                                bi--;
                                bj++;
                            }
                            if ((bi - 1 >= 0) && (bj + 1 <= 7) && board[bi - 1, bj + 1] > ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(bi - 1, bj + 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //white queen up and right (-row)(+col)
                            bi = i;
                            bj = j;
                            while ((bi + 1 >= 0) && (bj - 1 <= 7) && board[bi + 1, bj - 1] == ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(bi + 1, bj - 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                                bi++;
                                bj--;
                            }
                            if ((bi + 1 >= 0) && (bj - 1 <= 7) && board[bi + 1, bj - 1] > ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(bi + 1, bj - 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                        }
                        else if (board[i, j] == ChessPiece.WhiteRook)
                        {
                            //white rook down
                            int rj = j;
                            while ((rj + 1 <= 7) && board[i, rj + 1] == ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i, rj + 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                                rj++;
                            }
                            if ((rj + 1 <= 7) && board[i, rj + 1] > ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i, rj + 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //white rook right
                            int ri = i;
                            while ((ri + 1 <= 7) && board[ri + 1, j] == ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(ri + 1, j);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                                ri++;
                            }
                            if (ri + 1 <= 7 && board[ri + 1, j] > ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(ri + 1, j);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //white rook up
                            rj = j;
                            while ((rj - 1 >= 0) && board[i, rj - 1] == ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i, rj - 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                                rj--;
                            }
                            if (rj - 1 >= 0 && board[i, rj - 1] > ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i, rj - 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //white rook left
                            ri = i;
                            while ((ri - 1 >= 0) && board[ri - 1, j] == ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(ri - 1, j);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                                ri--;
                            }
                            if (rj - 1 >= 0 && board[ri - 1, j] > ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(ri - 1, j);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                        }
                        else if (board[i, j] == ChessPiece.WhiteKing)
                        {
                            //White King down 1 row
                            if ((j + 1 <= 7) && board[i, j + 1] >= ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i, j + 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //White King up 1 row
                            if ((j - 1 >= 0) && board[i, j - 1] >= ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i, j - 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //White King left 1 col
                            if ((i - 1 >= 0) && board[i - 1, j] >= ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i - 1, j);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //White King right 1 col
                            if ((i + 1 <= 7) && board[i + 1, j] >= ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i + 1, j);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //White King up (row) and right (col)
                            if ((j - 1 >= 0) && (i + 1 <= 7) && board[i + 1, j - 1] >= ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i + 1, j - 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //White king down (row) and right (col)
                            if ((j + 1 <= 7) && (i + 1 <= 7) && board[i + 1, j + 1] >= ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i + 1, j + 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //White king up (row) and left (col)
                            if ((j - 1 >= 0) && (i - 1 >= 0) && board[i - 1, j - 1] >= ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i - 1, j - 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                            //White King down (row) and left (col)
                            if ((j + 1 <= 7) && (i - 1 >= 0) && board[i - 1, j + 1] >= ChessPiece.Empty)
                            {
                                ChessLocation from = new ChessLocation(i, j);
                                ChessLocation to = new ChessLocation(i - 1, j + 1);
                                ChessMove move = new ChessMove(from, to);
                                validMoves.Add(move);
                            }
                        }
                    }
                }
                }
                return validMoves;
            }
        //returns true if the move will put the king of the test color in check
        public bool InCheck(ChessMove move, ChessBoard board, ChessColor testColor)
        {

            if (testColor == ChessColor.White)
            {
                ChessBoard tempBoard = new ChessBoard();
                tempBoard = board;
                tempBoard.MakeMove(move);

                foreach (ChessMove tempMove in GenMoves(tempBoard, ChessColor.Black))
                {
                    if (tempBoard[tempMove.To] == ChessPiece.WhiteKing)
                    {
                        return true;
                    }
                }
            }
            else if (testColor == ChessColor.Black)
            {
                ChessBoard tempBoard = new ChessBoard();
                tempBoard = board;
                tempBoard.MakeMove(move);

                foreach (ChessMove tempMove in GenMoves(tempBoard, ChessColor.White))
                {
                    if (tempBoard[tempMove.To] == ChessPiece.BlackKing)
                    {
                        return true;
                    }
                }

            }
            return false;
        }
        //public ChessMove Logic(Dictionary<int, List<Tuple<int, int>>> validMoves, ChessBoard board, ChessColor myColor)
        public ChessMove Logic(List<ChessMove> validMoves, ChessBoard board, ChessColor myColor)
            {
            Dictionary<ChessPiece, int> values = new Dictionary<ChessPiece, int>(); //create array for values of pieces
            values.Add(ChessPiece.WhitePawn, 1);
            values.Add(ChessPiece.WhiteRook, 5);
            values.Add(ChessPiece.WhiteKnight, 3);
            values.Add(ChessPiece.WhiteBishop, 3);
            values.Add(ChessPiece.WhiteQueen, 10);
            values.Add(ChessPiece.WhiteKing, 1000);
            values.Add(ChessPiece.BlackPawn, 1);
            values.Add(ChessPiece.BlackRook, 5);
            values.Add(ChessPiece.BlackKnight, 3);
            values.Add(ChessPiece.BlackBishop, 3);
            values.Add(ChessPiece.BlackQueen, 10);
            values.Add(ChessPiece.BlackKing, 1000);
            values.Add(ChessPiece.Empty, 0);
            const int TEN = 10;

            Dictionary<int, List<Tuple<int, int>>> scores = new Dictionary<int, List<Tuple<int, int>>>(); //create dictionary ('from', move values)
            Dictionary<ChessPiece, int> bestMoves = new Dictionary<ChessPiece, int>(); //dictionary (chesspiece, int for best move)
            int start = 0;
            int multiplier = 0;
            int score = 0;
            int from;
            if (myColor == 0) //my color is white
                {
                start = 8;
                multiplier = 1;

                }
            else  //my color is black
                {
                start = 0;
                multiplier = -1;
                }
            //for (int i = 0; i < 7; i++)
            //    {
            //    foreach (Tuple<int, int> t in validMoves[i + start]) //score all the validMoves for my color
            //        {
            //            if (t.Item2 == 1) //piece was taken
            //                {
            //                score = values[board[t.Item1 / TEN, t.Item1 % 10]] * multiplier; //score for the move is value of ChessPiece at location * multiplier for color
            //                }
            //            Tuple<int, int> moveScore = Tuple.Create(t.Item2, score);
            //            scores[i + start].Add(moveScore);
            //            }
            //        foreach (Tuple<int, int> t in scores[i + start]) //pick the best validMove for each piece
            //            {
            //            int maxForPc = 0;
            //            if (t.Item2 > maxForPc)
            //                {
            //                maxForPc = t.Item2;
            //                }
            //            bestMoves[(ChessPiece)Enum.ToObject(typeof(ChessPiece), i + start)] = t.Item1;
            //            }
            //        int maxForAll = 0;
            //        if (bestMoves[(ChessPiece)Enum.ToObject(typeof(ChessPiece), i + start)] > maxForAll)
            //            {
            //            maxForAll = bestMoves[(ChessPiece)Enum.ToObject(typeof(ChessPiece), i + start)];
            //            from = i + start;
            //            }
            //        }
            ChessLocation moveFrom = new ChessLocation(0,1);//from / 10, from % 10);
            ChessLocation moveTo = new ChessLocation(0,2);//to / 10, to % 10);
            return new ChessMove(moveFrom, moveTo);
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
            //Dictionary<int, List<Tuple<int, int>>> moves = GenMoves(board, myColor);
            List<ChessMove> validMoves = GenMoves(board, myColor);
            //List<ChessMove> validMoves = new List<ChessMove>();
            //validMoves.Add(new ChessMove(new ChessLocation(0, 1), new ChessLocation(0, 3)));
            ChessMove chosenMove = Logic(validMoves, board, myColor);
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
            return true;
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
