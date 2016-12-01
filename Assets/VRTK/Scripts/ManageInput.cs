using UnityEngine;
using UnityEngine.Events;
using VRTK;
using System.Collections;

public class ManageInput : MonoBehaviour {

    private SteamVR_Controller.Device device;
    private SteamVR_TrackedObject tracked_object;
    private GameManager game_manager;

    private UnityAction pointerOnListener;
    private UnityAction pointerOffListener;

    private VRTK_SimplePointer pointer_script;

	// Use this for initialization
	void Start () {
        pointer_script = GetComponent<VRTK_SimplePointer>();
        game_manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        tracked_object = GetComponent<SteamVR_TrackedObject>();
	}


    void Awake()
    {
        pointerOnListener = new UnityAction(TurnPointerOn);
        pointerOffListener = new UnityAction(TurnPointerOff);
    }

    void OnEnable()
    {
        EventManager.StartListening("AimModeEnable", pointerOnListener);
        EventManager.StartListening("AimModeDisable", pointerOffListener);
    }

    void OnDisable()
    {
        EventManager.StopListening("AimModeEnable", pointerOnListener);
        EventManager.StopListening("AimModeDisable", pointerOffListener);
    }

    void TurnPointerOn()
    {

        // set range indicator scale
        SpellManager.Element spell = game_manager.spellManager.currElement;

        switch (spell)
        {
            case SpellManager.Element.Fire:
                pointer_script.pointerTipScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;
            case SpellManager.Element.Earth:
                pointer_script.pointerTipScale = new Vector3(10.0f, 10.0f, 10.0f);
                break;
            case SpellManager.Element.Lightning:
                pointer_script.pointerTipScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;
            case SpellManager.Element.Frost:
                pointer_script.pointerTipScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;
            default:
                break;
        }

        pointer_script.enabled = true;

        //Debug.Log("aim mode enable was called!");
    }

    void TurnPointerOff()
    {
        pointer_script.enabled = false;
        //Debug.Log("aim mode disable was called!");
    }

    // Update is called once per frame
    void Update () {
        device = SteamVR_Controller.Input((int)tracked_object.index);
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            game_manager.TriggerSpellUI(transform);
            device.TriggerHapticPulse(700);
            //Debug.Log("trigger pressed :D " + transform.position.x);
        }
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            game_manager.UntriggerSpellUI();
        }
    }
}
