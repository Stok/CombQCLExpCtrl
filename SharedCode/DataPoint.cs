﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SharedCode
{
    public class DataPoint
    {
        public List<KeyValuePair<string, double>> kvPairs;

        public DataPoint(string name, double val)
        {
            kvPairs = new List<KeyValuePair<string, double>>();
            kvPairs.Add(new KeyValuePair<string, double>(name, val));
        }

        public DataPoint(string[] names, double[] vals)
        {
            kvPairs = new List<KeyValuePair<string, double>>();
            for (int i = 0; i < names.Length; i++)
            {
                kvPairs.Add(new KeyValuePair<string, double>(names[i], vals[i]));
            }
        }

        public int Dimensions()
        {
            return kvPairs.Count;
        }
        public JObject ToJson()
        {
            JObject o = new JObject();
            for(int i = 0; i < Dimensions(); i++)
            {
                o[kvPairs[i].Key] = kvPairs[i].Value;
            }
            return o;
        }

        public void Add(string[] names, double[] vals)
        {
            for (int i = 0; i < names.Length; i++)
            {
                kvPairs.Add(new KeyValuePair<string, double>(names[i], vals[i]));
            }
        }

        public double GetValueOfKey(string key)
        {
            foreach(KeyValuePair<string, double> p in kvPairs)
            {
                if(p.Key.Equals(key))
                {
                    return p.Value;
                }
            }
            ArgumentException a = new ArgumentException("No value found for this key.");
            throw a;
           
        }
        
    }

}