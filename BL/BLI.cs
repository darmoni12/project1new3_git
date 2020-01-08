using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLI
    {
        static IBL instance = null;
        static public IBL GetBL()
        {
            if (instance == null)
                instance = new BL_imp();
            return instance;
        }
    }
}
