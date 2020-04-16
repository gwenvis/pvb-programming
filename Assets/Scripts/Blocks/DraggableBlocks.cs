using UnityEngine;
using System.Linq;
using System;

public class DraggableBlocks : MonoBehaviour
{
	[HideInInspector] public GameObject blockArea;
	private new RectTransform transform;
	protected bool isClicked;
	protected bool isLockedIn = false;

	protected virtual void Start()
	{
		transform = GetComponent<RectTransform>();
		isClicked = true;
	}

	protected virtual void Update()
    {
		if (PlayLevel.playGame)
			return;
		if(Input.GetMouseButtonDown(0))
		{
			if (HitsTransform(isClicked, out Vector3 position))
			{
				isLockedIn = false;
				isClicked = true;
				transform.SetParent(blockArea.transform);
			}
		}
		else if (Input.GetMouseButton(0) && isClicked)
		{
			if (HitsTransform(isClicked, out Vector3 position))
			{
				transform.position = position;
			}
		}
		else
		{
			isClicked = false;

			if (!IsOnField() && !isLockedIn)
			{
				Destroy(gameObject);
			}
		}
    }

	private bool HitsTransform(bool isClicked ,out Vector3 position)
	{
		GameObject hit = GetGameObjectFromRay(out position);
		if (isClicked)
		{
			return true;
		}

		if (hit != null)
		{
			return hit == gameObject;
		}
		return false;
	}

	protected GameObject GetGameObjectFromRay(out Vector3 position)
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		ray.direction *= 1000;
		RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 1000f);
		position = ray.origin + ray.direction * 10;
		if (hit.transform != null)
		{
			return hit.transform.gameObject;
		}
		return null;
	}
	
	protected GameObject[] GetGameObjectsFromRay(out Vector3 position)
	{
		GameObject[] hitObjects = new GameObject[0];
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		ray.direction *= 1000;
		RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, 1000f);
		position = ray.origin + ray.direction * 10;
		if (hits.Length > 0)
		{
			hitObjects = Array.ConvertAll(hits, n => n.transform.gameObject);
			return hitObjects;
		}
		return hitObjects;
	}

	private bool IsOnField()
	{
		if (transform.localPosition.x > 0)
		{
			return true;
		}
		return false;
	}

	protected T FindComponentInChildWithTag<T>(GameObject parent, string tag) where T : Component
	{
		Transform t = parent.transform;
		foreach (Transform tr in t)
		{
			if (tr.tag == tag)
			{
				return tr.GetComponent<T>();
			}
		}
		return null;
	}
}