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

		private GameObject spawnedLine;

		private Action<Vector3, Vector3> lineCallback;
		
		protected void Update()
		{
			if (!InLineDrawMode)
				return;

			if (Input.GetMouseButtonDown(0))
			{
				
			}
		}

		public void EnterLineDrawMode([NotNull] Action<Vector3, Vector3> lineCallBack)
		{
			spawnedLine = GameObject.Instantiate(temporaryLinePrefab);
			spawnedLine.SetActive(false);
			this.lineCallback = lineCallBack;
			InLineDrawMode = true;
		}

		public void ExitLineDrawMode()
		{
			Destroy(temporaryLinePrefab);
		}
	}
}
