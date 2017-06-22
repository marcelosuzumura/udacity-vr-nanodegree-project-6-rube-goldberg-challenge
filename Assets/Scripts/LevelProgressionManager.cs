using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgressionManager : MonoBehaviour {

	public GameObject[] collectibles;
	public Material canCompleteLevelMaterial;
	public Material cannotCompleteLevelMaterial;

	private AntiCheatManager antiCheatManager;

	private int collectiblesCollected = 0;
	private bool cheating = false;
	private Renderer ballRenderer;

	void Start() {
		this.antiCheatManager = GameObject.Find ("Platform").GetComponent<AntiCheatManager> ();
		this.ballRenderer = this.gameObject.GetComponent<Renderer> ();
	}

	public void ResetLevelProgression() {
		for (int i = 0; i < this.collectibles.Length; i++) {
			this.collectibles [i].SetActive (true);
		}

		this.collectiblesCollected = 0;

		this.ResetBallMaterial ();
	}

	public void RegisterCollectibleCollected() {
		this.collectiblesCollected++;
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.CompareTag("Goal") && this.AllCollectiblesCollected() && !this.cheating) {
			Debug.Log ("LEVEL COMPLETE!!!!!");
		}
	}

	bool AllCollectiblesCollected() {
		return this.collectiblesCollected == this.collectibles.Length;
	}

	public void ChangeBallMaterialIfCheating() {
		if (this.antiCheatManager.isCheating ()) {
			this.ballRenderer.material = this.cannotCompleteLevelMaterial;
			this.cheating = true;
		} else {
			this.ResetBallMaterial ();
		}
	}

	public void ResetBallMaterial () {
		this.ballRenderer.material = this.canCompleteLevelMaterial;
		this.cheating = false;
	}

}
