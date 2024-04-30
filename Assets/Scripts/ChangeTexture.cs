using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeTexture : MonoBehaviour
{
    //Set these Textures in the Inspector
    public Texture m_MainTexture, m_BlueTexture, m_YellowTexture, m_GreenTexture;
    public GameObject player;
    Renderer m_Renderer;
    int playerNumber;

    // Use this for initialization
    void Start()
    {
        playerNumber = player.GetComponent<PlayerInput>().playerIndex;
        //Fetch the Renderer from the GameObject
        m_Renderer = GetComponent<Renderer>();

        //Set the Texture you assign in the Inspector as the main texture (Or Albedo)
        m_Renderer.material.SetTexture("_MainTex", ChooseTexture());

    }

    Texture ChooseTexture()
    {
        switch (playerNumber)
        {
            case 1:
                return m_BlueTexture;
            case 2:
                return m_YellowTexture;
            case 3:
                return m_GreenTexture;
            default: return m_MainTexture;
        }
    }

}
