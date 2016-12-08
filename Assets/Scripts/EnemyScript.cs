using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public float speed;
    float baseSpeed;
    public int health;
    public SpellManager.Element effect;
    public SpellManager.Element type;
    public Material FireMat, FrostMat, LightningMat, EarthMat;

    private bool stunned = false;
    private bool slowed = false;

    private float speed_scale_factor = 0.1f;

	// Use this for initialization
	void Start () {
		//moveObjectBySpeed (transform.position, Vector3.up*0.8f, speed);
        type = (SpellManager.Element)Random.Range(0, 4);

        switch (type)
        {
            case SpellManager.Element.Fire: gameObject.GetComponent<Renderer>().material = FireMat;
                health = 10;
                baseSpeed = speed = 0.25f;
                break;
            case SpellManager.Element.Frost: gameObject.GetComponent<Renderer>().material = FrostMat;
                health = 20;
                baseSpeed = speed = 0.125f;
                break;
            case SpellManager.Element.Lightning: gameObject.GetComponent<Renderer>().material = LightningMat;
                health = 10;
                baseSpeed = speed = 0.25f;
                break;
            case SpellManager.Element.Earth: gameObject.GetComponent<Renderer>().material = EarthMat;
                health = 20;
                baseSpeed = speed = 0.125f;
                break;
        }

        moveObjectBySpeed(transform.position, Vector3.zero);
    }

    // Update is called once per frame
    float slowTime = 5.0f;
    float stunTime = 3.0f;
    float frostTime = 0.0f, earthTime = 0.0f;
	void Update () {
	    switch (effect) {
            case SpellManager.Element.Frost:
                frostTime = Time.time;
                StartCoroutine(slow(slowTime));
                break;
            case SpellManager.Element.Earth:
                earthTime = Time.time;
                StartCoroutine(stun(stunTime));
                break;
            default:
                break;
        }
        effect = SpellManager.Element.None;
    }

    IEnumerator slow(float duration)
    {
        slowed = true;
        speed /= 2;
        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            yield return null;
        }
        slowed = false;
        if (!stunned)
        {
            speed = baseSpeed;
        }
    }

    IEnumerator stun(float duration)
    {
        stunned = true;
        speed = 0;
        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            yield return null;
        }
        stunned = false;
        speed = baseSpeed;
        if (slowed)
        {
            speed /= 2;
        }
    }

    void moveObjectBySpeed(Vector3 source, Vector3 target)
    {
        StartCoroutine(moveObject(source, target));
    }

    IEnumerator moveObject(Vector3 source, Vector3 target)
    {
        while ((target - source).magnitude > 0.1f)
        {
            transform.position += (target - source) * Time.deltaTime * speed * speed_scale_factor;
            yield return null;
        }
        transform.position = target;
    }
}
