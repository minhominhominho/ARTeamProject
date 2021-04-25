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
        
        // 일단 터치를 인식받는다. 
        if (Input.GetMouseButtonDown(0))
        {
            // Flame객체가 SetActive(true)면
            if(Flame&&Flame.activeSelf)
            {
                // Flame particle 2초간 발사
                m_FlameThrower.Play();
                Invoke("Stop", 2);
            }
            // Light객체가 SetActive(true)면 
            if (Light && Light.activeSelf)
            {
                // Light particle 2초간 발사
                m_LightThrower.Play();
                Invoke("Stop", 2);
            }
            // Electric객체가 SetActive(true)면
            if (Electric && Electric.activeSelf)
            {
                // Electric particle 2초간 발사
                ElectricParticle.SetActive(true);
                Invoke("Stop", 2);
            }
        }
    }
}
