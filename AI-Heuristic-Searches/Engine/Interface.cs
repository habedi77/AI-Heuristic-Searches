namespace AI_Project1_WFA.Engine
{
	using System;
	using System.Collections.Generic;
	using System.IO;

	public class Interface
	{
		private static PrimaryForm form;
		private int n;
		private static StreamWriter file;

		public Interface()
		{
			file = new StreamWriter("output.txt");
			n = 0;
		}

		public void run(PrimaryForm form, int mode)
		{
			Maze mz = new Maze();
			Interface.form = form;
			List<Cell> grid = ReadFile("test.txt");

			mz.Grid = grid;
			Console.WriteLine(grid[0] + " to " + grid[n - 1]);

			State s = mz.PathFind(mode, grid[0], grid[n - 1]);
			Console.WriteLine(s);
			file.WriteLine(s.ToString());

			file.Close();
		}

		private static int size = 60;
		private static int gap = 10;
		private static int x0 = 100;
		private static int y0 = 100;

		public static void DrawGrid(List<Cell> grid, Cell start, Cell end)
		{
			form.DrawRect(form.ColorBackGround, 0, 0, 1000, 1000);

			foreach (Cell c in grid)
			{
				form.DrawRect(form.ColorFloor, x0 + (c.Col * (size + gap)), y0 + (c.Row * (size + gap)), size, size);
				Console.WriteLine(c);
				if (file != null)
				{
					file.WriteLine(c.ToString() + " U:" + c.Up + "  R:" + c.Right + " D:" + c.Down + " L:" + c.Left);
				}

				if (c.Right != null)
				{
					form.DrawRect(form.ColorFloor, x0 + (c.Col * (size + gap)) + size, y0 + (c.Row * (size + gap)), gap, size);
				}

				if (c.Left != null)
				{
					form.DrawRect(form.ColorFloor, x0 + (c.Col * (size + gap)) - gap, y0 + (c.Row * (size + gap)), gap, size);
				}

				if (c.Up != null)
				{
					form.DrawRect(form.ColorFloor, x0 + (c.Col * (size + gap)), y0 + (c.Row * (size + gap)) - gap, size, gap);
				}

				if (c.Down != null)
				{
					form.DrawRect(form.ColorFloor, x0 + (c.Col * (size + gap)), y0 + (c.Row * (size + gap)) + size, size, gap);
				}
			}

			form.DrawRect(form.ColorGoal, x0 + (start.Col * (size + gap)), y0 + (start.Row * (size + gap)), size, size);
			form.DrawRect(form.ColorGoal, x0 + (end.Col * (size + gap)), y0 + (end.Row * (size + gap)), size, size);

		}

		public static void DrawActivecell(Cell cell)
		{
			form.DrawRect(form.ColorActiveFloor, x0 + (cell.Col * (size + gap)), y0 + (cell.Row * (size + gap)), size, size);
		}

		public static void UnDrawActivecell(Cell cell)
		{
			form.DrawRect(form.ColorDeactiveFloor, x0 + (cell.Col * (size + gap)), y0 + (cell.Row * (size + gap)), size, size);
		}

		public static void DrawPath(State res)
		{
			form.DrawRect(form.ColorPath, x0 + (res.Cell.Col * (size + gap)), y0 + (res.Cell.Row * (size + gap)), size, size);
			foreach (Cell c in res.History)
			{
				form.DrawRect(form.ColorPath, x0 + (c.Col * (size + gap)), y0 + (c.Row * (size + gap)), size, size);
			}
		}

		public static Cell GetByID(int id, List<Cell> grid)
		{
			foreach (Cell item in grid)
			{
				if (item.Id == id)
				{
					return item;
				}
			}

			return null;
		}

		private List<Cell> ReadFile(string file)
		{
			List<Cell> grid = new List<Cell>();

			TextReader reader = File.OpenText(file);
			reader.ReadLine();
			n = int.Parse(reader.ReadLine());
			int[,] data = new int[7, n];

			// data -> id, row, col, up, right, down, left
			for (int i = 0; i < n; i++)
			{
				string temp = reader.ReadLine();
				string[] bits = temp.Split(',');
				for (int j = 0; j < 7; j++)
				{
					data[j, i] = int.Parse(bits[j]);
				}

				Cell c = new Cell(data[1, i], data[2, i], data[0, i]);
				Console.WriteLine(c);
				grid.Add(c);
			}

			reader.Close();
			for (int i = 0; i < n; i++)
			{
				GetByID(i, grid).Up = data[3, i] == -1 ? null : GetByID(data[3, i], grid);
				GetByID(i, grid).Right = data[4, i] == -1 ? null : GetByID(data[4, i], grid);
				GetByID(i, grid).Down = data[5, i] == -1 ? null : GetByID(data[5, i], grid);
				GetByID(i, grid).Left = data[6, i] == -1 ? null : GetByID(data[6, i], grid);
			}

			return grid;
		}
	}
}
