using System.Collections.Generic;
using System.Linq;

namespace DN.Puzzle.Color
{
	public class ColorCommandQueue : ICommandQueue<ColorCommand>
	{
		private Queue<ColorCommand> colorCommands;

		public ColorCommandQueue(IEnumerable<ColorCommand> colorCommands)
		{
			this.colorCommands = new Queue<ColorCommand>(colorCommands);
		}

		public bool Empty => !colorCommands.Any();

		public ColorCommand RequestNext() => colorCommands.Dequeue();
	}
}
