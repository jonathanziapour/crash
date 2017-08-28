using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndSceneManager : MonoBehaviour {

    GameObject player;
    Woman w;
    GameObject woman_pos;
    bool hasDied;

    // Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        w = GameObject.FindGameObjectWithTag("Woman").GetComponent<Woman>();
        woman_pos = GameObject.FindGameObjectWithTag("WomanPos").gameObject;
        hasDied = false;
	}
	
	// Update is called once per frame
	void Update () {

        if ((Vector3.Distance(player.transform.position, woman_pos.transform.position) < 30.0f) && hasDied == false)
        {
            w.SetCloseEnough(true);
            hasDied = true;
        }
	}
}
