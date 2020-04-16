using System.Linq;
using UnityEngine;

public class OneWayBlock : DraggableBlocks
{
	
	protected override void Update()
	{
		if (Input.GetMouseButtonUp(0) && isClicked && !isLockedIn)
		{
			if (HoveringOverBlock(out GameObject otherObject))
			{
				isLockedIn = true;
				SetToLowestChild(otherObject);
			}
		}
		base.Update();
	}
	
	private bool HoveringOverBlock(out GameObject otherObject)
	{
		GameObject[] objects = GetGameObjectsFromRay(out Vector3 position);
		otherObject = objects.SingleOrDefault(p => p != gameObject && p.tag == "Block");
		if(otherObject != null)
		{
			return true;
		}

		return false;
	}

	private void SetToLowestChild(GameObject otherObject)
	{
		bool foundLowestChild = false;
		int i = 0;
		while (!foundLowestChild || i>50)
		{
			RectTransform underTransform = FindComponentInChildWithTag<RectTransform>(otherObject, "BlockUnder");
			if (underTransform.childCount > 0)
			{
				otherObject = underTransform.GetChild(0).gameObject;
			}
			else
			{
				transform.SetParent(underTransform);
				transform.position = underTransform.position;
				foundLowestChild = true;
			}
			i++;
		}
	}
}
