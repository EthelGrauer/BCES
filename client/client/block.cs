using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace client
{
    
    public class Block
    {
        private Block_Data header;
        private string hash_prev;
        private string hash_this;

        
        public Block(string hash,  User user)
        {
            this.hash_prev = hash;
           
            header = new Block_Data(user);
            cipher();
        }
        public void cipher()
        {
            Random rnd = new Random();
            hash_this = Encipher(hash_prev.Substring(5,7)+header.toString(),rnd.Next(1,26));
           
        }
        public  bool verifyMagic_num(int num)
        {
            return (num % 9 == 0);
            
        }
        private bool verify_info()
        {
            for (int i=0;i<26;i++)
            {
                if(CheckIDNo(Decipher(hash_this.Substring(7,9),i)))
                {
                    return true;
                }
                
            }
            return false;

        }
        public int get_magic_num()
        {
            return header.get_magic_num();
        }
        public void set_magic_num(int num)
        {
            this.header.set_magic_num(num);
        }
        public string getHash()
        {
            return this.hash_this;
        }
        private static char Cipher(char ch, int key)
        {
            //if (!char.IsLetter(ch))
            //    return ch;
            
            char offset =  'a';
            return (char)((((ch + key) - offset) % 26) + offset);
        }

        private static string Encipher(string input, int key)
        {
            string output = string.Empty;
            input= input.ToLower();
            foreach (char ch in input)
                output += Cipher(ch, key);

            return output;
        }

        private string Decipher(string input, int key)
        {
            return Encipher(input, 26 - key);
        }

        static bool CheckIDNo(string strID)
        {
            int[] id_12_digits = { 1, 2, 1, 2, 1, 2, 1, 2, 1 };
            int count = 0;

            if (strID == null)
                return false;

            strID = strID.PadLeft(9, '0');

            for (int i = 0; i < 9; i++)
            {
                int num = Int32.Parse(strID.Substring(i, 1)) * id_12_digits[i];

                if (num > 9)
                    num = (num / 10) + (num % 10);

                count += num;
            }

            return (count % 10 == 0);
        }

        //private static byte[] GetHash(string inputString)
        //{
        //    using (HashAlgorithm algorithm = SHA256.Create())
        //        return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        //}

        //public static string GetHashString(string inputString)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    foreach (byte b in GetHash(inputString))
        //        sb.Append(b.ToString("X2"));

        //    return sb.ToString();
        //}
        public bool verify_block()
        {
            if (this.verifyMagic_num(this.get_magic_num()) && this.verify_info())
                return true;
            return false;
        }
        public string tostring()
        {
           return header.get_magic_num().ToString() + "@" + header.toString()+"@" + hash_this + "@" + hash_prev;
        }
    }
    
}
