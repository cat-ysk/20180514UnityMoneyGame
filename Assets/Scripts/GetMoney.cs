using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMoney : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
        var ps = GetComponent<ParticleSystem>();
        Destroy(gameObject, ps.main.duration);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position;
	}
}
