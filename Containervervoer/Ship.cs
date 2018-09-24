using System;
using System.Collections.Generic;
using System.Text;

namespace Containervervoer
{
    public class Ship
    {
        private List<Row> rows;
        private int length;
        private int width;
        private int weight;

        public List<Row> Rows
        {
            get { return rows; }
        }
        public int Length
        {
            get { return length; }
        }
        public int Width
        {
            get { return width; }
        }
        public int Weight
        {
            get { return weight; }
        }

        public Ship(int length, int width, int weight)
        {
            this.length = length;
            this.width = width;
            this.weight = weight;
            rows = AddRows();
        }

        private List<Row> AddRows()
        {
            List<Row> rows = new List<Row>();
            for (int i = 0; i < length; i++)
            {
                rows.Add(new Row(width));
            }
            return rows;
        }
    }
}