using System;
using System.Collections.Generic;
using System.Text;

namespace Containervervoer
{
    public class Container
    {
        private int weight;
        private bool cool;
        private bool valuable;

        public int Weight
        {
            get { return weight; }
        }
        public bool Cool
        {
            get { return cool; }
        }
        public bool Valuable
        {
            get { return valuable; }
        }

        public Container(int weight, bool cool, bool valuable)
        {
            this.weight = 4 + weight;
            this.cool = cool;
            this.valuable = valuable;
        }
    }
}
