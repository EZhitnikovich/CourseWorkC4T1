using c4t1.Controller;
using c4t1.Generator;
using c4t1.Model;
using c4t1.Utils;

namespace c4t1;

public partial class Form1 : Form
{
    LabirinthController labirinthController;

    int CELL_SIZE = 20;

    public Form1()
    {
        InitializeComponent();
        labirinthController = new LabirinthController();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        int width = Convert.ToInt32(numericUpDownX.Value);
        int height = Convert.ToInt32(numericUpDownY.Value);

        labirinthController.Generate(width, height);

        WeightChanger.FillRandomWeights(labirinthController.Labirinth, 0, 25);
        WeightChanger.MakePositiveWeights(labirinthController.Labirinth);
        pictureBox1.Refresh();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        int width = Convert.ToInt32(numericUpDownX.Value);
        int height = Convert.ToInt32(numericUpDownY.Value);

        labirinthController.Generate(width, height);
        pictureBox1.Refresh();
    }
    private async void button3_Click(object sender, EventArgs e)
    {
        //TODO: clear

        if(labirinthController.GetFirstByState(CellState.Start) != new Point(-1,-1) && labirinthController.GetFirstByState(CellState.Finish) != new Point(-1, -1))
        {
            foreach (var item in labirinthController.Labirinth.Cells)
            {
                if (item.State == CellState.Path)
                {
                    item.State = CellState.Common;
                }
            }

            var path = new List<Point>();

            await Task.Run(() =>
            {
                path = PathFinder.FindPath(labirinthController.Labirinth.Cells, labirinthController.GetFirstByState(CellState.Start), labirinthController.GetFirstByState(CellState.Finish), pictureBox1.CreateGraphics());
            });

            if (!path.Any())
            {
                return;
            }

            var weight = 0;
            var max = int.MinValue;
            var min = int.MaxValue;
            var steps = path.Count();

            foreach (var item in path)
            {
                labirinthController.Labirinth.Cells[item.X, item.Y].State = CellState.Path;
                weight += labirinthController.Labirinth.Cells[item.X, item.Y].Weight;

                if(labirinthController.Labirinth.Cells[item.X, item.Y].Weight < min)
                {
                    min = labirinthController.Labirinth.Cells[item.X, item.Y].Weight;
                }

                if(labirinthController.Labirinth.Cells[item.X, item.Y].Weight > max)
                {
                    max = labirinthController.Labirinth.Cells[item.X, item.Y].Weight;
                }
            }

            pictureBox1.Refresh();

            MessageBox.Show($"Цена пути: {weight}\nМаксимальная высота: {max}\nМинимальная высота: {min}\nСредняя высота: {weight/steps}\nШагов: {steps}");
        }
    }

    private void pictureBox1_Paint(object sender, PaintEventArgs e)
    {
        LabirinthDrawer.Draw(e.Graphics, labirinthController.Labirinth, GradientGenerator.GetGradient(Color.Green, Color.Red, 100), CELL_SIZE);
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {
        var e2 = (MouseEventArgs)e;

        var x = e2.X/ CELL_SIZE;
        var y = e2.Y/ CELL_SIZE;

        if(radioButtonStart.Checked)
        {
            labirinthController.SetNewState(x, y, CellState.Start);
        }
        else if(radioButtonFinish.Checked)
        {
            labirinthController.SetNewState(x, y, CellState.Finish);
        }
        else if (radioButtonCommon.Checked)
        {
            labirinthController.SetCommonState(x, y);
        }

        pictureBox1.Refresh();
    }

    private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
    {
        var X = e.Location.X / CELL_SIZE;
        var Y = e.Location.Y / CELL_SIZE;
        if (labirinthController.TryGetWeight(X, Y, out var weight))
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
            x1 = e.Location.X / CELL_SIZE;
            y1 = e.Location.Y / CELL_SIZE;
        }
    }

    private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
    {
        if(checkBox1.Checked)
        {
            var X = e.Location.X / CELL_SIZE;
            var Y = e.Location.Y / CELL_SIZE;

            WeightChanger.AddWeightsInRange(labirinthController.Labirinth, Convert.ToInt32(numericUpDown1.Value), x1, y1, X, Y);
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