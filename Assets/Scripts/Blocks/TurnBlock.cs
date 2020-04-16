using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnBlock : OneWayBlock
{
	public TMP_Dropdown dropdown;
	string dropdownValue;

	protected override void Start()
	{
		base.Start();
		dropdown = GetComponentInChildren<TMP_Dropdown>();
	}

	protected override void Update()
	{
		base.Update();
	}
}
