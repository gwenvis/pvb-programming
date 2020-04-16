using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayLevel : MonoBehaviour
{
	static public bool playGame = false;
	public bool gameEnded = false;
	public GameObject player;
	[SerializeField]private GameObject blockArea;
	[SerializeField]private GameObject backGround;
	[SerializeField]private TMP_Text endText;
	private Component[] blocks;


	public void StartLevel()
	{
		List<Transform> transforms = new List<Transform>();
		if (blockArea.transform.childCount > 0)
		{
			for (int i = 0; i < blockArea.transform.childCount; ++i)
			{
				transforms.Add(blockArea.transform.GetChild(i));
			}

			Play(GetMostChildCount(transforms));
		}
		else
		{
			Debug.Log("No BlocksPlaced");
		}
	}

	private void Play(Transform block)
	{
		blocks = block.GetComponentsInChildren(typeof(DraggableBlocks), false);
		foreach(DraggableBlocks draggableBlock in blocks)
		{
			if(draggableBlock.GetType() == typeof(TurnBlock))
			{
				(draggableBlock as TurnBlock).dropdown.enabled = false;
			}
		}
		playGame = true;
		player.GetComponent<Player>().components = blocks;
		player.GetComponent<Player>().playLevel = this;
	}

	private Transform GetMostChildCount(List<Transform> transforms)
	{
		Transform hasMostChildren = transforms[0];
		int mostChildCount = 0;
		foreach (Transform t in transforms)
		{
			int childCount = t.GetComponentsInChildren<Transform>().Length;
			if (mostChildCount < childCount)
			{
				hasMostChildren = t;
				mostChildCount = childCount;
			}
		}
		return hasMostChildren;
	}
	public void Won()
	{
		endText.text = "You Won";
		backGround.SetActive(true);
		gameEnded = true;
	}

	public void GameOver()
	{
		endText.text = "You Lost";
		backGround.SetActive(true);
		gameEnded = true;
	}

	public void Retry()
	{
		SceneManager.LoadScene("MazePuzzle", LoadSceneMode.Single);
	}


}
