using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DN
{
	/// <summary>
	/// Simple class that fires an event once the item has been clicked.
	/// </summary>
	public class ClickableItem : MonoBehaviour, IPointerDownHandler
	{
		public event Action<ClickableItem> ClickedEvent; 
		
		public void OnPointerDown(PointerEventData eventData)
		{
			ClickedEvent?.Invoke(this);
		}
	}
}
