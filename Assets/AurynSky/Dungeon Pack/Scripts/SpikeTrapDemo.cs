using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapDemo : MonoBehaviour {

    //This script goes on the SpikeTrap prefab;

    bool activatedTraps = false;

    public Animator spikeTrapAnim; //Animator for the SpikeTrap;

    // Use this for initialization
    void Awake()
    {
        //get the Animator component from the trap;
        spikeTrapAnim = GetComponent<Animator>();
        //start opening and closing the trap for demo purposes;
        //StartCoroutine(OpenCloseTrap());
        Invoke("ActivateTraps", 2f);
    }


    IEnumerator OpenCloseTrap()
    {
        //play open animation;
        spikeTrapAnim.SetTrigger("open");
        //wait 2 seconds;
        yield return new WaitForSeconds(2);
        //play close animation;
        spikeTrapAnim.SetTrigger("close");
        //wait 2 seconds;
        yield return new WaitForSeconds(2);
        //Do it again;
        //StartCoroutine(OpenCloseTrap());

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& activatedTraps && !other.GetComponent<ItemControls>().isImmune)
        {
            StartCoroutine(OpenCloseTrap());
            other.GetComponent<PlayerController>().TrapStun();


        }
    }

    void ActivateTraps()
    {
        activatedTraps = true;

    }


}