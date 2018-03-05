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
            // GameFormMode2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.back_to_menu);
            this.Controls.Add(this.AnotherGame);
            this.Name = "GameFormMode2";
            this.Text = "GameFormMode2";
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
    }
}