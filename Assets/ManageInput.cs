using UnityEngine;
using System.Collections;

public class ManageInput : MonoBehaviour {

    private SteamVR_Controller.Device device;
    private SteamVR_TrackedObject tracked_object;
    private GameManager game_manager;

	// Use this for initialization
	void Start () {
        game_manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        tracked_object = GetComponent<SteamVR_TrackedObject>();
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
