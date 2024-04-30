using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public float attackRange;
    public LayerMask enemies;

    public AudioSource _as;
    public AudioClip[] audioClipArray;


    void Awake()
    {
        anim = GetComponent<Animator>();
        _as = GetComponent<AudioSource>();
    }
    void Update()
    {
        timeBtwAttack -= Time.deltaTime;
    }

    public void OnHit()
    {
        if(timeBtwAttack <= 0)
        {
            //Attack
            anim.SetTrigger("Attacking");
            Collider[] enemiesToAttack = Physics.OverlapSphere(attackPos.position, attackRange, enemies);
            for (int i = 0; i < enemiesToAttack.Length; i++)
            {
                _as.clip = audioClipArray[Random.Range(0, audioClipArray.Length)];
                _as.PlayOneShot(_as.clip);
                enemiesToAttack[i].GetComponent<PlayerController>().GetStunned();
            }
            timeBtwAttack = startTimeBtwAttack;
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackPos.position, attackRange);
    }

    void OnDisable()
    {
        Debug.Log("PrintOnDisable: Attack script was disabled");

    }

    void OnEnable()
    {
        Debug.Log("PrintOnEnable: Attack script was enabled");
    }
}
