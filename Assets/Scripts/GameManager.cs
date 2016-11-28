using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class GameManager : MonoBehaviour {

	float TowerHealth = 100.0f;

	private UnityAction someListener;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake ()
	{
		someListener = new UnityAction (SomeFunction);
	}

	void OnEnable ()
	{
		EventManager.StartListening ("test", someListener);
		EventManager.StartListening ("towerTakeDamage", DamageTower);
	}

	void OnDisable ()
	{
		EventManager.StopListening ("test", someListener);
		EventManager.StopListening ("towerTakeDamage", DamageTower);
	}

	void DamageTower ()
	{
		Debug.Log ("Some Function was called!");
	}

	void SomeFunction ()
	{
		Debug.Log ("Some Other Function was called!");
	}
}
