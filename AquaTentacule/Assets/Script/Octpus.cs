﻿using UnityEngine;
using System.Collections;

public class Octpus : MonoBehaviour {
	
	public float range;
	public float speed;
	public float slowDown;
	public int nutritionFact;
	private Rigidbody2D rb;
	public SpriteRenderer debugSprite;

	
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		debugSprite.enabled = false;
		float scale = (float) (Random.Range(50,130)/100);
		Debug.Log ("Scale : "+ scale);
		//transform.localScale = new Vector3(scale,scale,scale);
	}

	void Update () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");

		if (Vector3.Distance (transform.position, player.transform.position) < range) {
			transform.LookAt (player.transform.position);
			transform.Rotate (new Vector3 (0, 90, 0), Space.Self);
			transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
		}else{
			rb.velocity = rb.velocity * slowDown;

		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player"){
			GameManager.instance.RespawnOctopus(nutritionFact);
			Destroy(this.gameObject);
			
		}
		if(coll.gameObject.tag == "InhertElement"){
			Vector2 randomVector = Random.insideUnitCircle;
			//rb.velocity = new Vector2(randomVector.x*speed,randomVector.y*speed);
		}
		
	}

	public void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position,range);
	}
}
