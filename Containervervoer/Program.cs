using System;

namespace Containervervoer
{
    class Program
    {
        static void Main(string[] args)
        {
            Shipment shipment = new Shipment();
            ShowHelp();

            while (true)
            {
                string input = Console.ReadLine();
                if (input.Equals("Help", StringComparison.CurrentCultureIgnoreCase))
                {
                    ShowHelp();
                }
                else if (input.Equals("GenerateShipment", StringComparison.CurrentCultureIgnoreCase) || input.Equals("q", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.Clear();
                    shipment.GenerateShipment();
                    Console.WriteLine("Shipment generated\n");
                    ShowShipment(shipment);
                }
                else if (input.Equals("GenerateCustomShipment", StringComparison.CurrentCultureIgnoreCase) || input.Equals("e", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.Clear();
                    shipment = CustomShipment(shipment);
                    Console.WriteLine("\nShipment generated\n");
                }
                else if (input.Equals("ShowShipment", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.Clear();
                    ShowShipment(shipment);
                }
                else if (input.Equals("OrganizeShipment", StringComparison.CurrentCultureIgnoreCase) || input.Equals("w", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.Clear();
                    shipment.Organize();
                    Console.WriteLine("Shipment organized\n");
                    ShowShip(shipment);
                }
                else if (input.Equals("ShowShip", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.Clear();
                    ShowShip(shipment);
                }
            }
        }

        private static void ShowHelp()
        {
            Console.Clear();
            Console.WriteLine("Commands:");
            Console.WriteLine("Help");
            Console.WriteLine("GenerateShipment");
            Console.WriteLine("GenerateCustomShipment");
            Console.WriteLine("ShowShipment");
            Console.WriteLine("OrganizeShipment");
            Console.WriteLine("ShowShip");
        }

        private static void ShowShipment(Shipment shipment)
        {
            Console.WriteLine("Shipment");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Ship:");
            Console.WriteLine("Length: " + shipment.Ship.Length + ", Width: " + shipment.Ship.Width);
            Console.WriteLine("\nContainers:");
            foreach (Container c in shipment.Containers)
            {
                Console.WriteLine("Weight: " + c.Weight + ", Cool: " + c.Cool + ", Valuable: " + c.Valuable);
            }

            int cool = 0;
            int valuable = 0;
            int remaining = 0;
            foreach (Container c in shipment.Containers)
            {
                if (c.Cool)
                {
                    cool++;
                }
                else if (c.Valuable)
                {
                    valuable++;
                }
                else
                {
                    remaining++;
                }
            }
            Console.WriteLine("\nCool: " + cool + ", Valuable: " + valuable + ", Remaining: " + remaining);
        }

        private static void ShowShip(Shipment shipment)
        {
            Console.WriteLine("Ship: Length - " + shipment.Ship.Length + ", Width - " + shipment.Ship.Width + ", Weight - " + shipment.Ship.Weight);
            Console.WriteLine("----------------------------------------");

            int rowNr = 1;
            foreach (Row r in shipment.Ship.Rows)
            {
                Console.WriteLine("\nRow " + rowNr + ":");
                int stackNr = 1;
                foreach (Stack s in r.Stacks)
                {
                    Console.WriteLine("    Stack " + stackNr + ":");
                    foreach (Container c in s.Containers)
                    {
                        Console.WriteLine("        Weight: " + c.Weight + ", Cool: " + c.Cool + ", Valuable: " + c.Valuable);
                    }
                    stackNr++;
                }
                rowNr++;
            }

            int containerWeight = 0;
            foreach (Container c in shipment.Containers)
            {
                containerWeight += c.Weight;
            }
            if (containerWeight < shipment.Ship.Weight / 2)
            {
                Console.WriteLine("\nContainers too light, risk of capsizing");
            }
        }

        private static Shipment CustomShipment(Shipment shipment)
        {
            Console.WriteLine("Custom shipment");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Enter ship length:");
            int length = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nEnter ship width:");
            int width = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nEnter ship weight");
            int weight = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter amount of cooled containers:");
            int cool = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nEnter amount of valuable containers:");
            int valuable = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nEnter amount of normal containers");
            int normal = Convert.ToInt32(Console.ReadLine());
            shipment.GenerateCustomShipment(length, width, weight, cool, valuable, normal);
            return shipment;
        }
    }
}
