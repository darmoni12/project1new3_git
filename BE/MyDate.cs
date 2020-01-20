using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class MyDate : IComparable
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

       public MyDate() { }
        
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="d"></param>
        /// <param name="m"></param>
        /// <param name="y"></param>
        public MyDate(int d, int m, int y)
        {
            Day = d;
            Month = m;
            Year = y;
        }

        public MyDate(string date)
        {
            string[] s = date.Split('/');
            Day = Int32.Parse(s[0]);
            Month = Int32.Parse(s[1]);
            Year = Int32.Parse(s[2]);
        }

        /// <summary>
        /// override for to string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Day + "." + Month + "." + Year;
        }

        public static MyDate today()
        {
            return new MyDate(DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year);
        }

        /// <summary>
        /// add num of days to the MyDate object
        /// </summary>
        /// <param name="add"></param>
        public void addDays(int add)
        {

            int days = add + this.Day;
            int monthes = (days / 31) + this.Month;
            days = days % 31;
            if (days == 0)
            {
                days = 31;
                monthes -= 1;
            }
            int years = (monthes / 12) + this.Year;
            monthes = monthes % 12;
            if (monthes == 0)
            {
                monthes = 12;
                years -= 1;
            }
            this.Day = days;
            this.Month = monthes;
            this.Year = years;
        }

        /// <summary>
        /// compare to with icomparable interface
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            MyDate otherDate = obj as MyDate;

            if (this.Year > otherDate.Year)
                return 1;
            if (this.Year < otherDate.Year)
                return -1;

            if (this.Month > otherDate.Month)
                return 1;
            if (this.Month < otherDate.Month)
                return -1;

            if (this.Day > otherDate.Day)
                return 1;
            if (this.Day < otherDate.Day)
                return -1;


            return 0;
        }
    }
}
