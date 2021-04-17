using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private GameObject Flame, Light;
    public GameObject Electric;
    [SerializeField] private ParticleSystem m_FlameThrower, m_LightThrower;
    public float range = 10000;

    void Start()
    {
        m_FlameThrower.Stop();
        m_LightThrower.Stop();
        Flame = GameObject.FindWithTag("Flame");
        Light = GameObject.FindWithTag("Light");
        Electric.SetActive(false);
    }

    void Stop()
    {
        m_FlameThrower.Stop();
        m_LightThrower.Stop();
        Electric.SetActive(false);
    }

    //Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Input.GetMouseButtonDown(0))
        {
            if(Flame&&Flame.activeSelf)
            {
                m_FlameThrower.Play();
                Invoke("Stop", 2);
            }
            if (Light && Light.activeSelf)
            {
                m_LightThrower.Play();
                Invoke("Stop", 2);
            }
            if (Electric && !Electric.activeSelf)
            {
                Electric.SetActive(true);
                Invoke("Stop", 2);
            }
        }

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Shootable")
            {
                Debug.Log("Hit the bear");
            }
        }

    }
}
