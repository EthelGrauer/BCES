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

        public blockChain(Block block)
        {
            genesis.AddFirst(block);
        }
        public string getLastHash()
        {
            return genesis.Last.Value.getHash();
        }
        public void addBlock(Block block)
        {
            genesis.AddLast(block);


        }
        public Block LastOne()
        {
            return genesis.Last.Value;
        }
        public Block PrevtoLast()
        {
            return genesis.Last.Previous.Value;
        }
        public Block twoBeforeEnd()
        {
            return genesis.Last.Previous.Previous.Value;
        }
    }
}
