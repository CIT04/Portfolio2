﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Objects
{
    public class Rating
    {
     
        public string M_id { get; set; }
        public string ImdbRatings { get; set;}
        public string Ratings { get; set;}
        public string ImdbVotes { get; set;}
        public double AverageRating { get; set;}
        public int NumVotes { get; set;}

    }
}
