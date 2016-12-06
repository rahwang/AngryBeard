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
    private UnityAction hapticPing;
    private UnityAction hapticPulse;

    private VRTK_ControllerActions controller_actions;
    private VRTK_ControllerEvents controller_events;

    private VRTK_SimplePointer pointer_script;
    private SteamVR_ControllerManager controller_manager;

    public GameObject camera_rig;
    public bool isLeft;

    private float impactMagnifier = 120f;

    // Use this for initialization
    void Start () {
        pointer_script = GetComponent<VRTK_SimplePointer>();
        game_manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        tracked_object = GetComponent<SteamVR_TrackedObject>();
        controller_manager = camera_rig.GetComponent<SteamVR_ControllerManager>();
        controller_events = GetComponent<VRTK_ControllerEvents>();
        controller_actions = GetComponent<VRTK_ControllerActions>();
	}


    void Awake()
    {
        pointerOnListener = new UnityAction(TurnPointerOn);
        pointerOffListener = new UnityAction(TurnPointerOff);
        hapticPing = new UnityAction(HapticPing);
        hapticPulse = new UnityAction(HapticPulse);
    }

    void OnEnable()
    {
        EventManager.StartListening("AimModeEnable", pointerOnListener);
        EventManager.StartListening("AimModeDisable", pointerOffListener);
        EventManager.StartListening("HapticPing", hapticPing);
        EventManager.StartListening("HapticPulse", hapticPulse);
    }

    void OnDisable()
    {
        EventManager.StopListening("AimModeEnable", pointerOnListener);
        EventManager.StopListening("AimModeDisable", pointerOffListener);
        EventManager.StopListening("HapticPing", hapticPing);
        EventManager.StopListening("HapticPulse", hapticPulse);
    }

    void HapticPulse(float collisionForce)
    {
        controller_actions.TriggerHapticPulse((ushort)collisionForce, 0.5f, 0.01f);
    }

    void HapticPulse()
    {
        controller_actions.TriggerHapticPulse((ushort)3000, 0.5f, 0.01f);
    }



    void HapticPing()
    {
        if (isLeft)
        {
            SteamVR_Controller.Input((int)camera_rig.GetComponent<SteamVR_ControllerManager>().leftIndex).TriggerHapticPulse((ushort)3999);
        }
        else
        {
            SteamVR_Controller.Input((int)camera_rig.GetComponent<SteamVR_ControllerManager>().rightIndex).TriggerHapticPulse((ushort)3999);
        }
    }

    void TurnPointerOn()
    {

        // set range indicator scale
        SpellManager.Element spell = game_manager.spellManager.currElement;

        HapticPulse(3000);

        Debug.Log("Now enabling pointer ~~~~~~~~");
        pointer_script.pointerTipScale = game_manager.spellManager.getCurrentSpellRadius();

        pointer_script.enabled = true;

        //Debug.Log("aim mode enable was called!");
    }

    void TurnPointerOff()
    {
        // Store last pointer tip position
        Vector3 hit_pos = pointer_script.pointerTip.transform.position;
        game_manager.InstanceSpellHit(hit_pos);
        HapticPulse(3000);
        pointer_script.enabled = false;
        //Debug.Log("aim mode disable was called!");
    }

    // Update is called once per frame
    void Update () {
        device = SteamVR_Controller.Input((int)tracked_object.index);
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            game_manager.TriggerSpellUI(transform);
            //Debug.Log("trigger pressed :D " + transform.position.x);
        }
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            game_manager.UntriggerSpellUI();
        }
    }
}
