using System.Collections.Generic;
using System.Linq;

namespace DN.Puzzle.Color
{
	/// <summary>
	/// This is an implementation of the CommandQueue, which allows
	/// the user to request the next ColorCommand out of the queue.
	/// </summary>
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
