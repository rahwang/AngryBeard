using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class GameManager : MonoBehaviour {
    public bool playing;

    public GameObject SpellUI_prefab;
    public Transform headset_trans;

    private GameObject current_UI;
    private string current_cast_string;
    public int level = 1;

    public SpellManager spellManager;
    bool aimMode = false;

	private UnityAction someListener;

	// Use this for initialization
	void Start () {
        current_cast_string = "";
        spellManager = GameObject.Find("SpellManager").GetComponent<SpellManager>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (!playing) {
            // LOSE CONDITION
        }
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
		//Debug.Log ("Some Function was called!");
	}

	void SomeFunction ()
	{
		//Debug.Log ("Some Other Function was called!");
	}

    public void StartAimMode()
    {
        aimMode = true;
        // Aiming
        //Debug.Log("Aim mode enabled");
        EventManager.TriggerEvent("AimModeEnable");
        current_cast_string = "";
        Destroy(current_UI);
        //START POINTER

    }

    public void TriggerSpellUI(Transform trans)
    {
            Debug.Log("trigger spell ui");
            // Instantiate spell ui
            Quaternion spawn_rot = Quaternion.LookRotation(new Vector3(0, 3.8f, 0));
            current_UI = (GameObject)Instantiate(SpellUI_prefab, trans.position, headset_trans.rotation);
            Debug.Log(name);
    }

    public void UntriggerSpellUI()
    {
        if (aimMode)
        {
            //STOP POINTER
            //FIRE
            Debug.Log("Fire Spell: " + spellManager.currElement);
            EventManager.TriggerEvent("AimModeDisable");
            spellManager.currElement = SpellManager.Element.None;
            aimMode = false;
        }
        else
        {
            Destroy(current_UI);
            current_cast_string = "";
        }
    }


    public void InstanceSpellHit(Vector3 hit_point)
    {
        Vector3 spawn_pos = hit_point;
        //Quaternion spawn_rot = Quaternion.LookRotation(-(spawn_pos));
        Quaternion spawn_rot = spellManager.getCurrentSpellRotation();
        GameObject spell_prefab = spellManager.getCurrentSpellEffect();
        GameObject spell = (GameObject) Instantiate(spell_prefab, spawn_pos, spawn_rot);
        spell.GetComponent<SpellHitEffect>().type = spellManager.currElement;
        spell.transform.localScale = spellManager.getCurrentSpellRadius();

    }

    public void RegisterCastPoint(string name)
    {
        if ( name.CompareTo("CastPoint (1)") == 0)
        {
            current_cast_string += "A";
        }
        else if (name.CompareTo("CastPoint (2)") == 0)
        {
            current_cast_string += "B";
        }
        else if (name.CompareTo("CastPoint (3)") == 0)
        {
            current_cast_string += "C";
        }
        else if (name.CompareTo("CastPoint (4)") == 0)
        {
            current_cast_string += "D";
        }
        EventManager.TriggerEvent("HapticPing");
        if (spellManager.loadSpell(current_cast_string))
        {
            StartAimMode();
        }
        Debug.Log(current_cast_string);
    }
}
