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
        MainForm mainForm;
        SaveLoadManager sm;
        Dictionary<int, Color> idcolor_map;
        Point delta;
        string path = "mode2";
        List<Block> lastMoved = new List<Block>();

        public GameFormMode2(MainForm mainForm)
        {
            DoubleBuffered = true;
            InitializeComponent();

            idcolor_map = new Dictionary<int, Color>();
            sm = new SaveLoadManager();
            this.clicked = false;
            this.showWin = false;
            this.rnd = new Random();

            this.mainForm = mainForm;
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
                this.Hide();
                mainForm.Show();
            }
            else
            { 
                string fname = $"{files[game_level_index]}";
                this.settings = sm.load(fname);            

                this.playground = new int[this.settings.rows, this.settings.cols];
                this.Size = new Size(settings.window_width, settings.window_height);                
                this.draw_game();
            }
        }

        private void GameFormMode2_Paint(object sender, PaintEventArgs e)
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
                if (!idcolor_map.ContainsKey(block.id))
                {
                    idcolor_map.Add(block.id, block.color);
                }                
            }
            
            int indentX = this.settings.cols * this.settings.cell_size;
            for (int i = 0; i < this.settings.cols; i++)
            {
                for (int j = 0; j < this.settings.rows; j++)
                {
                    
                    if(idcolor_map.ContainsKey(settings.playground[j, i]))
                    {
                        Color c = idcolor_map[settings.playground[j, i]];
                        Brush brush = new SolidBrush(c);
                        e.Graphics.FillRectangle(brush, i * this.settings.cell_size + (indentX + this.settings.cell_size), j * this.settings.cell_size, this.settings.cell_size, this.settings.cell_size);

                        
                    }
                    else
                    {
                        Pen blackPen = new Pen(Color.Black, 1);
                        e.Graphics.DrawRectangle(blackPen, i * this.settings.cell_size, j * this.settings.cell_size, this.settings.cell_size, this.settings.cell_size);
                        Pen pen = new Pen(Color.Black, 1);
                        e.Graphics.DrawRectangle(pen, i * this.settings.cell_size + (indentX + this.settings.cell_size), j * this.settings.cell_size, this.settings.cell_size, this.settings.cell_size);
                    }
                    
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

        void draw_game()
        {
            Invalidate();
            this.AnotherGame.Hide();

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
                        if ((e.X <= settings.cols * this.settings.cell_size) && (e.Y <= settings.rows * this.settings.cell_size))
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
                        }
                        else
                        {
                            update_colors(selected.color);
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

            var files = Directory.GetFiles(path).OrderBy(name => name).ToArray();
            
            if (game_level_index < max_game_level_index)                                   //tu chybalo =
            {
                string next_level_path = $"{files[game_level_index]}";
                this.settings = sm.load(next_level_path);
                this.playground = new int[this.settings.rows, this.settings.cols];
                this.Size = new Size(settings.window_width, settings.window_height);
                idcolor_map = new Dictionary<int, Color>();

                this.draw_game();
            }
            else
            {
                label1.Text = "Koniec";
                AnotherGame.Hide();
            }
            Invalidate();
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
