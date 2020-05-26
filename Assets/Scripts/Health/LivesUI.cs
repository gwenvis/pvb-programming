using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DN.UI
{
	/// <summary>
	/// creates the ui for the lives
	/// </summary>
	public class LivesUI
	{
		private float offset = 30f;
		private float size = 30f;
		private GameObject heart;
		private List<GameObject> hearts;
		private RectTransform canvas;
		private GameObject canvasObject;
		private Lives lives;

		public LivesUI(GameObject canvas, Lives newLives, float heartSize)
		{
			canvasObject = canvas;
			lives = newLives;
			size = heartSize;
			Start();
		}

		private void Start()
		{
			canvas = canvasObject.GetComponent<RectTransform>();
			hearts = new List<GameObject>();
			heart = Resources.Load<GameObject>("Prefab/heart");

			for (int i = 0; i < lives.CurrentLives; i++)
			{
				GameObject life = Object.Instantiate(heart);
				RectTransform rTransform = life.GetComponent<RectTransform>();			
				life.transform.SetParent(canvasObject.transform);
				rTransform.sizeDelta = new Vector2(size, size);
				rTransform.position = new Vector2(rTransform.rect.width + (size + rTransform.rect.width) * i + offset, Screen.height - size - offset);
				hearts.Add(life);
			}
			lives.LifeLostEvent += OnLifeLostEvent;
			lives.AllLifeLost += OnAllLifeLostEvent;
		}

		private void OnAllLifeLostEvent()
		{
			lives.LifeLostEvent -= OnLifeLostEvent;
		}

		public List<GameObject> GetHearts()
		{
			return hearts;
		}

		private void OnLifeLostEvent(int obj)
		{
			Object.Destroy(hearts[hearts.Count - 1]);
			hearts.RemoveAt(hearts.Count - 1);
		}

		public void LoseLife()
		{
			lives.LoseLife();
		}

		private void OnDestroy()
		{
			lives.LifeLostEvent -= OnLifeLostEvent;
			lives.AllLifeLost -= OnAllLifeLostEvent;
		}
	}
}
