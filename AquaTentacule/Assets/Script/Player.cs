﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Limits
{
	public float xMin, xMax, zMin, zMax;
}

public class Player : MonoBehaviour
{
	public float speed;
	public Limits lim;

	void Start()
	{
	}
	
	void FixedUpdate ()
	{
		float moveHor = Input.GetAxis ("Horizontal");
		float moveVert = Input.GetAxis ("Vertical");
		
		Vector3 move = new Vector3 (moveHor, 0.0f, moveVert);
		GetComponent<Rigidbody>().velocity = move * speed;
		
		GetComponent<Rigidbody>().position = new Vector3 
			(
				Mathf.Clamp (GetComponent<Rigidbody>().position.x, lim.xMin, lim.xMax), 
				0.0f, 
				Mathf.Clamp (GetComponent<Rigidbody>().position.z, lim.zMin, lim.zMax)
				);
	}
}