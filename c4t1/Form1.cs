using System.Diagnostics;
using System.Drawing;

namespace c4t1
{
    public partial class Form1 : Form
    {
        Labirinth labirinth;

        public Form1()
        {
            InitializeComponent();
            labirinth = new();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            labirinth = LabirinthGenerator.Generate(Convert.ToInt32(numericUpDownX.Value), Convert.ToInt32(numericUpDownY.Value));
            WeightGenerator.FillWeights(labirinth, 0, 25000);
            WeightGenerator.MakePositiveWeights(labirinth);
            pictureBox1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            labirinth = LabirinthGenerator.Generate(Convert.ToInt32(numericUpDownX.Value), Convert.ToInt32(numericUpDownY.Value));
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            LabirinthDrawer.Draw(e.Graphics, labirinth, GradientGenerator.GetGradient(Color.Green, Color.Red, 10));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var e2 = (MouseEventArgs)e;

            var X = e2.X/10;
            var Y = e2.Y/10;

            if (checkBox1.Checked)
            {
                labirinth.AddWeight(X, Y, Convert.ToInt32(numericUpDown1.Value));
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            var X = e.Location.X / 10;
            var Y = e.Location.Y / 10;
            if (labirinth.TryGetWeight(X, Y, out var weight))
            {
                label3.Text = $"Высота:\n[{X+1},{Y+1}]:{weight}";
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}