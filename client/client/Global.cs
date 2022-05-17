using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    public static class Global
    {
        public static NetworkFuncClient FuncClient { get; set; }
        public static NetworkFuncServer FuncServer { get; set; }
        public static blockChain blkchn { get; set; }
        
    }
}
