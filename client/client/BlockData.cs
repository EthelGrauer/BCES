using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    public class Block_Data
    {
        private int magic_num;
        private User us;
        public Block_Data(User user)
        {
            int num = 0, sum = 0;
            magic_num = 0;
            Random rnd = new Random();//geberates magic num. 4 random digits. adding 5 digits must be dividable by 9
            
            for (int i = 0; i < 4; i++)
            {

                num = rnd.Next(1, 10);
                sum += num;
                magic_num += num;
                magic_num *= 10;
            }
            this.magic_num += 9 - (sum % 9);
            //intialize
            us = new User();
            us.id = user.id;
            us.name = user.name;
            us.vote = user.vote;
        }
        public string toString()
        {
            return us.id.ToString() + "@" + us.name + "@" + us.vote;
        }
        public int get_magic_num()
        {
            return this.magic_num;
        }
        public void set_magic_num(int num)
        {
            this.magic_num = num;
        }
        public User get_user()
        {
            return us;
        }
    }
}
