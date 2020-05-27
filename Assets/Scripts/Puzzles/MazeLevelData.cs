using System.Collections.Generic;
using System.Linq;
using DN.Puzzle.Maze.UI;
using UnityEngine;

namespace DN
{
	/// <summary>
	/// level data for the maze levels
	/// </summary>
	public class MazeLevelData : LevelData
	{
		public int MaxBlocks => maxBlocks;
		public Texture2D Image => image;
		
        [SerializeField] private Texture2D image;
        [SerializeField] private int maxBlocks;
        private List<UnityEngine.Color> mazeBlockColors;
        
        public MazeBlocks[][] GetMazeFromImage()
        {
	        int levelWidth = image.width;
	        int levelHeight = image.height;

	        var level = new MazeBlocks[levelHeight-1][];
	        mazeBlockColors = new List<UnityEngine.Color>();

	        for (int i = 0; i < 3; i++)
	        {
		        mazeBlockColors.Add(image.GetPixel(i, levelHeight-1));
	        }

	        for (int y = 1; y < levelHeight; y++)
	        {
		        int yy = y - 1;
		        level[yy] = new MazeBlocks[levelWidth];
		        for (int x = 0; x < levelWidth; x++)
		        {
			        var color = image.GetPixel(x, levelHeight-y-1);
			        if (mazeBlockColors.Contains(color))
			        {
				        
				        level[yy][x] = (MazeBlocks)mazeBlockColors.IndexOf(color);
			        }
			        else
			        {
				        level[yy][x] = MazeBlocks.None;
			        }
		        }
	        }

	        return level;
        }
	}
}
