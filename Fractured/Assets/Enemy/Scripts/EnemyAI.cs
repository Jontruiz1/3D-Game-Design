using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    private float step;
    public float speed;
    private GameObject player;
    public Animator animator;

    public float rotationSpeed;

    public List<Transform> positions = new List<Transform>();
    private float waitTime;
    public float startWaitTime;
    private bool chase = false;
    private bool huntDown = false;

    private float j, k;

    public Collider initialArea;
    private Bounds currBound;

    private AudioSource[] sounds;
    private AudioSource growl, alert, footsteps, footstepsFast;
    private Rigidbody rb;

    public AudioSource chaseMusic;
    private void Awake()
    {
        waitTime = startWaitTime;
        player = GameObject.FindWithTag("Player");
        sounds = GetComponentsInChildren<AudioSource>();
        alert = sounds[0];
        growl = sounds[1];
        footsteps = sounds[2];
        footstepsFast = sounds[3];
        currBound = initialArea.bounds;
        //Debug.Log(currBound);
        j = 153.939f;
        k = 102.88f;
    }

    public void changeArea(Collider b)
    {
        currBound = b.bounds;
        waitTime = 0;
    }

    
    private void Update()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, 40.6f, player.transform.position.z);
        if (!chase && !huntDown)
        {
            Vector3 wayPos = new Vector3(j, 40.6f, k);
            chaseMusic.Pause();
            //Debug.Log("Footsteps SHOULD be playing");
            footstepsFast.Play();
            if (Vector3.Distance(transform.position, wayPos) < 2f)
            {
                footstepsFast.Pause();
                if (waitTime <= 0)
                {
                    do {
                        j = Random.Range(currBound.min.x, currBound.max.x);
                        k = Random.Range(currBound.min.z, currBound.max.z);
                    }
                    while(!Physics.CheckSphere(new Vector3(j, 44.0f, k), 0.5f));
                    //Debug.Log("enemy moving");
                    waitTime = startWaitTime;
                }
                else
                {
                    animator.SetBool("IsRunning", false);
                    waitTime -= Time.deltaTime;
                    transform.LookAt(playerPos);
                }
            }
            else
            {
                animator.SetBool("IsRunning", true);
                transform.LookAt(wayPos);
                transform.position = Vector3.MoveTowards(transform.position, wayPos, speed * Time.deltaTime);
            }

        }
        else if (huntDown)
        {
            footstepsFast.Play();
            animator.SetBool("IsRunning", true);
            speed = 7.0f;
            transform.LookAt(playerPos);
            transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player && !huntDown)
        {
            Vector3 playPos = new Vector3(player.transform.position.x, 40.6f, player.transform.position.z);
            footstepsFast.Play();
            speed = 5.0f;
            chase = true;
            animator.SetBool("IsRunning", true);
            if (player.GetComponent<PlayerControls>().moveSpeed >= 20)
            {
                speed = 7.0f;
            }
            step = speed * Time.deltaTime;
            transform.LookAt(player.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, playPos, speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        switch (tag)
        {
            case "Fence":
                chase = false;
                huntDown = false;
                break;
            case "Player":
                if (!huntDown)
                {
                    chaseMusic.Play();
                    footstepsFast.Play();
                    alert.PlayOneShot(alert.clip);
                }
                break;
        }
    }

    public void startHunt()
    {
        huntDown = true;
        growl.PlayOneShot(growl.clip);
        chaseMusic.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player && !huntDown)
        {
            chase = false;
            speed = 5f;
        }
    }
}
