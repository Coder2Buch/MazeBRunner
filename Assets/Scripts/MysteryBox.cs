using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBox : MonoBehaviour
{
    // Start is called before the first frame update
    ItemControls item;
    public ParticleSystem particle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            Destroy(gameObject);
            item = _other.GetComponent<ItemControls>();
            //item.itemNumber = 1;
            item.itemNumber = Random.Range(0, item.itemMaterials.Length);
            Debug.Log("Item Number: " + item.itemNumber);
            //item.itemCollected = true;
            item.AddItem();
            Instantiate(particle, transform.position, Quaternion.identity);
        }
    }
}
