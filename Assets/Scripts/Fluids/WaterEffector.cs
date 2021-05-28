﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEffector : MonoBehaviour
{
	[Range(-10, 10)] public float strength = 1;
	public Water water = null;

	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			water.Touch(ray, strength);
		}
	}
}
