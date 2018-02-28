using System;
using System.Collections.Generic;


namespace Minisoft1
{
	[Serializable]
	public class Settings
	{
		public int rows; 
		public int cols;
		public int cell_size;
		public int blockCount;
        public List<Block> blocks;
        public int[,] playground;
        public int window_width;
        public int window_height;
    }	
}
