using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInteraction : MonoBehaviour {

	public SteamVR_TrackedObject trackedObj;
	public SteamVR_Controller.Device device;

	public ObjectMenuManager objectMenuManager;

	private bool isMenuEnabled = false;

	void Start () {
		this.trackedObj = this.GetComponent<SteamVR_TrackedObject> ();
	}
	
	void Update () {
		this.device = SteamVR_Controller.Input ((int)this.trackedObj.index);

		if (this.device.GetTouchDown (SteamVR_Controller.ButtonMask.Touchpad)) {
			this.objectMenuManager.MenuEnable ();
			this.isMenuEnabled = true;
		}

		if (this.device.GetTouchUp (SteamVR_Controller.ButtonMask.Touchpad)) {
			this.objectMenuManager.MenuDisable ();
			this.isMenuEnabled = false;
		}

		if (this.device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
			float touchCurrent = this.device.GetAxis (Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
			if (touchCurrent <= 0) {
				this.MenuLeft ();
			} else {
				this.MenuRight ();
			}
		}

		if (this.device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && this.isMenuEnabled) {
			this.SpawnObject ();
		}
	}

	void MenuLeft() {
		this.objectMenuManager.MenuLeft ();
	}
	
	void MenuRight() {
		this.objectMenuManager.MenuRight ();
	}
	
	void SpawnObject() {
		this.objectMenuManager.SpawnCurrentObject ();
	}

}
