using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private GameObject Flame, Light, Electric;
    [SerializeField] private ParticleSystem m_FlameThrower, m_LightThrower;
    public GameObject ElectricParticle;
    
    void Start()
    {
        // 객체들 받아오기
        Flame = GameObject.FindWithTag("Flame");
        Light = GameObject.FindWithTag("Light");
        Electric = GameObject.FindWithTag("Electric");
        // 각 무기별 총알들 파티클 잠시 꺼두기
        m_FlameThrower.Stop();
        m_LightThrower.Stop();
        ElectricParticle.SetActive(false);
    }

    void Stop()
    {
        m_FlameThrower.Stop();
        m_LightThrower.Stop();
        ElectricParticle.SetActive(false);
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
            if (Electric && Electric.activeSelf)
            {
                ElectricParticle.SetActive(true);
                Invoke("Stop", 2);
            }
        }
    }
}
