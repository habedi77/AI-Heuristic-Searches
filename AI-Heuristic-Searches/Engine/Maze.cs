namespace AI_Project1_WFA.Engine
{
	using System;
	using System.Collections.Generic;

	public class Maze
	{
		public List<Cell> Grid { get; set; }

		/// <summary>
		/// Represents an array of <see cref="Cell"/>s that
		/// are connected to eachother, forming a maze
		/// </summary>
		/// <param name="mode">BFS=1, DFS=2, UCS=3</param>
		/// <param name="start">starting cell</param>
		/// <param name="end">ending cell</param>
		/// <returns>null if not path is found</returns>
		public State PathFind(int mode, Cell start, Cell end)
		{
			Interface.DrawGrid(Grid, start, end);
			State res = null;

			switch (mode)
			{
				case 1:
					res = BFS(start, end);
					break;
				case 2:
					res = DFS(start, end);
					break;
				case 3:
					res = UCS(start, end);
					break;
				default:

					break;
			}

			Interface.DrawPath(res);
			return res;
		}

		private State UCS(Cell start, Cell end)
		{
			var closed = new List<Cell>();
			var open = new PriorityQueue<State>();

			Cell cr = start;
			State st = new State(cr, null);
			open.Enqueue(st);

			while (true)
			{
				if (open.Count() == 0)
				{
					// failed to find answer
					return null;
				}

				st = open.Dequeue();
				Interface.DrawActivecell(st.Cell);
				System.Threading.Thread.Sleep(500);
				cr = st.Cell;
				Console.WriteLine("UCS exploring {0}", st.Cell);

				if (ReferenceEquals(cr, end))
				{
					return st;
				}

				var temp = st.GetChildren() ?? new List<Cell>();

				foreach (Cell item in temp)
				{
					if (!closed.Contains(item))
					{
						open.Enqueue(new State(item, st));
					}
				}

				Interface.UnDrawActivecell(st.Cell);
				closed.Add(cr);
			}
		}

		private State DFS(Cell start, Cell end)
		{
			var closed = new List<Cell>();
			var open = new Stack<State>();

			Cell cr = start;
			State st = new State(cr, null);
			open.Push(st);

			while (true)
			{
				if (open.Count == 0)
				{
					// failed to find answer
					return null;
				}

				st = open.Pop();
				Interface.DrawActivecell(st.Cell);
				System.Threading.Thread.Sleep(500);
				cr = st.Cell;
				closed.Add(cr);
				var temp = st.GetChildren();
				Console.WriteLine("DFS exploring {0}", st.Cell);

				// search gnerated children for answer
				foreach (Cell item in temp)
				{
					if (ReferenceEquals(item, end))
					{
						return new State(item, st);
					}
				}

				foreach (Cell item in temp)
				{
					if (!closed.Contains(item))
					{
						open.Push(new State(item, st));
					}
				}

				Interface.UnDrawActivecell(st.Cell);
			}
		}

		private State BFS(Cell start, Cell end)
		{
			List<Cell> closed = new List<Cell>();
			Queue<State> open = new Queue<State>();
			List<Cell> temp = new List<Cell>();

			Cell cr = start;
			State st = new State(cr, null);
			open.Enqueue(st);

			while (true)
			{
				if (open.Count == 0)
				{
					// failed to find answer
					return null;
				}

				st = open.Dequeue();
				Console.WriteLine("BFS exploring {0}", st.Cell);
				Interface.DrawActivecell(st.Cell);
				System.Threading.Thread.Sleep(500);

				cr = st.Cell;
				temp = st.GetChildren();

				// search gnerated children for answer
				foreach (Cell item in temp)
				{
					if (ReferenceEquals(item, end))
					{
						st = new State(item, st);
						return st;
					}
				}

				foreach (Cell item in temp)
				{
					if (!closed.Contains(item))
					{
						open.Enqueue(new State(item, st));
					}
				}

				closed.Add(cr);
				Interface.UnDrawActivecell(st.Cell);
			}
		}

		/// <summary>
		/// resets all <c>Explored</c> in cells of maze to <c>false</c>
		/// </summary>
		public void ResetExplored()
		{
			foreach (Cell c in Grid)
			{
				c.Explored = false;
			}
		}
	}
}
