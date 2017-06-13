using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputManager : MonoBehaviour {

	public SteamVR_TrackedObject trackedObj;
	public SteamVR_Controller.Device device;

	private LineRenderer laser;
	public Material validLocationMaterial;
	public Material invalidLocationMaterial;
	
	public GameObject teleportAimerObject;
	public Vector3 teleportLocation;
	public GameObject player;
	public LayerMask laserMask;
	public float yNudgeAmount = 0.5f; // specific to teleportAimerObject height

	private bool validTeleportLocation = false;

	void Start () {
		this.trackedObj = this.GetComponent<SteamVR_TrackedObject> ();
		this.laser = this.GetComponentInChildren<LineRenderer> ();
		this.laser.gameObject.SetActive (false);
	}
	
	void Update () {
		this.device = SteamVR_Controller.Input ((int)this.trackedObj.index);

		if (this.device.GetPress (SteamVR_Controller.ButtonMask.Touchpad)) {
			this.laser.gameObject.SetActive (true);
			teleportAimerObject.SetActive (true);

			// laser start point
			laser.SetPosition (0, this.gameObject.transform.position);

			RaycastHit hit;
			if (Physics.Raycast (this.transform.position, this.transform.forward, out hit, 15, this.laserMask.value)) {
				this.teleportLocation = hit.point;
				this.laser.SetPosition (1, this.teleportLocation);
				// aimer position
				this.teleportAimerObject.transform.position = new Vector3 (this.teleportLocation.x, this.teleportLocation.y + this.yNudgeAmount, this.teleportLocation.z);

				this.validTeleportLocation = true;

			} else {
				//this.teleportLocation = new Vector3 (this.transform.forward.x * 15 + this.transform.position.x, this.transform.forward.y * 15 + this.transform.position.y, this.transform.forward.z * 15 + this.transform.position.z);
				this.teleportLocation = this.transform.position + this.transform.forward * 15;

				RaycastHit groundRayHit;
				if (Physics.Raycast (this.teleportLocation, -Vector3.up, out groundRayHit, 17, this.laserMask.value)) {
					this.teleportLocation = new Vector3 (
						this.transform.position.x + this.transform.forward.x * 15,
						groundRayHit.point.y,
						this.transform.position.z + this.transform.forward.z * 15
					);
					this.validTeleportLocation = true;
				} else {
					this.validTeleportLocation = false;
				}

				this.laser.SetPosition (1, this.transform.position + this.transform.forward * 15);
				// aimer position
				this.teleportAimerObject.transform.position = this.teleportLocation + new Vector3(0, this.yNudgeAmount, 0);
			}

			if (this.validTeleportLocation) {
				this.laser.material = this.validLocationMaterial;
				this.teleportAimerObject.GetComponent<Renderer> ().material = this.validLocationMaterial;
			} else {
				this.laser.material = this.invalidLocationMaterial;
				this.teleportAimerObject.GetComponent<Renderer> ().material = this.invalidLocationMaterial;
			}
		}

		if (this.device.GetPressUp (SteamVR_Controller.ButtonMask.Touchpad)) {
			this.laser.gameObject.SetActive (false);
			this.teleportAimerObject.SetActive (false);
			
			if (this.validTeleportLocation) {
				this.player.transform.position = this.teleportLocation;
			}
		}
	}

}
