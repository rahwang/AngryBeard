using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public float speed;
    int health;

	// Use this for initialization
	void Start () {
		moveObjectBySpeed (transform.position, Vector3.zero, speed);
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
