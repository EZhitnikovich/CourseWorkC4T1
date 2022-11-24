using System.Diagnostics;
using System.Drawing;
using c4t1.Model;

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
            labirinth = LabirinthChanger.Generate(Convert.ToInt32(numericUpDownX.Value), Convert.ToInt32(numericUpDownY.Value));
            WeightChanger.FillRandomWeights(labirinth, 0, 2500);
            WeightChanger.MakePositiveWeights(labirinth);
            pictureBox1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            labirinth = LabirinthChanger.Generate(Convert.ToInt32(numericUpDownX.Value), Convert.ToInt32(numericUpDownY.Value));
            pictureBox1.Refresh();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            foreach (var item in labirinth.Cells)
            {
                if(item.State == CellState.Path)
                {
                    item.State = CellState.Common;
                }
            }

            var path = PathFinder.FindPath(labirinth.Cells, LabirinthChanger.GetFirstByState(labirinth, CellState.Start), LabirinthChanger.GetFirstByState(labirinth, CellState.Finish));

            var weight = 0;

            foreach (var item in path)
            {
                labirinth.Cells[item.X, item.Y].State = CellState.Path;
                weight += labirinth.Cells[item.X, item.Y].Weight;
            }

            pictureBox1.Refresh();

            MessageBox.Show($"{weight}");
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            LabirinthDrawer.Draw(e.Graphics, labirinth, GradientGenerator.GetGradient(Color.Green, Color.Red, 100));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var e2 = (MouseEventArgs)e;

            var x = e2.X/10;
            var y = e2.Y/10;

            if(radioButtonStart.Checked)
            {
                LabirinthChanger.SetNewStartPoint(labirinth, x, y);
            }
            else if(radioButtonFinish.Checked)
            {
                LabirinthChanger.SetNewFinishPoint(labirinth, x, y);
            }
            else if (radioButtonCommon.Checked)
            {
                LabirinthChanger.SetCommonState(labirinth, x, y);
            }

            pictureBox1.Refresh();
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

        int x1 = 0;
        int y1 = 0;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if(checkBox1.Checked)
            {
                x1 = e.Location.X / 10;
                y1 = e.Location.Y / 10;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if(checkBox1.Checked)
            {
                var X = e.Location.X / 10;
                var Y = e.Location.Y / 10;

                WeightChanger.AddWeightsInRange(labirinth, Convert.ToInt32(numericUpDown1.Value), x1, y1, X, Y);
                pictureBox1.Refresh();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(((CheckBox)sender).Checked)
            {
                radioButtonCommon.Checked = false;
                radioButtonFinish.Checked = false;
                radioButtonStart.Checked = false;
            }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                checkBox1.Checked = false;
            }
        }
    }
}