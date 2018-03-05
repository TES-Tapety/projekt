namespace Minisoft1
{
    partial class Editor
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
            this.CellSize_editor = new System.Windows.Forms.NumericUpDown();
            this.NumberOfCols = new System.Windows.Forms.NumericUpDown();
            this.NumberOfRows = new System.Windows.Forms.NumericUpDown();
            this.CellLabel = new System.Windows.Forms.Label();
            this.ColsLabel = new System.Windows.Forms.Label();
            this.RowsLabel = new System.Windows.Forms.Label();
            this.pick_color = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.block_height = new System.Windows.Forms.NumericUpDown();
            this.block_width = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.create_block = new System.Windows.Forms.Button();
            this.save_2_playground = new System.Windows.Forms.Button();
            this.save_first_mode = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.save_2_blocks = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.delete_block = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CellSize_editor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfCols)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.block_height)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.block_width)).BeginInit();
            this.SuspendLayout();
            // 
            // back_to_menu
            // 
            this.back_to_menu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.back_to_menu.Location = new System.Drawing.Point(497, 376);
            this.back_to_menu.Name = "back_to_menu";
            this.back_to_menu.Size = new System.Drawing.Size(75, 23);
            this.back_to_menu.TabIndex = 0;
            this.back_to_menu.Text = "Menu";
            this.back_to_menu.UseVisualStyleBackColor = true;
            this.back_to_menu.Click += new System.EventHandler(this.back_to_menu_Click);
            // 
            // CellSize_editor
            // 
            this.CellSize_editor.Location = new System.Drawing.Point(100, 81);
            this.CellSize_editor.Name = "CellSize_editor";
            this.CellSize_editor.Size = new System.Drawing.Size(48, 20);
            this.CellSize_editor.TabIndex = 20;
            this.CellSize_editor.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.CellSize_editor.ValueChanged += new System.EventHandler(this.CellSize_editor_ValueChanged);
            // 
            // NumberOfCols
            // 
            this.NumberOfCols.Location = new System.Drawing.Point(100, 57);
            this.NumberOfCols.Name = "NumberOfCols";
            this.NumberOfCols.Size = new System.Drawing.Size(48, 20);
            this.NumberOfCols.TabIndex = 19;
            this.NumberOfCols.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.NumberOfCols.ValueChanged += new System.EventHandler(this.NumberOfCols_ValueChanged);
            // 
            // NumberOfRows
            // 
            this.NumberOfRows.Location = new System.Drawing.Point(100, 33);
            this.NumberOfRows.Name = "NumberOfRows";
            this.NumberOfRows.Size = new System.Drawing.Size(48, 20);
            this.NumberOfRows.TabIndex = 18;
            this.NumberOfRows.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.NumberOfRows.ValueChanged += new System.EventHandler(this.NumberOfRows_ValueChanged);
            // 
            // CellLabel
            // 
            this.CellLabel.Location = new System.Drawing.Point(12, 81);
            this.CellLabel.Name = "CellLabel";
            this.CellLabel.Size = new System.Drawing.Size(88, 16);
            this.CellLabel.TabIndex = 16;
            this.CellLabel.Text = "Veľkosť štvorca:";
            // 
            // ColsLabel
            // 
            this.ColsLabel.Location = new System.Drawing.Point(12, 57);
            this.ColsLabel.Name = "ColsLabel";
            this.ColsLabel.Size = new System.Drawing.Size(88, 16);
            this.ColsLabel.TabIndex = 15;
            this.ColsLabel.Text = "Počet stĺpcov:";
            // 
            // RowsLabel
            // 
            this.RowsLabel.Location = new System.Drawing.Point(12, 33);
            this.RowsLabel.Name = "RowsLabel";
            this.RowsLabel.Size = new System.Drawing.Size(88, 16);
            this.RowsLabel.TabIndex = 14;
            this.RowsLabel.Text = "Počet riadkov:";
            // 
            // pick_color
            // 
            this.pick_color.Location = new System.Drawing.Point(12, 194);
            this.pick_color.Name = "pick_color";
            this.pick_color.Size = new System.Drawing.Size(88, 23);
            this.pick_color.TabIndex = 21;
            this.pick_color.Text = "Vyber farbu";
            this.pick_color.UseVisualStyleBackColor = true;
            this.pick_color.Click += new System.EventHandler(this.pick_color_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.textBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox1.Location = new System.Drawing.Point(123, 196);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(25, 23);
            this.textBox1.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Vytvorenie Tapety";
            // 
            // block_height
            // 
            this.block_height.Location = new System.Drawing.Point(100, 165);
            this.block_height.Name = "block_height";
            this.block_height.Size = new System.Drawing.Size(48, 20);
            this.block_height.TabIndex = 27;
            this.block_height.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // block_width
            // 
            this.block_width.Location = new System.Drawing.Point(100, 141);
            this.block_width.Name = "block_width";
            this.block_width.Size = new System.Drawing.Size(48, 20);
            this.block_width.TabIndex = 26;
            this.block_width.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 25;
            this.label2.Text = "Vyska: ";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 24;
            this.label3.Text = "Sirka:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Vytvorenie Mriezky";
            // 
            // create_block
            // 
            this.create_block.Location = new System.Drawing.Point(12, 225);
            this.create_block.Name = "create_block";
            this.create_block.Size = new System.Drawing.Size(133, 23);
            this.create_block.TabIndex = 29;
            this.create_block.Text = "Vytvor Tapetu";
            this.create_block.UseVisualStyleBackColor = true;
            this.create_block.Click += new System.EventHandler(this.create_block_Click);
            // 
            // save_2_playground
            // 
            this.save_2_playground.Location = new System.Drawing.Point(12, 347);
            this.save_2_playground.Name = "save_2_playground";
            this.save_2_playground.Size = new System.Drawing.Size(133, 23);
            this.save_2_playground.TabIndex = 30;
            this.save_2_playground.Text = "Ulož riešenie";
            this.save_2_playground.UseVisualStyleBackColor = true;
            this.save_2_playground.Click += new System.EventHandler(this.save_2_playground_Click);
            // 
            // save_first_mode
            // 
            this.save_first_mode.Location = new System.Drawing.Point(12, 300);
            this.save_first_mode.Name = "save_first_mode";
            this.save_first_mode.Size = new System.Drawing.Size(133, 23);
            this.save_first_mode.TabIndex = 31;
            this.save_first_mode.Text = "Ulož hru";
            this.save_first_mode.UseVisualStyleBackColor = true;
            this.save_first_mode.Click += new System.EventHandler(this.save_first_mode_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 284);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Ukladanie 1. mód";
            // 
            // save_2_blocks
            // 
            this.save_2_blocks.Enabled = false;
            this.save_2_blocks.Location = new System.Drawing.Point(12, 376);
            this.save_2_blocks.Name = "save_2_blocks";
            this.save_2_blocks.Size = new System.Drawing.Size(133, 23);
            this.save_2_blocks.TabIndex = 33;
            this.save_2_blocks.Text = "Ulož tapety";
            this.save_2_blocks.UseVisualStyleBackColor = true;
            this.save_2_blocks.Click += new System.EventHandler(this.save_2_blocks_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 331);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 34;
            this.label6.Text = "Ukladanie 2. mód";
            // 
            // delete_block
            // 
            this.delete_block.Location = new System.Drawing.Point(12, 250);
            this.delete_block.Name = "delete_block";
            this.delete_block.Size = new System.Drawing.Size(133, 23);
            this.delete_block.TabIndex = 35;
            this.delete_block.Text = "Začni Mazanie";
            this.delete_block.UseVisualStyleBackColor = true;
            this.delete_block.Click += new System.EventHandler(this.delete_block_Click);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 411);
            this.Controls.Add(this.delete_block);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.save_2_blocks);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.save_first_mode);
            this.Controls.Add(this.save_2_playground);
            this.Controls.Add(this.create_block);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.block_height);
            this.Controls.Add(this.block_width);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pick_color);
            this.Controls.Add(this.CellSize_editor);
            this.Controls.Add(this.NumberOfCols);
            this.Controls.Add(this.NumberOfRows);
            this.Controls.Add(this.CellLabel);
            this.Controls.Add(this.ColsLabel);
            this.Controls.Add(this.RowsLabel);
            this.Controls.Add(this.back_to_menu);
            this.Name = "Editor";
            this.Text = "Editor";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Editor_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Editor_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Editor_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Editor_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.CellSize_editor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfCols)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.block_height)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.block_width)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button back_to_menu;
        private System.Windows.Forms.NumericUpDown CellSize_editor;
        private System.Windows.Forms.NumericUpDown NumberOfCols;
        private System.Windows.Forms.NumericUpDown NumberOfRows;
        private System.Windows.Forms.Label CellLabel;
        private System.Windows.Forms.Label ColsLabel;
        private System.Windows.Forms.Label RowsLabel;
        private System.Windows.Forms.Button pick_color;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown block_height;
        private System.Windows.Forms.NumericUpDown block_width;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button create_block;
        private System.Windows.Forms.Button save_2_playground;
        private System.Windows.Forms.Button save_first_mode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button save_2_blocks;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button delete_block;
    }
}