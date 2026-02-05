using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Complier
{  
 
    public class Env
    {
        private System.Collections.Hashtable table;
        protected Env Prev;
        public Env(Env n)
        {
            table = new System.Collections.Hashtable();
            Prev = n;
        }
        public void Add(Token w, ID i)
        {
            table.Add(w, i);
        }
        public ID Get(Token w)
        {
            for (Env e = this; e != null; e = e.Prev)
            {
                ID found = (ID)table[w];
                if (found != null)
                {
                    return found;
                }
            }
            return null;
        }
    } 

}
