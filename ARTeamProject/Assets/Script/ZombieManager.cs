using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;



public class ZombieManager : MonoBehaviour
{
    public GameObject objectToInstantiate;
    private GameObject spawned;

    private ARPlaneManager planeManager;
    private ARPlane selectedPlane;

    private Animator animator;
    public AudioSource FallingSound;
    public AudioSource ZombieSound;

    private bool isSpawned = false;

    private float waitingTime = 10.0f;

    void Start()
    {
        planeManager = GetComponent<ARPlaneManager>();
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitingTime);

            //When there is no Zombie.
            if (!isSpawned && planeManager.trackables.count > 0)
            {
                int RandomNum = Random.Range(0, planeManager.trackables.count);
                int count = 0;

                foreach (ARPlane plane in planeManager.trackables)
                {
                    if (count == RandomNum)
                    {
                        selectedPlane = plane;
                    }
                    count += 1;
                }

                spawned = Instantiate(objectToInstantiate, selectedPlane.transform.position, selectedPlane.transform.rotation);
                isSpawned = true;
                ZombieSound.Play();

                waitingTime = Random.Range(8.0f, 12.0f);
            }
        }
    }

    void Update()
    {
        //When there is Zombie.
        if (isSpawned)
        {
            spawned.transform.LookAt(Camera.main.transform.position);
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Zombie" && hit.collider.enabled == true)
                    {
                        animator = hit.transform.GetComponent<Animator>();
                        animator.SetBool("IsFalling", true);
                        hit.collider.enabled = false;
                        Destroy(hit.collider.gameObject, 3);
                        isSpawned = false;
                        FallingSound.Play();
                    }
                }
            }
        }
    }
}