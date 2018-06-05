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
            this.show_final_state = new Glass.GlassButton();
            this.back_to_menu = new Glass.GlassButton();
            this.AnotherGame = new Glass.GlassButton();
            this.SuspendLayout();
            // 
            // show_final_state
            // 
            this.show_final_state.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.show_final_state.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.show_final_state.ForeColor = System.Drawing.Color.MidnightBlue;
            this.show_final_state.GlowColor = System.Drawing.Color.CornflowerBlue;
            this.show_final_state.InnerBorderColor = System.Drawing.Color.DeepSkyBlue;
            this.show_final_state.Location = new System.Drawing.Point(476, 297);
            this.show_final_state.Name = "show_final_state";
            this.show_final_state.OuterBorderColor = System.Drawing.Color.DeepSkyBlue;
            this.show_final_state.Size = new System.Drawing.Size(96, 23);
            this.show_final_state.TabIndex = 14;
            this.show_final_state.Text = "Ukáž riešenie";
            this.show_final_state.Click += new System.EventHandler(this.show_final_state_Click);
            // 
            // back_to_menu
            // 
            this.back_to_menu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.back_to_menu.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.back_to_menu.ForeColor = System.Drawing.Color.MidnightBlue;
            this.back_to_menu.GlowColor = System.Drawing.Color.CornflowerBlue;
            this.back_to_menu.InnerBorderColor = System.Drawing.Color.DeepSkyBlue;
            this.back_to_menu.Location = new System.Drawing.Point(497, 326);
            this.back_to_menu.Name = "back_to_menu";
            this.back_to_menu.OuterBorderColor = System.Drawing.Color.DeepSkyBlue;
            this.back_to_menu.Size = new System.Drawing.Size(75, 23);
            this.back_to_menu.TabIndex = 2;
            this.back_to_menu.Text = "Menu";
            this.back_to_menu.Click += new System.EventHandler(this.back_to_menu_Click);
            // 
            // AnotherGame
            // 
            this.AnotherGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AnotherGame.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.AnotherGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.AnotherGame.ForeColor = System.Drawing.Color.MidnightBlue;
            this.AnotherGame.GlowColor = System.Drawing.Color.CornflowerBlue;
            this.AnotherGame.InnerBorderColor = System.Drawing.Color.DeepSkyBlue;
            this.AnotherGame.Location = new System.Drawing.Point(489, 65);
            this.AnotherGame.Name = "AnotherGame";
            this.AnotherGame.OuterBorderColor = System.Drawing.Color.DeepSkyBlue;
            this.AnotherGame.Size = new System.Drawing.Size(83, 23);
            this.AnotherGame.TabIndex = 0;
            this.AnotherGame.Text = "Ďalšia hra";
            this.AnotherGame.Click += new System.EventHandler(this.AnotherGame_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.show_final_state);
            this.Controls.Add(this.back_to_menu);
            this.Controls.Add(this.AnotherGame);
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

        private Glass.GlassButton AnotherGame;
        private Glass.GlassButton back_to_menu;
        private Glass.GlassButton show_final_state;
    }
}
