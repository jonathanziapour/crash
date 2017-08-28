using UnityEngine;
using System.Collections;

public class ForestManager : MonoBehaviour {

	private ParticleSystem fairyLights;
	private SpawnGhost spawnGhost1;
	private GameObject ghostSpawner1;
	private SpawnGhost spawnGhost2;
	private GameObject ghostSpawner2;

	void Start()
	{
		fairyLights = GameObject.FindGameObjectWithTag ("FairyLights").GetComponent<ParticleSystem> ();
		spawnGhost1 = GameObject.FindGameObjectWithTag ("GhostSpawner1").GetComponent<SpawnGhost> ();
		spawnGhost2 = GameObject.FindGameObjectWithTag ("GhostSpawner2").GetComponent<SpawnGhost> ();
	}

	void OnTriggerEnter(Collider other)
	{
			this.GetComponent<AudioSource> ().Play ();
			fairyLights.Play ();
			SpawnGhosts ();
	}

	void OnTriggerExit(Collider other)
	{
		fairyLights.Stop ();
		this.GetComponent<BoxCollider> ().enabled = false;
	}

	void SpawnGhosts()
	{
		spawnGhost1.Spawn ();
		StartCoroutine (SpawnWait());
	}

	IEnumerator SpawnWait()
	{
		yield return new WaitForSeconds (10);
		spawnGhost2.Spawn ();
	}
}
