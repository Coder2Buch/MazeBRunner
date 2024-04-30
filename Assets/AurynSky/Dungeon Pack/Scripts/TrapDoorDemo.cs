using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorDemo : MonoBehaviour {

    //This script goes on the TrapDoor prefab;

    public Animator TrapDoorAnim; //Animator for the trap door;
    public bool enter = true;
    public bool exit = true;

    // Use this for initialization
    void Awake()
    {
        //get the Animator component from the trap;
        TrapDoorAnim = GetComponent<Animator>();
        //start opening and closing the trap for demo purposes;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enter)
        {
           StartCoroutine(OpenCloseTrap());
           Debug.Log("entered");
        }
    }
    
     IEnumerator OpenCloseTrap()
     {
         //play open animation;
         TrapDoorAnim.SetTrigger("open");
         //wait 2 seconds;
         yield return new WaitForSeconds(2);
         //play close animation;
         TrapDoorAnim.SetTrigger("close");
         //wait 2 seconds;
         yield return new WaitForSeconds(2);
         //Do it again;
         //StartCoroutine(OpenCloseTrap());

     }

}