using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerWeaponChange : MonoBehaviour
{
    private ManagerWeapon mngrWeapon;
    public Transform pivotR;
    private int indexPreviousWeapon;
    // Start is called before the first frame update
    void Start()
    {
        mngrWeapon = GameObject.Find("ManagerWeapon").GetComponent<ManagerWeapon>();
        GameObject tempDefaultWeapon = mngrWeapon.weapons[0];
        Instantiate(tempDefaultWeapon, pivotR);
        indexPreviousWeapon = 0;
    }

    public void ChangeWeapon(int weaponIndex)
    {
        if(weaponIndex != indexPreviousWeapon) 
        {
            Destroy(pivotR.GetChild(0).gameObject);
            GameObject tempWeapon = mngrWeapon.weapons[weaponIndex];
            Instantiate(tempWeapon, pivotR);
            indexPreviousWeapon = weaponIndex;


        }
        
    }

   
}
