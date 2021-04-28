using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{
    public GameObject Flame, Light, Electric;
    // Start is called before the first frame update
    void Start()
    {
        /*Flame = GameObject.FindWithTag("Flame").GetComponent<Weapon>();
        Light = GameObject.FindWithTag("Light").GetComponent<Weapon>(); ;
        Electric = GameObject.FindWithTag("Electric").GetComponent<Weapon>();*/

    }

    public void FireChange()
    {
        Flame.gameObject.SetActive(true);
        Light.gameObject.SetActive(false);
        Electric.gameObject.SetActive(false);
    }
    public void LightChange()
    {
        Flame.gameObject.SetActive(false);
        Light.gameObject.SetActive(true);
        Electric.gameObject.SetActive(false);
    }

    public void ElectricChange()
    {
        Flame.gameObject.SetActive(false);
        Light.gameObject.SetActive(false);
        Electric.gameObject.SetActive(true);
    }


}
