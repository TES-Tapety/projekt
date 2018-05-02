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
using System.Windows.Forms.VisualStyles;

namespace Minisoft1
{
    public partial class GameFormMode2 : Form
    {
        Settings settings;
        Block selected;
        bool clicked, showWin, next_game_shown;
        Random rnd;
        int[,] playground;
        int game_level_index;
        int max_game_level_index;
        List<Block> gridBlocks;
        List<Block> positionedBlocks;
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
        public obdlznik[] ob;
        Block[] blocks;
        public List<Block> startBlocks;

        public GameFormMode2(MainForm mainForm)
        {
            DoubleBuffered = true;
            next_game_shown = false;
            KeyPreview = true;
            InitializeComponent();

            idcolor_map = new Dictionary<int, Color>();
            sm = new SaveLoadManager();
            this.clicked = false;
            this.showWin = false;
            this.rnd = new Random();
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
            INDENT_X = 300;
            INDENT_Y = 250;

            this.mainForm = mainForm;
        }        

        private void GameFormMode2_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            //if (this.settings.blocks != null)
            //{
            //    foreach (Block block in this.settings.blocks)
            //    {
            //        if (block.in_playground)
            //        {
            //            block.x += INDENT_X - old_indentX;
            //            block.y += INDENT_Y - old_indentY;
            //        }
            //    }
            //}

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
                startBlocks = new List<Block>(this.settings.blocks);
                INDENT_X = 300;
                INDENT_Y = 250;  
                if (settings.cols > 7 || settings.rows > 7)
                {
                    this.WindowState = FormWindowState.Maximized;
                    INDENT_X = 600;
                    INDENT_Y = 400;    
                }    

                // rozmiestni okolo hracej plochy
                PositionAlgoritm();
               
                this.playground = new int[this.settings.rows, this.settings.cols];
                this.MinimumSize = new Size((settings.cols * settings.cell_size) * 4, 600);
                this.OnResize(EventArgs.Empty);

                Invalidate();
            }
        }
        
        private void PositionAlgoritm()
        {
            this.blocks = new Block[this.settings.blockCount];
                int ix = 0;       
                int posX = INDENT_X - settings.cell_size ;
                int posY = INDENT_Y + this.settings.cell_size * this.settings.cols;
                Rectangle boundRect = new Rectangle(0, INDENT_Y + this.settings.cell_size * this.settings.cols, 2000, 10);

                foreach (Block obdl in this.settings.blocks)
                {
                    int switched = 0;
                    int round = 0;
                    //Color color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                    int W = obdl.W;
                    int H = obdl.H;
                    int width = this.settings.cell_size * W;
                    int height = this.settings.cell_size * H;

                    while (true)
                    {
                        if (switched == 1)
                        {
                            posY -= height;
                        }

                        Rectangle r1 = new Rectangle(posX - 5, posY - 5, width + 5, height + 5);
                        Rectangle r2 = new Rectangle(INDENT_X - 1, INDENT_Y - 1,
                            settings.cols * settings.cell_size + 1, settings.rows * settings.cell_size + 1);

                        Boolean free = true;
                        foreach (Block b in positionedBlocks)
                        {
                            Rectangle r = new Rectangle(b.x - 5 , b.y - 5, b.width +5, b.height + 5);
                            if (r1.IntersectsWith(r))
                            {
                                free = false;
                            }
                        }

                        if (!(r1.IntersectsWith(r2)) && free && !(r1.IntersectsWith(boundRect)))
                        {
                            /*if (r1.X > 0 && r1.Y > 0 && r1.X + r1.Width < this.Size.Width &&
                                r1.Y + r1.Height < this.Size.Height)
                            {*/
                                Block block = new Block(ix + 1, posX, posY, W, H, this.settings.cell_size, obdl.color);
                                block.finalX = obdl.finalX;
                                block.finalY = obdl.finalY;
                                blocks[ix] = block;
                                ix += 1;
                                posX = INDENT_X - settings.cell_size;
                                posY = INDENT_Y + this.settings.cell_size * this.settings.cols;
                                positionedBlocks.Add(block);
                                break;
                            //}
                        }
                        else
                        {
                            if (posY > INDENT_Y - 5 * round)
                            {
                                posY -= 5;
                            }
                            else if (posX + width < INDENT_X + settings.cols * settings.cell_size)
                            {
                                posX += 5;
                                if (switched == 0)
                                {
                                    switched =  1;
                                }
                                else if (switched == 1)
                                {
                                    switched = 2;
                                }
                            }
                            else
                            {
                                round += 1;
                                posX = INDENT_X - (1 + round) * settings.cell_size - 5;
                                posY = INDENT_Y + this.settings.cell_size * this.settings.cols;
                                switched = 0;
                            }

                        }
                    }
                }
                this.settings.blocks = positionedBlocks;
        }

        private void GameFormMode2_Paint(object sender, PaintEventArgs e)
        {

            if (next_game_shown)
            {
                AnotherGame.Show();
            }
            else
            {
                AnotherGame.Hide();
            }

            // map block id to its color
            foreach (Block block in this.settings.blocks)
            {
                if (!idcolor_map.ContainsKey(block.id))
                {
                    idcolor_map.Add(block.id, block.color);
                }
            }

            Control control = (Control) sender;
            int local_indentX = INDENT_X + (settings.cols * settings.cell_size) + settings.cell_size;
            int local_indentY = INDENT_Y;

            for (int i = 0; i < this.settings.cols; i++)
            {
                for (int j = 0; j < this.settings.rows; j++)
                {
                    // draw playing area
                    Pen blackPen = new Pen(Color.Black, 1);
                    e.Graphics.DrawRectangle(blackPen, i * this.settings.cell_size + INDENT_X,
                        j * this.settings.cell_size + INDENT_Y, this.settings.cell_size, this.settings.cell_size);

                    // draw final state example area
                    e.Graphics.DrawRectangle(blackPen, i * this.settings.cell_size + local_indentX,
                        j * this.settings.cell_size + local_indentY, this.settings.cell_size, this.settings.cell_size);

                    if (idcolor_map.ContainsKey(settings.playground[j, i]))
                    {
                        Color c = idcolor_map[settings.playground[j, i]];
                        Brush brush = new SolidBrush(c);
                        e.Graphics.FillRectangle(brush, i * this.settings.cell_size + local_indentX,
                            j * this.settings.cell_size + local_indentY, this.settings.cell_size,
                            this.settings.cell_size);
                    }
                }
            }

            // draw blocks last
            foreach (Block block in settings.blocks)
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
                selected.x = e.X - delta.X;
                selected.y = e.Y - delta.Y;
                Invalidate();
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

                        // moved block - check if in playground and mark or rewrite ids in playground
                        int fromX = noindentX / this.settings.cell_size;
                        int fromY = noindentY / this.settings.cell_size;

                        int toX = (selected.width / this.settings.cell_size) + fromX;
                        int toY = (selected.height / this.settings.cell_size) + fromY;

                        // if in playground borders
                        if ((toX <= settings.cols) && (toY <= settings.rows) && (fromX >= 0) && (fromY >= 0))
                        {
                            // set ocupied space to selected block id
                            foreach (Block block in this.settings.blocks)
                            {
                                
                                // ak je uz v ploche odpocitaj INDENT
                                if (block.in_playground)
                                {
                                    fromX = (block.x - INDENT_X) / this.settings.cell_size;
                                    fromY = (block.y - INDENT_Y) / this.settings.cell_size;
                                }
                                else
                                {
                                    fromX = block.x  / this.settings.cell_size;
                                    fromY = block.y / this.settings.cell_size;
                                }

                                toX = (block.width / this.settings.cell_size) + fromX;
                                toY = (block.height / this.settings.cell_size) + fromY;

                                // prepise sa playground podla aktualnych tapiet
                                if ((toX <= settings.cols) && (toY <= settings.rows) && (fromX >= 0) && (fromY >= 0))
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

                            // show block color 

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
                        else
                        {
                            // return to start
                            selected.x = selected.startX;
                            selected.y = selected.startY;
                            if (gridBlocks.Contains(selected))
                            {
                                gridBlocks.Remove(selected);
                                update_colors();
                            }

                            selected.in_playground = false;
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

                        selected.in_playground = false;

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

        private void GameFormMode2_KeyDown(object sender, KeyEventArgs e)
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

        private void AnotherGame_Click(object sender, EventArgs e)
        {
            // TODO: load another level
            game_level_index++;
            showWin = false;
            this.gridBlocks = new List<Block>();
            this.positionedBlocks = new List<Block>();
            update_colors();

            var files = Directory.GetFiles(path).OrderBy(name => name).ToArray();
            
            if (game_level_index < max_game_level_index)                                   //tu chybalo =
            {
                string next_level_path = $"{files[game_level_index]}";
                this.settings = sm.load(next_level_path);
                startBlocks = new List<Block>(this.settings.blocks);
                INDENT_X = 300;
                INDENT_Y = 250;  
                if (settings.cols > 7 || settings.rows > 7)
                {
                    this.WindowState = FormWindowState.Maximized;
                    INDENT_X = 600;
                    INDENT_Y = 400;    
                }    
                this.playground = new int[this.settings.rows, this.settings.cols];
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

        private void show_final_state_Click(object sender, EventArgs e)
        {
            List<Block> orderList = new List<Block>();
            for (int i=0; i < startBlocks.Count(); i++)
            {
                for (int j = 0; j < this.settings.blocks.Count(); j++)
                {
                    if (startBlocks[i].color == this.settings.blocks[j].color)
                    {
                        this.settings.blocks[j].x = INDENT_X + (this.settings.blocks[j].finalX * settings.cell_size);
                        this.settings.blocks[j].y = INDENT_Y + (this.settings.blocks[j].finalY * settings.cell_size);
                        orderList.Add(this.settings.blocks[j]);
                        
                        if (!gridBlocks.Contains(this.settings.blocks[j]))
                        {
                            gridBlocks.Add(this.settings.blocks[j]);
                            lastMoved.Add(this.settings.blocks[j]);
                        }

                        break;
                    }
                }
            }

            this.settings.blocks = orderList;
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
