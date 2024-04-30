using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Names : MonoBehaviour
{
    string word = null;
    int wordIndex = 0;
    string alpha;
    public TextMeshProUGUI myName = null;
    public GameObject keyboardPanel;
    public GameObject player;
    

    public void alphabetFunction (string alphabet)
    {
        wordIndex++;
        word = word + alphabet;        
        myName.text = word;
    }
    public void BackSpace()
    {
        string value = word;
        value = value.Substring( 0 , value.Length - 1 );
        word = value;
        myName.text = word;
    }
    
    public void Submit()
    {
        keyboardPanel.SetActive(false);
        player.GetComponent<PlayerSetup>().SetDefaultMap("Player");
        player.GetComponent<PlayerSetup>().playerName = myName.text;
    }
    void OnSubmitName()
    {
        keyboardPanel.SetActive( false );
        player.GetComponent<PlayerSetup>().SetDefaultMap("Player");
        player.GetComponent<PlayerSetup>().playerName = myName.text;
    }
}
