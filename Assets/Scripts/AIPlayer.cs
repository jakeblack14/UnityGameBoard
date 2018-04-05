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

        private static uint mode = 2;

        public static void setMode(int modeNumber)
        {
            if (modeNumber > 0 && modeNumber < 3)
            {
                mode = 2 - (uint)modeNumber;
            }
        }

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
            ulong[,] AISQUARES = { { 1, 2, 4, 8, 16, 32, 64, 128 },
                { 256, 512, 1024, 2048, 4096, 8192, 16384, 32768 },
                { 65536, 131072, 262144, 524288, 1048576, 2097152, 4194304, 8388608 },
                {  16777216, 33554432, 67108864, 134217728, 268435456, 536870912, 1073741824, 2147483648 },
                { 4294967296, 8589934592, 17179869184, 34359738368, 68719476736, 137438953472, 274877906944, 549755813888 },
                { 1099511627776, 2199023255552, 4398046511104, 8796093022208, 17592186044416, 35184372088832, 70368744177664, 140737488355328 },
                { 281474976710656, 562949953421312, 1125899906842624, 2251799813685248, 4503599627370496, 9007199254740992, 18014398509481984, 36028797018963968 },
                { 72057594037927936, 144115188075855872, 288230376151711744, 576460752303423488,
                    1152921504606846976, 2305843009213693952, 4611686018427387904, 9223372036854775808}
            };


            ulong black = 0;
            ulong white = 0;

            for (uint i = 0; i < 8; i++)
            {
                for (uint j = 0; j < 8; j++)
                {
                    if (board.blackRows[i] % board.COLUMNS[j] == 0)
                    {
                        black += AISQUARES[i, j];
                    }
                    if (board.whiteRows[i] % board.COLUMNS[j] == 0)
                    {
                        white += AISQUARES[i, j];
                    }
                }
            }


            //Get the move from the AI DLL
            //pass IsWhite for if IsWhitesTurn, since, if we are white, and it is our turn, then it is whites turn
            AICore.AIMove nextMove = AICore.AICore.AIGetMove(board.blackCount, board.whiteCount, black, white, isWhite, mode);

             //Convert the AIMove to a Move class
             if (isWhite)
             {
                 result.Begin.row = 7 - checked((int)nextMove.row);
                 result.Begin.col = 7 - checked((int)nextMove.col);
                 result.End.row = 7 - checked((int)nextMove.row + 1);
                result.End.col = 7 - (checked((int)nextMove.col) + checked((int)nextMove.target) - 1);

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
