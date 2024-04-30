using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision _other)
    {
        //Debug.Log(_other.gameObject.name);
        //if(_other.gameObject.CompareTag("Player"))
        //{
        //    var input = _other.gameObject.GetComponent<PlayerInput>();
        //    Debug.Log("Hit Player " + input.playerIndex);
        //}
    }
}
