namespace AI_Project1_WFA.Engine
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;

	public class State : IComparable, IComparable<State>
	{
		public Cell Cell { get; set; }

		public ArrayList History { get; set; }

		public int Cost { get; set; }

		public State(Cell current, State parent)
		{
			Cell = current;
			if (parent == null)
			{
				Cost = 0;
				History = new ArrayList();
			}
			else
			{
				Cost = parent.Cost + 1;
				History = new ArrayList(parent.History)
				{
					parent.Cell,
				};
			}
		}

		public List<Cell> GetChildren()
		{
			List<Cell> temp = new List<Cell>();
			if (Cell.Down != null)
			{
				temp.Add(Cell.Down);
			}

			if (Cell.Up != null)
			{
				temp.Add(Cell.Up);
			}

			if (Cell.Right != null)
			{
				temp.Add(Cell.Right);
			}

			if (Cell.Left != null)
			{
				temp.Add(Cell.Left);
			}

			return temp;
		}

		public override string ToString()
		{
			// TODO better array list to string
			var strings = from object o in History select o.ToString();

			string theString = string.Join(" -> ", strings.ToArray());

			return "{cell: " + Cell + " , " + theString + "}";
		}

		public int CompareTo(object obj)
		{
			if (obj == null) return 1;

			if (obj is State os)
			{
				return Cost.CompareTo(os.Cost);
			}
			else
			{
				throw new ArgumentException("State");
			}
		}

		public int CompareTo(State other)
		{
			return Cost.CompareTo(other.Cost);
		}
	}
}
