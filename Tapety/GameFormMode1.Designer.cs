﻿using System.Drawing;

namespace Minisoft1
{
    partial class GameFormMode1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AnotherGame = new System.Windows.Forms.Button();
            this.back_to_menu = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.show_final_state = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AnotherGame
            // 
            this.AnotherGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AnotherGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.AnotherGame.Location = new System.Drawing.Point(491, 65);
            this.AnotherGame.Name = "AnotherGame";
            this.AnotherGame.Size = new System.Drawing.Size(81, 23);
            this.AnotherGame.TabIndex = 5;
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
            this.back_to_menu.TabIndex = 7;
            this.back_to_menu.Text = "Menu";
            this.back_to_menu.UseVisualStyleBackColor = true;
            this.back_to_menu.Click += new System.EventHandler(this.back_to_menu_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label1.Location = new System.Drawing.Point(493, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 8;
            // 
            // show_final_state
            // 
            this.show_final_state.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.show_final_state.Location = new System.Drawing.Point(476, 297);
            this.show_final_state.Name = "show_final_state";
            this.show_final_state.Size = new System.Drawing.Size(96, 23);
            this.show_final_state.TabIndex = 13;
            this.show_final_state.Text = "Ukáž riešenie";
            this.show_final_state.UseVisualStyleBackColor = true;
            this.show_final_state.Click += new System.EventHandler(this.show_final_state_Click);
            // 
            // GameFormMode1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.show_final_state);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.back_to_menu);
            this.Controls.Add(this.AnotherGame);
            this.Name = "GameFormMode1";
            this.Text = "Tapety";
            this.Shown += new System.EventHandler(this.GameFormMode1_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameFormMode1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GameFormMode1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GameFormMode1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GameFormMode1_MouseUp);
            this.Resize += new System.EventHandler(this.GameFormMode1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button AnotherGame;
        private System.Windows.Forms.Button back_to_menu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button show_final_state;
    }
}