using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject cam;
    public GameObject navigator;
    Animator anim;
    Animator camAnim;
    Vector2 move;
    Vector2 rotate;
    public float movespeed;
    float rotationSpeed = 200f;
    private float stunTime;
    public float startStunTime;
    public float freezeTime;
    public float startFreezeTime;
    public bool stunned;
    public bool freezed;
    public bool immune;
    public bool compass = false;
    bool NavStarted = false;

    void Awake()
    {

        anim = GetComponent<Animator>();
        camAnim = cam.GetComponent<Animator>();
        navigator.SetActive(false);
        ResetSpeed();

    }

    void Update()
    {
        if (stunTime <= 0)
        {
            //movespeed = 4f;
            //rotationSpeed = 200f;
            stunned = false;
        }
        else
        {
            //movespeed = 0f;
            //rotationSpeed = 0f;
            stunned = true;
            stunTime -= Time.deltaTime;
        }

        if (freezeTime <= 0)
        {
            freezed = false;
            camAnim.SetBool("Freeze", false);
        }
        else
        {
            freezed = true;
            freezeTime -= Time.deltaTime;
        }
        Move();
        Look();

    }
    private void OnMove(InputValue _value)
    {
        move = _value.Get<Vector2>();


    }

    private void OnLook(InputValue _value)
    {
        rotate = _value.Get<Vector2>();
    }

    void Move()
    {
        if (!stunned && !freezed)
        {
            Vector3 m = new Vector3(move.x, 0, move.y) * Time.deltaTime;
            transform.Translate(m * movespeed);
            anim.SetFloat("Speed", move.magnitude);
            anim.SetFloat("VelocityX", move.x);
            anim.SetFloat("VelocityY", move.y);
        }


    }
    void Look()
    {
        if (!stunned && !freezed)
        {
            Vector3 r = new Vector3(0, rotate.x, 0) * rotationSpeed * Time.deltaTime;
            transform.Rotate(r);
        }

    }

    public void ResetSpeed()
    {
        movespeed = 4f;
    }
    public void Slow()
    {
        movespeed = 2.25f;
        Invoke("ResetSpeed", 5f);
    }

    public void GetStunned()
    {
        stunned = true;
        stunTime = startStunTime;
        anim.SetTrigger("Hit");
        anim.SetTrigger("Stun");
        camAnim.SetTrigger("Shake");

    }
    public void GetFreezed()
    {
        freezed = true;
        freezeTime = startFreezeTime;
        anim.SetFloat("Speed", 0f);
        //camAnim.SetTrigger("Shake");
        camAnim.SetBool("Freeze", true);

    }
    void OnDisable()
    {
        Debug.Log("PrintOnDisable: script was disabled");
        anim.SetFloat("Speed", 0);
        StopAllCoroutines();
        navigator.SetActive(false);
        NavStarted = false;

    }

    void OnEnable()
    {
        Debug.Log("PrintOnEnable: script was enabled");
        if(!NavStarted)
        {
            StartCoroutine(ShowNavigator());
            navigator.SetActive(false);
            NavStarted = true;
        }
    }

    public void InvokeResetSpeed()
    {
        Invoke("ResetSpeed", 5f);
    }

    public void TrapStun()
    {
        Invoke("GetStunned", 0.25f);
    }

    public void StopAnimation()
    {
        camAnim.enabled = false;
    }

    IEnumerator ShowNavigator()
    {
        //Wait 30 seconds;
        yield return new WaitForSeconds(30);
        //Show Navigator
        //navigator.SetActive(true);
        compass = true;
        //Do it again;
        //StartCoroutine(ShowNavigator());
        //Wait 2 seconds;
        //yield return new WaitForSeconds(2);
        ////Hide Navigator
        ////navigator.SetActive(false);

    }

    IEnumerator HideNavigator()
    {
        //Wait 2 seconds;
        yield return new WaitForSeconds(2);
        //Hide Navigator
        navigator.SetActive(false);
    }

    public void StartCounter(int num)
    {
        if(num == 0)
        {
            StartCoroutine(ShowNavigator());
        }
        else if( num == 1)
        {
            StartCoroutine(HideNavigator());
        }

    }

    
}
