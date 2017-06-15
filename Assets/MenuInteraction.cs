using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInteraction : MonoBehaviour {

	public SteamVR_TrackedObject trackedObj;
	public SteamVR_Controller.Device device;

	// swipe
	public float swipeSum;
	public float touchLast;
	public float touchCurrent;
	public float distance;
	public bool hasSwipedLeft;
	public bool hasSwipedRight;
	public ObjectMenuManager objectMenuManager;

	void Start () {
		this.trackedObj = this.GetComponent<SteamVR_TrackedObject> ();
	}
	
	void Update () {
		this.device = SteamVR_Controller.Input ((int)this.trackedObj.index);

		if (this.device.GetTouchDown (SteamVR_Controller.ButtonMask.Touchpad)) {
			this.touchLast = this.device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
		}

		if (this.device.GetTouch (SteamVR_Controller.ButtonMask.Touchpad)) {
			this.touchCurrent = this.device.GetAxis (Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
			this.distance = this.touchCurrent - this.touchLast;
			this.touchLast = this.touchCurrent;
			this.swipeSum += this.distance;

			if (!this.hasSwipedRight && this.swipeSum > 0.5f) {
				this.swipeSum = 0;
				this.SwipeRight ();
				this.hasSwipedRight = true;
				this.hasSwipedLeft = false;
			}
			if (!this.hasSwipedLeft && this.swipeSum < -0.5f) {
				this.swipeSum = 0;
				this.SwipeLeft ();
				this.hasSwipedRight = false;
				this.hasSwipedLeft = true;
			}
		}

		if (this.device.GetTouchUp (SteamVR_Controller.ButtonMask.Touchpad)) {
			this.swipeSum = 0;
			this.touchCurrent = 0;
			this.touchLast = 0;
			this.hasSwipedLeft = false;
			this.hasSwipedRight = false;
		}

		if (this.device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
			this.SpawnObject ();
		}
	}

	void SwipeLeft() {
		this.objectMenuManager.MenuLeft ();
		Debug.Log ("SwipeLeft");
	}
	
	void SwipeRight() {
		this.objectMenuManager.MenuRight ();
		Debug.Log ("SwipeRight");
	}
	
	void SpawnObject() {
		this.objectMenuManager.SpawnCurrentObject ();
	}

}
