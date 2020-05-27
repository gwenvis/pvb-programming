using System;
using UnityEngine;

namespace DN.Music
{
	/// <summary>
	/// Song data
	/// </summary>
	[Serializable]
	public class Song
	{
		public AudioClip AllStems => allStems;
		public AudioClip SomeStems => someStems;
		
		[SerializeField] private AudioClip allStems;
		[SerializeField] private AudioClip someStems;
	}
}
