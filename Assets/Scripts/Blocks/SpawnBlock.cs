using UnityEngine;

public class SpawnBlock : MonoBehaviour
{
	[SerializeField] private GameObject block;
	[SerializeField] private GameObject blockArea;
	 
	protected void Update()
	{
		if (Input.GetMouseButtonDown(0) && !PlayLevel.playGame)
		{
			if (HitsTransform())
			{
				GameObject newBlock = Instantiate(block, transform.position, block.transform.rotation, blockArea.transform);
				(newBlock.GetComponent(typeof(DraggableBlocks)) as DraggableBlocks).blockArea = blockArea;
			}
		}
	}

	private bool HitsTransform()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		ray.direction *= 1000;
		RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 1000f);

		if (hit.transform != null)
		{
			return hit.transform.gameObject == gameObject;
		}
		return false;
	}
}
