using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    public float Yspeed;
    public float Xspeed;
    public float Zspeed;

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(Xspeed *Time.deltaTime, Yspeed*Time.deltaTime, Zspeed*Time.deltaTime, Space.Self);
	}
}
