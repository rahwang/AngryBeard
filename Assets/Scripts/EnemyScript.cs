using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public float speed;
    public int health;
    public SpellManager.Element type;
    public Material FireMat, FrostMat, LightningMat, EarthMat;

	// Use this for initialization
	void Start () {
		moveObjectBySpeed (transform.position, Vector3.up*0.8f, speed);
        type = (SpellManager.Element)Random.Range(0, 3);

        switch (type)
        {
            case SpellManager.Element.Fire: gameObject.GetComponent<Renderer>().material = FireMat;
                health = 10;
                speed = 1;
                break;
            case SpellManager.Element.Frost: gameObject.GetComponent<Renderer>().material = FrostMat;
                health = 20;
                speed = 0.5f;
                break;
            case SpellManager.Element.Lightning: gameObject.GetComponent<Renderer>().material = LightningMat;
                health = 10;
                speed = 1;
                break;
            case SpellManager.Element.Earth: gameObject.GetComponent<Renderer>().material = EarthMat;
                health = 20;
                speed = 0.5f;
                break;
        }

        moveObjectBySpeed(transform.position, Vector3.zero, speed);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	void moveObjectBySpeed(Vector3 source, Vector3 target, float speed)
	{
		float pathLength = (target - source).magnitude;
		float duration = pathLength / speed;
		StartCoroutine(moveObject(source, target, duration));
	}

	IEnumerator moveObject(Vector3 source, Vector3 target, float duration)
	{
		float startTime = Time.time;
		while(Time.time < startTime + duration)
		{
			transform.position = Vector3.Lerp(source, target, (Time.time - startTime) / duration);
			yield return null;
		}
		transform.position = target;
	}
}
