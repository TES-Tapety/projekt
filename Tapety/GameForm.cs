using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;



namespace Minisoft1
{
    public struct obdlznik
    {
        public int x, y;
        public int posX, posY;
    }

    public partial class GameForm : Form
	{
		Settings settings;
		Block[] blocks;
	    List<Block> gridBlocks;
	    List<Block> positionedBlocks;
	    List<Label> colorLabels;
		Block selected;
		bool clicked, showWin;
		Random rnd;
        int[,] playground;
        MainForm mainForm;
        int deltaX, deltaY;
        int old_indentX, old_indentY;
        int INDENT_X, INDENT_Y;
        public obdlznik[] ob;

        public GameForm(Settings settings, MainForm mainForm)
		{	
			DoubleBuffered = true;
			InitializeComponent();
			
			this.settings = settings;
            this.INDENT_X = Screen.PrimaryScreen.Bounds.Width - (settings.cols * settings.cell_size) - 100;
            this.INDENT_Y = Screen.PrimaryScreen.Bounds.Height - (settings.rows * settings.cell_size) - 54 - 100;          
            this.clicked = false;
            this.showWin = false;
			this.rnd = new Random();
            this.playground = new int[this.settings.rows, this.settings.cols];
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

        private void GameForm_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            old_indentX = INDENT_X;
            old_indentY = INDENT_Y;

            this.INDENT_X = control.Size.Width - (settings.cols * settings.cell_size) - 100;
            this.INDENT_Y = control.Size.Height - (settings.rows * settings.cell_size) - 54 - 100;

            //Console.WriteLine($"{old_indentX} {old_indentY} || {INDENT_X} {INDENT_Y}");

            if (this.blocks != null)
            {
                foreach (Block block in this.blocks)
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

        void GameFormShown(object sender, EventArgs e)
        {
            //int min_width = (settings.cols * settings.cell_size) * 3;
            //int min_height = Screen.PrimaryScreen.WorkingArea.Height;

            //Console.WriteLine($"SHOWN {min_width} {min_height}");

            //if (Screen.PrimaryScreen.WorkingArea.Width < min_width * 2)
            //    this.WindowState = FormWindowState.Maximized;
            //if (Screen.PrimaryScreen.WorkingArea.Height < min_height)
            //    this.WindowState = FormWindowState.Maximized;

            this.MinimumSize = new Size((settings.cols * settings.cell_size) * 3, 600);
            this.OnResize(EventArgs.Empty);
            this.draw_game();
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

        void draw_game()
        {
            Invalidate();
            this.AnotherGame.Hide();
            this.blocks = new Block[this.settings.blockCount];

            // sort by the ob.x and ob.y
            Array.Sort(ob, delegate (obdlznik o1, obdlznik o2) {

                int xdiff = o1.x.CompareTo(o2.x);

                if (xdiff != 0)
                    return xdiff;

                else return o1.y.CompareTo(o2.y);
            });

            // algoritmus rozmiestenia okolo hracej plochy
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
                   
                    Rectangle r1 = new Rectangle(posX-5, posY-5, width+5, height+5);
                    Rectangle r2 = new Rectangle(INDENT_X-1, INDENT_Y-1,
                        settings.cols * settings.cell_size+1, settings.rows * settings.cell_size+1);
                    
                    Boolean free = true;
                    foreach (Block b in positionedBlocks)
                    {
                        Rectangle r = new Rectangle(b.x-5 , b.y-5, b.width+5, b.height+5);
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
                        else if (posX + width < INDENT_X+settings.cols * settings.cell_size)
                        {
                            posX += 5;
                            //Debug.WriteLine("idem doprava");
                        }
                        else
                        {
                            round += 1;
                            posX = INDENT_X - (1+round)*settings.cell_size - 5;
                            posY = INDENT_Y + this.settings.cell_size * this.settings.cols;
                        }
                        //Debug.WriteLine("x" + " " + posX + " / " + "y" + " "+ posY + " "+ "round"+round);
                    }
                } 
            }     
        }      



        void GameFormPaint(object sender, PaintEventArgs e)
		{
			// draw playing area
            // it goes first by the colls - X
			for( int i=0; i < this.settings.cols; i++)
			{
				for( int j=0; j < this.settings.rows; j++)
				{
					Pen blackPen = new Pen(Color.Black, 1);
					e.Graphics.DrawRectangle(blackPen, i*this.settings.cell_size+INDENT_X, j*this.settings.cell_size+INDENT_Y, this.settings.cell_size, this.settings.cell_size);
				}
			}
		    
		    /*Rectangle r2 = new Rectangle(INDENT_X - 5,
		        INDENT_Y - 5,
		        settings.rows * settings.cell_size + 10, settings.cols * settings.cell_size + 10);
		    Pen gPen = new Pen(Color.Gold, 1);
		    e.Graphics.DrawRectangle(gPen, r2);*/
		    
            // draw blocks
            foreach(Block block in blocks)
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

        void GameFormMouseDown(object sender, MouseEventArgs e){
			//if (e.Button == MouseButtons.Left)
   //         {
                for (int i = 0; i < blocks.Length; i++)
                {
                    if (e.X < blocks[i].x + blocks[i].width && e.X > blocks[i].x)
                    {
                        if (e.Y < blocks[i].y + blocks[i].height && e.Y > blocks[i].y)
                        {
                            selected = blocks[i];
                            // remember selected block and clicked coords
                            deltaX = e.X - selected.x;
                            deltaY = e.Y - selected.y;
                            clicked = true;

                            // set selected as last in array so it is above all other blocks
                            Block last = blocks[blocks.Length - 1];
                            blocks[blocks.Length - 1] = blocks[i];
                            blocks[i] = last;
                            Invalidate();
                            break;
                        }
                    }
                }
            //}            
        }

        void GameFormMouseMove(object sender, MouseEventArgs e)
		{
            //if (e.Button == MouseButtons.Left)
            //{
                if (clicked)
                {
                    int dx = e.X - deltaX;
                    int dy = e.Y - deltaY;

                    selected.x = dx;
                    selected.y = dy;
                    Invalidate();
                }
            //}
		}

        private void show_final_state_Click(object sender, EventArgs e)
        {
            foreach (Block block in blocks)
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

        private void AnotherGame_Click(object sender, EventArgs e)
        {
            //this.Size = new Size((settings.rows * settings.cell_size), (settings.cols * settings.cell_size));
            this.showWin = false;
            this.gridBlocks = new List<Block>();
            update_colors();
            generate_blocks();
            this.draw_game();
        }

        void GameFormMouseUp(object sender, MouseEventArgs e)
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
                        Debug.WriteLine("pripad1");
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
        //}

        private void back_to_menu_Click(object sender, EventArgs e)
        {
            this.Hide();
            mainForm.Show();
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
