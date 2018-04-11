using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        bool clicked, showWin, next_game_shown;
        Random rnd;
        int[,] playground;
        int game_level_index;
        int max_game_level_index;
        MainForm mainForm;
        SaveLoadManager sm;
        Dictionary<int, Color> idcolor_map;
        string path = "mode1";
        public obdlznik[] ob;
        List<Block> gridBlocks;
        List<Block> positionedBlocks;
        List<Label> colorLabels;
        Block[] blocks;

        int old_indentX, old_indentY;
        int INDENT_X, INDENT_Y;

        int deltaX, deltaY;

        public GameFormMode1(MainForm mainForm)
        {
            DoubleBuffered = true;
            KeyPreview = true;
            next_game_shown = false;
            InitializeComponent();

            this.clicked = false;
            this.showWin = false;
            this.rnd = new Random();
            this.sm = new SaveLoadManager();
            this.mainForm = mainForm;
            
            this.gridBlocks = new List<Block>();
            this.positionedBlocks = new List<Block>();
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
        }

        private void GameFormMode1_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            // loaded in shown
            old_indentX = INDENT_X;
            old_indentY = INDENT_Y;

            this.INDENT_X = control.Size.Width - (settings.cols * settings.cell_size) - 100;
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

        private void GameFormMode1_Shown(object sender, EventArgs e)
        {
            
            Directory.CreateDirectory(path);
            var files = Directory.GetFiles(path).OrderBy(name => name).ToArray();
            game_level_index = 0;
            max_game_level_index = files.Length;

            // TODO: spravit o uroven vyssie
            if (max_game_level_index == 0)
            {
                this.Hide();
                mainForm.Show();
            }
            else
            {
                string fname = $"{files[game_level_index]}";  // first inde starts from 0
                this.settings = sm.load(fname);
                
                this.INDENT_X = this.Size.Width - (settings.cols * settings.cell_size) - 100;
                this.INDENT_Y = this.Size.Height - (settings.rows * settings.cell_size) + 50;
                


                // rozmiestni okolo hracej plochy
                PositionAlgoritm();
                this.playground = new int[this.settings.rows, this.settings.cols];
                this.MinimumSize = new Size((settings.cols * settings.cell_size) * 3, 600);
                this.OnResize(EventArgs.Empty);

                Invalidate();
            }
        }

        private void PositionAlgoritm()
        {
            Debug.WriteLine("tapety");
            Debug.WriteLine(INDENT_X);
            Debug.WriteLine(INDENT_Y);
            this.blocks = new Block[this.settings.blockCount];
                int ix = 0;       
                int posX = INDENT_X - settings.cell_size ;
                int posY = INDENT_Y + this.settings.cell_size * this.settings.cols;
                Rectangle boundRect = new Rectangle(0, INDENT_Y + this.settings.cell_size * this.settings.cols, 2000, 10);

                foreach (Block obdl in this.settings.blocks)
                {
                    int round = 0;
                    //Color color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                    int W = obdl.W;
                    int H = obdl.H;
                    int width = this.settings.cell_size * W;
                    int height = this.settings.cell_size * H;

                    while (true)
                    {

                        Rectangle r1 = new Rectangle(posX - 5, posY - 5, width + 5, height + 5);
                        Rectangle r2 = new Rectangle(INDENT_X - 1, INDENT_Y - 1,
                            settings.cols * settings.cell_size + 1, settings.rows * settings.cell_size + 1);

                        Boolean free = true;
                        foreach (Block b in positionedBlocks)
                        {
                            Rectangle r = new Rectangle(b.x - 5, b.y - 5, b.width + 5, b.height + 5);
                            if (r1.IntersectsWith(r))
                            {
                                free = false;
                            }
                        }

                        if (!(r1.IntersectsWith(r2)) && free && !(r1.IntersectsWith(boundRect)))
                        {
                            Block block = new Block(ix + 1, posX, posY, W, H, this.settings.cell_size, obdl.color);
                            block.finalX = obdl.finalX;
                            block.finalY = obdl.finalY;
                            blocks[ix] = block;
                            ix += 1;
                            posX = INDENT_X - settings.cell_size;
                            posY = INDENT_Y + this.settings.cell_size * this.settings.cols;
                            positionedBlocks.Add(block);
                            Debug.WriteLine("UMIESTNUJEM");
                            break;
                        }
                        else
                        {
                            if (posY > INDENT_Y - settings.cell_size * round)
                            {
                                posY -= 5;
                                //Debug.WriteLine("idem hore");
                            }
                            else if (posX + width < INDENT_X + settings.cols * settings.cell_size)
                            {
                                posX += 5;
                                //Debug.WriteLine("idem doprava");
                            }
                            else
                            {
                                round += 1;
                                posX = INDENT_X - (1 + round) * settings.cell_size - 5;
                                posY = INDENT_Y + this.settings.cell_size * this.settings.cols;
                            }

                            //Debug.WriteLine("x" + " " + posX + " / " + "y" + " "+ posY + " "+ "round"+round);
                        }
                    }
                }

                
                
                this.settings.blocks = positionedBlocks;
        }
        private void GameFormMode1_Paint(object sender, PaintEventArgs e)
        {

            if (next_game_shown)
            {
                AnotherGame.Show();
            }
            else
            {
                AnotherGame.Hide();
            }

            // draw playing area
            // it goes first by the colls - X
            Debug.WriteLine("mriezka");
            Debug.WriteLine(INDENT_X);
            Debug.WriteLine(INDENT_Y);
            for (int i = 0; i < this.settings.cols; i++)
            {
                for (int j = 0; j < this.settings.rows; j++)
                {
                    Pen blackPen = new Pen(Color.Black, 1);
                    e.Graphics.DrawRectangle(blackPen, i * this.settings.cell_size + INDENT_X, j * this.settings.cell_size + INDENT_Y, this.settings.cell_size, this.settings.cell_size);
                }
            }

            // draw blocks

            foreach (Block block in positionedBlocks)
            {
                block.Kresli(e.Graphics);
            }

            if (showWin)
            {
                Graphics g = e.Graphics;
                Bitmap main_image = new Bitmap("smile.png");
                next_game_shown = true;

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
            this.positionedBlocks = new List<Block>();
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
                next_game_shown = false;
                PositionAlgoritm();
            }
            else
            {
                label1.Text = "Koniec";
                next_game_shown = false;
            }
            Invalidate();
        }

        private void GameFormMode1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                if (AnotherGame.Visible)
                {
                    next_game_shown = false;
                    Invalidate();
                }
                else
                {
                    next_game_shown = true;
                    Invalidate();
                }

            }
        }

        private void back_to_menu_Click(object sender, EventArgs e)
        {
            this.Hide();
            mainForm.Show();
        }

        private void show_final_state_Click(object sender, EventArgs e)
        {
            foreach (Block block in settings.blocks)
            {
                block.x = INDENT_X + (block.finalX * settings.cell_size);
                block.y = INDENT_Y + (block.finalY * settings.cell_size);

                if (!gridBlocks.Contains(block))
                {
                    gridBlocks.Add(block);   
                }
            }

            update_colors();
            Invalidate();
        }

        private void GameFormMode1_MouseUp(object sender, MouseEventArgs e)
        {
            clicked = false;
            if (MouseButtons.Left == e.Button)
            {
                if (selected != null)
                {
                    clicked = false;
                    int half_size = settings.cell_size / 2;

                    if ((selected.x >= INDENT_X - half_size && selected.x < INDENT_X + half_size + (this.settings.cols * this.settings.cell_size)) &&
                        (selected.y >= INDENT_Y - half_size && selected.y < INDENT_Y + half_size + (this.settings.rows * this.settings.cell_size)))
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

                        Console.WriteLine($"{INDENT_X} {INDENT_Y}, {selected.x} {selected.y}");

                        int toX = (selected.width / this.settings.cell_size) + fromX;
                        int toY = (selected.height / this.settings.cell_size) + fromY;

                        bool return_to_start = false;

                        // if in playground borders
                        if ((toX <= settings.cols) && (toY <= settings.rows) && (fromX >= 0) && (fromY >= 0))
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
                            if (return_to_start == false)
                            {
                                // TODO: chyba gridBlocks

                                if (!gridBlocks.Contains(selected))
                                {
                                    gridBlocks.Add(selected);
                                    update_colors();
                                }
                            }
                            // check game over
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
                            selected.in_playground = false;                          

                            // TODO: chyba gridBlocks
                            if (gridBlocks.Contains(selected))
                            {
                                gridBlocks.Remove(selected);
                                update_colors();
                            }
                        }
                    }
                    else
                    {
                        selected.x = selected.startX;
                        selected.y = selected.startY;
                        selected.in_playground = false;

                        // TODO: chyba gridBlocks
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
        }
        private void update_colors()
        {
            for (int i=0 ; i<colorLabels.Count; i++)
            {
                if (i < gridBlocks.Count)
                {
                    colorLabels[i].BackColor = gridBlocks[i].color;
                    colorLabels[i].Visible = true;
                }
                else
                {
                    colorLabels[i].BackColor = SystemColors.Control;
                    colorLabels[i].Visible = false;
                }
	            
            }
        }
    }
}
