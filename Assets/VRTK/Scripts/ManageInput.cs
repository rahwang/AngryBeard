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
    private UnityAction trailOnListener;
    private UnityAction trailOffListener;

    private VRTK_ControllerActions controller_actions;
    private VRTK_ControllerEvents controller_events;

    private VRTK_SimplePointer pointer_script;
    private SteamVR_ControllerManager controller_manager;
    private TrailRenderer trail_renderer;

    public GameObject camera_rig;
    public GameObject cast_fx;

    public AudioClip spell_fire_sound;
    public AudioClip spell_load_sound;

    private float impactMagnifier = 120f;

    // Use this for initialization
    void Start () {
        pointer_script = GetComponent<VRTK_SimplePointer>();
        game_manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        tracked_object = GetComponent<SteamVR_TrackedObject>();
        controller_manager = camera_rig.GetComponent<SteamVR_ControllerManager>();
        controller_events = GetComponent<VRTK_ControllerEvents>();
        controller_actions = GetComponent<VRTK_ControllerActions>();
        trail_renderer = GetComponent<TrailRenderer>();

        cast_fx.SetActive(false);
    }


    void Awake()
    {
        pointerOnListener = new UnityAction(TurnPointerOn);
        pointerOffListener = new UnityAction(TurnPointerOff);
        hapticPing = new UnityAction(HapticPing);
        hapticPulse = new UnityAction(HapticPulse);
        trailOnListener = new UnityAction(TurnTrailOn);
        trailOffListener = new UnityAction(TurnTrailOff);
    }

    void OnEnable()
    {
        EventManager.StartListening("AimModeEnable", pointerOnListener);
        EventManager.StartListening("AimModeDisable", pointerOffListener);
        EventManager.StartListening("HapticPing", hapticPing);
        EventManager.StartListening("HapticPulse", hapticPulse);
        EventManager.StartListening("EnableTrail", trailOnListener);
        EventManager.StartListening("DisableTrail", trailOffListener);
    }

    void OnDisable()
    {
        EventManager.StopListening("AimModeEnable", pointerOnListener);
        EventManager.StopListening("AimModeDisable", pointerOffListener);
        EventManager.StopListening("HapticPing", hapticPing);
        EventManager.StopListening("HapticPulse", hapticPulse);
        EventManager.StopListening("EnableTrail", trailOnListener);
        EventManager.StopListening("DisableTrail", trailOffListener);
    }

    void HapticPulse(float collisionForce)
    {
        controller_actions.TriggerHapticPulse((ushort)collisionForce, 0.5f, 0.01f);
    }

    void HapticPulse()
    {
        controller_actions.TriggerHapticPulse((ushort)3000, 0.5f, 0.01f);
    }

    void TurnTrailOn()
    {
         trail_renderer.enabled = true; 
    }

    void TurnTrailOff()
    {
        trail_renderer.enabled = false;
    }

    void HapticPing()
    {
            SteamVR_Controller.Input((int)camera_rig.GetComponent<SteamVR_ControllerManager>().rightIndex).TriggerHapticPulse((ushort)3999);
    }

    void TurnPointerOn()
    {

        // set range indicator scale
        SpellManager.Element spell = game_manager.spellManager.currElement;

        HapticPulse(3000);

        Debug.Log("Now enabling pointer ~~~~~~~~");
        pointer_script.pointerTipScale = game_manager.spellManager.getCurrentSpellRadius();

        pointer_script.enabled = true;
        GetComponent<AudioSource>().clip = spell_load_sound;
        GetComponent<AudioSource>().Play();

        cast_fx.SetActive(true);

        //Debug.Log("aim mode enable was called!");
    }

    void TurnPointerOff()
    {
        // Store last pointer tip position
        Vector3 hit_pos = pointer_script.pointerTip.transform.position;
        GetComponent<AudioSource>().clip = spell_fire_sound;
        GetComponent<AudioSource>().Play();
        game_manager.InstanceSpellHit(hit_pos);
        HapticPulse(3000);
        pointer_script.enabled = false;
        cast_fx.SetActive(false);
        //Debug.Log("aim mode disable was called!");
    }

    // Update is called once per frame
    void Update () {
        device = SteamVR_Controller.Input((int)tracked_object.index);
        if (transform.position.y < game_manager.headset_trans.position.y)
        {
            game_manager.mana_charging_on = false;
        } else if (!game_manager.mana_charging_on)
        {
            game_manager.mana_charging_on = true;
            StartCoroutine(ChargeMana());
        }
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && !game_manager.mana_charging_on)
        {
            game_manager.TriggerSpellUI(transform);
           
            //Debug.Log("trigger pressed :D " + transform.position.x);
        }
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            game_manager.UntriggerSpellUI();
        }
    }

    public IEnumerator ChargeMana()
    {
        Vector3 prev_pos = transform.position;
        // STOP COROUTINE WHEN LET GO OR LOWER WAND
        while (game_manager.mana < 100 && game_manager.mana_charging_on)
        {
            float delta_pos = (transform.position - prev_pos).magnitude;
            game_manager.mana += delta_pos * 10;
            float offset = 0.2f - (game_manager.mana / 100.0f) * 0.4f;
            GameObject.Find("ManaSphere").GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0, offset));
            // replace with haptic pulse
            prev_pos = transform.position;
            Debug.Log("delta_pos = " + delta_pos);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
