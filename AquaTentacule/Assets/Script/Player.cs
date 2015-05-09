﻿using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour
{
	public static Player instance = null;
	public float speedRotation;
	public float force;
	public float pushForce;
	public float slowDown;	
	public float timeVolume;
	private Animator animator;
	public float dampTime = 0.2f;
	private Vector3 velocity = Vector3.zero;

	public int timetoInverteBack = 10;
	public float scaleRatio;
	public SpriteRenderer debugSprite;

	public AudioSource musicSource1;
	public AudioSource musicSource2;
	public AudioSource musicSource3;
	public AudioSource musicSource4;
	public AudioSource musicSource5;
	private int currentZone;

	private Rigidbody2D rb;
	private Vector3 startScale; 


	public GameObject ten1;
	public GameObject ten2;
	public GameObject ten3;

	private float inverted = 1;
	void Awake () {	
		if (instance == null){
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}
		
		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		animator = GetComponent<Animator> ();	

		rb = GetComponent<Rigidbody2D>();
		resetMusique();
		debugSprite.enabled = false;
		GameManager.instance.setPlayer(this);
		scaleRatio = 1;
		startScale = new Vector3(transform.localScale.x,transform.localScale.y,transform.localScale.z);
	}
	public void resetMusique(){
		musicSource1.volume = 1;
		musicSource2.volume = 0;
		musicSource3.volume = 0;
		musicSource4.volume = 0;
		musicSource5.volume = 0;
		currentZone = 1 ;
	}

	void OnCollisionEnter2d(Collision2D coll){
		if (coll.gameObject.tag == "meduse"){
			inverted = -1.0f;
			StartCoroutine(timer());
		}
	}
	
	void FixedUpdate ()
	{

		if (Input.GetKey (KeyCode.Q)) {
			rb.angularVelocity = speedRotation * inverted;
		} else if (Input.GetKey (KeyCode.D)) {
			rb.angularVelocity = -speedRotation * inverted;
		} else {
			rb.angularVelocity = 0f;
		}
		
		Vector2 forceVect = transform.up * Mathf.Pow (force, pushForce);
		
		if (Input.GetKey (KeyCode.Z)) {
			rb.AddForce (forceVect * inverted);
			animator.SetBool ("swim", true);
		} else if (Input.GetKey (KeyCode.S)) {
			rb.AddForce (-forceVect * inverted);
			animator.SetBool ("swim", true);
		} else {
			rb.velocity = rb.velocity * slowDown;
			animator.SetBool ("swim", false);
		}
	}
	public IEnumerator timer(){
		yield return new WaitForSeconds(timetoInverteBack);
		inverted = 1;
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.tag == "Zone1"){
			currentZone = 2;
			StartCoroutine (VolumeDown(musicSource1));
			StartCoroutine (Volumeup(musicSource2));
		}
		if(other.gameObject.tag == "Zone2"){
			currentZone = 3;
			StartCoroutine (VolumeDown(musicSource2));
			StartCoroutine (Volumeup(musicSource3));
		}
		if(other.gameObject.tag == "Zone3"){
			currentZone = 4;
			StartCoroutine (VolumeDown(musicSource3));
			StartCoroutine (Volumeup(musicSource4));
		}
		if(other.gameObject.tag == "Zone4"){
			currentZone = 5;
			StartCoroutine (VolumeDown(musicSource4));
			StartCoroutine (Volumeup(musicSource5));
		}
	}

	void OnTriggerEnter2D(Collider2D other) {

		if(other.gameObject.tag == "Zone1" && currentZone == 2){
			currentZone = 1;
			StartCoroutine (Volumeup(musicSource1));
			StartCoroutine (VolumeDown(musicSource2));
		}
		if(other.gameObject.tag == "Zone2" && currentZone == 3){
			currentZone = 2;
			StartCoroutine (Volumeup(musicSource2));
			StartCoroutine (VolumeDown(musicSource3));
		}
		if(other.gameObject.tag == "Zone3" && currentZone == 4){
			currentZone = 3;
			StartCoroutine (Volumeup(musicSource3));
			StartCoroutine (VolumeDown(musicSource4));
		}
		if(other.gameObject.tag == "Zone4" && currentZone == 5){
			currentZone = 4;
			StartCoroutine (Volumeup(musicSource4));
			StartCoroutine (VolumeDown(musicSource5));
		}

	}

	public void Eat(){
		animator.SetTrigger("eat");
	}

	public void setScale(float adj){
		scaleRatio = Mathf.MoveTowards(scaleRatio,adj,0.04f);
		transform.localScale = startScale * scaleRatio;
	}

	private IEnumerator Volumeup (AudioSource source) {
		while(source.volume <1){
			yield return new WaitForSeconds(1/100);
			source.volume = source.volume + 1/(timeVolume*100);
		}
	}

	private IEnumerator VolumeDown (AudioSource source) {
		while(source.volume > 0){
			yield return new WaitForSeconds(1/100);
			source.volume = source.volume - 1/(timeVolume*100);
		}
	}

}