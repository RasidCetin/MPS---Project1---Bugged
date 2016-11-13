using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
    private float rot_speed = 1.0f;
    private Vector3 rot_vec;
	// Use this for initialization
	void Start () {
        rot_vec = new Vector3(0, 0, rot_speed);
	}
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;
        this.transform.Rotate(rot_vec);
	}
}
