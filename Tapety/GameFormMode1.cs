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
        bool clicked, showWin;
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
      
         public obdlznik[] generate_blocks()
        {
            Debug.WriteLine(this.settings.blockCount);
            ob = new obdlznik[this.settings.blockCount];
            ob[0].x = this.settings.cols;
            ob[0].y = this.settings.rows;
            ob = rozdel(this.settings.blockCount, ob);
            while (ob == null)
            {
                ob = new obdlznik[this.settings.blockCount];
                ob[0].x = this.settings.cols;
                ob[0].y = this.settings.rows;
                ob = rozdel(this.settings.blockCount, ob);
            }
            return ob;
        }

        public obdlznik[] rozdel(int pocet_casti, obdlznik[] obdl)
        {        // funkcia
            bool spravne = false;
            bool jednotkovahrana = false;
            obdl[0].posX = 0;
            obdl[0].posY = 0;
            int r, s, t;                                                //nahodne premenne
            Random rnd = new Random();                                //vytvorenie random generatoru 
            int rozdiel = 0;
            int counter = 0;
            // Console.WriteLine("posX: " + obdl[0].posX + " posY: " + obdl[0].posY + " x: " + obdl[0].x + " y: " + obdl[0].y);
            for (int i = 0; i < pocet_casti - 1; i++)
            {                   //ideme delit v kazdej iteracii cyklu 1 nahodnu cast na 2 mensie
                while (!spravne)
                {                                        //nie kazdu malu cast vieme rozdelit(taku co ma velkost jedna uz nerozdelime)
                                                         //preto ideme dovtedy vo while cykle, az kym nerozdelime na 2 spravne casti o velkosti apson 1
                    r = rnd.Next(i + 1);                            //nahodne vyberieme, ze ktory utvar ideme delit
                    s = rnd.Next(2);                              //nahodne vyberieme, ci ho rozrezeme na vysku alebo na sirku      
                    counter++;
                    if (counter == 40) return null;
                    if (s == 0)
                    {                                  //ak 0, tak ideme rezat na sirku
                        if ((obdl[r].x > 2) && (obdl[r].y > 1))
                        {                          //ak mozme rezat, teda ak ma sirku aspon 2, inak znova prejde while cyklus a znovu sa vygeneruju nahodne premenne
                            if (!jednotkovahrana)
                            {
                                t = rnd.Next(1, obdl[r].x);               //nahodne miesto kde ho rozdelime
                                if ((t == 1) || (t == (obdl[r].x - 1)))
                                {
                                    jednotkovahrana = true;
                                }
                                obdl[i + 1].x = obdl[r].x - t;              //vypocitame a priradime sirku noveho
                                obdl[i + 1].y = obdl[r].y;               //priradime rovnaku vysku aku mal stary aj novemu
                                obdl[r].x = t;                            //stary skratime o velkost noveho
                                obdl[i + 1].posX = obdl[r].posX + t;
                                obdl[i + 1].posY = obdl[r].posY;
                                rozdiel = t;
                                spravne = true;                           //nastalo spravne rozdelenie a tym padom uz bool spravne bude true, cize skonci while cyklus
                            }
                            else
                            {
                                t = rnd.Next(2, obdl[r].x - 1);               //nahodne miesto kde ho rozdelime
                                if ((t > 2) && (t < obdl[r].x - 1))
                                {
                                    obdl[i + 1].x = obdl[r].x - t;              //vypocitame a priradime sirku noveho
                                    obdl[i + 1].y = obdl[r].y;               //priradime rovnaku vysku aku mal stary aj novemu
                                    obdl[r].x = t;                            //stary skratime o velkost noveho
                                    obdl[i + 1].posX = obdl[r].posX + t;
                                    obdl[i + 1].posY = obdl[r].posY;
                                    rozdiel = t;
                                    spravne = true;                           //nastalo spravne rozdelenie a tym padom uz bool spravne bude true, cize skonci while cyklus
                                }
                            }
                        }
                    }
                    else
                    {
                        if ((obdl[r].y > 2) && (obdl[r].x > 1))
                        {                          //ak nie nula, teda inak, ideme rezat na vysku
                            if (!jednotkovahrana)
                            {
                                t = rnd.Next(1, obdl[r].y);               //to iste ako for nad nami len nerezeme na sirku ale na vysku
                                if ((t == 1) || (t == (obdl[r].y - 1)))
                                {
                                    jednotkovahrana = true;
                                }
                                obdl[i + 1].y = obdl[r].y - t;
                                obdl[i + 1].x = obdl[r].x;
                                obdl[r].y = t;
                                obdl[i + 1].posX = obdl[r].posX;
                                obdl[i + 1].posY = obdl[r].posY + t;
                                rozdiel = t;
                                spravne = true;
                            }
                            else
                            {
                                t = rnd.Next(2, obdl[r].y - 1);               //nahodne miesto kde ho rozdelime
                                if ((t > 2) && (t < obdl[r].y - 1))
                                {
                                    obdl[i + 1].y = obdl[r].y - t;
                                    obdl[i + 1].x = obdl[r].x;
                                    obdl[r].y = t;
                                    obdl[i + 1].posX = obdl[r].posX;
                                    obdl[i + 1].posY = obdl[r].posY + t;
                                    rozdiel = t;
                                    spravne = true;
                                }
                            }
                        }
                    }
                }
                // Console.WriteLine("posX: " + obdl[i + 1].posX + " posY: " + obdl[i + 1].posY + " x: " + obdl[i + 1].x + " y: " + obdl[i + 1].y + " t: " + rozdiel);
                spravne = false;                                //opat vratime na povodnu hodnotu, aby nam v dalsej iteracii for cyklu bezal aj while cyklus
                counter = 0;
            }
            return obdl;      //ked skonci for, vraciame vysledne pole v ktorom na kazdom policku je objekt s vyskou a sirkou
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
                generate_blocks();
                
                this.INDENT_X = Screen.PrimaryScreen.Bounds.Width - (settings.cols * settings.cell_size) - 1050;
                this.INDENT_Y = Screen.PrimaryScreen.Bounds.Height - (settings.rows * settings.cell_size) - 414;
                

                // rozmiestni okolo hracej plochy
                // TODO: musi sa zlepsit !!!!
                this.blocks = new Block[this.settings.blockCount];
                int ix = 0;       
                int posX = INDENT_X - settings.cell_size ;
                int posY = INDENT_Y + this.settings.cell_size * this.settings.cols;
                Rectangle boundRect = new Rectangle(0, INDENT_Y + this.settings.cell_size * this.settings.cols, 2000, 10);

                foreach (obdlznik obdl in ob)
                {
                    int round = 0;
                    Color color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                    int W = obdl.x;
                    int H = obdl.y;
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
                            Block block = new Block(ix + 1, posX, posY, W, H, this.settings.cell_size, color);
                            block.finalX = obdl.posX;
                            block.finalY = obdl.posY;
                            blocks[ix] = block;
                            ix += 1;
                            posX = INDENT_X - settings.cell_size;
                            posY = INDENT_Y + this.settings.cell_size * this.settings.cols;
                            positionedBlocks.Add(block);
                            //Debug.WriteLine("UMIESTNUJEM");
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
                this.playground = new int[this.settings.rows, this.settings.cols];
                this.MinimumSize = new Size((settings.cols * settings.cell_size) * 3, 600);
                this.OnResize(EventArgs.Empty);

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
            generate_blocks();
            Invalidate();
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

                // TODO: chyba gridBlocks
                if (!gridBlocks.Contains(block))
                {
                    gridBlocks.Add(block);   
                }
            }

            // TODO: chyba update_colors
            //update_colors();
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
            //else if (MouseButtons.Right == e.Button)
            //{
            //    for (int i = 0; i < settings.blocks.Count; i++)
            //    {
            //        if (e.X < settings.blocks[i].x + settings.blocks[i].width && e.X > settings.blocks[i].x)
            //        {
            //            if (e.Y < settings.blocks[i].y + settings.blocks[i].height && e.Y > settings.blocks[i].y)
            //            {
            //                // rotate - change W and H
            //                int W = settings.blocks[i].W;
            //                settings.blocks[i].W = settings.blocks[i].H;
            //                settings.blocks[i].H = W;

            //                settings.blocks[i].recalculate_shape(settings.blocks[i].cell_size);
            //                Invalidate();
            //                break;
            //            }
            //        }
            //    }
            //}
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
