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
                result *= 5;
                break;
            case SpellManager.Element.Earth:
                result *= 10;
                break;
            case SpellManager.Element.Lightning:
                result *= 3;
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

    public bool loadSpell(string spell)
    {
        switch (spell)
        {
            case "ACDBC": currElement = Element.Fire;
                if (gameManager.mana < 10) {
                    return false;
                }
                gameManager.mana -= 10;
                return true;
            case "ABCDA": currElement = Element.Earth;
                if (gameManager.mana < 10)
                {
                    return false;
                }
                gameManager.mana -= 10;
                return true;
            case "ADBC": currElement = Element.Lightning;
                if (gameManager.mana < 10)
                {
                    return false;
                }
                gameManager.mana -= 10;
                return true;
            case "DBCD": currElement = Element.Frost;
                if (gameManager.mana < 10)
                {
                    return false;
                }
                gameManager.mana -= 10;
                return true;
            default: currElement = Element.None;
                return false;
        }
    }
}
