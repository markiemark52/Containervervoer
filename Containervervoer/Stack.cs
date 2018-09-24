using System;
using System.Collections.Generic;
using System.Text;

namespace Containervervoer
{
    public class Stack
    {
        private List<Container> containers;
        private int weight;
        private bool full;
        private bool front;

        public List<Container> Containers
        {
            get { return containers; }
        }
        public int Weight
        {
            get { return weight; }
        }
        public bool Full
        {
            get { return full; }
        }
        public bool Front
        {
            get { return front; }
        }

        public Stack()
        {
            containers = new List<Container>();
            weight = 0;
            full = false;
            front = false;
        }

        public bool AddCoolContainer(Container container)
        {
            if (weight == 0)
            {
                containers.Add(container);
                weight += container.Weight;
                front = true;
                return true;
            }
            else if ((weight - containers[0].Weight) + container.Weight <= 120)
            {
                containers.Add(container);
                weight += container.Weight;
                front = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddNormalContainer(Container container)
        {
            if (weight == 0)
            {
                containers.Add(container);
                weight += container.Weight;
                return true;
            }
            else if ((weight - containers[0].Weight) + container.Weight <= 90)
            {
                containers.Add(container);
                weight += container.Weight;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddValuableContainer(Container container, int heightFront, int heightBack, bool frontValuable, bool frontFixed)
        {
            if (containers.Count >= heightFront)
            {
                if (!frontValuable)
                {
                    containers.Add(container);
                    full = true;
                    front = true;
                    return true;
                }
                else if ((containers.Count >= heightBack || heightBack == -1) && frontFixed)
                {
                    containers.Add(container);
                    full = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (containers.Count >= heightBack || heightBack == -1)
            {
                containers.Add(container);
                full = true;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
