using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
	Rigidbody body;
	public float speed = 2;
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody> ();
		//transform.forward = Vector3.Slerp (transform.forward,body.velocity.normalized, Time.deltaTime);
		var fwd = transform.TransformDirection(Vector3.forward);
		body.AddForce(fwd * speed, ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (transform.position + body.velocity);
	}
}
