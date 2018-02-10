using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
   

    public class Player
    {
        private identity playerIdentity;
        public bool turn = false;
     
        public Player(identity newIdentity)
        {
            playerIdentity = newIdentity;
        }

        public void setPlayer(identity iden)
        {
            playerIdentity = iden;
        }

        public identity getIdentity()
        {
            return playerIdentity;
        }

        public virtual Move getMove()
        {
            return null;
        }

        public virtual bool isAI()
        {
            return false;
        }

        public virtual bool isNetwork()
        {
            return false;
        }

        public virtual void requestMove()
        { }

        public virtual bool hasRequestedMove()
        {
            return false;
        }


        public virtual bool hasMove()
        {
            return false;
        }

        /*public char getIdentity()
        {
            if (playerIdentity == identity.X)
            {
                return Config.XCHAR;
            }
            else
            {
                return Config.OCHAR;
            }
            //return (char)playerIdentity;
        }*/
    }
}
