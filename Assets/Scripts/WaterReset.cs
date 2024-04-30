using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterReset : MonoBehaviour
{
     Vector3 originalPos;
   
    private void Start()
    {
        originalPos = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
    }
    void OnTriggerEnter(Collider other)
    {
      if(other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = originalPos;
        }
    }
}
