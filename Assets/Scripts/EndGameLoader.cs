using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndGameLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("Title");
    }
}
