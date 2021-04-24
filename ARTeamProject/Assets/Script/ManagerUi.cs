using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ManagerUi : MonoBehaviour
{
    private ManagerWeaponChange mngrWeaponChange;
    // Start is called before the first frame update
    void Start()
    {
        mngrWeaponChange = GameObject.Find("Weapon").GetComponent<ManagerWeaponChange>();   
    }

    public void ButtonWeaponChange()
    {
        GameObject tempBtn = EventSystem.current.currentSelectedGameObject;
        int tempBtnIndex = tempBtn.transform.GetSiblingIndex();
        mngrWeaponChange.ChangeWeapon(tempBtnIndex);
        //Debug.Log(tempBtn + ":" + tempBtnIndex);

    }


}
