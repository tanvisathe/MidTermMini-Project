using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects2Multiple
{
    class Person
    {
        private City homeCity;
        public string name { get; set; }
        static private Random rng = new Random();

        bool berry = false;//this is the program that i changed to match original.

        public Person(String name, City homeCity)
        {
            this.name = name;
            this.homeCity = homeCity;
        }

        public Person(City homeCity)
        {
            Console.WriteLine("Name your new villager!");
            this.name = Console.ReadLine();
            this.homeCity = homeCity;
        }

        public void Work()
        {
            Console.WriteLine();
            Console.WriteLine("Time for "+name+" to go to Work!");
            Console.WriteLine("Assign "+name+" a task:");
            Console.WriteLine("1) Chop Wood");
            Console.WriteLine("2) Get Water");
			Console.WriteLine("3) Get Food");
			Console.WriteLine("4) Get Seed");
            Console.WriteLine("5) Build a house (Requires 5 wood)");
            Console.WriteLine("6) Build a well (Gives 1 gallon of water per day -- Requires 6 wood)");
			Console.WriteLine("7) Build a garden (Gives 1 bushel of food per day -- Requires 3 seeds and 3 water)");
			Console.WriteLine("8) Scavenge (find random resource)");
			Console.WriteLine("9) Build a castle (Win Game -- Requires 20 wood)");

			string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    ChopWood();
                    break;
                case "2":
                    FindWater();
                    break;
				case "3":
					FindFood();
					break;
                case "4":
                    FindSeed();
                    break;
                case "5":
                    BuildHouse();
                    break;
                case "6":
                    BuildWell();
                    break;
                case "7":
                    BuildGarden();
                    break;
				case "8":
					Scavenge();
					break;
				case "9":
					BuildCastle();
					break;
				default:
                    Console.WriteLine("I'm sorry I didn't understand, let's try again.");
                    Work();
                    break;
            }
        }

        public void ChopWood()
        {
            homeCity.IncreaseWood(berry);
            homeCity.PrintWood();
            homeCity.Pause();
        }
        public void ChopWood(int amount)
        {
            homeCity.IncreaseWood(amount);
            homeCity.PrintWood();
            homeCity.Pause();
        }


        public void FindWater()
        {
            int amount = rng.Next(1, 4);
            Console.WriteLine(name + " found " + amount + " gallons of water.");
            homeCity.Water += amount;
            homeCity.PrintWater();
            homeCity.Pause();
        }

        public void FindWater(int amount)
        {
            Console.WriteLine(name + " found " + amount + " gallons of water.");
            homeCity.Water += amount;
            homeCity.PrintWater();
            homeCity.Pause();
        }

		public void FindFood()
		{
			int amount = rng.Next(1, 4);
			Console.WriteLine(name + " found " + amount + " bushels of food.");
			homeCity.Food += amount;
			homeCity.PrintFood();
			homeCity.Pause();
		}

		public void FindFood(int amount)
		{
			Console.WriteLine(name + " found " + amount + " bushels of food.");
			homeCity.Food += amount;
			homeCity.PrintFood();
			homeCity.Pause();
		}

		public void FindSeed()
		{
			int amount = rng.Next(1, 4);
			Console.WriteLine(name + " found " + amount + " seed(s).");
			homeCity.Seeds += amount;
			homeCity.PrintSeeds();
			homeCity.Pause();
		}

		public void FindSeed(int amount)
		{
			Console.WriteLine(name + " found " + amount + " seed(s).");
			homeCity.Seeds += amount;
			homeCity.PrintSeeds();
			homeCity.Pause();
		}


        //Spoil Food or  Water
        public void SpoilFoodorWater(int amount)
        {
            string[] spoilFoodorWater = new string[] { "water", "food" };
            Random spoil = new Random();
            int s = spoil.Next(0, 2);
            string option = spoilFoodorWater[s];

            switch (option)
            {
                case "water":
                    Console.WriteLine("Oops.. your village water has been infected..");
                    Console.WriteLine(name + " lost " + amount + " gallons of water");
                    homeCity.Water -= amount;
                    homeCity.PrintWater();
                    homeCity.Pause();
                    break;

                case "food":
                    Console.WriteLine("Oops.. your village food has been infected..");
                    Console.WriteLine(name + " lost " + amount + " bushels of food");
                    homeCity.Food -= amount;
                    homeCity.PrintFood();
                    homeCity.Pause();
                    break;
            }


        }

        public void BuildHouse()
        {
            homeCity.BuildHouse();
            homeCity.Pause();
        }

        public void BuildWell()
        {
            homeCity.BuildWell();
            homeCity.Pause();
        }

		public void BuildCastle()
		{
			homeCity.BuildCastle();
			homeCity.Pause();
		}

		public void BuildGarden()
		{
			homeCity.BuildGarden();
			homeCity.Pause();
		}

		public void Scavenge()
        {
            string[] resources = { "wood", "water", "food","nothing","death", "seed", "disease","barbarians" };
            int r = rng.Next(resources.Count());
            string choice = resources[r];
            int amount = rng.Next(2, 6);
            //Console.WriteLine(choice);
            switch (choice)
            {
                case "wood":
                    Console.WriteLine(name + " found " + amount + " " + choice);
                    ChopWood(amount);
                    break;
                case "water":
                    Console.WriteLine(name + " found " +amount +" "+choice);
                    FindWater(amount);
                    break;
				case "food":
					Console.WriteLine(name + " found " + amount + " " + choice);
					FindFood(amount);
					break;
				case "nothing":
                    Console.WriteLine(name + " found nothing");
                    break;
                case "death":
                    Console.WriteLine(name + " died");
                    homeCity.KillPerson(this);
                    break;
				case "seed":
					Console.WriteLine(name + " found " + amount + " " + choice);
					FindSeed(amount);
					break;
                case "disease":
                    SpoilFoodorWater(amount);
                    break;
                case "barbarians":
                    Console.WriteLine(name + " was attacked by filthy " + choice);
                    Console.WriteLine(name + " died");
                    homeCity.KillPerson(this);
                    break;
                case "berry":
                    Console.WriteLine(name + " found a magic berry.");
                    Console.WriteLine("Your supply of wood has doubled!");
                    berry = true;
                    homeCity.IncreaseWood(amount, berry);
                    break;

                default:
                    Scavenge();
                    break;

            }

        }

        public bool Drink()
        {
            bool survive;
            if (homeCity.Water > 0)
            {
                survive = true;
                homeCity.Water--;
                Console.WriteLine(name + " drinks 1 water");
            }
            else
            {
                survive = false;
                Console.WriteLine(name + " couldn't find a drink! They died of thirst!");
            }
            return survive;
        }

		public bool Eat()
		{
			bool survive;
			if (homeCity.Food > 0)
			{
				survive = true;
				homeCity.Food--;
				Console.WriteLine(name + " ate 1 food");
			}
			else
			{
				survive = false;
				Console.WriteLine(name + " couldn't find food to eat! They died of hunger!");
			}
			return survive;
		}
	}
}
