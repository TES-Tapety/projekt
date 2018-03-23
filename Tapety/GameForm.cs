using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace Minisoft1
{
	public partial class GameForm : Form
	{
		Settings settings;
		Block[] blocks;
		Block selected;
		bool clicked, showWin;
		Random rnd;
        int[,] playground;
        MainForm mainForm;


        const int INDENT_X = 0;
		const int INDENT_Y = 0;

        int deltaX, deltaY;
		
		public GameForm(Settings settings, MainForm mainForm)
		{	
			DoubleBuffered = true;
			InitializeComponent();
			
			this.settings = settings;
			this.clicked = false;
            this.showWin = false;
			this.rnd = new Random();
            this.playground = new int[this.settings.rows, this.settings.cols];
            this.mainForm = mainForm;
				
		}

        void GameFormShown(object sender, EventArgs e)
        {
            this.Size = new Size((settings.rows*settings.cell_size), (settings.cols * settings.cell_size));
            this.draw_game();
        }

        public struct obdlznik
        {
            public int x, y;
        }

        public obdlznik[] rozdel(int pocet_casti, obdlznik[] obdl)
        {        // funkcia
            bool spravne = false;
            bool jednotkovahrana = false;
            int r, s, t;                                                //nahodne premenne
            Random rnd = new Random();                                //vytvorenie random generatoru 
            for (int i = 0; i < pocet_casti - 1; i++)
            {                   //ideme delit v kazdej iteracii cyklu 1 nahodnu cast na 2 mensie
                while (!spravne)
                {                                        //nie kazdu malu cast vieme rozdelit(taku co ma velkost jedna uz nerozdelime)
                                                         //preto ideme dovtedy vo while cykle, az kym nerozdelime na 2 spravne casti o velkosti apson 1
                    r = rnd.Next(i + 1);                            //nahodne vyberieme, ze ktory utvar ideme delit
                    s = rnd.Next(2);                              //nahodne vyberieme, ci ho rozrezeme na vysku alebo na sirku                                     
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
                                    spravne = true;
                                }
                            }
                        }
                    }
                }
                spravne = false;                                //opat vratime na povodnu hodnotu, aby nam v dalsej iteracii for cyklu bezal aj while cyklus
            }
            return obdl;      //ked skonci for, vraciame vysledne pole v ktorom na kazdom policku je objekt s vyskou a sirkou
        }

        //public obdlznik[] rozdel(int pocet_casti, obdlznik[] obdl)
        //{        // funkcia
        //    bool spravne = false;
        //    int r, s, t;                                                //nahodne premenne
        //    Random rnd = new Random();                                //vytvorenie random generatoru 
        //    for (int i = 0; i < pocet_casti - 1; i++)
        //    {                   //ideme delit v kazdej iteracii cyklu 1 nahodnu cast na 2 mensie
        //        while (!spravne)
        //        {                                        //nie kazdu malu cast vieme rozdelit(taku co ma velkost jedna uz nerozdelime)
        //                                                    //preto ideme dovtedy vo while cykle, az kym nerozdelime na 2 spravne casti o velkosti apson 1
        //            r = rnd.Next(i + 1);                            //nahodne vyberieme, ze ktory utvar ideme delit
        //            s = rnd.Next(2);                              //nahodne vyberieme, ci ho rozrezeme na vysku alebo na sirku
        //            if (s == 0)
        //            {                                  //ak 0, tak ideme rezat na sirku
        //                if (obdl[r].x > 1)
        //                {                          //ak mozme rezat, teda ak ma sirku aspon 2, inak znova prejde while cyklus a znovu sa vygeneruju nahodne premenne
        //                    t = rnd.Next(1, obdl[r].x);               //nahodne miesto kde ho rozdelime
        //                    obdl[i + 1].x = obdl[r].x - t;              //vypocitame a priradime sirku noveho
        //                    obdl[i + 1].y = obdl[r].y;               //priradime rovnaku vysku aku mal stary aj novemu
        //                    obdl[r].x = t;                            //stary skratime o velkost noveho
        //                    spravne = true;                           //nastalo spravne rozdelenie a tym padom uz bool spravne bude true, cize skonci while cyklus
        //                }
        //            }
        //            else
        //            {
        //                if (obdl[r].y > 1)
        //                {                          //ak nie nula, teda inak, ideme rezat na vysku
        //                    t = rnd.Next(1, obdl[r].y);               //to iste ako for nad nami len nerezeme na sirku ale na vysku
        //                    obdl[i + 1].y = obdl[r].y - t;
        //                    obdl[i + 1].x = obdl[r].x;
        //                    obdl[r].y = t;
        //                    spravne = true;
        //                }
        //            }
        //        }
        //        spravne = false;                                //opat vratime na povodnu hodnotu, aby nam v dalsej iteracii for cyklu bezal aj while cyklus
        //    }
        //    return obdl;      //ked skonci for, vraciame vysledne pole v ktorom na kazdom policku je objekt s vyskou a sirkou
        //}

        void draw_game()
        {
            Invalidate();
            this.AnotherGame.Hide();
            this.blocks = new Block[this.settings.blockCount];
            obdlznik[] ob = new obdlznik[this.settings.blockCount];

            ob[0].x = this.settings.cols;
            ob[0].y = this.settings.rows;
            ob = rozdel(this.settings.blockCount, ob);

            // sort by the ob.x and ob.y
            Array.Sort(ob, delegate (obdlznik o1, obdlznik o2) {

                int xdiff = o1.x.CompareTo(o2.x);

                if (xdiff != 0)
                    return xdiff;

                else return o1.y.CompareTo(o2.y);
            });

            int ix = 0;
            int gap = 5;
            int px = (this.settings.cols * this.settings.cell_size) + gap;
            int py = (this.settings.rows * this.settings.cell_size) + gap;

            int posunX_vedla = gap;
            int posunX_dole = gap;

            int posunY_dole_max = 0;
                    
            int x, y;

            foreach (obdlznik obdl in ob)
            {
                // TODO: rozumny generator farieb - mozno nastavit BG color na bielu a spavit mnozinu farieb a to len pridelit Bloku
                // TODO: GameForm - dalsia hra pomocou nejakeho obrazku pre deti

                Color color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                int W = obdl.x; 
                int H = obdl.y;
                int width = this.settings.cell_size * W;
                int height = this.settings.cell_size * H;

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
                    if ((gap+ width + posunX_dole) > this.ClientRectangle.Width)
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

                Block block = new Block(ix+1, x, y, W, H, this.settings.cell_size, color);
                blocks[ix] = block;
                ix += 1;
            }

            // resize WIDTH if needed
            if ((px + posunX_vedla + this.settings.cell_size) > ClientRectangle.Width)
            {
                this.Size = new Size((px + posunX_vedla + gap + (2*this.settings.cell_size)), ClientRectangle.Height);
            }


            // resize HEIGHT if needed
            if ((py + this.settings.cell_size + posunY_dole_max) > ClientRectangle.Height)
            {
                this.Size = new Size(ClientRectangle.Width, (2*this.settings.cell_size + py + posunY_dole_max));
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

        private void AnotherGame_Click(object sender, EventArgs e)
        {
            this.Size = new Size((settings.rows * settings.cell_size), (settings.cols * settings.cell_size));
            this.showWin = false;
            this.draw_game();
        }

        void GameFormMouseUp(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left)
            //{

                if (selected != null)
                {
                    clicked = false;

                    if ((selected.x >= 0 && selected.x < this.settings.cols * this.settings.cell_size) &&
                        (selected.y >= 0 && selected.y < this.settings.rows * this.settings.cell_size))
                    {
                        // doskakovanie
                        int dx = selected.x % this.settings.cell_size;
                        int dy = selected.y % this.settings.cell_size;
                        int half_size = settings.cell_size / 2;

                        // dole v pravo
                        if (dx > half_size && dy > half_size)
                        {
                            selected.x += settings.cell_size - dx;
                            selected.y += settings.cell_size - dy;
                        }
                        // hore v pravo
                        else if (dx > half_size && dy <= half_size)
                        {
                            selected.x += settings.cell_size - dx;
                        }
                        // dole v lavo
                        else if (dx <= half_size && dy > half_size)
                        {
                            selected.y += settings.cell_size - dy;
                        }

                        // konecny prepocet doskakovania
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
                            if (return_to_start == false)
                            {
                                update_colors(selected.color);
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
        //}

        private void back_to_menu_Click(object sender, EventArgs e)
        {
            this.Hide();
            mainForm.Show();
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
