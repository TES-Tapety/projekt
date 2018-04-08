using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using static Glass.GlassButton;

namespace Minisoft1
{
	public partial class MainForm : Form
	{		
		SaveLoadManager sm;
		Settings settings;
		Random rnd;
		GameForm gameForm;
        GameFormMode2 mode2_game_form;
        Editor editor;
        int count, max;
        bool showEditor;
		
		public MainForm()
		{
			InitializeComponent();
			DoubleBuffered = true;
			
			sm = new SaveLoadManager();
		    settings = sm.load("basic_settgins.dat");
            rnd = new Random();
            showEditor = false;
            KeyPreview = true;
        }
		
		void MainFormPaint(object sender, PaintEventArgs e)
		{
		    Graphics g = e.Graphics;
            // draw image	
            Image main_image = Image.FromFile("main_picture.png");
            e.Graphics.DrawImage(main_image, 200, 50, 400, 300);

        }
		
		void MainFormShown(object sender, EventArgs e)
		{			
			if (settings != null) 
			{
				NumberOfRows.Value = settings.rows;
				NumberOfCols.Value = settings.cols;
				CellSize.Value = settings.cell_size;
				CountOfBlocks.Value = settings.blockCount;
                count = settings.blockCount;
                game_editor_button.Hide();
                label2.Hide();
            }
                

		}

        private void NumberOfRows_ValueChanged(object sender, EventArgs e)
        {
            this.settings.rows = Convert.ToInt32(NumberOfRows.Value);
        }

        private void NumberOfCols_ValueChanged(object sender, EventArgs e)
        {
            this.settings.cols = Convert.ToInt32(NumberOfCols.Value);
        }

        private void CellSize_ValueChanged(object sender, EventArgs e)
        {
            this.settings.cell_size = Convert.ToInt32(CellSize.Value);
        }

        private void CountOfBlocks_ValueChanged(object sender, EventArgs e)
        {
            count = Convert.ToInt32(CountOfBlocks.Value);
            max = this.settings.rows * this.settings.cols;
            if (count > max)
            {
                count = max;
            }
            this.settings.blockCount = count;
        }

        private void button_first_mode_Click(object sender, EventArgs e)
        {

            count = Convert.ToInt32(CountOfBlocks.Value);
            max = this.settings.rows * this.settings.cols;
            if (count > max)
            {
                count = max;
            }

            settings = new Settings
            {
                rows = Convert.ToInt32(NumberOfRows.Value),
                cols = Convert.ToInt32(NumberOfCols.Value),
                cell_size = Convert.ToInt32(CellSize.Value),
                blockCount = count
            };
            gameForm = new GameForm(settings, this);
            gameForm.ob = gameForm.generate_blocks();

            this.Hide();
            gameForm.Show();
        }

        private void button_second_mode_Click(object sender, EventArgs e)
        {
            mode2_game_form = new GameFormMode2(this);

            this.Hide();
            mode2_game_form.Show();
        }

        private void mode_one_prepared_Click(object sender, EventArgs e)
        {
            count = Convert.ToInt32(CountOfBlocks.Value);
            max = this.settings.rows * this.settings.cols;
            if (count > max)
            {
                count = max;
            }

            GameFormMode1 gameForm_mode1 = new GameFormMode1(this);

            this.Hide();
            gameForm_mode1.Show();
        }

        private void game_editor_button_Click(object sender, EventArgs e)
        {
            editor = new Editor(settings, this);

            this.Hide();
            editor.Show();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                if (showEditor)
                {
                    showEditor = false;
                    game_editor_button.Hide();
                    label2.Hide();
                    Invalidate();
                }
                else
                {
                    showEditor = true;
                    game_editor_button.Show();
                    label2.Show();
                    Invalidate();
                }

            }
        }        
    }
}
