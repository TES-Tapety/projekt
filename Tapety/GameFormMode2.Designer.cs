﻿using System.Drawing;

namespace Minisoft1
{
    partial class GameFormMode2
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
            this.back_to_menu = new System.Windows.Forms.Button();
            this.AnotherGame = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.color_lab1= new System.Windows.Forms.Label();
            this.color_lab2= new System.Windows.Forms.Label();
            this.color_lab3= new System.Windows.Forms.Label();
            this.color_lab4= new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // back_to_menu
            // 
            this.back_to_menu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.back_to_menu.Location = new System.Drawing.Point(497, 329);
            this.back_to_menu.Name = "back_to_menu";
            this.back_to_menu.Size = new System.Drawing.Size(75, 23);
            this.back_to_menu.TabIndex = 5;
            this.back_to_menu.Text = "Menu";
            this.back_to_menu.UseVisualStyleBackColor = true;
            this.back_to_menu.Click += new System.EventHandler(this.back_to_menu_Click);
            // 
            // AnotherGame
            // 
            this.AnotherGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AnotherGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.AnotherGame.Location = new System.Drawing.Point(489, 65);
            this.AnotherGame.Name = "AnotherGame";
            this.AnotherGame.Size = new System.Drawing.Size(83, 23);
            this.AnotherGame.TabIndex = 3;
            this.AnotherGame.Text = "Ďalšia hra";
            this.AnotherGame.UseVisualStyleBackColor = true;
            this.AnotherGame.Click += new System.EventHandler(this.AnotherGame_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.label1.Location = new System.Drawing.Point(497, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 22);
            this.label1.TabIndex = 6;
            // 
            // color_labels
            // 
            this.color_lab1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.color_lab1.Location = new System.Drawing.Point(450, 313);
            this.color_lab1.Name = "color_lab1";
            this.color_lab1.Size = new System.Drawing.Size(20, 20);
            this.color_lab1.BackColor = Color.Aqua;
            this.color_lab2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.color_lab2.Location = new System.Drawing.Point(425, 313);
            this.color_lab2.Name = "color_lab2";
            this.color_lab2.Size = new System.Drawing.Size(20, 20);
            this.color_lab2.BackColor = Color.Aqua;
            this.color_lab3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.color_lab3.Location = new System.Drawing.Point(400, 313);
            this.color_lab3.Name = "color_lab3";
            this.color_lab3.Size = new System.Drawing.Size(20, 20);
            this.color_lab3.BackColor = Color.Aqua;
            this.color_lab4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.color_lab4.Location = new System.Drawing.Point(375, 313);
            this.color_lab4.Name = "color_lab4";
            this.color_lab4.Size = new System.Drawing.Size(20, 20);
            this.color_lab4.BackColor = Color.Aqua;
            // 
            // GameFormMode2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.back_to_menu);
            this.Controls.Add(this.color_lab1);
            this.Controls.Add(this.color_lab2);
            this.Controls.Add(this.color_lab3);
            this.Controls.Add(this.color_lab4);
            this.Controls.Add(this.AnotherGame);
            this.Name = "GameFormMode2";
            this.Text = "Tapety";
            this.Shown += new System.EventHandler(this.GameFormMode2_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameFormMode2_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GameFormMode2_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GameFormMode2_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GameFormMode2_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button back_to_menu;
        private System.Windows.Forms.Button AnotherGame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label color_lab1;
        private System.Windows.Forms.Label color_lab2;
        private System.Windows.Forms.Label color_lab3;
        private System.Windows.Forms.Label color_lab4;
    }
}