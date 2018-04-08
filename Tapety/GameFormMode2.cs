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

            this.mainForm = mainForm;
        }
        
        public obdlznik[] generate_blocks()
        {
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
                generate_blocks();


                this.INDENT_X = Screen.PrimaryScreen.Bounds.Width - (settings.cols * settings.cell_size) - 1050;
                this.INDENT_Y = Screen.PrimaryScreen.Bounds.Height - (settings.rows * settings.cell_size) - 614;

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
            foreach (Block block in settings.blocks)
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
                        if ((toX <= settings.cols) && (toY <= settings.rows))
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
            generate_blocks();

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
            for (int i=0; i < settings.blocks.Count; i++)
            {
                settings.blocks[i].x = INDENT_X + (settings.blocks[i].finalX * settings.cell_size);
                settings.blocks[i].y = INDENT_Y + (settings.blocks[i].finalY * settings.cell_size);

                if (!gridBlocks.Contains(settings.blocks[i]))
                {
                    gridBlocks.Add(settings.blocks[i]);
                    lastMoved.Add(settings.blocks[i]);
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
