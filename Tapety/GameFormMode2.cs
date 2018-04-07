using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Minisoft1
{
    public partial class GameFormMode2 : Form
    {
        Settings settings;
        Block selected;
        bool clicked, showWin;
        Random rnd;
        int[,] playground;
        int game_level_index;
        int max_game_level_index;
        List<Block> gridBlocks;
        List<Label> colorLabels;
        MainForm mainForm;
        SaveLoadManager sm;
        Dictionary<int, Color> idcolor_map;
        Point delta;
        string path = "mode2";
        List<Block> lastMoved = new List<Block>();
        int old_indentX, old_indentY;
        int INDENT_X, INDENT_Y;
        int deltaX, deltaY;

        public GameFormMode2(MainForm mainForm)
        {
            DoubleBuffered = true;
            InitializeComponent();

            idcolor_map = new Dictionary<int, Color>();
            sm = new SaveLoadManager();
            this.clicked = false;
            this.showWin = false;
            this.rnd = new Random();
            this.gridBlocks = new List<Block>();
            this.colorLabels = new List<Label>();
            this.colorLabels.Add(color_lab1);
            this.colorLabels.Add(color_lab2);
            this.colorLabels.Add(color_lab3);
            this.colorLabels.Add(color_lab4);
            this.colorLabels.Add(color_lab5);
            this.colorLabels.Add(color_lab6);
            this.colorLabels.Add(color_lab7);
            this.colorLabels.Add(color_lab8);
            this.colorLabels.Add(color_lab9);
            this.colorLabels.Add(color_lab10);

            this.mainForm = mainForm;
        }

        private void GameFormMode2_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            // loaded in shown
            old_indentX = INDENT_X;
            old_indentY = INDENT_Y;

            this.INDENT_X = control.Size.Width - (settings.cols * settings.cell_size) - 300;
            this.INDENT_Y = control.Size.Height - (settings.rows * settings.cell_size) - 54 - 100;

            //Console.WriteLine($"{old_indentX} {old_indentY} || {INDENT_X} {INDENT_Y}");

            if (this.settings.blocks != null)
            {
                foreach (Block block in this.settings.blocks)
                {
                    if (block.in_playground)
                    {
                        block.x += INDENT_X - old_indentX;
                        block.y += INDENT_Y - old_indentY;
                    }
                }
            }

            Invalidate();
        }

        private void GameFormMode2_Shown(object sender, EventArgs e)
        {
            // load level - remember how many levels
            Directory.CreateDirectory(path);
            var files = Directory.GetFiles(path).OrderBy(name => name).ToArray();
            game_level_index = 0;
            max_game_level_index = files.Length;

            if (max_game_level_index == 0)
            {
                this.Hide(); // o uroven vyssie?
                mainForm.Show();
            }
            else
            { 
                string fname = $"{files[game_level_index]}";
                this.settings = sm.load(fname);


                this.INDENT_X = Screen.PrimaryScreen.Bounds.Width - (settings.cols * settings.cell_size) - 100;
                this.INDENT_Y = Screen.PrimaryScreen.Bounds.Height - (settings.rows * settings.cell_size) - 54 - 100;

                // rozmiestni okolo hracej plochy
                // TODO: musi sa zlepsit !!!!

                int gap = 5;
                int px = (this.settings.cols * this.settings.cell_size) + gap;
                int py = (this.settings.rows * this.settings.cell_size) + gap;

                int posunX_vedla = gap;
                int posunX_dole = gap;
                int posunY_dole_max = 0;

                int x, y;

                foreach (Block block in settings.blocks)
                {

                    Color color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

                    int width = this.settings.cell_size * block.W;
                    int height = this.settings.cell_size * block.H;

                    // na pravo od hracej plochy
                    if (height >= width)
                    {
                        x = px + posunX_vedla;
                        y = 0;

                        posunX_vedla += width + gap;
                    }
                    // pod hraciu plochu
                    else
                    {
                        // placing out of window WIDTH
                        if ((gap + width + posunX_dole) > this.ClientRectangle.Width)
                        {
                            py += posunY_dole_max + gap;
                            posunY_dole_max = 0;
                            posunX_dole = gap;
                        }

                        // max height
                        if (height > posunY_dole_max)
                        {
                            posunY_dole_max = height;
                        }

                        x = posunX_dole;
                        y = py;

                        posunX_dole += width + gap;
                    }

                    block.x = x;
                    block.y = y;
                    block.startX = x;
                    block.startY = y;
                }

                this.playground = new int[this.settings.rows, this.settings.cols];
                this.MinimumSize = new Size((settings.cols * settings.cell_size) * 3, 600);
                this.OnResize(EventArgs.Empty);

                Invalidate();
                this.AnotherGame.Hide();
            }
        }

        private void GameFormMode2_Paint(object sender, PaintEventArgs e)
        {

            // map block id to its color
            foreach (Block block in this.settings.blocks)
            {                
                if (!idcolor_map.ContainsKey(block.id))
                {
                    idcolor_map.Add(block.id, block.color);
                }
            }

            Control control = (Control)sender;
            int local_indentX = control.Size.Width - 300 + settings.cell_size;
            int local_indentY = control.Size.Height - (settings.rows * settings.cell_size) - 54 - 100;

            for (int i = 0; i < this.settings.cols; i++)
            {
                for (int j = 0; j < this.settings.rows; j++)
                {
                    // draw playing area
                    Pen blackPen = new Pen(Color.Black, 1);
                    e.Graphics.DrawRectangle(blackPen, i * this.settings.cell_size + INDENT_X, j * this.settings.cell_size + INDENT_Y, this.settings.cell_size, this.settings.cell_size);

                    // draw final state example area
                    e.Graphics.DrawRectangle(blackPen, i * this.settings.cell_size + local_indentX, j * this.settings.cell_size + local_indentY, this.settings.cell_size, this.settings.cell_size);

                    if (idcolor_map.ContainsKey(settings.playground[j, i]))
                    {
                        Color c = idcolor_map[settings.playground[j, i]];
                        Brush brush = new SolidBrush(c);
                        e.Graphics.FillRectangle(brush, i * this.settings.cell_size + local_indentX, j * this.settings.cell_size + local_indentY, this.settings.cell_size, this.settings.cell_size);
                    }

                }
            }

            // draw blocks last
            foreach (Block block in this.settings.blocks)
            {
                block.Kresli(e.Graphics);
            }

            if (showWin)
            {
                Graphics g = e.Graphics;
                Bitmap main_image = new Bitmap("smile.png");

                Color backColor = main_image.GetPixel(1, 1);
                main_image.MakeTransparent(backColor);

                e.Graphics.DrawImage(
                    main_image, this.Width - 100, 0, 64, 64);
            }     
        }

        private void GameFormMode2_MouseDown(object sender, MouseEventArgs e)
        {
            //for (int i = 0; i < settings.blocks.Count; i++)
            for (int i = settings.blocks.Count-1; i >= 0 ; i--)
            {
                if (e.X < settings.blocks[i].x + settings.blocks[i].width && e.X > settings.blocks[i].x)
                {
                    if (e.Y < settings.blocks[i].y + settings.blocks[i].height && e.Y > settings.blocks[i].y)
                    {
                        // remember selected block and clicked coords
                        selected = settings.blocks[i];
                        if ((selected.x >= INDENT_X  && selected.x < INDENT_X + (this.settings.cols * this.settings.cell_size)) &&
                            (selected.y >= INDENT_Y  && selected.y < INDENT_Y + (this.settings.rows * this.settings.cell_size)))
                        {
                            if (lastMoved.Last() != selected)
                            {
                                selected = lastMoved.Last();
                                return;
                            }
                            else
                            {
                                lastMoved.RemoveAt(lastMoved.Count - 1);
                            }
                        }
                        delta = new Point(e.X - selected.x, e.Y - selected.y);
                        clicked = true;

                        // set selected as last in array so it is above all other blocks
                        for (int j = i; j < settings.blocks.Count-1; j++)
                        {
                            settings.blocks[j] = settings.blocks[j + 1];
                        }
                        settings.blocks[settings.blocks.Count-1] = selected;
                        Invalidate();
                        break;
                    }
                }
            }
        }

        private void GameFormMode2_MouseMove(object sender, MouseEventArgs e)
        {
            if (clicked)
            {
                int dx = e.X - delta.X;
                int dy = e.Y - delta.Y;

                // if in window size
                //if (dx > 0 && dx + selected.width < this.ClientSize.Width)
                //{
                //    if (dy > 0 && dy + selected.height < this.ClientSize.Height)
                //    {
                        selected.x = dx;
                        selected.y = dy;
                        Invalidate();
                //    }
                //}
            }
        }

        private void GameFormMode2_MouseUp(object sender, MouseEventArgs e)
        {
            clicked = false;
            if (MouseButtons.Left == e.Button)
            {
                if (selected != null)
                {
                    clicked = false;
                    int half_size = settings.cell_size / 2;

                    if ((selected.x >= 0 + INDENT_X - half_size  && selected.x < INDENT_X + half_size  + this.settings.cols * this.settings.cell_size) &&
                        (selected.y >= 0 + INDENT_Y - half_size  && selected.y < INDENT_Y + half_size  + this.settings.rows * this.settings.cell_size))
                    {
                        // suradnice bez odsunutia
                        var noindentX = (selected.x - INDENT_X);
                        var noindentY = (selected.y - INDENT_Y);

                        // doskakovanie
                        int dx = noindentX % this.settings.cell_size;
                        int dy = noindentY % this.settings.cell_size;


                        // dole v pravo
                        if (dx > half_size && dy > half_size)
                        {
                            noindentX += settings.cell_size - dx;
                            noindentY += settings.cell_size - dy;
                        }
                        // hore v pravo
                        else if (dx > half_size && dy <= half_size)
                        {
                            noindentX += settings.cell_size - dx;
                        }
                        // dole v lavo
                        else if (dx <= half_size && dy > half_size)
                        {
                            noindentY += settings.cell_size - dy;
                        }

                        // konecny prepocet doskakovania
                        selected.x = (noindentX / this.settings.cell_size) * this.settings.cell_size + INDENT_X;
                        selected.y = (noindentY / this.settings.cell_size) * this.settings.cell_size + INDENT_Y;

                        selected.in_playground = true;

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
                        int fromX = noindentX / this.settings.cell_size;
                        int fromY = noindentY / this.settings.cell_size;

                        int toX = (selected.width / this.settings.cell_size) + fromX;
                        int toY = (selected.height / this.settings.cell_size) + fromY;

                        bool return_to_start = false;

                        // if in playground borders
                        if ((toX <= settings.cols) && (toY <= settings.rows))
                        {
                            // set ocupied space to selected block id
                            foreach (Block block in this.settings.blocks)
                            {
                                fromX = block.x / this.settings.cell_size;
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

                            // check game over
                            int num = 0;

                            for (int i = 0; i < this.settings.cols; i++)
                            {
                                for (int j = 0; j < this.settings.rows; j++)
                                {

                                    if (settings.playground[j, i] != playground[j, i])
                                    {
                                        num++;
                                    }
                                }
                            }

                            // game over -- filled all blocks
                            if (num == 0)
                            {
                                for (int r = 0; r < this.settings.rows; r++)
                                {
                                    for (int s = 0; s < this.settings.cols; s++)
                                    {
                                        // empty place aby sa nam resetla po vyhrati matica na nuly
                                        playground[r, s] = 0;
                                    }

                                }
                                AnotherGame.Show();
                                showWin = true;
                            }

                        }
                        else
                        {
                            return_to_start = true;
                        }

                        // move to start position
                        if (return_to_start)
                        {
                            selected.x = selected.startX;
                            selected.y = selected.startY;
                            if (gridBlocks.Contains(selected))
                            {
                                gridBlocks.Remove(selected);
                                update_colors();
                            }
                        }
                        else
                        {
                            if (!gridBlocks.Contains(selected))
                            {
                                gridBlocks.Add(selected);
                                update_colors();
                            }
                            if (lastMoved.Contains(selected) == false)
                            {
                                lastMoved.Add(selected);
                            }
                        }
                    }
                    else
                    {
                        selected.x = selected.startX;
                        selected.y = selected.startY;
                        if (gridBlocks.Contains(selected))
                        {
                            gridBlocks.Remove(selected);
                            update_colors();
                        }

                        // moved out of the playground
                        for (int r = 0; r < this.settings.rows; r++)
                        {
                            for (int s = 0; s < this.settings.cols; s++)
                            {
                                // reset
                                if (playground[r, s] == selected.id)
                                {
                                    playground[r, s] = 0;
                                }
                            }
                        }
                    }
                    deltaX = 0;
                    deltaY = 0;
                    selected = null;
                    Invalidate();
                }
            }
            else if (MouseButtons.Right == e.Button)
            {
                for (int i = 0; i < settings.blocks.Count; i++)
                {
                    if (e.X < settings.blocks[i].x + settings.blocks[i].width && e.X > settings.blocks[i].x)
                    {
                        if (e.Y < settings.blocks[i].y + settings.blocks[i].height && e.Y > settings.blocks[i].y)
                        {
                            // rotate - change W and H
                            int W = settings.blocks[i].W;
                            settings.blocks[i].W = settings.blocks[i].H;
                            settings.blocks[i].H = W;

                            settings.blocks[i].recalculate_shape(settings.blocks[i].cell_size);
                            Invalidate();
                            break;
                        }
                    }
                }
            }
        }

        private void back_to_menu_Click(object sender, EventArgs e)
        {
            this.Hide();
            mainForm.Show();
        }

        private void AnotherGame_Click(object sender, EventArgs e)
        {
            // TODO: load another level
            game_level_index++;
            showWin = false;
            this.gridBlocks = new List<Block>();
            update_colors();

            var files = Directory.GetFiles(path).OrderBy(name => name).ToArray();
            
            if (game_level_index < max_game_level_index)                                   //tu chybalo =
            {
                string next_level_path = $"{files[game_level_index]}";
                this.settings = sm.load(next_level_path);
                this.playground = new int[this.settings.rows, this.settings.cols];
                this.Size = new Size(settings.window_width, settings.window_height);
                idcolor_map = new Dictionary<int, Color>();
                this.AnotherGame.Hide();
            }
            else
            {
                label1.Text = "Koniec";
                AnotherGame.Hide();
            }
            Invalidate();
        }

        private void show_final_state_Click(object sender, EventArgs e)
        {
            //foreach (Block block in settings.blocks)
            //{
            //    block.x = INDENT_X + (block.finalX * settings.cell_size);
            //    block.y = INDENT_Y + (block.finalY * settings.cell_size);

            //    if (!gridBlocks.Contains(block))
            //    {
            //        gridBlocks.Add(block);
            //    }
            //}

            for (int i=0; i < settings.blocks.Count; i++)
            {
                settings.blocks[i].x = INDENT_X + (settings.blocks[i].finalX * settings.cell_size);
                settings.blocks[i].y = INDENT_Y + (settings.blocks[i].finalY * settings.cell_size);

                if (!gridBlocks.Contains(settings.blocks[i]))
                {
                    gridBlocks.Add(settings.blocks[i]);
                }
            }

            update_colors();
            Invalidate();
        }

        private void update_colors()
        {
            for (int i=0 ; i<colorLabels.Count; i++)
            {
                if (i < gridBlocks.Count)
                {
                    colorLabels[i].BackColor = gridBlocks[i].color;
                }
                else
                {
                    colorLabels[i].BackColor = SystemColors.Control;
                }
	            
            }
        }
    }
}
