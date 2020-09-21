namespace AI_Project1_WFA
{
	using System;
	using System.Drawing;
	using System.Windows.Forms;
	using AI_Project1_WFA.Engine;

	public partial class PrimaryForm : Form
	{
		public Color ColorBackGround { get; private set; }

		public Color ColorFloor { get; private set; }

		public Color ColorWall { get; private set; }

		public Color ColorActiveFloor { get; private set; }

		public Color ColorDeactiveFloor { get; private set; }

		public Color ColorPath { get; private set; }

		public Color ColorGoal { get; private set; }

		public PrimaryForm()
		{
			InitializeComponent();

			ColorBackGround = Color.FromArgb(10, 20, 50);
			ColorFloor = Color.FromArgb(15, 0, 120);
			ColorWall = Color.FromArgb(0, 50, 255);
			ColorActiveFloor = Color.FromArgb(150, 100, 200);
			ColorDeactiveFloor = Color.FromArgb(90, 50, 120);
			ColorPath = Color.FromArgb(100, 200, 200);
			ColorGoal = Color.FromArgb(40, 130, 30);
		}

		internal void DrawRect(int r, int g, int b, int x, int y, int w, int h)
		{

			DrawRect(System.Drawing.Color.FromArgb(r, g, b), x, y, w, h);

		}

		internal void DrawRect(Color color, int x, int y, int w, int h)
		{

			System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(color);
			System.Drawing.Graphics formGraphics;
			formGraphics = this.CreateGraphics();
			formGraphics.FillRectangle(myBrush, new Rectangle(x, y, w, h));
			myBrush.Dispose();
			formGraphics.Dispose();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Interface i = new Interface();
			i.run(this, 1);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Interface i = new Interface();
			i.run(this, 2);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			Interface i = new Interface();
			i.run(this, 3);
		}
	}
}
