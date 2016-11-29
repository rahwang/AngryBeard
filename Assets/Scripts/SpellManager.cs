using UnityEngine;
using System.Collections;

public class SpellManager : MonoBehaviour {

    public enum Element
    {
        Fire,
        Earth,
        Lightning,
        Frost,
        None
    };

    public Element currElement;


    // Use this for initialization
    void Start() {
        currElement = Element.None;
    }

    // Update is called once per frame
    void Update() {

    }

    public void loadSpell(string spell)
    {
        switch (spell)
        {
            case "ACDBC": currElement = Element.Fire;
                break;
            case "ABCDA": currElement = Element.Earth;
                break;
            case "ADBC": currElement = Element.Lightning;
                break;
            case "ACDB": currElement = Element.Frost;
                break;
            default: currElement = Element.None;
                break;
        }
        Debug.Log("current spell is " + currElement);
    }
}
