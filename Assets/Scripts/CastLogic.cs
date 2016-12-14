using UnityEngine;
using System.Collections;

public class CastLogic : MonoBehaviour {

    private GameManager game_manager;

	// Use this for initialization
	void Start () {
        game_manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("CastPoint"))
        {
            col.gameObject.GetComponent<AudioSource>().Play();
            game_manager.RegisterCastPoint(col.gameObject.name);
        }
    }
}
