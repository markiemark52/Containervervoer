using System;
using System.Collections.Generic;
using System.Text;

namespace Containervervoer
{
    public class Row
    {
        private List<Stack> stacks;
        private int width;

        public List<Stack> Stacks
        {
            get { return stacks; }
        }

        public Row(int width)
        {
            this.width = width;
            stacks = AddStacks();
        }

        private List<Stack> AddStacks()
        {
            List<Stack> stacks = new List<Stack>();
            for (int i = 0; i < width; i++)
            {
                stacks.Add(new Stack());
            }
            return stacks;
        }
    }
}
