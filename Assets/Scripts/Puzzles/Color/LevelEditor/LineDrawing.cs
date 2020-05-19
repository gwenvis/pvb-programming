using System;
using JetBrains.Annotations;
using UnityEngine;

namespace DN.Puzzle.Color.Editor
{
	/// <summary>
	/// Handles drawing a line in the editor.
	/// </summary>
	public class LineDrawing : MonoBehaviour
	{
		public bool InLineDrawMode { get; private set; }
		
		[SerializeField] private GameObject temporaryLinePrefab;
		[SerializeField] private GameObject raycastBlocker;
		[SerializeField] private Canvas canvas;

		private GameObject spawnedLine;
		private Line line;
		private Vector3 startPoint;
		private Vector3 endPoint;

		private Action<Vector3, Vector3> lineCallback;
		
		protected void Update()
		{
			if (!InLineDrawMode)
				return;

			if (Input.GetMouseButtonDown(0))
			{
				startPoint = Input.mousePosition;
				spawnedLine.SetActive(true);
				spawnedLine.transform.position = startPoint;
			}

			if (startPoint == Vector3.zero) return;
			
			endPoint = Input.mousePosition;
			line.SetRotation(endPoint);
			line.SetLength(startPoint, endPoint);

			Debug.DrawLine(startPoint, endPoint);

			if (!Input.GetMouseButtonUp(0)) return;
			
			lineCallback(startPoint, endPoint);
			ExitLineDrawMode();
		}

		public void EnterLineDrawMode([NotNull] Action<Vector3, Vector3> lineCallBack)
		{
			spawnedLine = GameObject.Instantiate(temporaryLinePrefab, transform);
			line = spawnedLine.GetComponent<Line>();
			spawnedLine.SetActive(false);
			this.lineCallback = lineCallBack;
			InLineDrawMode = true;
			canvas = GetComponent<Canvas>();
			raycastBlocker.SetActive((true));
		}

		public void ExitLineDrawMode()
		{
			Destroy(spawnedLine);
			line = null;
			spawnedLine = null;
			startPoint = endPoint = Vector3.zero;
			InLineDrawMode = false;
			raycastBlocker.SetActive(false);
		}
	}
}
