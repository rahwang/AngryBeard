using UnityEngine;
using System.Collections;

public class FaceHeadset : MonoBehaviour {

    private GameManager game_manager;

	// Use this for initialization
	void Start () {
        game_manager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        Transform target = game_manager.headset_trans;
        Vector3 lookDir = target.position - transform.position;
        Vector3 lookPoint = target.position - transform.position;
        lookPoint.y = target.position.y;
        transform.LookAt(lookPoint);
	}
}
