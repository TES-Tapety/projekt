using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Minisoft1
{
    public partial class GameFormMode1 : Form
    {

        Settings settings;
        Block selected;
        bool clicked, showWin;
        Random rnd;
        int[,] playground;
        int game_level_index;
        int max_game_level_index;
        MainForm mainForm;
        SaveLoadManager sm;
        Dictionary<int, Color> idcolor_map;
        string path = "mode1";

        const int INDENT_X = 0;
        const int INDENT_Y = 0;

        int deltaX, deltaY;

        public GameFormMode1(MainForm mainForm)
        {
            DoubleBuffered = true;
            InitializeComponent();

            this.clicked = false;
            this.showWin = false;
            this.rnd = new Random();
            this.sm = new SaveLoadManager();
            this.mainForm = mainForm;
        }

        private void GameFormMode1_Shown(object sender, EventArgs e)
        {
            
            Directory.CreateDirectory(path);
            var files = Directory.GetFiles(path).OrderBy(name => name).ToArray();
            game_level_index = 0;
            max_game_level_index = files.Length;

            if (max_game_level_index == 0)
            {
                this.Hide();
                mainForm.Show();
            }
            else
            {
                string fname = $"{files[game_level_index]}";  // first inde starts from 0
                this.settings = sm.load(fname);

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
                this.Size = new Size(settings.window_width, settings.window_height);
                Invalidate();
                this.AnotherGame.Hide();
            }
        }

        private void GameFormMode1_Paint(object sender, PaintEventArgs e)
        {
            // draw playing area
            // it goes first by the colls - X

            for (int i = 0; i < this.settings.cols; i++)
            {
                for (int j = 0; j < this.settings.rows; j++)
                {
                    Pen blackPen = new Pen(Color.Black, 1);
                    e.Graphics.DrawRectangle(blackPen, i * this.settings.cell_size, j * this.settings.cell_size, this.settings.cell_size, this.settings.cell_size);
                }
            }

            // draw blocks

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

        private void GameFormMode1_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < settings.blocks.Count; i++)
            {
                if (e.X < settings.blocks[i].x + settings.blocks[i].width && e.X > settings.blocks[i].x)
                {
                    if (e.Y < settings.blocks[i].y + settings.blocks[i].height && e.Y > settings.blocks[i].y)
                    {
                        // remember selected block and clicked coords
                        selected = settings.blocks[i];
                        deltaX = e.X - selected.x;
                        deltaY = e.Y - selected.y;
                        clicked = true;

                        // set selected as last in array so it is above all other blocks
                        Block last = settings.blocks[settings.blocks.Count - 1];
                        settings.blocks[settings.blocks.Count - 1] = settings.blocks[i];
                        settings.blocks[i] = last;
                        Invalidate();
                        break;
                    }
                }
            }
        }

        private void GameFormMode1_MouseMove(object sender, MouseEventArgs e)
        {
            if (clicked)
            {
                int dx = e.X - deltaX;
                int dy = e.Y - deltaY;

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

        private void AnotherGame_Click(object sender, EventArgs e)
        {
            // TODO: load another level
            game_level_index++;
            showWin = false;

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

        private void back_to_menu_Click(object sender, EventArgs e)
        {
            this.Hide();
            mainForm.Show();
        }

        private void show_final_state_Click(object sender, EventArgs e)
        {
            foreach (Block block in this.settings.blocks)
            {
                block.x = block.finalX * settings.cell_size;
                block.y = block.finalY * settings.cell_size;
            }
            Invalidate();
        }

        private void GameFormMode1_MouseUp(object sender, MouseEventArgs e)
        {
            clicked = false;
            if (MouseButtons.Left == e.Button)
            {
                if (selected != null)
                {

                    if ((selected.x >= 0 && selected.x < this.settings.cols * this.settings.cell_size) &&
                        (selected.y >= 0 && selected.y < this.settings.rows * this.settings.cell_size))
                    {
                        selected.x = (selected.x / this.settings.cell_size) * this.settings.cell_size;
                        selected.y = (selected.y / this.settings.cell_size) * this.settings.cell_size;

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
                        int fromX = selected.x / this.settings.cell_size;
                        int fromY = selected.y / this.settings.cell_size;

                        int toX = (selected.width / this.settings.cell_size) + fromX;
                        int toY = (selected.height / this.settings.cell_size) + fromY;

                        bool return_to_start = false;

                        // if in playground borders
                        if ((toX <= settings.cols) && (toY <= settings.rows))
                        {
                            // set ocupied space to selected block id
                            for (int r = fromY; r < toY; r++)
                            {
                                for (int s = fromX; s < toX; s++)
                                {
                                    // occupied place
                                    if (playground[r, s] != 0)
                                    {
                                        return_to_start = true;
                                        break;
                                    }
                                    else
                                    {
                                        playground[r, s] = selected.id;
                                    }
                                }
                            }

                            // check if game is over
                            int num = 0;
                            for (int r = 0; r < this.settings.rows; r++)
                            {
                                for (int s = 0; s < this.settings.cols; s++)
                                {
                                    // empty place
                                    if (playground[r, s] == 0)
                                    {
                                        num += 1;
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
                                showWin = true;
                                AnotherGame.Show();
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
                        }
                        else
                        {
                            update_colors(selected.color);
                        }
                    }
                    else
                    {
                        selected.x = selected.startX;
                        selected.y = selected.startY;

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
        private void update_colors(Color color)
        {
            color_lab1.BackColor = color_lab2.BackColor;
            color_lab2.BackColor = color_lab3.BackColor;
            color_lab3.BackColor = color_lab4.BackColor;
            color_lab4.BackColor = color;
        }
    }
}
