using UnityEngine;
using System.Collections;

public class TriggerMana : MonoBehaviour {

    public GameObject manaSphere;
    public GameObject spellBook;

    private SteamVR_Controller.Device device;
    private SteamVR_TrackedObject tracked_object;

    // Use this for initialization
    void Start () {
        tracked_object = GetComponent<SteamVR_TrackedObject>();
        spellBook.SetActive(false);
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
