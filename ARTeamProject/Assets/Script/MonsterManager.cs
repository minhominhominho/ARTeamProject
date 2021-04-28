using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;



public class MonsterManager : MonoBehaviour
{
    public List <GameObject> Monsters;
    private int i;
    private GameObject spawned;
    private GameObject Flame, Light, Electric;

    private ARPlaneManager planeManager;
    private ARPlane selectedPlane;

    private Animator animator;
    public List <AudioSource> SpawnSound;
    public List <AudioSource> FallingSound;
    public AudioSource HurtSound;

    private bool isSpawned = false;
    private bool isRunning = false;
    private bool isHit = false;


    private float waitingTime = 10.0f;

    void Start()
    {
        planeManager = GetComponent<ARPlaneManager>();
        Flame = GameObject.FindWithTag("Flame");
        Light = GameObject.FindWithTag("Light");
        Electric = GameObject.FindWithTag("Electric");

        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitingTime);

            //When there is no monster.
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

                i = Random.Range(0, 3);
                if (Vector3.Distance(selectedPlane.transform.position, Camera.main.transform.position) >= 1.5f)
                {
                    spawned = Instantiate(Monsters[i], selectedPlane.transform.position, selectedPlane.transform.rotation);
                    spawned.transform.LookAt(Camera.main.transform.position);
                    isSpawned = true;
                    Invoke("StartToRun", 3.5f);
                    SpawnSound[i].Play();

                    waitingTime = Random.Range(8.0f, 12.0f);
                }
            }
        }
    }

    void Update()
    {
        if (isSpawned && !isHit && isRunning)
        {
            float step = 0.2f * Time.deltaTime;
            spawned.transform.LookAt(Camera.main.transform.position);
            spawned.transform.position = Vector3.MoveTowards(spawned.transform.position, Camera.main.transform.position, step);
        }

        if (isSpawned && !isHit && isRunning && Vector3.Distance(spawned.transform.position, Camera.main.transform.position) <= 0.5f)
        {
            HurtSound.Play();
            Destroy(spawned);
            isSpawned = false;
            isRunning = false;
            //여기에 하트 깎이는거 추가
        }
    }

    void StartToRun()
    {
        isRunning = true;
    }

    private void FixedUpdate()
    {
        if (isSpawned && !isHit)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0.0f));
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 3.0f))
                {
                    if (hit.collider.tag == "Clown" && hit.collider.enabled == true && Light && Light.activeSelf)
                    {
                        isHit = true;
                        hit.collider.enabled = false;
                        animator = hit.transform.GetComponent<Animator>();
                        animator.SetBool("IsFalling", true);
                        Destroy(hit.collider.gameObject, 3);
                        isSpawned = false;
                        FallingSound[i].Play();
                        isHit = false;
                        isRunning = false;
                    }
                    else if (hit.collider.tag == "Parasite" && hit.collider.enabled == true && Electric && Electric.activeSelf)
                    {
                        isHit = true;
                        hit.collider.enabled = false;
                        animator = hit.transform.GetComponent<Animator>();
                        animator.SetBool("IsFalling", true);
                        Destroy(hit.collider.gameObject, 3);
                        isSpawned = false;
                        FallingSound[i].Play();
                        isHit = false;
                        isRunning = false;
                    }
                    else if (hit.collider.tag == "Zombie" && hit.collider.enabled == true && Flame && Flame.activeSelf)
                    {
                        isHit = true;
                        hit.collider.enabled = false;
                        animator = hit.transform.GetComponent<Animator>();
                        animator.SetBool("IsFalling", true);
                        Destroy(hit.collider.gameObject, 3);
                        isSpawned = false;
                        FallingSound[i].Play();
                        isHit = false;
                        isRunning = false;
                    }
                }
            }
        }
    }
}