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
            Dictionary<int, HashSet<Tuple<int,int>>> moves = new Dictionary<int, HashSet<Tuple<int, int>>>(); //int is the 2-digit 'from' move, tuple is 'to', 'weight'
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; i < 7; i++)
                {
                    int curLocation = (i * 10) + j;
                    Tuple<int, int> move;
                    if (board[i, j] == ChessPiece.Empty)
                    {//do nothing
                    }
                    else if (board[i, j] == ChessPiece.BlackKing)
                    {
                        //black King down 1
                        if ((i + 1 <= 7) && board[i + 1, j] >= ChessPiece.Empty)
                        {
                            if (board[i + 1, j] > ChessPiece.Empty)
                            {
                                move = Tuple.Create(curLocation + 10, 1);
                            }
                            else
                            {
                                move = Tuple.Create(curLocation + 10, 2);
                            }
                            moves[curLocation].Add(move);
                        }
                        //black King up 1
                        if ((i - 1 >= 0) && board[i - 1, j] >= ChessPiece.Empty)
                        {
                            if (board[i - 1, j] > ChessPiece.Empty)
                            {
                                move = Tuple.Create(curLocation - 10, 1);
                            }
                            else
                            {
                                move = Tuple.Create(curLocation - 10, 2);
                            }
                            moves[curLocation].Add(move);
                        }
                        //black King left 1
                        if ((j - 1 >= 0) && board[i, j - 1] >= ChessPiece.Empty)
                        {
                            if (board[i, j - 1] > ChessPiece.Empty)
                            {
                                move = Tuple.Create(curLocation - 1, 1);
                            }
                            else
                            {
                                move = Tuple.Create(curLocation - 1, 2);
                            }
                            moves[curLocation].Add(move);
                        }
                        //black King right 1
                        if ((j + 1 <= 7) && board[i, j + 1] >= ChessPiece.Empty)
                        {
                            if (board[i, j + 1] > ChessPiece.Empty)
                            {
                                move = Tuple.Create(curLocation + 1, 1);
                            }
                            else
                            {
                                move = Tuple.Create(curLocation + 1, 2);
                            }
                            moves[curLocation].Add(move);
                        }
                        //black King up and right
                        if ((j + 1 <= 7) && (i + 1 <= 7) && board[i + 1, j + 1] >= ChessPiece.Empty)
                        {
                            if (board[i + 1, j + 1] > ChessPiece.Empty)
                            {
                                move = Tuple.Create(curLocation + 11, 1);
                            }
                            else
                            {
                                move = Tuple.Create(curLocation + 11, 2);
                            }
                            moves[curLocation].Add(move);
                        }
                        //black king down and right
                        if ((j + 1 <= 7) && (i - 1 >= 0) && board[i - 1, j + 1] >= ChessPiece.Empty)
                        {
                            int moveLocation = ((i - 1) * 10) + (j + 1);
                            if (board[i - 1, j + 1] > ChessPiece.Empty)
                            {
                                move = Tuple.Create(moveLocation, 1);
                            }
                            else
                            {
                                move = Tuple.Create(moveLocation, 2);
                            }
                            moves[curLocation].Add(move);
                        }
                        //black king up and left
                        if ((j - 1 >= 0) && (i + 1 <= 7) && board[i + 1, j - 1] >= ChessPiece.Empty)
                        {
                            int moveLocation = ((i + 1) * 10) + (j - 1);
                            if (board[i + 1, j - 1] > ChessPiece.Empty)
                            {
                                move = Tuple.Create(moveLocation, 1);
                            }
                            else
                            {
                                move = Tuple.Create(moveLocation, 2);
                            }
                            moves[curLocation].Add(move);
                        }
                        //black King down and left
                        if ((j - 1 >= 0) && (i - 1 >= 0) && board[i - 1, j - 1] >= ChessPiece.Empty)
                        {
                            int moveLocation = ((i - 1) * 10) + (j - 1);
                            if (board[i - 1, j - 1] > ChessPiece.Empty)
                            {
                                move = Tuple.Create(moveLocation, 1);
                            }
                            else
                            {
                                move = Tuple.Create(moveLocation, 2);
                            }
                            moves[curLocation].Add(move);
                        }
                    }
                    else if (board[i, j] == ChessPiece.BlackQueen)
                    {
                        int moveLocation;
                        //black queen down
                        int ri = i + 1;
                        while ((ri <= 7) && board[ri, j] == ChessPiece.Empty)
                        {
                            if (ri + 1 <= 7 && board[ri + 1, j] > ChessPiece.Empty)
                            {
                                moveLocation = ((ri + 1) * 10 + j);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (ri * 10) + j;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            ri++;
                        }
                        //black queen right
                        int rj = i + 1;
                        while ((rj <= 7) && board[i, rj] == ChessPiece.Empty)
                        {
                            if (rj + 1 <= 7 && board[i, rj + 1] > ChessPiece.Empty)
                            {
                                moveLocation = ((i) * 10 + rj + 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (i * 10) + rj;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            rj++;
                        }
                        //black queen up
                        ri = i - 1;
                        while ((ri >= 0) && board[ri, j] == ChessPiece.Empty)
                        {
                            if (ri - 1 >= 0 && board[ri - 1, j] > ChessPiece.Empty)
                            {
                                moveLocation = ((ri - 1) * 10 + j);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (ri * 10) + j;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            ri--;
                        }
                        //black queen left
                        rj = i - 1;
                        while ((rj >= 0) && board[i, rj] == ChessPiece.Empty)
                        {
                            if (rj - 1 >= 0 && board[i, rj - 1] > ChessPiece.Empty)
                            {
                                moveLocation = ((i) * 10 + rj - 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (i * 10) + rj;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            rj--;
                        }
                        //black queen down and right
                        int bi = i + 1;
                        int bj = j + 1;
                        while ((bi <= 7) && (bj <= 7) && board[bi, bj] == ChessPiece.Empty)
                        {
                            if ((bi + 1 <= 7) && (bj + 1 <= 7) && board[bi + 1, bj + 1] > ChessPiece.Empty)
                            {
                                moveLocation = ((bi + 1) * 10) + (bj + 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (bi * 10) + bj;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            bi++;
                            bj++;
                        }
                        //black queen up and left
                        bi = i - 1;
                        bj = j - 1;
                        while ((bi >= 0) && (bj >= 0) && board[bi, bj] == ChessPiece.Empty)
                        {
                            if ((bi - 1 >= 0) && (bj - 1 >= 0) && board[bi - 1, bj - 1] > ChessPiece.Empty)
                            {
                                moveLocation = ((bi - 1) * 10) + (bj - 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (bi * 10) + bj;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            bi--;
                            bj--;
                        }
                        //black queen down and left
                        bi = i + 1;
                        bj = j - 1;
                        while ((bi <= 7) && (bj >= 0) && board[bi, bj] == ChessPiece.Empty)
                        {
                            if ((bi + 1 <= 7) && (bj - 1 >= 0) && board[bi + 1, bj - 1] > ChessPiece.Empty)
                            {
                                moveLocation = ((bi + 1) * 10) + (bj - 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (bi * 10) + bj;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            bi++;
                            bj--;
                        }
                        //black queen up and right
                        bi = i - 1;
                        bj = j + 1;
                        while ((bi >= 0) && (bj <= 7) && board[bi, bj] == ChessPiece.Empty)
                        {
                            if ((bi - 1 >= 0) && (bj + 1 <= 7) && board[bi - 1, bj + 1] > ChessPiece.Empty)
                            {
                                moveLocation = ((bi + 1) * 10) + (bj + 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (bi * 10) + bj;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            bi--;
                            bj++;
                        }
                    }
                    else if (board[i, j] == ChessPiece.BlackKnight)
                    {
                        int moveLocation;
                        //black knight down and right
                        if ((i + 2 <= 7) && (j + 1 <= 7) && board[i + 2, j + 1] >= ChessPiece.Empty)
                        {
                            if (board[i + 2, j + 1] > ChessPiece.Empty)
                            {
                                moveLocation = ((i + 2) * 10) + (j + 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            else
                            {
                                moveLocation = ((i + 2) * 10) + (j + 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                        }
                        //black knight up and left
                        if ((i - 2 >= 0) && (j - 1 >= 0) && board[i - 2, j - 1] >= ChessPiece.Empty)
                        {
                            if (board[i - 2, j - 1] > ChessPiece.Empty)
                            {
                                moveLocation = ((i - 2) * 10) + (j - 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            else
                            {
                                moveLocation = ((i - 2) * 10) + (j - 1);
                                move = Tuple.Create(moveLocation, 1);
                                moves[curLocation].Add(move);
                            }
                        }
                        //black knight up and right
                        if ((i - 2 >= 0) && (j + 1 <= 7) && board[i - 2, j + 1] >= ChessPiece.Empty)
                        {
                            if (board[i - 2, j + 1] > ChessPiece.Empty)
                            {
                                moveLocation = ((i - 2) * 10) + (j + 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            else
                            {
                                moveLocation = ((i - 2) * 10) + (j + 1);
                                move = Tuple.Create(moveLocation, 1);
                                moves[curLocation].Add(move);
                            }
                        }
                        //black knight down and left
                        if ((i + 2 <= 7) && (j - 1 >= 0) && board[i + 2, j - 1] >= ChessPiece.Empty)
                        {
                            if (board[i + 2, j - 1] > ChessPiece.Empty)
                            {
                                moveLocation = ((i + 2) * 10) + (j - 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            else
                            {
                                moveLocation = ((i + 2) * 10) + (j - 1);
                                move = Tuple.Create(moveLocation, 1);
                                moves[curLocation].Add(move);
                            }
                        }
                        //black knight right and down
                        if ((i + 1 <= 7) && (j + 2 <= 7) && board[i + 1, j +2] >= ChessPiece.Empty)
                        {
                            if (board[i + 1, j + 2] > ChessPiece.Empty)
                            {
                                moveLocation = ((i + 1) * 10) + (j + 2);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            else
                            {
                                moveLocation = ((i + 1) * 10) + (j + 2);
                                move = Tuple.Create(moveLocation, 1);
                                moves[curLocation].Add(move);
                            }
                        }
                        //black knight left and up
                        if ((i - 1 >= 0) && (j + 2 <= 7) && board[i - 1, j + 2] >= ChessPiece.Empty)
                        {
                            if (board[i - 1, j + 2] > ChessPiece.Empty)
                            {
                                moveLocation = ((i - 1) * 10) + (j + 2);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            else
                            {
                                moveLocation = ((i - 1) * 10) + (j + 2);
                                move = Tuple.Create(moveLocation, 1);
                                moves[curLocation].Add(move);
                            }
                        }
                        //black knight left and down
                        if ((i + 1 <= 7) && (j - 2 >= 0) && board[i + 1, j - 2] >= ChessPiece.Empty)
                        {
                            if (board[i + 1, j - 2] > ChessPiece.Empty)
                            {
                                moveLocation = ((i + 1) * 10) + (j - 2);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            else
                            {
                                moveLocation = ((i + 1) * 10) + (j - 2);
                                move = Tuple.Create(moveLocation, 1);
                                moves[curLocation].Add(move);
                            }
                        }
                        //black knight right and up
                        if ((i - 1 <= 0) && (j + 2 <= 7) && board[i - 1, j + 2] >= ChessPiece.Empty)
                        {
                            if (board[i - 1, j + 2] > ChessPiece.Empty)
                            {
                                moveLocation = ((i - 1) * 10) + (j + 2);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            else
                            {
                                moveLocation = ((i - 1) * 10) + (j + 2);
                                move = Tuple.Create(moveLocation, 1);
                                moves[curLocation].Add(move);
                            }
                        }
                    }
                    else if (board[i, j] == ChessPiece.BlackBishop)
                    {
                        //black Bishop down and right
                        int bi = i + 1;
                        int bj = j + 1;
                        while ((bi <= 7) && (bj <= 7) && board[bi, bj] == ChessPiece.Empty)
                        {
                            int moveLocation;
                            if ((bi + 1 <= 7) && (bj + 1 <= 7) && board[bi + 1, bj + 1] > ChessPiece.Empty)
                            {
                                moveLocation = ((bi + 1) * 10) + (bj + 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (bi * 10) + bj;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            bi++;
                            bj++;
                        }
                        //black Bishop up and left
                        bi = i - 1;
                        bj = j - 1;
                        while ((bi >= 0) && (bj >= 0) && board[bi, bj] == ChessPiece.Empty)
                        {
                            int moveLocation;
                            if ((bi - 1 >= 0) && (bj - 1 >= 0) && board[bi - 1, bj - 1] > ChessPiece.Empty)
                            {
                                moveLocation = ((bi - 1) * 10) + (bj - 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (bi * 10) + bj;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            bi--;
                            bj--;
                        }
                        //black Bishop down and left
                        bi = i + 1;
                        bj = j - 1;
                        while ((bi <= 7) && (bj >= 0) && board[bi, bj] == ChessPiece.Empty)
                        {
                            int moveLocation;
                            if ((bi + 1 <= 7) && (bj - 1 >= 0) && board[bi + 1, bj - 1] > ChessPiece.Empty)
                            {
                                moveLocation = ((bi + 1) * 10) + (bj - 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (bi * 10) + bj;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            bi++;
                            bj--;
                        }
                        //black Bishop up and right
                        bi = i - 1;
                        bj = j + 1;
                        while ((bi >= 0) && (bj <= 7) && board[bi, bj] == ChessPiece.Empty)
                        {
                            int moveLocation;
                            if ((bi - 1 >= 0) && (bj + 1 <= 7) && board[bi - 1, bj + 1] > ChessPiece.Empty)
                            {
                                moveLocation = ((bi + 1) * 10) + (bj + 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (bi * 10) + bj;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            bi--;
                            bj++;
                        }
                    }
                    else if (board[i, j] == ChessPiece.BlackRook)
                    {
                        int moveLocation;
                        //black rook down
                        int ri = i + 1;
                        while ((ri <= 7) && board[ri, j] == ChessPiece.Empty)
                        {
                            if (ri + 1 <= 7 && board[ri + 1, j] > ChessPiece.Empty)
                            {
                                moveLocation = ((ri + 1) * 10 + j);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (ri * 10) + j;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            ri++;
                        }
                        //black rook right
                        int rj = i + 1;
                        while ((rj <= 7) && board[i, rj] == ChessPiece.Empty)
                        {
                            if (rj + 1 <= 7 && board[i, rj + 1] > ChessPiece.Empty)
                            {
                                moveLocation = ((i) * 10 + rj + 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (i * 10) + rj;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            rj++;
                        }
                        //black rook up
                        ri = i - 1;
                        while ((ri >= 0) && board[ri, j] == ChessPiece.Empty)
                        {
                            if (ri - 1 >= 0 && board[ri - 1, j] > ChessPiece.Empty)
                            {
                                moveLocation = ((ri - 1) * 10 + j);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (ri * 10) + j;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            ri--;
                        }
                        //black rook left
                        rj = i - 1;
                        while ((rj >= 0) && board[i, rj] == ChessPiece.Empty)
                        {
                            if (rj - 1 >= 0 && board[i, rj - 1] > ChessPiece.Empty)
                            {
                                moveLocation = ((i) * 10 + rj - 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (i * 10) + rj;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            rj--;
                        }
                    }
                    else if (board[i, j] == ChessPiece.BlackPawn)
                    {
                        if (i == 1)
                        {
                            //black pawn forward 2
                            if (board[i + 2, j] == ChessPiece.Empty && board[i + 1, j] == ChessPiece.Empty)
                            {
                                move = Tuple.Create(curLocation + 20, 1);
                                moves[curLocation].Add(move);
                            }
                        }
                        //black pawn forward 1
                        if ((i + 1 <= 7) && board[i + 1, j] == ChessPiece.Empty)
                        {
                            move = Tuple.Create(curLocation + 10, 1);
                            moves[curLocation].Add(move);
                        }
                        //black pawn diaganal attack right
                        if ((i + 1 <= 7) && (j + 1 <= 7) && board[i + 1, j + 1] > ChessPiece.Empty)
                        {
                            move = Tuple.Create(curLocation + 11, 2);
                            moves[curLocation].Add(move);
                        }
                        //black pawn diaganal attack left
                        if ((i + 1 <= 7) && (j - 1 >= 0) && board[i + 1, j - 1] > ChessPiece.Empty)
                        {
                            int moveLocation = ((i + 1) * 10) + j - 1;
                            move = Tuple.Create(moveLocation, 2);
                            moves[curLocation].Add(move);
                        }
                    }
                    else if (board[i, j] == ChessPiece.WhitePawn)
                    {
                        if (i == 6)
                        {
                            //white pawn forward 2
                            if (board[i - 2, j] == ChessPiece.Empty && board[i-1, j] == ChessPiece.Empty)
                            {
                                move = Tuple.Create(curLocation - 20, 1);
                                moves[curLocation].Add(move);
                            }
                        }
                        //white pawn forward 1
                        if ((i - 1 >= 0) && board[i - 1, j] == ChessPiece.Empty)
                        {
                            move = Tuple.Create(curLocation - 10, 1);
                            moves[curLocation].Add(move);
                        }
                        //white pawn diaganal attack right
                        if ((j + 1 <= 7) && (i - 1 >= 0) && board[i - 1, j + 1] < ChessPiece.Empty)
                        {
                            move = Tuple.Create(curLocation + 11, 2);
                            moves[curLocation].Add(move);
                        }
                        //white pawn diaganal attack left
                        if ((j - 1 >= 0) && (i - 1 >= 0) && board[i - 1, j - 1] < ChessPiece.Empty)
                        {
                            int moveLocation = ((i + 1) * 10) + j - 1;
                            move = Tuple.Create(moveLocation, 2);
                            moves[curLocation].Add(move);
                        }
                    }
                    else if (board[i, j] == ChessPiece.WhiteBishop)
                    {
                        //white Bishop down and right
                        int bi = i + 1;
                        int bj = j + 1;
                        while ((bi <= 7) && (bj <= 7) && board[bi, bj] == ChessPiece.Empty)
                        {
                            int moveLocation;
                            if ((bi + 1 <= 7) && (bj + 1 <= 7) && board[bi + 1, bj + 1] < ChessPiece.Empty)
                            {
                                moveLocation = ((bi + 1) * 10) + (bj + 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (bi * 10) + bj;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            bi++;
                            bj++;
                        }
                        //white Bishop up and left
                        bi = i - 1;
                        bj = j - 1;
                        while ((bi >= 0) && (bj >= 0) && board[bi, bj] == ChessPiece.Empty)
                        {
                            int moveLocation;
                            if ((bi - 1 >= 0) && (bj - 1 >= 0) && board[bi - 1, bj - 1] < ChessPiece.Empty)
                            {
                                moveLocation = ((bi - 1) * 10) + (bj - 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (bi * 10) + bj;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            bi--;
                            bj--;
                        }
                        //white Bishop down and left
                        bi = i + 1;
                        bj = j - 1;
                        while ((bi <= 7) && (bj >= 0) && board[bi, bj] == ChessPiece.Empty)
                        {
                            int moveLocation;
                            if ((bi + 1 <= 7) && (bj - 1 >= 0) && board[bi + 1, bj - 1] < ChessPiece.Empty)
                            {
                                moveLocation = ((bi + 1) * 10) + (bj - 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (bi * 10) + bj;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            bi++;
                            bj--;
                        }
                        //white Bishop up and right
                        bi = i - 1;
                        bj = j + 1;
                        while ((bi >= 0) && (bj <= 7) && board[bi, bj] == ChessPiece.Empty)
                        {
                            int moveLocation;
                            if ((bi - 1 >= 0) && (bj + 1 <= 7) && board[bi - 1, bj + 1] < ChessPiece.Empty)
                            {
                                moveLocation = ((bi * 1) * 10) + (bj + 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (bi * 10) + bj;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            bi--;
                            bj++;
                        }
                    }
                    else if (board[i, j] == ChessPiece.WhiteKnight)
                    {
                        int moveLocation;
                        //White knight down and right
                        if ((i + 2 <= 7) && (j + 1 <= 7) && board[i + 2, j + 1] <= ChessPiece.Empty)
                        {
                            if (board[i + 2, j + 1] < ChessPiece.Empty)
                            {
                                moveLocation = ((i + 2) * 10) + (j + 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            else
                            {
                                moveLocation = ((i + 2) * 10) + (j + 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                        }
                        //White knight up and left
                        if ((i - 2 >= 0) && (j - 1 >= 0) && board[i - 2, j - 1] <= ChessPiece.Empty)
                        {
                            if (board[i - 2, j - 1] < ChessPiece.Empty)
                            {
                                moveLocation = ((i - 2) * 10) + (j - 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            else
                            {
                                moveLocation = ((i - 2) * 10) + (j - 1);
                                move = Tuple.Create(moveLocation, 1);
                                moves[curLocation].Add(move);
                            }
                        }
                        //White knight up and right
                        if ((i - 2 >= 0) && (j + 1 <= 7) && board[i - 2, j + 1] <= ChessPiece.Empty)
                        {
                            if (board[i - 2, j + 1] < ChessPiece.Empty)
                            {
                                moveLocation = ((i - 2) * 10) + (j + 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            else
                            {
                                moveLocation = ((i - 2) * 10) + (j + 1);
                                move = Tuple.Create(moveLocation, 1);
                                moves[curLocation].Add(move);
                            }
                        }
                        //White knight down and left
                        if ((i + 2 <= 7) && (j - 1 >= 0) && board[i + 2, j - 1] <= ChessPiece.Empty)
                        {
                            if (board[i + 2, j - 1] < ChessPiece.Empty)
                            {
                                moveLocation = ((i + 2) * 10) + (j - 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            else
                            {
                                moveLocation = ((i + 2) * 10) + (j - 1);
                                move = Tuple.Create(moveLocation, 1);
                                moves[curLocation].Add(move);
                            }
                        }
                        //White knight right and down
                        if ((i + 1 <= 7) && (j + 2 <= 7) && board[i + 1, j + 2] <= ChessPiece.Empty)
                        {
                            if (board[i + 1, j + 2] < ChessPiece.Empty)
                            {
                                moveLocation = ((i + 1) * 10) + (j + 2);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            else
                            {
                                moveLocation = ((i + 1) * 10) + (j + 2);
                                move = Tuple.Create(moveLocation, 1);
                                moves[curLocation].Add(move);
                            }
                        }
                        //White knight left and up
                        if ((i - 1 >= 0) && (j + 2 <= 7) && board[i - 1, j + 2] <= ChessPiece.Empty)
                        {
                            if (board[i - 1, j + 2] < ChessPiece.Empty)
                            {
                                moveLocation = ((i - 1) * 10) + (j + 2);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            else
                            {
                                moveLocation = ((i - 1) * 10) + (j + 2);
                                move = Tuple.Create(moveLocation, 1);
                                moves[curLocation].Add(move);
                            }
                        }
                        //White knight left and down
                        if ((i + 1 <= 7) && (j - 2 >= 0) && board[i + 1, j - 2] <= ChessPiece.Empty)
                        {
                            if (board[i + 1, j - 2] < ChessPiece.Empty)
                            {
                                moveLocation = ((i + 1) * 10) + (j - 2);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            else
                            {
                                moveLocation = ((i + 1) * 10) + (j - 2);
                                move = Tuple.Create(moveLocation, 1);
                                moves[curLocation].Add(move);
                            }
                        }
                        //White knight right and up
                        if ((i - 1 <= 0) && (j + 2 <= 7) && board[i - 1, j + 2] <= ChessPiece.Empty)
                        {
                            if (board[i - 1, j + 2] < ChessPiece.Empty)
                            {
                                moveLocation = ((i - 1) * 10) + (j + 2);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            else
                            {
                                moveLocation = ((i - 1) * 10) + (j + 2);
                                move = Tuple.Create(moveLocation, 1);
                                moves[curLocation].Add(move);
                            }
                        }
                    }
                    else if (board[i, j] == ChessPiece.BlackQueen)
                    {
                        int moveLocation;
                        //white queen down
                        int ri = i + 1;
                        while ((ri <= 7) && board[ri, j] == ChessPiece.Empty)
                        {
                            if (ri + 1 <= 7 && board[ri + 1, j] < ChessPiece.Empty)
                            {
                                moveLocation = ((ri + 1) * 10 + j);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (ri * 10) + j;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            ri++;
                        }
                        //white queen right
                        int rj = i + 1;
                        while ((rj <= 7) && board[i, rj] == ChessPiece.Empty)
                        {
                            if (rj + 1 <= 7 && board[i, rj + 1] < ChessPiece.Empty)
                            {
                                moveLocation = ((i) * 10 + rj + 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (i * 10) + rj;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            rj++;
                        }
                        //white queen up
                        ri = i - 1;
                        while ((ri >= 0) && board[ri, j] == ChessPiece.Empty)
                        {
                            if (ri - 1 >= 0 && board[ri - 1, j] < ChessPiece.Empty)
                            {
                                moveLocation = ((ri - 1) * 10 + j);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (ri * 10) + j;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            ri--;
                        }
                        //white queen left
                        rj = i - 1;
                        while ((rj >= 0) && board[i, rj] == ChessPiece.Empty)
                        {
                            if (rj - 1 >= 0 && board[i, rj - 1] < ChessPiece.Empty)
                            {
                                moveLocation = ((i) * 10 + rj - 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (i * 10) + rj;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            rj--;
                        }
                        //white queen down and right
                        int bi = i + 1;
                        int bj = j + 1;
                        while ((bi <= 7) && (bj <= 7) && board[bi, bj] == ChessPiece.Empty)
                        {
                            if ((bi + 1 <= 7) && (bj + 1 <= 7) && board[bi + 1, bj + 1] < ChessPiece.Empty)
                            {
                                moveLocation = ((bi + 1) * 10) + (bj + 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (bi * 10) + bj;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            bi++;
                            bj++;
                        }
                        //white queen up and left
                        bi = i - 1;
                        bj = j - 1;
                        while ((bi >= 0) && (bj >= 0) && board[bi, bj] == ChessPiece.Empty)
                        {
                            if ((bi - 1 >= 0) && (bj - 1 >= 0) && board[bi - 1, bj - 1] < ChessPiece.Empty)
                            {
                                moveLocation = ((bi - 1) * 10) + (bj - 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (bi * 10) + bj;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            bi--;
                            bj--;
                        }
                        //white queen down and left
                        bi = i + 1;
                        bj = j - 1;
                        while ((bi <= 7) && (bj >= 0) && board[bi, bj] == ChessPiece.Empty)
                        {
                            if ((bi + 1 <= 7) && (bj - 1 >= 0) && board[bi + 1, bj - 1] < ChessPiece.Empty)
                            {
                                moveLocation = ((bi + 1) * 10) + (bj - 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (bi * 10) + bj;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            bi++;
                            bj--;
                        }
                        //white queen up and right
                        bi = i - 1;
                        bj = j + 1;
                        while ((bi >= 0) && (bj <= 7) && board[bi, bj] == ChessPiece.Empty)
                        {
                            if ((bi - 1 >= 0) && (bj + 1 <= 7) && board[bi - 1, bj + 1] < ChessPiece.Empty)
                            {
                                moveLocation = ((bi + 1) * 10) + (bj + 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (bi * 10) + bj;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            bi--;
                            bj++;
                        }
                    }
                    else if (board[i, j] == ChessPiece.WhiteRook)
                    {
                        int moveLocation;
                        //white rook down
                        int ri = i + 1;
                        while ((ri <= 7) && board[ri, j] == ChessPiece.Empty)
                        {
                            if (ri + 1 <= 7 && board[ri + 1, j] < ChessPiece.Empty)
                            {
                                moveLocation = ((ri + 1) * 10 + j);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (ri * 10) + j;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            ri++;
                        }
                        //white rook right
                        int rj = i + 1;
                        while ((rj <= 7) && board[i, rj] == ChessPiece.Empty)
                        {
                            if (rj + 1 <= 7 && board[i, rj + 1] < ChessPiece.Empty)
                            {
                                moveLocation = ((i) * 10 + rj + 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (i * 10) + rj;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            rj++;
                        }
                        //white rook up
                        ri = i - 1;
                        while ((ri >= 0) && board[ri, j] == ChessPiece.Empty)
                        {
                            if (ri - 1 >= 0 && board[ri - 1, j] < ChessPiece.Empty)
                            {
                                moveLocation = ((ri - 1) * 10 + j);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (ri * 10) + j;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            ri--;
                        }
                        //white rook left
                        rj = i - 1;
                        while ((rj >= 0) && board[i, rj] == ChessPiece.Empty)
                        {
                            if (rj - 1 >= 0 && board[i, rj - 1] < ChessPiece.Empty)
                            {
                                moveLocation = ((i) * 10 + rj - 1);
                                move = Tuple.Create(moveLocation, 2);
                                moves[curLocation].Add(move);
                            }
                            moveLocation = (i * 10) + rj;
                            move = Tuple.Create(moveLocation, 1);
                            moves[curLocation].Add(move);
                            rj--;
                        }
                    }
                    else if (board[i, j] == ChessPiece.WhiteKing)
                    {
                        //whiteKing down 1
                        if ((i + 1 <= 7) && board[i + 1, j] <= ChessPiece.Empty)
                        {
                            if (board[i + 1, j] < ChessPiece.Empty)
                            {
                                move = Tuple.Create(curLocation + 10, 1);
                            }
                            else
                            {
                                move = Tuple.Create(curLocation + 10, 2);
                            }
                            moves[curLocation].Add(move);
                        }
                        //whiteKing up 1
                        if ((i - 1 >= 0) && board[i - 1, j] <= ChessPiece.Empty)
                        {
                            if (board[i - 1, j] < ChessPiece.Empty)
                            {
                                move = Tuple.Create(curLocation - 10, 1);
                            }
                            else
                            {
                                move = Tuple.Create(curLocation - 10, 2);
                            }
                            moves[curLocation].Add(move);
                        }
                        //whiteKing left 1
                        if ((j - 1 >= 0) && board[i, j - 1] <= ChessPiece.Empty)
                        {
                            if (board[i, j - 1] < ChessPiece.Empty)
                            {
                                move = Tuple.Create(curLocation - 1, 1);
                            }
                            else
                            {
                                move = Tuple.Create(curLocation - 1, 2);
                            }
                            moves[curLocation].Add(move);
                        }
                        //whiteKing right 1
                        if ((j + 1 <= 7) && board[i, j + 1] <= ChessPiece.Empty)
                        {
                            if (board[i, j + 1] < ChessPiece.Empty)
                            {
                                move = Tuple.Create(curLocation + 1, 1);
                            }
                            else
                            {
                                move = Tuple.Create(curLocation + 1, 2);
                            }
                            moves[curLocation].Add(move);
                        }
                        //whiteKing up and right
                        if ((j + 1 <= 7) && (i + 1 <= 7) && board[i + 1, j + 1] <= ChessPiece.Empty)
                        {
                            if (board[i + 1, j + 1] < ChessPiece.Empty)
                            {
                                move = Tuple.Create(curLocation + 11, 1);
                            }
                            else
                            {
                                move = Tuple.Create(curLocation + 11, 2);
                            }
                            moves[curLocation].Add(move);
                        }
                        //whiteking down and right
                        if ((j + 1 <= 7) && (i - 1 >= 0) && board[i - 1, j + 1] <= ChessPiece.Empty)
                        {
                            int moveLocation = ((i - 1) * 10) + (j + 1);
                            if (board[i - 1, j + 1] < ChessPiece.Empty)
                            {
                                move = Tuple.Create(moveLocation, 1);
                            }
                            else
                            {
                                move = Tuple.Create(moveLocation, 2);
                            }
                            moves[curLocation].Add(move);
                        }
                        //whiteking up and left
                        if ((j - 1 >= 0) && (i + 1 <= 7) && board[i + 1, j - 1] <= ChessPiece.Empty)
                        {
                            int moveLocation = ((i + 1) * 10) + (j - 1);
                            if (board[i + 1, j - 1] < ChessPiece.Empty)
                            {
                                move = Tuple.Create(moveLocation, 1);
                            }
                            else
                            {
                                move = Tuple.Create(moveLocation, 2);
                            }
                            moves[curLocation].Add(move);
                        }
                        //whiteKing down and left
                        if ((j - 1 >= 0) && (i - 1 >= 0) && board[i - 1, j - 1] <= ChessPiece.Empty)
                        {
                            int moveLocation = ((i - 1) * 10) + (j - 1);
                            if (board[i - 1, j - 1] < ChessPiece.Empty)
                            {
                                move = Tuple.Create(moveLocation, 1);
                            }
                            else
                            {
                                move = Tuple.Create(moveLocation, 2);
                            }
                            moves[curLocation].Add(move);
                        }
                    }
                }
            }
            return moves;
        }
        public ChessMove logic(Dictionary<int, HashSet<Tuple<int,int>>> validMoves, ChessColor myColor) {
            Tuple<int,int> bestMoves = new Tuple<int, int>(); //create dictionary for each 'from' with best move choice
            Tuple<int,int> chosenMove;
            int start = 0;
            int multiplier = 0;
            int score;
            if(myColor == 0) //my color is white
            {
                start = 0;
                multiplier = 1;

            }
            else  //my color is black
            {
                start = 7;
                multiplier = -1;
            }
            for(int i = start; i< i+7; ++i)
            {
                foreach(Tuple t in validMoves[i].value)
                {
                    score = t[i].Item2.max() * multiplier;
                    bestMoves.Add(validMove[i].Key, score);
                }
            }
            foreach(Tuple<int,int> t in bestMoves)
            {
                chosenMove = (t.Item1, t.Item2.max());
            }
            ChessLocation moveFrom = new ChessLocation(chosenMove.Item1/10, chosenMove.Item1%10);
            ChessLocation moveTo = new ChessLocation(chosenMove.Item2/10, chosenMove.Item2%10);
            ChessMove move = new ChessMove(moveFrom, moveTo);
            return move;
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
            Dictionary<int, HashSet<Tuple<int,int>>> moves = GenMoves(board);
            ChessMove chosenMove = logic(moves, myColor);
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
