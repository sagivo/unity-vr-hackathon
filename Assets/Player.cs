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

	// Use this for initialization
	void Start () {
		leap_controller = new Controller();
	}
	
	// Update is called once per frame
	void Update () {
		if (leap_controller.Frame ().Hands.Count == 2) {
			leftHand = leap_controller.Frame ().Hands.Leftmost;
			rightHand = leap_controller.Frame ().Hands.Rightmost;
			var distancePalms = Vector3.Distance(new Vector3(0,0,leftHand.PalmPosition.z), new Vector3(0,0,rightHand.PalmPosition.z) );
			if (distancePalms < maxPalmDistanceToCharge) charged = true;
			else if (charged) {
				var thumb = rightHand.Fingers.FingerType(Finger.FingerType.TYPE_THUMB)[0];
				var pointer = rightHand.Fingers.FingerType(Finger.FingerType.TYPE_INDEX)[0];
				var distanceFingers = Vector3.Distance(thumb.Bone(Bone.BoneType.TYPE_DISTAL).Center.ToUnity(), pointer.Bone(Bone.BoneType.TYPE_DISTAL).Center.ToUnity());
				if (distancePalms > minDistanceHandsToShoot && distanceFingers > minFIngersDistanceToShoot) shoot();
			}
		}
	}

	void shoot(){
		Debug.Log("BOOM");
		charged = false;
	}
}
