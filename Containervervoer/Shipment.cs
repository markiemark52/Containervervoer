using System;
using System.Collections.Generic;
using System.Text;

namespace Containervervoer
{
    public class Shipment
    {
        private Ship ship;
        private List<Container> containers;

        public Ship Ship
        {
            get { return ship; }
        }
        public List<Container> Containers
        {
            get { return containers; }
        }

        public Shipment()
        {
            containers = new List<Container>();
        }

        public void GenerateShipment()
        {
            ship = GenerateShip();
            containers = GenerateContainers();
        }

        public void GenerateCustomShipment(int length, int width, int weight, int cool, int valuable, int normal)
        {
            ship = new Ship(length, width, weight);
            containers = AddContainers(cool, valuable, normal);
        }

        private Ship GenerateShip()
        {
            Random r = new Random();
            int length = r.Next(5, 11);
            int width = r.Next(3, 6);
            int weight = r.Next(1000, 3001);
            return new Ship(length, width, weight);
        }

        private List<Container> GenerateContainers()
        {
            Random r = new Random();
            int amount = r.Next(50, 151);
            List<Container> containers = new List<Container>();

            for (int i = 0; i < amount; i++)
            {
                int weight = r.Next(27);
                bool cool = false;
                bool valuable = false;

                int coolOrValuable = r.Next(10);
                if (coolOrValuable == 1 || coolOrValuable == 2)
                {
                    cool = true;
                }
                else if (coolOrValuable == 0)
                {
                    valuable = true;
                }

                containers.Add(new Container(weight, cool, valuable));
            }

            return containers;
        }

        private List<Container> AddContainers(int cool, int valuable, int normal)
        {
            Random r = new Random();
            List<Container> containers = new List<Container>();
            for (int c = 0; c < cool; c++)
            {
                containers.Add(new Container(r.Next(27), true, false));
            }
            for (int v = 0; v < valuable; v++)
            {
                containers.Add(new Container(r.Next(27), false, true));
            }
            for (int n = 0; n < normal; n++)
            {
                containers.Add(new Container(r.Next(27), false, false));
            }
            return containers;
        }

        public void Organize()
        {
            List<Container> coolContainers = GetCool();
            List<Container> valuableContainers = GetValuable();

            OrganizeCool(coolContainers);
            OrganizeNormal(containers);
            OrganizeValuable(valuableContainers);

            ReAddContainers(coolContainers, valuableContainers);
        }

        private List<Container> GetCool()
        {
            List<Container> coolContainers = new List<Container>();
            foreach (Container c in containers)
            {
                if (c.Cool)
                {
                    coolContainers.Add(c);
                }
            }
            foreach (Container c in coolContainers)
            {
                containers.Remove(c);
            }
            return coolContainers;
        }

        private List<Container> GetValuable()
        {
            List<Container> valuableContainers = new List<Container>();
            foreach (Container c in containers)
            {
                if (c.Valuable)
                {
                    valuableContainers.Add(c);
                }
            }
            foreach (Container c in valuableContainers)
            {
                containers.Remove(c);
            }
            return valuableContainers;
        }

        private void ReAddContainers(List<Container> coolContainers, List<Container> valuableContainers)
        {
            foreach (Container c in coolContainers)
            {
                containers.Add(c);
            }

            foreach (Container c in valuableContainers)
            {
                containers.Add(c);
            }
        }

        private void OrganizeCool(List<Container> containers)
        {
            foreach (Container c in containers)
            {
                foreach (Stack s in ship.Rows[0].Stacks)
                {
                    if (s.Weight == GetLowestWeight(ship.Rows[0].Stacks))
                    {
                        if (s.AddCoolContainer(c))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Not all cool containers fit on the ship");
                        }
                    }
                }
            }
        }

        private void OrganizeNormal(List<Container> containers)
        {
            foreach (Container c in containers)
            {
                for (int r = 1; r < ship.Length; r++)
                {
                    bool placed = false;
                    foreach (Stack s in ship.Rows[r].Stacks)
                    {
                        if (s.Weight == GetLowestWeight(ship.Rows[r].Stacks))
                        {
                            if (s.AddNormalContainer(c))
                            {
                                placed = true;
                                break;
                            }
                        }
                    }
                    if (placed)
                    {
                        break;
                    }
                    else if (c == containers[containers.Count - 1] && r == ship.Length - 1)
                    {
                        Console.WriteLine("Not all normal containers fit on the ship");
                    }
                }
            }
        }

        private void OrganizeValuable(List<Container> containers)
        {
            bool placed = false;
            for (int c = 0; c < containers.Count; c++)
            {
                placed = false;
                for (int r = 1; r < ship.Length; r++)
                {
                    for (int s = 0; s < ship.Rows[r].Stacks.Count; s++)
                    {
                        if (!ship.Rows[r].Stacks[s].Full)
                        {
                            int heightFront = ship.Rows[r - 1].Stacks[s].Containers.Count;
                            bool frontValuable = ship.Rows[r - 1].Stacks[s].Full;
                            int heightBack = -1;
                            if (r < ship.Rows.Count - 1)
                            {
                                heightBack = ship.Rows[r + 1].Stacks[s].Containers.Count;
                            }
                            bool frontFixed = ship.Rows[r - 1].Stacks[s].Front;

                            if (ship.Rows[r].Stacks[s].AddValuableContainer(containers[c], heightFront, heightBack, frontValuable, frontFixed))
                            {
                                placed = true;
                                break;
                            }
                        }
                    }
                    if (placed)
                    {
                        break;
                    }
                    else if (c == containers.Count - 1 && r == ship.Length - 1)
                    {
                        Console.WriteLine("Not all valuable containers fit on the ship");
                    }
                }
            }
        }

        private int GetLowestWeight(List<Stack> stacks)
        {
            int lowestWeight = stacks[0].Weight;
            foreach (Stack s in stacks)
            {
                if (s.Weight < lowestWeight)
                {
                    lowestWeight = s.Weight;
                }
            }
            return lowestWeight;
        }
        
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void OrganizeCoolRecursively(List<Container> containers)
        {
            int highestWeight = 0;
            foreach (Container c in containers)
            {
                highestWeight = PlaceContainer(c, highestWeight, false);
            }
        }

        private int PlaceContainer(Container container, int highestWeight, bool retry)
        {
            foreach (Stack s in ship.Rows[0].Stacks)
            {
                if ((s.Containers.Count == 0 || retry) && s.AddCoolContainer(container))
                {
                    return GetHighestWeight();
                }
                else if (s.Weight < highestWeight && s.AddCoolContainer(container))
                {
                    Console.WriteLine("Y O I N K");
                    return GetHighestWeight();
                }
                else if (s == ship.Rows[0].Stacks[ship.Rows[0].Stacks.Count - 1])
                {
                    PlaceContainer(container, highestWeight, true);
                }
                else
                {
                    Console.WriteLine("Y I K E R S");
                }
            }
            throw new Exception("Row empty!");
        }

        private int GetHighestWeight()
        {
            int highestWeight = 0;
            foreach (Stack s in ship.Rows[0].Stacks)
            {
                if (s.Weight > highestWeight)
                {
                    highestWeight = s.Weight;
                }
            }
            return highestWeight;
        }

        //private int PlaceContainerBackup(Container container, int maxWeight, bool retry)
        //{
        //    foreach (Stack s in ship.Rows[0].Stacks)
        //    {
        //        //if ((retry || s.Containers.Count == 0/*s.Weight == 0*/) && s.AddCoolContainer(container))
        //        //{
        //        //    Console.WriteLine(s.Weight + " - " + retry);Console.ReadKey();
        //        //    return 0;
        //        //}
        //        if (retry || s.Containers.Count == 0)
        //        {
        //            Console.WriteLine(s.Containers.Count); Console.ReadKey();
        //            s.AddCoolContainer(container);
        //            return s.Weight - s.Containers[0].Weight;
        //        }
        //        else
        //        {
        //            Console.WriteLine("StackWeight: " + (s.Weight - s.Containers[0].Weight) + ", MaxWeight: " + maxWeight); Console.ReadKey();
        //            if ((s.Weight - s.Containers[0].Weight) + container.Weight <= maxWeight /*&& s.AddCoolContainer(container)*/)
        //            {
        //                Console.WriteLine("REEEEEEEEEEEEEEEEE"); Console.ReadKey();
        //                return (s.Weight - s.Containers[0].Weight) + container.Weight;
        //            }
        //        }
        //    }
        //    Console.WriteLine("YEEEEET");Console.ReadKey();
        //    PlaceContainer(container, maxWeight, true);
        //    return 0;
        //}
    }
}
