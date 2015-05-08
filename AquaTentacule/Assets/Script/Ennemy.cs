﻿using UnityEngine;
using System.Collections;

public class Ennemy : MonoBehaviour {

	public int nutritionFact;
	public float speedMax;

	private Transform target;
	private Rigidbody2D rb;
	private float speed;
	// Use this for initialization
	void Start () {
		speed  = (float)(Random.Range(0,speedMax*10)/10);
		rb = GetComponent<Rigidbody2D>();
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go.transform;
		Vector2 randomVector = Random.insideUnitCircle;
		rb.velocity = new Vector2(randomVector.x*speed,randomVector.y*speed);
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player"){
			GameManager.instance.eatFood(nutritionFact);
			Destroy(this.gameObject);
		}
	}

}
