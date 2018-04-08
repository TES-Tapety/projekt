/*
 * Created by SharpDevelop.
 * User: Martin
 * Date: 7.10.2017
 * Time: 15:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using Glass;

namespace Minisoft1
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label TapetyLabel;
		private System.Windows.Forms.Label ColsLabel;
		private System.Windows.Forms.Label CellLabel;
		private Glass.GlassButton button_first_mode;
		private System.Windows.Forms.Label BlockLabel;
		private System.Windows.Forms.NumericUpDown NumberOfRows;
		private System.Windows.Forms.NumericUpDown NumberOfCols;
		private System.Windows.Forms.NumericUpDown CellSize;
		private System.Windows.Forms.NumericUpDown CountOfBlocks;
		private System.Windows.Forms.Label RowsLabel;
		
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
            this.TapetyLabel = new System.Windows.Forms.Label();
            this.RowsLabel = new System.Windows.Forms.Label();
            this.ColsLabel = new System.Windows.Forms.Label();
            this.CellLabel = new System.Windows.Forms.Label();
            this.button_first_mode = new Glass.GlassButton();
            this.BlockLabel = new System.Windows.Forms.Label();
            this.NumberOfRows = new System.Windows.Forms.NumericUpDown();
            this.NumberOfCols = new System.Windows.Forms.NumericUpDown();
            this.CellSize = new System.Windows.Forms.NumericUpDown();
            this.CountOfBlocks = new System.Windows.Forms.NumericUpDown();
            this.button_second_mode = new Glass.GlassButton();
            this.game_editor_button = new Glass.GlassButton();
            this.mode_one_prepared = new Glass.GlassButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfCols)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CellSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CountOfBlocks)).BeginInit();
            this.SuspendLayout();
            // 
            // TapetyLabel
            // 
            this.TapetyLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TapetyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.TapetyLabel.Location = new System.Drawing.Point(8, 8);
            this.TapetyLabel.Name = "TapetyLabel";
            this.TapetyLabel.Size = new System.Drawing.Size(300, 24);
            this.TapetyLabel.TabIndex = 0;
            this.TapetyLabel.Text = "Nastavenia pre prvý Mód - náhodne";
            // 
            // RowsLabel
            // 
            this.RowsLabel.Location = new System.Drawing.Point(16, 40);
            this.RowsLabel.Name = "RowsLabel";
            this.RowsLabel.Size = new System.Drawing.Size(88, 16);
            this.RowsLabel.TabIndex = 1;
            this.RowsLabel.Text = "Počet riadkov:";
            // 
            // ColsLabel
            // 
            this.ColsLabel.Location = new System.Drawing.Point(16, 64);
            this.ColsLabel.Name = "ColsLabel";
            this.ColsLabel.Size = new System.Drawing.Size(88, 16);
            this.ColsLabel.TabIndex = 2;
            this.ColsLabel.Text = "Počet stĺpcov:";
            // 
            // CellLabel
            // 
            this.CellLabel.Location = new System.Drawing.Point(16, 88);
            this.CellLabel.Name = "CellLabel";
            this.CellLabel.Size = new System.Drawing.Size(88, 16);
            this.CellLabel.TabIndex = 3;
            this.CellLabel.Text = "Veľkosť štvorca:";
            // 
            // button_first_mode
            // 
            this.button_first_mode.Location = new System.Drawing.Point(12, 138);
            this.button_first_mode.Name = "button_first_mode";
            this.button_first_mode.Size = new System.Drawing.Size(135, 33);
            this.button_first_mode.TabIndex = 4;
            this.button_first_mode.Text = "Hrať prvý mód";
            this.button_first_mode.UseVisualStyleBackColor = true;
            this.button_first_mode.Click += new System.EventHandler(this.button_first_mode_Click);
            // 
            // BlockLabel
            // 
            this.BlockLabel.Location = new System.Drawing.Point(16, 112);
            this.BlockLabel.Name = "BlockLabel";
            this.BlockLabel.Size = new System.Drawing.Size(88, 16);
            this.BlockLabel.TabIndex = 5;
            this.BlockLabel.Text = "Počet tapiet:";
            // 
            // NumberOfRows
            // 
            this.NumberOfRows.Location = new System.Drawing.Point(104, 40);
            this.NumberOfRows.Maximum = new decimal(new int[] {
            14,
            0,
            0,
            0});
            this.NumberOfRows.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.NumberOfRows.Name = "NumberOfRows";
            this.NumberOfRows.Size = new System.Drawing.Size(48, 20);
            this.NumberOfRows.TabIndex = 10;
            this.NumberOfRows.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.NumberOfRows.ValueChanged += new System.EventHandler(this.NumberOfRows_ValueChanged);
            // 
            // NumberOfCols
            // 
            this.NumberOfCols.Location = new System.Drawing.Point(104, 64);
            this.NumberOfCols.Maximum = new decimal(new int[] {
            14,
            0,
            0,
            0});
            this.NumberOfCols.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.NumberOfCols.Name = "NumberOfCols";
            this.NumberOfCols.Size = new System.Drawing.Size(48, 20);
            this.NumberOfCols.TabIndex = 11;
            this.NumberOfCols.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.NumberOfCols.ValueChanged += new System.EventHandler(this.NumberOfCols_ValueChanged);
            // 
            // CellSize
            // 
            this.CellSize.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.CellSize.Location = new System.Drawing.Point(104, 88);
            this.CellSize.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.CellSize.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.CellSize.Name = "CellSize";
            this.CellSize.Size = new System.Drawing.Size(48, 20);
            this.CellSize.TabIndex = 12;
            this.CellSize.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.CellSize.ValueChanged += new System.EventHandler(this.CellSize_ValueChanged);
            // 
            // CountOfBlocks
            // 
            this.CountOfBlocks.Location = new System.Drawing.Point(104, 112);
            this.CountOfBlocks.Name = "CountOfBlocks";
            this.CountOfBlocks.Size = new System.Drawing.Size(48, 20);
            this.CountOfBlocks.TabIndex = 13;
            this.CountOfBlocks.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // button_second_mode
            // 
            this.button_second_mode.Location = new System.Drawing.Point(12, 252);
            this.button_second_mode.Name = "button_second_mode";
            this.button_second_mode.Size = new System.Drawing.Size(135, 33);
            this.button_second_mode.TabIndex = 14;
            this.button_second_mode.Text = "Hrať druhý mód";
            this.button_second_mode.UseVisualStyleBackColor = true;
            this.button_second_mode.Click += new System.EventHandler(this.button_second_mode_Click);
            // 
            // game_editor_button
            // 
            this.game_editor_button.Location = new System.Drawing.Point(12, 316);
            this.game_editor_button.Name = "game_editor_button";
            this.game_editor_button.Size = new System.Drawing.Size(135, 33);
            this.game_editor_button.TabIndex = 15;
            this.game_editor_button.Text = "Editor";
            this.game_editor_button.UseVisualStyleBackColor = true;
            this.game_editor_button.Click += new System.EventHandler(this.game_editor_button_Click);
            // 
            // mode_one_prepared
            // 
            this.mode_one_prepared.Location = new System.Drawing.Point(12, 214);
            this.mode_one_prepared.Name = "mode_one_prepared";
            this.mode_one_prepared.Size = new System.Drawing.Size(135, 33);
            this.mode_one_prepared.TabIndex = 16;
            this.mode_one_prepared.Text = "Hrať prvý mód";
            this.mode_one_prepared.UseVisualStyleBackColor = true;
            this.mode_one_prepared.Click += new System.EventHandler(this.mode_one_prepared_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.label1.Location = new System.Drawing.Point(12, 178);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 24);
            this.label1.TabIndex = 17;
            this.label1.Text = "Pripravene levely";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.label2.Location = new System.Drawing.Point(12, 284);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 24);
            this.label2.TabIndex = 18;
            this.label2.Text = "Editor";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 359);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mode_one_prepared);
            this.Controls.Add(this.game_editor_button);
            this.Controls.Add(this.button_second_mode);
            this.Controls.Add(this.CountOfBlocks);
            this.Controls.Add(this.CellSize);
            this.Controls.Add(this.NumberOfCols);
            this.Controls.Add(this.NumberOfRows);
            this.Controls.Add(this.BlockLabel);
            this.Controls.Add(this.button_first_mode);
            this.Controls.Add(this.CellLabel);
            this.Controls.Add(this.ColsLabel);
            this.Controls.Add(this.RowsLabel);
            this.Controls.Add(this.TapetyLabel);
            this.Name = "MainForm";
            this.Text = "Tapety";
            this.Shown += new System.EventHandler(this.MainFormShown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainFormPaint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfCols)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CellSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CountOfBlocks)).EndInit();
            this.ResumeLayout(false);

		}

        private System.Windows.Forms.Button button_second_mode;
        private System.Windows.Forms.Button game_editor_button;
        private System.Windows.Forms.Button mode_one_prepared;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
