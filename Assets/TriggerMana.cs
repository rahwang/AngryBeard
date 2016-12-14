using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TriggerMana : MonoBehaviour {

    public GameObject camera_rig;
    public GameObject manaSphere;
    public GameObject spellBook;

    private SteamVR_Controller.Device device;
    private SteamVR_TrackedObject tracked_object;

    private UnityAction manaEmptyListener;

    void Awake()
    {
        manaEmptyListener = new UnityAction(ManaEmpty);
    }

    void OnEnable()
    {
        EventManager.StartListening("ManaEmpty", manaEmptyListener);
    }

    void OnDisable()
    {
        EventManager.StopListening("ManaEmpty", manaEmptyListener);
    }

    // Use this for initialization
    void Start () {
        tracked_object = GetComponent<SteamVR_TrackedObject>();
        spellBook.SetActive(false);
    }

    void ManaEmpty()
    {
        GetComponent<AudioSource>().Play();
        SteamVR_Controller.Input((int)camera_rig.GetComponent<SteamVR_ControllerManager>().leftIndex).TriggerHapticPulse((ushort)3999);
    }

    void SpellbookEnable()
    {
        //manaSphere.SetActive(false);
        spellBook.SetActive(true);
    }

    void SpellbookDisable()
    {
        //manaSphere.SetActive(true);
        spellBook.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        device = SteamVR_Controller.Input((int)tracked_object.index);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("turn on spell");
            SpellbookEnable();
        }
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("turn off spell");
            SpellbookDisable();
        }
    }
}
