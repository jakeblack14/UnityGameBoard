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
