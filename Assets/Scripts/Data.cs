using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    Menu menu;
    public bool cupMode;
    public int rounds;
    public bool[] maps;
    void Start()
    {
        //menu = canvas.GetComponent<Menu>();
        DontDestroyOnLoad(this.gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
