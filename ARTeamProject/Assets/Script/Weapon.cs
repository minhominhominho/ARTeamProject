using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private GameObject Flame, Light, Electric;
    [SerializeField] private ParticleSystem m_FlameThrower, m_LightThrower;
    public GameObject ElectricParticle;
    public List<AudioSource> WeaponSound;

    void Start()
    {
        // 객체들 받아오기
        Flame = GameObject.FindWithTag("Flame");
        Light = GameObject.FindWithTag("Light");
        Electric = GameObject.FindWithTag("Electric");
        // 객체 SetActive(false)하기
        Flame.SetActive(false);
        Light.SetActive(false);
        Electric.SetActive(false);
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
        
        // 터치 && 파티클들이 플레이 되고 있지 않을 때 
        if (Input.GetMouseButtonUp(0)&&!m_FlameThrower.isPlaying &&!m_LightThrower.isPlaying&&!ElectricParticle.activeSelf)
        {
            // Flame객체가 SetActive(true)면
            if(Flame&&Flame.activeSelf)
            {
                // Flame particle 2초간 발사
                m_FlameThrower.Play();
                WeaponSound[0].Play();
                Invoke("Stop", 1);
            }
            // Light객체가 SetActive(true)면 
            if (Light && Light.activeSelf)
            {
                // Light particle 2초간 발사
                m_LightThrower.Play();
                WeaponSound[1].Play();
                Invoke("Stop", 1);
            }
            // Electric객체가 SetActive(true)면
            if (Electric && Electric.activeSelf)
            {
                // Electric particle 2초간 발사
                ElectricParticle.SetActive(true);
                WeaponSound[2].Play();
                Invoke("Stop", 1);
            }
        }
    }

    public void FireChange()
    {
        Stop();
        Flame.gameObject.SetActive(true);
        Light.gameObject.SetActive(false);
        Electric.gameObject.SetActive(false);
    }

    public void LightChange()
    {
        Stop();
        Flame.gameObject.SetActive(false);
        Light.gameObject.SetActive(true);
        Electric.gameObject.SetActive(false);
    }

    public void ElectricChange()
    {
        Stop();
        Flame.gameObject.SetActive(false);
        Light.gameObject.SetActive(false);
        Electric.gameObject.SetActive(true);
    }
}
