using DN.UI;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace DN.Puzzle.Maze.UI
{
	/// <summary>
	/// Creates the blocks that spawn blocks
	/// </summary>
	public class CreateSpawnerBlocks : MonoBehaviour
	{
		[SerializeField] private GameObject[] spawnBlocks;
		[SerializeField] private List<GameObject> spawnedBlocks = new List<GameObject>();
		[SerializeField] private Canvas canvas;
		[SerializeField] private float xOffset = 5f;

		private void Start()
		{
			float xPos = 0;
			for(int i = 0; i < spawnBlocks.Length; i++)
			{
				GameObject block = Instantiate(spawnBlocks[i], transform);

				Destroy(block.GetComponent<DraggableItem>());
				List<IDroppable> iDroppables = block.GetComponentsInChildren<IDroppable>().ToList();
				iDroppables.ForEach(x => Destroy(x as Component));

				block.GetComponent<MazeDraggableItem>().GetHeight();
				float width = block.GetComponent<RectTransform>().rect.width;
				xPos = xPos + width + xOffset;
				block.transform.localPosition = new Vector2(xPos, xOffset*6);

				SpawnBlock spawnBlock = block.AddComponent<SpawnBlock>();
				spawnBlock.SetDraggableItem(spawnBlocks[i]);
				spawnBlock.SetCanvas(canvas);
				spawnedBlocks.Add(block);
			}
		}
	}
}
