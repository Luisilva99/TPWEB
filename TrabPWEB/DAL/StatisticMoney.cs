using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TrabPWEB.DAL
{
    [DataContract]
    public class StatisticMoney
    {
        public StatisticMoney(int x, decimal y, String label)
        {
            this.X = x;
            this.Y = y;
            this.LABEL = label;
        }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "x")]
        public Nullable<int> X = null;

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public Nullable<decimal> Y = null;

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "label")]
        public String LABEL = null;
    }
}