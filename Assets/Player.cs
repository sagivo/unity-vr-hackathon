using UnityEngine;
using System.Collections;
using Leap;

public class Player : MonoBehaviour {
	Controller leap_controller;
	Hand leftHand;
	Hand rightHand;
	int minDistanceHandsToShoot = 150;
	bool canShoot;
	int minFIngersDistanceToShoot = 50;
	int maxPalmDistanceToCharge = 20;
	bool charged;
	public GameObject arrow;
	public float arrowSpeed = 10;
	GameObject currentArrow;
	public HandController handController;

	void Start () {
		leap_controller = new Controller();
	}
	
	void Update () {
		if (leap_controller.Frame ().Hands.Count == 2) {
			leftHand = leap_controller.Frame ().Hands.Leftmost;
			rightHand = leap_controller.Frame ().Hands.Rightmost;
			var distancePalms = Vector3.Distance(new Vector3(0,0,leftHand.PalmPosition.z), new Vector3(0,0,rightHand.PalmPosition.z) );
			if (!charged && distancePalms < maxPalmDistanceToCharge && arrow) { //charge
				charged = true;
				currentArrow = Instantiate (arrow, Vector3.zero, Quaternion.identity) as GameObject;
				//currentArrow.transform = new Vector3(.01f,.01f,01f);
				Debug.Log("charged!");

			}
			else if (charged) {
				var thumb = rightHand.Fingers.FingerType(Finger.FingerType.TYPE_THUMB)[0];
				var pointer = rightHand.Fingers.FingerType(Finger.FingerType.TYPE_INDEX)[0];
				var distanceFingers = Vector3.Distance(thumb.Bone(Bone.BoneType.TYPE_DISTAL).Center.ToUnity(), pointer.Bone(Bone.BoneType.TYPE_DISTAL).Center.ToUnity());

				if (currentArrow) {
					currentArrow.transform.position = handController.transform.TransformPoint( rightHand.PalmPosition.ToUnityScaled() );
					currentArrow.transform.rotation = Quaternion.LookRotation( leftHand.PalmPosition.ToUnity () - rightHand.PalmPosition.ToUnity ());
				}

				if (distancePalms > minDistanceHandsToShoot && distanceFingers > minFIngersDistanceToShoot) shoot();
			}
		}

	}

	void shoot(){
		if (currentArrow) {
			currentArrow.GetComponent<Rigidbody> ().AddRelativeForce (Vector3.forward * arrowSpeed, ForceMode.Impulse);
			currentArrow.GetComponent<Arrow>().Fly();
			currentArrow = null;
			Debug.Log ("BOOM");
			charged = false;
		}
	}
}
