using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects2Multiple
{
	class City
	{
		private string name;
		private int day = 0;

		private int houses = 1;
		private bool castle = true;

		private int wood = 0;
		private int water = 0;
		private int seeds = 0;

		private List<Person> villagers = new List<Person>();
		private List<WaterSource> waterSources = new List<WaterSource>();
		private List<FoodSource> foodSources = new List<FoodSource>();

		public bool Won = false;

		public int Seeds { get; set; }
		public int Water { get; set; }
		public int Food { get; set; }

		public City(int pop)
		{
			Console.WriteLine("Name your city:");
			name = Console.ReadLine();
			Water = 10;
			Food = 10;
			Seeds = 0;

			houses = pop;

			for (int i = 0; i < pop; i++)
			{
				villagers.Add(new Person(this));
			}

			Console.WriteLine("Welcome to " + name + " with " + villagers.Count + " Population");
			Console.WriteLine("Goal is to keep your population alive and get to 10 population.");
			Console.WriteLine();

			Turn();
		}

		public int GetPop()
		{
			return villagers.Count;
		}

		public void IncreasePop(int amount)
		{
			for (int i = 0; i < amount; i++)
			{
				villagers.Add(new Person(this));
			}
		}

		public void IncreaseWood()
		{
			wood++;
		}

		public void IncreaseWood(int amount)
		{
			wood += amount;
		}

		public void IncreaseSeeds()
		{
			seeds++;
		}

		public void IncreaseSeeds(int amount)
		{
			seeds += amount;
		}

		public void BuildHouse()
		{
			if (wood >= 5)
			{
				wood -= 5;
				houses++;
				Console.WriteLine("You Built a house!");
			}
			else
			{
				Console.WriteLine("You didn't build a house! You need at least 5 wood.");
			}
		}

		public void BuildCastle()
		{
			if (wood > 0)//changed condition from '>=' to '>0'
			{
				wood -= 20;
				castle = true;

				Winner("castled");
			}
			else
			{
				Console.WriteLine("You didn't build a castle! You need at least 20 wood.");
			}
		}

		public void BuildWell()
		{
			if (wood >= 6)
			{
				wood -= 6;
				waterSources.Add(new WaterSource("Well", 1));
				Console.WriteLine("You Built a well!");
			}
			else
			{
				Console.WriteLine("You didn't build a well! You need at least 6 wood.");
			}
		}

		public void BuildGarden()
		{
			if (Water >= 3 && Seeds >= 3)
			{
				Water -= 3;
				Seeds -= 3;
				foodSources.Add(new FoodSource("Garden", 1));
				Console.WriteLine("You Built a garden!");
			}
			else
			{
				Console.WriteLine("You didn't build a garden! You need at least 3 water and 3 seeds.");
			}
		}

		public void KillPerson(Person p)
		{
			villagers.Remove(p);
		}

		public int CalculateWaterPerTurn()
		{
			int total = 0;
			foreach (WaterSource w in waterSources)
			{
				Console.WriteLine("Your " + w.Name + " produces " + w.Supply + " gallons of water per turn");
				total += w.Supply;
			}
			Console.WriteLine("Your total water per turn is " + total);
			return total;
		}

		public void PrintWater()
		{
			Console.WriteLine(name + " has " + Water + " gallons of water.");
		}

		public int CalculateFoodPerTurn()
		{
			int total = 0;
			foreach (FoodSource f in foodSources)
			{
				Console.WriteLine("Your " + f.Name + " produces " + f.Supply + " bushels of food per turn.");
				total += f.Supply;
			}
			Console.WriteLine("Your total food per turn is " + total);
			return total;
		}

		public int CalculateSeedsPerTurn()
		{
			int total = 0;
			int supply = 1;

			foreach (FoodSource w in foodSources)
			{
				Console.WriteLine("Your " + w.Name + " produces " + supply + " seeds per turn.");
				total += supply;
			}
			Console.WriteLine("Your total seed(s) per turn is " + total + ".");
			return total;
		}

		public void PrintFood()
		{
			Console.WriteLine(name + " has " + Food + " bushels of food.");
		}

		public void PrintSeeds()
		{
			Console.WriteLine(name + " has " + Seeds + " seed(s).");
		}

		public void PrintPop()
		{
			Console.WriteLine("Your city has " + villagers.Count + " Population.");
		}

		public void PrintWood()
		{
			Console.WriteLine("You have " + wood + " wood.");
		}

		public void PrintHouses()
		{
			Console.WriteLine("Your city has " + houses + " houses.");
		}

		public void PrintStats()
		{
			PrintPop();

			PrintFood();
			PrintSeeds();
			PrintWater();

			PrintWood();
			PrintHouses();

			Pause();
		}

		public void Pause()
		{
			Console.WriteLine("Press Enter to continue.");
			Console.ReadLine();
		}

		public void Turn()
		{
			if (!Won)
			{
				day++;
				Console.WriteLine("__________________________________");
				Console.WriteLine("It's the start of day " + day + "!");
				Console.WriteLine();

				Water += CalculateWaterPerTurn();
				Food += CalculateFoodPerTurn();
				Seeds += CalculateSeedsPerTurn();

				PrintStats();

				if (GetPop() > 0 && GetPop() < 10)
				{
					if (houses > GetPop())
					{
						int diff = houses - GetPop();
						for (int i = 0; i < diff; i++)
						{
							Console.WriteLine("A new person has moved into your village!");
							villagers.Add(new Person(this));
						}
					}
					List<Person> deadVillagers = new List<Person>();
					for (int i = 0; i < villagers.Count; i++)
					{
						Person p = villagers[i];
						p.Work();
					}

					//Eating loop
					foreach (Person p in villagers)
					{
						if (p.Drink() == false)
						{
							//If they don't drink they die 
							//We build up a seperate list since you can't modify 
							//a list in a foreach loop
							deadVillagers.Add(p);
						}
						else if (p.Eat() == false)
						{
							//If they don't eat they die 
							//We build up a seperate list since you can't modify 
							//a list in a foreach loop
							deadVillagers.Add(p);
						}
					}
					PrintWater();
					Pause();

					foreach (Person p in deadVillagers)
					{
						//Remove dead people from the list 
						//The list.remove() method searches by item and removes any matches
						villagers.Remove(p);
					}
					Turn();
				}
				else if (GetPop() == 10)
				{
					Winner("population");
				}
				else
				{
					Console.WriteLine();
					Console.WriteLine("Everyone in " + name + " is dead, sorry!");
					Console.WriteLine("You made it to day " + day);
				}
			}
		}

		public void Winner(string condition)
		{
			Console.WriteLine("You Won!!!");
			if (condition == "population")
			{
				Console.WriteLine("You have 10 or more villagers!");
			}
			else
			{
				Console.WriteLine("You built a castle!");
			}
			Console.WriteLine("Goodbye!");

			Won = true;
		}
	}
}
