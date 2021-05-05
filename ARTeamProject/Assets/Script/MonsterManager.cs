using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;



public class MonsterManager : MonoBehaviour
{
    public List <GameObject> Monsters;

    private int i;
    private GameObject spawned;
    private GameObject Flame, Light, Electric;
    public GameObject GameoverScreen;

    private ARPlaneManager planeManager;
    private ARPlane selectedPlane;

    private Animator animator;
    public List <AudioSource> SpawnSound;
    public List <AudioSource> FallingSound;
    public AudioSource HurtSound;

    private bool isSpawned = false;
    private bool isRunning = false;
    private bool isHit = false;
    private bool isPlaying = true;

    private int killCount = 0;
    private int HurtCount = 0;

    public Vector3 monsterPosition;


    void Start()
    {
        planeManager = GetComponent<ARPlaneManager>();
        Flame = GameObject.FindWithTag("Flame");
        Light = GameObject.FindWithTag("Light");
        Electric = GameObject.FindWithTag("Electric");
        GameoverScreen.SetActive(false);
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            //When there is no monster.
            if (!isSpawned && planeManager.trackables.count > 0 && isPlaying)
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

                
                if (Vector3.Distance(selectedPlane.transform.position, Camera.main.transform.position) >= 2.0f)
                {
                    monsterPosition = selectedPlane.transform.position;
                    isSpawned = true;
                    Invoke("SpawnPrefabinCondition", Random.Range(3.0f,5.0f));
                    Invoke("StartToRun", 3.5f);
                }
            }
        }
    }

    public void SpawnPrefabinCondition()
    {
        i = Random.Range(0, 3);
        spawned = Instantiate(Monsters[i], monsterPosition, selectedPlane.transform.rotation);
        spawned.transform.LookAt(Camera.main.transform.position);

        Invoke("StartToRun", 3.5f);
        SpawnSound[i].Play();

        isSpawned = true;
    }

    void Update()
    {
        if (isSpawned && !isHit && isRunning && isPlaying)
        {
            animator = spawned.transform.GetComponent<Animator>();
            if (animator.GetCurrentAnimatorStateInfo(0).nameHash == Animator.StringToHash("Base Layer.Running"))
            {
                float step = 0.4f * Time.deltaTime;
                spawned.transform.LookAt(Camera.main.transform.position);
                spawned.transform.position = Vector3.MoveTowards(spawned.transform.position, Camera.main.transform.position, step);
            }
        }

        if (isSpawned && !isHit && isRunning && isPlaying && Vector3.Distance(spawned.transform.position, Camera.main.transform.position) <= 0.5f)
        {
            HurtSound.Play();
            Destroy(spawned);
            isSpawned = false;
            isRunning = false;

            if (HurtCount < 3)
            {
                HurtCount++;
                Destroy(GameObject.FindWithTag("Heart"));
            }
            
            if(HurtCount == 3)
            {
                Setup(killCount);
            }    
        }
    }

    


    public void Setup(int score)
    {
        Flame.SetActive(false);
        Light.SetActive(false);
        Electric.SetActive(false);
        isPlaying = false;
        GameoverScreen.SetActive(true);
        GameObject scoretext = GameObject.FindWithTag("Score");
        Text pointText = scoretext.GetComponent<Text>();
        pointText.text = score + " POINTS";
    }

    void StartToRun()
    {
        isRunning = true;
    }

    private void FixedUpdate()
    {
        if (isSpawned && !isHit && isPlaying)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0.0f));
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 5.0f))
                {
                    if (hit.collider.tag == "Clown" && hit.collider.enabled == true && Light && Light.activeSelf)
                    {
                        animator = hit.transform.GetComponent<Animator>();
                        if (animator.GetCurrentAnimatorStateInfo(0).nameHash == Animator.StringToHash("Base Layer.Running"))
                        {
                            isHit = true;
                            hit.collider.enabled = false;
                            killCount++;

                            animator.SetBool("IsFalling", true);
                            Destroy(hit.collider.gameObject, 3);
                            
                            FallingSound[i].Play();
                            isHit = false;
                            isRunning = false;
                            isSpawned = false;
                        }
                    }
                    else if (hit.collider.tag == "Parasite" && hit.collider.enabled == true && Electric && Electric.activeSelf)
                    {
                        animator = hit.transform.GetComponent<Animator>();
                        if (animator.GetCurrentAnimatorStateInfo(0).nameHash == Animator.StringToHash("Base Layer.Running"))
                        {
                            isHit = true;
                            hit.collider.enabled = false;
                            killCount++;

                            animator.SetBool("IsFalling", true);
                            Destroy(hit.collider.gameObject, 3);
                            
                            FallingSound[i].Play();
                            isHit = false;
                            isRunning = false;
                            isSpawned = false;
                        }
                    }
                    else if (hit.collider.tag == "Zombie" && hit.collider.enabled == true && Flame && Flame.activeSelf)
                    {
                        animator = hit.transform.GetComponent<Animator>();
                        if (animator.GetCurrentAnimatorStateInfo(0).nameHash == Animator.StringToHash("Base Layer.Running"))
                        {
                            isHit = true;
                            hit.collider.enabled = false;
                            killCount++;

                            animator.SetBool("IsFalling", true);
                            Destroy(hit.collider.gameObject, 3);
                            
                            FallingSound[i].Play();
                            isHit = false;
                            isRunning = false;
                            isSpawned = false;
                        }
                    }
                }
            }
        }
    }
}