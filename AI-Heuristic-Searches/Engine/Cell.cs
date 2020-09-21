namespace AI_Project1_WFA.Engine
{
	using System;
	using System.Collections.Generic;

	public class Cell : IEquatable<Cell>
	{
		public Cell()
		{
		}

		public Cell(int row, int col, int id)
		{
			Row = row;
			Col = col;
			Id = id;
		}

		public int Row { get; set; }

		public int Col { get; set; }

		public List<object> Content { get; set; }

		public bool Explored { get; set; }

		public int Id { get; set; }

		public Cell Up { get; set; }

		public Cell Down { get; set; }

		public Cell Left { get; set; }

		public Cell Right { get; set; }

		public bool Equals(Cell other)
		{
			return Col == other.Col &&
					Row == other.Row &&
					Up == other.Up &&
					Down == other.Down &&
					Right == other.Right &&
					Left == other.Left;
		}

		public override string ToString()
		{
			return Id + "(" + Row + "," + Col + ")";
		}
	}
}
