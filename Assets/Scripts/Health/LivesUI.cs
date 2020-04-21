using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DN.UI
{
	/// <summary>
	/// ADD CLASS SUMMARY!
	/// </summary>
	public class LivesUI : MonoBehaviour
	{
		[SerializeField] private float size = 50f;
		private Sprite heart;
		private List<GameObject> hearts;
		private RectTransform canvas;
		Lives lives = new Lives();

		private void Start()
		{
			canvas = gameObject.GetComponent<RectTransform>();
			hearts = new List<GameObject>();
			heart = Resources.Load<Sprite>("Sprites/heart");

			for (int i = 0; i < lives.CurrentLives; i++)
			{
				GameObject life = new GameObject($"heart {i}");
				life.AddComponent<Image>().sprite = heart;
				RectTransform rTransform = life.GetComponent<RectTransform>();
				life.transform.parent = transform;
				rTransform.sizeDelta = new Vector2(size, size);
				Debug.Log(canvas.rect.height);
				rTransform.position = new Vector2(rTransform.rect.width + (size / 4 + rTransform.rect.width) * i, Screen.height - size);
				hearts.Add(life);
			}
			lives.LifeLostEvent += OnLiveLostEvent;
		}

		private void OnLiveLostEvent(int obj)
		{
			Destroy(hearts[hearts.Count - 1]);
			hearts.RemoveAt(hearts.Count - 1);
		}

		public void LoseLife()
		{
			lives.LoseLife();
		}
	}
}
