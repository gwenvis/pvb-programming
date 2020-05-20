using DN.UI;
using UnityEngine;

namespace DN.Puzzle.Maze.UI
{
	/// <summary>
	/// Creates the blocks that spawn blocks
	/// </summary>
	public class CreateSpawnerBlocks : MonoBehaviour
	{
		[SerializeField] private GameObject[] spawnBlocks;
		[SerializeField] private Canvas canvas;
		[SerializeField] private float yOffset = -2f;

		private void Start()
		{
			float yPos = GetComponent<RectTransform>().rect.height/2;
			for(int i = 0; i < spawnBlocks.Length; i++)
			{
				GameObject block = Instantiate(spawnBlocks[i], transform);
				Destroy(block.GetComponent<DraggableItem>());
				Destroy(block.GetComponent<BoxCollider2D>());
				yPos = yPos - block.GetComponent<RectTransform>().rect.height + yOffset;
				block.transform.localPosition = new Vector2(0, yPos);
				SpawnBlock spawnBlock = block.AddComponent<SpawnBlock>();
				spawnBlock.SetDraggableItem(spawnBlocks[i]);
				spawnBlock.SetCanvas(canvas);
			}
		}
	}
}
