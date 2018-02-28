using System;
using System.Drawing;

namespace Minisoft1
{
    [Serializable]
    public class Block
    {
        public int id, x, y, startX, startY, width, height, W, H, cell_size;
        public Color color;

        public Block(int id, int x, int y, int W, int H, int cell_size, Color color)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            this.startX = x;
            this.startY = y;
            this.color = color;
            this.W = W;
            this.H = H;
            this.cell_size = cell_size;
            this.width = cell_size * W;
            this.height = cell_size * H;
        }

        public void recalculate_shape(int new_size)
        {
            this.width = W * new_size;
            this.height = H * new_size;
        }

        public void Kresli(Graphics g)
        {
            SolidBrush brush = new SolidBrush(this.color);
            Rectangle rect = new Rectangle(this.x, this.y, this.width, this.height);
            g.FillRectangle(brush, rect);

            Pen pen = new Pen(Color.Black, 1);
            g.DrawRectangle(pen, rect);
        }

        public void Cross(Graphics g, float y)
        {

        }

        public void KresliEditovaci(Graphics g)
        {
            SolidBrush brush = new SolidBrush(this.color);
            Rectangle rect = new Rectangle(this.x, this.y, this.width, this.height);
            g.FillRectangle(brush, rect);

            Pen pen = new Pen(Color.Black, 3);
            g.DrawRectangle(pen, this.x-2, this.y-2, this.width+3, this.height+3);
            int r = 5;
            int d = r * 2;

            g.FillEllipse(Brushes.Black, x - d, y - d, d, d);
            g.FillEllipse(Brushes.Black, x+width, y - d, d, d);
            g.FillEllipse(Brushes.Black, x - d, y+height, d, d);
            g.FillEllipse(Brushes.Black, x+width, y+height, d, d);
        }
    }
}
