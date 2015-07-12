using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
	Rigidbody body;
	public bool flying;

	void Start () {
		body = GetComponent<Rigidbody> ();
	}

	public void Fly(){
		Invoke ("lookat", .1f);
	}

	void lookat(){
		flying = true;
	}
	void Update () {
		if (flying) transform.LookAt (transform.position + body.velocity);
	}
}
