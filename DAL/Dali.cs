using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Dali
    {
        static Idal instance = null;
        static public Idal GetDal()
        {
            if (instance == null)
                instance = new Dal_XML_imp();
            return instance;
        }
    }
}
