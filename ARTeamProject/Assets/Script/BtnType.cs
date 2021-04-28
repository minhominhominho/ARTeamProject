using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class BtnType : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    //handle main menu button

    public BTNType currentType;
    public Transform buttonScale;
    Vector3 defaultSize;
    private void Start()
    {
        defaultSize = buttonScale.localScale;
    }

    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BTNType.Start:
                Debug.Log("Start");
                break;

            case BTNType.Option:
                Debug.Log("option");
                break;

            case BTNType.Quit:
                Debug.Log("quit");
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultSize * 1.2f;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultSize;
    }
}

