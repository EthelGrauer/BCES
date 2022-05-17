using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    public class blockChain
    {
        private LinkedList<Block> genesis;
        public blockChain()
        {

        }
        public blockChain(Block block)
        {
            genesis.AddFirst(block);
        }
        public string getLastHash()
        {
            return genesis.Last.Value.getHash();
        }
        public bool isEmpty()
        {
            if (genesis==null)
            {
                return true;
            }
            return false;
        }
        public void addBlock(Block block)
        {
            if (isEmpty())
            {
                genesis = new LinkedList<Block>();
                genesis.AddFirst(block);
            }  
            else
                genesis.AddLast(block);


        }
        public Block LastOne()
        {
            return genesis.Last.Value;
        }
        public Block PrevtoLast()
        {
            if (genesis.Count>1)
                 return genesis.Last.Previous.Value;
            return null;
        }
        public Block twoBeforeEnd()
        {
            
            if (genesis.Count > 2)
                return genesis.Last.Previous.Previous.Value;
            return null;
        }
    }
}
