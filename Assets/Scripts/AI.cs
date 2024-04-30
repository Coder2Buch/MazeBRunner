using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;
    Vector3 destination;
    Rigidbody rb;
    public Transform[] targets;
    int target;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        destination = targets[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        destination = targets[target].position;
        agent.destination = destination;

        if (Vector3.Distance(transform.position, destination) < 0.3f)
        {
            if(target == targets.Length - 1)
            {
                target = 0;
            }
            target++;
            destination = targets[target].position;
            anim.SetFloat("Speed", 0);
        }
        else
        {
            anim.SetFloat("Speed", 1);
        }
        
    }
}
