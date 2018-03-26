/*
 * Created by SharpDevelop.
 * User: Martin
 * Date: 8.10.2017
 * Time: 23:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System.Drawing;

namespace Minisoft1
{
	partial class GameForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.AnotherGame = new System.Windows.Forms.Button();
            this.back_to_menu = new System.Windows.Forms.Button();
            this.color_lab1 = new System.Windows.Forms.Label();
            this.color_lab2 = new System.Windows.Forms.Label();
            this.color_lab3 = new System.Windows.Forms.Label();
            this.color_lab4 = new System.Windows.Forms.Label();
            this.color_lab5 = new System.Windows.Forms.Label();
            this.color_lab6 = new System.Windows.Forms.Label();
            this.color_lab7 = new System.Windows.Forms.Label();
            this.color_lab8 = new System.Windows.Forms.Label();
            this.color_lab9 = new System.Windows.Forms.Label();
            this.color_lab10 = new System.Windows.Forms.Label();
            this.text_lab1 = new System.Windows.Forms.Label();
            this.show_final_state = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AnotherGame
            // 
            this.AnotherGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AnotherGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.AnotherGame.Location = new System.Drawing.Point(489, 65);
            this.AnotherGame.Name = "AnotherGame";
            this.AnotherGame.Size = new System.Drawing.Size(83, 23);
            this.AnotherGame.TabIndex = 0;
            this.AnotherGame.Text = "Ďalšia hra";
            this.AnotherGame.UseVisualStyleBackColor = true;
            this.AnotherGame.Click += new System.EventHandler(this.AnotherGame_Click);
            // 
            // back_to_menu
            // 
            this.back_to_menu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.back_to_menu.Location = new System.Drawing.Point(497, 326);
            this.back_to_menu.Name = "back_to_menu";
            this.back_to_menu.Size = new System.Drawing.Size(75, 23);
            this.back_to_menu.TabIndex = 2;
            this.back_to_menu.Text = "Menu";
            this.back_to_menu.UseVisualStyleBackColor = true;
            this.back_to_menu.Click += new System.EventHandler(this.back_to_menu_Click);
            // 
            // color_lab1
            // 
            this.color_lab1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.color_lab1.Location = new System.Drawing.Point(100, 326);
            this.color_lab1.Name = "color_lab1";
            this.color_lab1.Size = new System.Drawing.Size(20, 20);
            this.color_lab1.TabIndex = 3;
            // 
            // color_lab2
            // 
            this.color_lab2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.color_lab2.Location = new System.Drawing.Point(125, 326);
            this.color_lab2.Name = "color_lab2";
            this.color_lab2.Size = new System.Drawing.Size(20, 20);
            this.color_lab2.TabIndex = 4;
            // 
            // color_lab3
            // 
            this.color_lab3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.color_lab3.Location = new System.Drawing.Point(150, 326);
            this.color_lab3.Name = "color_lab3";
            this.color_lab3.Size = new System.Drawing.Size(20, 20);
            this.color_lab3.TabIndex = 5;
            // 
            // color_lab4
            // 
            this.color_lab4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.color_lab4.Location = new System.Drawing.Point(175, 326);
            this.color_lab4.Name = "color_lab4";
            this.color_lab4.Size = new System.Drawing.Size(20, 20);
            this.color_lab4.TabIndex = 6;
            // 
            // color_lab5
            // 
            this.color_lab5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.color_lab5.Location = new System.Drawing.Point(200, 326);
            this.color_lab5.Name = "color_lab5";
            this.color_lab5.Size = new System.Drawing.Size(20, 20);
            this.color_lab5.TabIndex = 6;
            // 
            // color_lab6
            // 
            this.color_lab6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.color_lab6.Location = new System.Drawing.Point(225, 326);
            this.color_lab6.Name = "color_lab6";
            this.color_lab6.Size = new System.Drawing.Size(20, 20);
            this.color_lab6.TabIndex = 6;
            // 
            // color_lab7
            // 
            this.color_lab7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.color_lab7.Location = new System.Drawing.Point(250, 326);
            this.color_lab7.Name = "color_lab7";
            this.color_lab7.Size = new System.Drawing.Size(20, 20);
            this.color_lab7.TabIndex = 6;
            // 
            // color_lab8
            // 
            this.color_lab8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.color_lab8.Location = new System.Drawing.Point(250, 326);
            this.color_lab8.Name = "color_lab8";
            this.color_lab8.Size = new System.Drawing.Size(20, 20);
            this.color_lab8.TabIndex = 6;
            // 
            // color_lab9
            // 
            this.color_lab9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.color_lab9.Location = new System.Drawing.Point(250, 326);
            this.color_lab9.Name = "color_lab9";
            this.color_lab9.Size = new System.Drawing.Size(20, 20);
            this.color_lab9.TabIndex = 6;
            // 
            // color_lab10
            // 
            this.color_lab10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.color_lab10.Location = new System.Drawing.Point(250, 326);
            this.color_lab10.Name = "color_lab10";
            this.color_lab10.Size = new System.Drawing.Size(20, 20);
            this.color_lab10.TabIndex = 6;
            // 
            // text_lab1
            // 
            this.text_lab1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.text_lab1.Location = new System.Drawing.Point(10, 326);
            this.text_lab1.Name = "text_lab1";
            this.text_lab1.Size = new System.Drawing.Size(109, 23);
            this.text_lab1.TabIndex = 3;
            this.text_lab1.Text = "Postupnosť farieb";
            // 
            // show_final_state
            // 
            this.show_final_state.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.show_final_state.Location = new System.Drawing.Point(476, 297);
            this.show_final_state.Name = "show_final_state";
            this.show_final_state.Size = new System.Drawing.Size(96, 23);
            this.show_final_state.TabIndex = 14;
            this.show_final_state.Text = "Ukáž riešenie";
            this.show_final_state.UseVisualStyleBackColor = true;
            this.show_final_state.Click += new System.EventHandler(this.show_final_state_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.show_final_state);
            this.Controls.Add(this.back_to_menu);
            this.Controls.Add(this.color_lab1);
            this.Controls.Add(this.color_lab2);
            this.Controls.Add(this.color_lab3);
            this.Controls.Add(this.color_lab4);
            this.Controls.Add(this.color_lab5);
            this.Controls.Add(this.color_lab6);
            this.Controls.Add(this.color_lab7);
            this.Controls.Add(this.color_lab8);
            this.Controls.Add(this.color_lab9);
            this.Controls.Add(this.color_lab10);
            this.Controls.Add(this.AnotherGame);
            this.Controls.Add(this.text_lab1);
            this.Name = "GameForm";
            this.Text = "Tapety";
            this.Shown += new System.EventHandler(this.GameFormShown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameFormPaint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GameFormMouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GameFormMouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GameFormMouseUp);
            this.Resize += new System.EventHandler(this.GameForm_Resize);
            this.ResumeLayout(false);

		}

        private System.Windows.Forms.Button AnotherGame;
        private System.Windows.Forms.Button back_to_menu;
		private System.Windows.Forms.Label color_lab1;
		private System.Windows.Forms.Label color_lab2;
		private System.Windows.Forms.Label color_lab3;
		private System.Windows.Forms.Label color_lab4;
		private System.Windows.Forms.Label color_lab5;
		private System.Windows.Forms.Label color_lab6;
		private System.Windows.Forms.Label color_lab7;
		private System.Windows.Forms.Label color_lab8;
		private System.Windows.Forms.Label color_lab9;
		private System.Windows.Forms.Label color_lab10;
		private System.Windows.Forms.Label text_lab1;
        private System.Windows.Forms.Button show_final_state;
    }
}
