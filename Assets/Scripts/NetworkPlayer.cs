using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.Threading;
using TechPlanet.SpaceRace;

namespace GameCore
{
    public class NetworkPlayer : Player 
    {

        //Imports C++ AI DLL
        //[DllImport(@"AICore.dll")]
        //private static extern AIMove AIGetMove(int blackCount, int whiteCount, uint[] blackRows, uint[] whiteRows, bool isWhitesTurn);

        public Move newMove = null;
        public bool movePending = false;


        public NetworkPlayer(identity newIdentity) :
        base(newIdentity)
        {
        }

        public override bool isNetwork()
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
            // Need to change this function
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

            //Note: defining X as always white as always first here!
            identity first = identity.X;

            //get the board state
            Board board = BoardManager.getBoard();

            //Decide if this is whites turn or not
            bool isWhitesTurn = (first == this.getIdentity());


            //Get the move from the AI DLL
            AICore.AIMove nextMove = AICore.AICore.AIGetMove(board.blackCount, board.whiteCount, board.blackRows, board.whiteRows, isWhitesTurn);

            //Convert the AIMove to a Move class
            result.Begin.row = nextMove.row;
            result.Begin.col = nextMove.col;
            if (isWhitesTurn)
            {
                result.End.row = nextMove.row + 1;
            }
            else
            {
                result.End.row = nextMove.row - 1;
            }
            result.End.col = nextMove.col + nextMove.target - 1;

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
    }
}
