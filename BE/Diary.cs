﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BE
{
    [Serializable]
    public class Diary
    {
        [XmlIgnore]
        public bool[,] diary { get; set; }

        [XmlArray("diary")]
        public bool[] DiaryDto
        {
            get { return diary.Flatten(); }
            set { diary = value.Expand(12); } //12 is the number of roes in the matrix
        }
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
