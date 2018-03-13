﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Minisoft1
{
    public partial class Editor : Form
    {
        SaveLoadManager sm;
        MainForm mainform;
        Settings settings;
        Graphics g;
        int INDENT_X;
        int ix, old_cell_size;
        List<Block> blocks;

        Block selected, changed;
        Point delta, prev_change;
        bool bupper_left, bupper_right, blower_left, blower_right, start_deletion;
        Color[] colours;
        int colorInQueue;

        int[,] playground, mode2_playground;

        public Editor(Settings settings, MainForm mainform)
        {
            DoubleBuffered = true;
            InitializeComponent();

            this.settings = settings;
            CellSize_editor.Value = settings.cell_size;
            NumberOfCols.Value = settings.cols;
            NumberOfRows.Value = settings.rows;

            old_cell_size = settings.cell_size;
            this.mainform = mainform;
            g = CreateGraphics();
            ix = 0;
            INDENT_X = 180;
            INDENT_X -= INDENT_X % Convert.ToInt32(CellSize_editor.Value);

            this.colours = new Color[]{Color.Aqua, Color.Yellow, Color.Red, Color.Green, Color.GreenYellow, Color.Gold, Color.Pink, Color.Gray, Color.Purple};
            colorInQueue = 0;

            this.playground = new int[this.settings.rows, this.settings.cols];
            this.mode2_playground = new int[this.settings.rows, this.settings.cols];

            sm = new SaveLoadManager();

            blocks = new List<Block>();
        }

        private void Editor_Paint(object sender, PaintEventArgs e)
        {
            int cols = Convert.ToInt32(NumberOfCols.Value);
            int rows = Convert.ToInt32(NumberOfRows.Value);
            int cell_size = Convert.ToInt32(CellSize_editor.Value);

            // draw grid
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    Pen blackPen = new Pen(Color.Black, 1);
                    e.Graphics.DrawRectangle(blackPen, i * cell_size + INDENT_X, j * cell_size, cell_size, cell_size);
                }
            }

            // draw blocks
            foreach (Block block in blocks)
            {
                block.KresliEditovaci(e.Graphics);
            }
        }

        private void pick_color_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Assigns an array of custom colors to the CustomColors property
            MyDialog.CustomColors = new int[]{6916092, 15195440, 16107657, 1836924,
           3758726, 12566463, 7526079, 7405793, 6945974, 241502, 2296476, 5130294,
           3102017, 7324121, 14993507, 11730944,};

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
                textBox1.BackColor = MyDialog.Color;
        }

        private void create_block_Click(object sender, EventArgs e)
        {
            // get needed information from user's input
            int indentY = Convert.ToInt32(NumberOfRows.Value)* this.settings.cell_size + 5;
            int H = Convert.ToInt32(block_height.Value);
            int W = Convert.ToInt32(block_width.Value);
            int height = this.settings.cell_size * H;
            int width = this.settings.cell_size * W;
            Color color = textBox1.BackColor;

            // create new block
            Block block = new Block(ix + 1, INDENT_X, indentY, W, H, this.settings.cell_size, color);
            blocks.Add(block);
            Invalidate();
            ix++;
            if (colorInQueue < colours.Length-1)
            {
                colorInQueue++;
            }
            else
            {
                colorInQueue = 0;
            }            
            textBox1.BackColor = colours[colorInQueue];
        }

        private void back_to_menu_Click(object sender, EventArgs e)
        {
            this.Hide();
            mainform.Show();
        }

        private void NumberOfRows_ValueChanged(object sender, EventArgs e)
        {
            this.settings.rows = Convert.ToInt32(NumberOfRows.Value);
            this.playground = new int[this.settings.rows, this.settings.cols];
            this.mode2_playground = new int[this.settings.rows, this.settings.cols];
            Invalidate();
        }

        private void CellSize_editor_ValueChanged(object sender, EventArgs e)
        {
            if(blocks != null)
            {
                this.settings.cell_size = Convert.ToInt32(CellSize_editor.Value);
                int delta_coord = this.settings.cell_size - old_cell_size;

                foreach (Block block in blocks)
                {
                    block.recalculate_shape(this.settings.cell_size);
                    block.cell_size = this.settings.cell_size;
                    block.x += delta_coord * (block.x - INDENT_X) / old_cell_size;
                    block.y += delta_coord * (block.y / old_cell_size);
                }
                old_cell_size = this.settings.cell_size;
                Invalidate();
            }

        }

        private void NumberOfCols_ValueChanged(object sender, EventArgs e)
        {
            this.settings.cols = Convert.ToInt32(NumberOfCols.Value);
            this.playground = new int[this.settings.rows, this.settings.cols];
            this.mode2_playground = new int[this.settings.rows, this.settings.cols];
            Invalidate();
        }

        private void delete_block_Click(object sender, EventArgs e)
        {
            if(start_deletion == false)
            {
                start_deletion = true;
                delete_block.Text = "Skonči Mazanie";
            }
            else
            {
                start_deletion = false;
                delete_block.Text = "Začni Mazanie";
            }

        }

        private void Editor_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left == e.Button)
            {
                int r = 5;
                int d = r * 2;
                float left_upper, right_upper, left_lower, right_lower;

                for (int i = blocks.Count - 1; i >= 0; i--)
                {
                    left_upper = Convert.ToSingle(Math.Sqrt(e.X - (blocks[i].x - d) ^ 2) + Math.Sqrt((e.Y - (blocks[i].y - d)) ^ 2));
                    right_upper = Convert.ToSingle(Math.Sqrt(e.X - (blocks[i].x + blocks[i].width) ^ 2) + Math.Sqrt((e.Y - (blocks[i].y - d)) ^ 2));
                    left_lower = Convert.ToSingle(Math.Sqrt(e.X - (blocks[i].x - d) ^ 2) + Math.Sqrt((e.Y - (blocks[i].y + blocks[i].height)) ^ 2));
                    right_lower = Convert.ToSingle(Math.Sqrt(e.X - (blocks[i].x + blocks[i].width) ^ 2) + Math.Sqrt((e.Y - (blocks[i].y + blocks[i].height)) ^ 2));

                    // selected upper left corner circle
                    if (left_upper <= r)
                    {
                        bupper_left = true;
                        prev_change = new Point(e.X, e.Y);
                        changed = blocks[i];
                        break;
                    }
                    // selected upper right corner circle
                    else if (right_upper <= r)
                    {
                        bupper_right = true;
                        prev_change = new Point(e.X, e.Y);
                        changed = blocks[i];
                        break;
                    }
                    // selected lower left corner circle
                    else if (left_lower <= r)
                    {
                        blower_left = true;
                        prev_change = new Point(e.X, e.Y);
                        changed = blocks[i];
                        break;
                    }
                    // selected lower right corner circle
                    else if (right_lower <= r)
                    {
                        blower_right = true;
                        prev_change = new Point(e.X, e.Y);
                        changed = blocks[i];
                        break;
                    }
                    else
                    {
                        if (e.X < blocks[i].x + blocks[i].width && e.X > blocks[i].x)
                        {
                            if (e.Y < blocks[i].y + blocks[i].height && e.Y > blocks[i].y)
                            {
                                if (start_deletion)
                                {
                                    blocks.Remove(blocks[i]);
                                }
                                else
                                {
                                    // remember selected block and clicked coords
                                    selected = blocks[i];
                                    delta = new Point(e.X - selected.x, e.Y - selected.y);

                                    // set selected as last in array so it is above all other blocks
                                    for (int j = i; j < blocks.Count - 1; j++)
                                    {
                                        blocks[j] = blocks[j + 1];
                                    }
                                    blocks[blocks.Count - 1] = selected;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            Invalidate();
        }

        private void Editor_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left == e.Button)
            {
                // selected move
                if(selected != null)
                {
                    int dx = e.X - delta.X;
                    int dy = e.Y - delta.Y;

                    // if in window size
                    //if (dx > 150 && dx + selected.width < this.ClientSize.Width)
                    //{
                    //    if (dy > 0 && dy + selected.height < this.ClientSize.Height)
                    //    {
                            selected.x = dx;
                            selected.y = dy;
                            Invalidate();
                    //    }
                    //}
                }
                // upper left move
                else if (bupper_left)
                {
                    int dx = e.X - prev_change.X;
                    int dy = e.Y - prev_change.Y;

                    if((changed.x + dx < changed.x + changed.width - (settings.cell_size / 2 )) && 
                        (changed.y + dy < changed.y + changed.height - (settings.cell_size / 2)))
                    {
                        if (e.X < prev_change.X)
                        {
                            changed.x += dx;
                            changed.width -= dx;
                        }
                        if (e.X > prev_change.X)
                        {
                            changed.x += dx;
                            changed.width -= dx;
                        }
                        if (e.Y < prev_change.Y)
                        {
                            changed.y += dy;
                            changed.height -= dy;
                        }
                        if (e.Y > prev_change.Y)
                        {
                            changed.y += dy;
                            changed.height -= dy;
                        }
                    }
                    prev_change = new Point(e.X, e.Y);
                    Invalidate();
                }
                // upper right move
                else if (bupper_right)
                {

                    int dx = e.X - prev_change.X;
                    int dy = e.Y - prev_change.Y;

                    if ((changed.width + dx > (settings.cell_size / 2)) &&
                        (changed.y + dy < changed.y + changed.height - (settings.cell_size / 2)))
                    {
                        if (e.X < prev_change.X)
                        {
                            changed.width += dx;
                        }
                        if (e.X > prev_change.X)
                        {
                            changed.width += dx;
                        }
                        if (e.Y < prev_change.Y)
                        {
                            changed.y += dy;
                            changed.height -= dy;
                        }
                        if (e.Y > prev_change.Y)
                        {
                            changed.y += dy;
                            changed.height -= dy;
                        }
                    }

                    prev_change = new Point(e.X, e.Y);
                    Invalidate();
                }
                // lower left move
                else if (blower_left)
                {
                    int dx = e.X - prev_change.X;
                    int dy = e.Y - prev_change.Y;

                    if ((changed.x + dx < changed.x + changed.width - (settings.cell_size / 2)) &&
                        (changed.height + dy > (settings.cell_size / 2)))
                    {
                        if (e.X < prev_change.X)
                        {
                            changed.x += dx;
                            changed.width -= dx;
                        }
                        if (e.X > prev_change.X)
                        {
                            changed.x += dx;
                            changed.width -= dx;
                        }
                        if (e.Y < prev_change.Y)
                        {
                            changed.height += dy;
                        }
                        if (e.Y > prev_change.Y)
                        {
                            changed.height += dy;
                        }
                    }
                    prev_change = new Point(e.X, e.Y);
                    Invalidate();
                }
                // lower right move
                else if (blower_right)
                {
                    int dx = e.X - prev_change.X;
                    int dy = e.Y - prev_change.Y;

                    if ((changed.width + dx > (settings.cell_size / 2)) &&
                        (changed.height + dy > (settings.cell_size / 2)))
                    {
                        if (e.X < prev_change.X)
                        {
                            changed.width += dx;
                        }
                        if (e.X > prev_change.X)
                        {
                            changed.width += dx;
                        }
                        if (e.Y < prev_change.Y)
                        {
                            changed.height += dy;
                        }
                        if (e.Y > prev_change.Y)
                        {
                            changed.height += dy;
                        }
                    }
                    prev_change = new Point(e.X, e.Y);
                    Invalidate();
                }
            }

        }

        private void Editor_MouseUp(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left == e.Button)
            {
                // finished moving
                if (selected != null)
                {
                    int cols = Convert.ToInt32(NumberOfCols.Value);
                    int rows = Convert.ToInt32(NumberOfRows.Value);
                    int cell_size = Convert.ToInt32(CellSize_editor.Value);

                    selected.x = ((selected.x - INDENT_X) / cell_size) * cell_size + INDENT_X;
                    selected.y = (selected.y / cell_size) * cell_size;

                    if ((selected.x >= 150 && selected.x < this.settings.cols * this.settings.cell_size + INDENT_X) &&
                                        (selected.y >= 0 && selected.y < this.settings.rows * this.settings.cell_size))
                    {  

                        // moved same object
                        for (int r = 0; r < this.settings.rows; r++)
                        {
                            for (int s = 0; s < this.settings.cols; s++)
                            {
                                // moved same object
                                if (playground[r, s] == selected.id)
                                {
                                    playground[r, s] = 0;
                                }
                            }
                        }
                        // playing
                        int fromX = (selected.x-INDENT_X) / this.settings.cell_size;
                        int fromY = selected.y / this.settings.cell_size;

                        int toX = (selected.width / this.settings.cell_size) + fromX;
                        int toY = (selected.height / this.settings.cell_size) + fromY;

                        // if in playground borders
                        if ((toX <= settings.cols) && (toY <= settings.rows))
                        {
                            foreach (Block block in this.blocks)
                            {
                                fromX = (block.x - INDENT_X) / this.settings.cell_size;
                                fromY = block.y / this.settings.cell_size;

                                toX = (block.width / this.settings.cell_size) + fromX;
                                toY = (block.height / this.settings.cell_size) + fromY;

                                if ((toX <= settings.cols) && (toY <= settings.rows))
                                {
                                    for (int r = fromY; r < toY; r++)
                                    {
                                        for (int s = fromX; s < toX; s++)
                                        {
                                            playground[r, s] = block.id;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            selected.x = selected.startX;
                            selected.y = selected.startY;
                        }
                    }
                    selected = null;
                }
                // Upper Left reset and resize
                if (bupper_left)
                {
                    bupper_left = false;

                    if (changed.width % settings.cell_size != 0)
                    {
                        int dx = settings.cell_size - (changed.width % settings.cell_size);
                        changed.x -= dx;
                        changed.width += dx;
                    }

                    if (changed.height % settings.cell_size != 0)
                    {
                        int dy = settings.cell_size - (changed.height % settings.cell_size);
                        changed.y -= dy;
                        changed.height += dy;
                    }
                    changed.W = changed.width / settings.cell_size;
                    changed.H = changed.height / settings.cell_size;
                }
                // Upper Right reset and resize
                if (bupper_right)
                {
                    bupper_right = false;
                    if (changed.width % settings.cell_size != 0)
                    {
                        int dx = settings.cell_size - (changed.width % settings.cell_size);
                        changed.width += dx;
                    }

                    if (changed.height % settings.cell_size != 0)
                    {
                        int dy = settings.cell_size - (changed.height % settings.cell_size);
                        changed.y -= dy;
                        changed.height += dy;
                    }
                    changed.W = changed.width / settings.cell_size;
                    changed.H = changed.height / settings.cell_size;
                }
                // Lower Left reset and resize
                if (blower_left)
                {
                    blower_left = false;
                    if (changed.width % settings.cell_size != 0)
                    {
                        int dx = settings.cell_size - (changed.width % settings.cell_size);
                        changed.x -= dx;
                        changed.width += dx;
                    }

                    if (changed.height % settings.cell_size != 0)
                    {
                        int dy = settings.cell_size - (changed.height % settings.cell_size);
                        changed.height += dy;
                    }
                    changed.W = changed.width / settings.cell_size;
                    changed.H = changed.height / settings.cell_size;
                }
                // Lower Right reset and resize
                if (blower_right)
                {
                    blower_right = false;
                    if (changed.width % settings.cell_size != 0)
                    {
                        int dx = settings.cell_size - (changed.width % settings.cell_size);
                        changed.width += dx;
                    }

                    if (changed.height % settings.cell_size != 0)
                    {
                        int dy = settings.cell_size - (changed.height % settings.cell_size);
                        changed.height += dy;
                    }
                    changed.W = changed.width / settings.cell_size;
                    changed.H = changed.height / settings.cell_size;
                }
                Invalidate();
            }
            else if (MouseButtons.Right == e.Button)
            {
                for (int i = 0; i < blocks.Count; i++)
                {
                    if (e.X < blocks[i].x + blocks[i].width && e.X > blocks[i].x)
                    {
                        if (e.Y < blocks[i].y + blocks[i].height && e.Y > blocks[i].y)
                        {
                            // rotate - change W and H
                            int W = blocks[i].W;
                            blocks[i].W = blocks[i].H;
                            blocks[i].H = W;

                            blocks[i].recalculate_shape(blocks[i].cell_size);
                            Invalidate();
                            break;
                        }
                    }
                }
            }
        }

        // BUTTONS
        private void save_first_mode_Click(object sender, EventArgs e)
        {
            if (blocks.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Naozaj chcete uložiť úlohu?", "Uloženie pre Mód 1.", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string path = "mode1";
                    Directory.CreateDirectory(path);
                    var files = Directory.GetFiles(path).OrderBy(name => name).ToArray();
                    int n = files.Length + 1;
                    foreach (Block block in blocks)
                    {
                        block.x -= (INDENT_X - 30);
                        block.startX = block.x;
                        block.startY = block.y;
                    }
                    settings.blockCount = blocks.Count;
                    settings.blocks = blocks;
                    settings.playground = playground;
                    settings.window_width = Size.Width;
                    settings.window_height = Size.Height;

                    string fname = "";

                    if (n >= 1 && n <= 9)
                        fname = $"{path}/level0{n}";
                    else
                        fname = $"{path}/level{n}";

                    sm.save(settings, fname);

                    blocks = new List<Block>();                                               //hura na dalsi level
                    this.playground = new int[this.settings.rows, this.settings.cols];
                    this.mode2_playground = new int[this.settings.rows, this.settings.cols];
                    ix = 0;
                    colorInQueue = 0;
                    textBox1.BackColor = colours[colorInQueue];
                    Invalidate();
                }
            }
            else
            {
                MessageBox.Show("Nevytvoril si žiadne tapety");
            }
        }

        private void save_2_playground_Click(object sender, EventArgs e)
        {
            // save only playground for Mode 2
            if (blocks.Count > 0)
            {
                mode2_playground = playground;
                save_2_blocks.Enabled = true;
                save_2_playground.Enabled = false;
            }
            else
            {
                MessageBox.Show("Nevytvoril si žiadne tapety");
            }
        }

        private void save_2_blocks_Click(object sender, EventArgs e)
        {
            // save blocks
            DialogResult dialogResult = MessageBox.Show("Naozaj chcete uložiť úlohu?", "Uloženie pre Mód 2.", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string path = "mode2";
                Directory.CreateDirectory(path);
                var files = Directory.GetFiles(path).OrderBy(name => name).ToArray();
                int n = files.Length + 1;

                settings.blockCount = blocks.Count;
                foreach (Block block in blocks)
                {
                    block.startX = block.x;
                    block.startY = block.y;
                }
                settings.blocks = blocks;
                settings.playground = mode2_playground;
                settings.window_width = Size.Width;
                settings.window_height = Size.Height;

                string fname = "";

                if (n >= 1 && n <= 9)
                    fname = $"{path}/level0{n}";
                else
                    fname = $"{path}/level{n}";

                sm.save(settings, fname);

                blocks = new List<Block>();                                               //hura na dalsi level
                this.playground = new int[this.settings.rows, this.settings.cols];
                this.mode2_playground = new int[this.settings.rows, this.settings.cols];
                ix = 0;
                colorInQueue = 0;
                textBox1.BackColor = colours[colorInQueue];
                Invalidate();

                save_2_blocks.Enabled = false;          //spatne prepnutie
                save_2_playground.Enabled = true;
            }
        }
    }
}