using UnityEngine;
using System.Collections;

public class SpawnGhost : MonoBehaviour {

	public GameObject ghost;

	public void Spawn()
	{
		Instantiate(ghost, this.transform.position, this.transform.rotation);
		this.transform.gameObject.SetActive (false);
	}
}
