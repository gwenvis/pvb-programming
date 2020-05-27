using UnityEngine;

namespace DN
{
	/// <summary>
	/// Scale the ui object correctly
	/// </summary>
	public class UiObjectScaler : MonoBehaviour
	{
		[SerializeField] private float width;
		[SerializeField] private float height;
		[SerializeField] private float posX;
		[SerializeField] private float posY;
		[SerializeField] private bool changePosition = false;
		private RectTransform rectTransform;
		private BoxCollider2D collider;
		private bool useCollider;

		private void Start()
		{
			rectTransform = GetComponent<RectTransform>();
			useCollider = GetComponent<BoxCollider2D>();
			if(useCollider)
				collider = GetComponent<BoxCollider2D>();
			SetValues();
		}

		public void ChangeValues(Vector2 size, Vector2 position, bool changePos)
		{
			width = size.x;
			height = size.y;
			posX = position.x;
			posY = position.y;
			changePosition = changePos;
			SetValues();
		}

		private void SetValues()
		{
			rectTransform.sizeDelta = new Vector2(width * Screen.width, height * Screen.height);
			if (useCollider)
				collider.size = new Vector2(width * Screen.width, height * Screen.height);
			if (changePosition)
			{
				rectTransform.localPosition = new Vector2(posX * Screen.width, posY * Screen.height);
			}
		}

		private void Update()
		{
			rectTransform = GetComponent<RectTransform>();
			rectTransform.sizeDelta = new Vector2(width * Screen.width, height * Screen.height);
			if (changePosition)
			{
				rectTransform.localPosition = new Vector2(posX * Screen.width, posY * Screen.height);
			}
		}
	}
}
