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
		[SerializeField] private float yOffset = 2f;

		private void Start()
		{
			float yPos = 0;//GetComponent<RectTransform>().rect.height/2;
			for(int i = 0; i < spawnBlocks.Length; i++)
			{
				GameObject block = Instantiate(spawnBlocks[i], transform);

				Destroy(block.GetComponent<DraggableItem>());
				List<IDroppable> iDroppables = block.GetComponentsInChildren<IDroppable>().ToList();
				iDroppables.ForEach(x => Destroy(x as Component));

				block.GetComponent<MazeDraggableItem>().GetHeight();
				float height = spawnedBlocks.Count <= 0 ? block.GetComponent<MazeDraggableItem>().Height : spawnedBlocks[spawnedBlocks.Count - 1].GetComponent<MazeDraggableItem>().Height;
				yPos = yPos - height - yOffset;
				block.transform.localPosition = new Vector2(0, yPos);

				SpawnBlock spawnBlock = block.AddComponent<SpawnBlock>();
				spawnBlock.SetDraggableItem(spawnBlocks[i]);
				spawnBlock.SetCanvas(canvas);
				spawnedBlocks.Add(block);
			}
		}
	}
}
