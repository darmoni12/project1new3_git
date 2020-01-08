using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Diary
    {
        public bool[,] diary;
        /// <summary>
        /// ctor
        /// </summary>
        public Diary()
        {
            diary = new bool[12, 31];
        }
        //indexer
        public bool this[MyDate date]
        {
            get
            {
                return diary[date.Month - 1, date.Day - 1];
            }
            set
            {
                diary[date.Month - 1, date.Day - 1] = value;
            }
        }
       
    }
}
