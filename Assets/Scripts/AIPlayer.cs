using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;

namespace GameCore
{
    class AIPlayer : Player
    {

        //Imports C++ AI DLL
        //[DllImport(@"AICore.dll")]
        //private static extern AIMove AIGetMove(int blackCount, int whiteCount, uint[] blackRows, uint[] whiteRows, bool isWhitesTurn);

        private Move newMove = null;
        private bool movePending = false;
        private bool isWhite;

        public AIPlayer(identity newIdentity, bool isWhite) :
        base(newIdentity)
        {
            //Save whether the AI is white or not
            this.isWhite = isWhite;
        }


        public override bool isAI()
        {
            return true;
        }

        public override bool hasMove()
        {
            if (newMove == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public override void requestMove()
        {
            Thread thread = new Thread(executeRequestMove);
            thread.Start();
            movePending = true;
        }

        public override bool hasRequestedMove()
        {
            return movePending;
        }

        private void executeRequestMove()
        {
            //Create the resulting move
            Move result = new Move();

            //Get first player identity and board


            //get the board state
            Board board = BoardManager.getBoard();

            //Convert to AISpace
            uint[] AICOLUMNS = new uint[] { 1, 2, 4, 8, 16, 32, 64, 128 }; //A-H

            uint[] blackRows = new uint[8];
            uint[] whiteRows = new uint[8];


            for (int i = 0; i < 8; i++)
            {
                blackRows[i] = 0;
                whiteRows[i] = 0;
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board.blackRows[i] % board.COLUMNS[j] == 0)
                    {
                        blackRows[i] += AICOLUMNS[j];
                    }
                    if (board.whiteRows[i] % board.COLUMNS[j] == 0)
                    {
                        whiteRows[i] += AICOLUMNS[j];
                    }
                }
            }

            //Get the move from the AI DLL
            //pass IsWhite for if IsWhitesTurn, since, if we are white, and it is our turn, then it is whites turn
            AICore.AIMove nextMove = AICore.AICore.AIGetMove(board.blackCount, board.whiteCount, blackRows, whiteRows, isWhite, 0);

            //Convert the AIMove to a Move class
            if (isWhite)
            {
                result.Begin.row = 7 - checked((int)nextMove.row);
                result.Begin.col = 7 - checked((int)nextMove.col);
                result.End.row = 7 - checked((int)nextMove.row + 1);
                int target;
                switch (nextMove.target)
                {
                    case 0:
                        target = 2;
                        break;
                    case 1:
                        target = 1;
                        break;
                    default: //case 2:
                        target = 0;
                        break;
                }
                result.End.col = 7 - checked((int)nextMove.col) + target - 1;
            }
            else
            {
                result.Begin.row = checked((int)nextMove.row);
                result.Begin.col = checked((int)nextMove.col);
                result.End.row = checked((int)nextMove.row - 1);
                result.End.col = checked((int)nextMove.col) + checked((int)nextMove.target) - 1;

            }

            newMove = result;
        }

        public override Move getMove()
        {
            //Return the result and reset newMove
            Move result = newMove;
            newMove = null;
            movePending = false;
            return result;

        }

        ~AIPlayer()
        {
            AICore.AICore.EmptyMemory();
        }


    }
}
