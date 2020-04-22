namespace DN.Puzzle
{
	public interface ICommandQueue<T>
	{
		T RequestNext();
		bool Empty { get; }
	}
}
