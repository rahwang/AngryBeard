using UnityEngine;
using System.Collections;

public class SpellManager : MonoBehaviour {

    public GameObject fire_effect;
    public GameObject frost_effect;
    public GameObject earth_effect;
    public GameObject lightning_effect;
    public GameManager gameManager;

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
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update() {

    }

    public Vector3 getCurrentSpellRadius()
    {
        Vector3 result = Vector3.one;
        switch (currElement)
        {
            case SpellManager.Element.Fire:
                result *= 3;
                break;
            case SpellManager.Element.Earth:
                result *= 10;
                break;
            case SpellManager.Element.Lightning:
                result = new Vector3 (1, 3, 15);
                break;
            case SpellManager.Element.Frost:
                result *= 5;
                break;
            default:
                break;
        }
        return result;
    }

    public Quaternion getCurrentSpellRotation()
    {
        Quaternion result = Quaternion.identity;
        switch (currElement)
        {
            case SpellManager.Element.Fire:
                break;
            case SpellManager.Element.Earth:
                break;
            case SpellManager.Element.Lightning:
                break;
            case SpellManager.Element.Frost:
                break;
            default:
                break;
        }
        return result;
    }

    public GameObject getCurrentSpellEffect()
    {
        GameObject result = fire_effect;
        switch (currElement)
        {
            case SpellManager.Element.Fire:
                result = fire_effect;
                break;
            case SpellManager.Element.Earth:
                result = earth_effect;
                break;
            case SpellManager.Element.Lightning:
                result = lightning_effect;
                break;
            case SpellManager.Element.Frost:
                result = frost_effect;
                break;
            default:
                break;
        }
        return result;
    }

    // if sufficent mana, subtract given mana cost from pool,
    // else return false.
    bool checkSufficientMana(int manaCost)
    {
        if (gameManager.mana < manaCost)
        {
            EventManager.TriggerEvent("ManaEmpty");
            return false;
        }
        gameManager.mana -= manaCost;
        return true;
    }

    public bool loadSpell(string spell)
    {
        switch (spell)
        {
            case "ACDBC": currElement = Element.Fire;
                return checkSufficientMana(10);
            case "ABCDA": currElement = Element.Earth;
                return checkSufficientMana(10);
            case "ADBC": currElement = Element.Lightning;
                return checkSufficientMana(10);
            case "DBCD": currElement = Element.Frost;
                return checkSufficientMana(10);
            default: currElement = Element.None;
                return false;
        }
    }
}
