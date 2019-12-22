using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StatisticTime
{
    //DataContract for Serializing Data - required to serve in JSON format
    [DataContract]
    public class StatisticTimeData
    {
        public StatisticTimeData(String x, int y, string label)
        {
            this.X = x;
            this.Y = y;
            this.LABEL = label;
        }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "x")]
        public String X = null;

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public Nullable<int> Y = null;

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "label")]
        public string LABEL = null;
    }
}