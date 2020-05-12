namespace DN.Puzzle
{
	/// <summary>
	/// Implement this to make a command queue that can peek if it's empty
	/// or to dequeue the next one.
	/// </summary>
	/// <typeparam name="T">Type of the queue to return</typeparam>
	public interface ICommandQueue<T>
	{
		T RequestNext();
		bool Empty { get; }
	}
}
