using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;



public class MonsterManager : MonoBehaviour
{
    public GameObject objectToInstantiate;
    private int i;
    private GameObject spawned;

    private ARPlaneManager planeManager;
    private ARPlane selectedPlane;

    private Animator animator;
    public AudioSource SpawnSound;
    public AudioSource FallingSound;

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
                    count++;
                }

                i = Random.Range(0, 2);

                spawned = Instantiate(objectToInstantiate, selectedPlane.transform.position, selectedPlane.transform.rotation);
                isSpawned = true;
                SpawnSound.Play();

                waitingTime = Random.Range(8.0f, 12.0f);
            }
        }
    }

    void Update()
    {
        if (isSpawned)
        {
            float step = 3.0f * Time.deltaTime;
            spawned.transform.LookAt(Camera.main.transform.position);
            spawned.transform.position = Vector3.MoveTowards(spawned.transform.position, Camera.main.transform.position, step);
        }

        if (isSpawned)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Clown" && hit.collider.enabled == true)
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