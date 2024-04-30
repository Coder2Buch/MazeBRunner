using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ItemControls : MonoBehaviour
{
    bool normal = true;
    bool itemCollectedSlot1;
    bool itemCollectedSlot2;
    bool itemShot1;
    bool itemShot2;
    public bool isImmune;
    int slotItemNum1;
    int slotItemNum2;
    float speedUp = 7f;
    Vector3 actPos;

    
    int itemCount;
    public Image itemSlot1;
    public Image itemSlot2;
    public Image compassSlot;
    public Image itemSlot1Button;
    public Image itemSlot2Button;
    public Image compassSlotButton;
    public Sprite[] itemMaterials;
    public GameObject r2;
    public GameObject l2;
    public GameObject quad;
    public GameObject trail;

    public PlayerController player;
    public GameObject trap1;
    public GameObject trap2;
    public GameObject shield; 
    public int itemNumber;
    float wubb = 0.9f;
    float wubbdir = 1.0f;
    float wubbspeed = 2.3f;
    Color whiteColor1;
    Color whiteColor2;
    Color transparentColor;


    // Start is called before the first frame update
    void Start()
    {

        ItemSetup();

    }

    public void ItemsReset()
    {
        isImmune = false;

        itemShot1 = true;
        itemShot2 = true;
        itemCollectedSlot1 = false;
        itemCollectedSlot2 = false;

        r2.SetActive( false );
        l2.SetActive( false );
        itemSlot1.sprite = null;
        itemSlot1.color = transparentColor;
        itemSlot2.sprite = null;
        itemSlot2.color = transparentColor;
    }
    public void ItemSetup()
    {
        isImmune = false;
        whiteColor1 = itemSlot1.color;
        whiteColor2 = itemSlot2.color;
        transparentColor.a = 0f;
        r2.SetActive( false );
        l2.SetActive( false );
        itemShot1 = true;
        itemShot2 = true;
        itemCollectedSlot1 = false;
        itemCollectedSlot2 = false;

        itemSlot1.sprite = null;
        itemSlot1.color = transparentColor;
        itemSlot2.sprite = null;
        itemSlot2.color = transparentColor;
    }
    // Update is called once per frame
    void Update()
    {

        ShieldRotation();

        //itemCount = itemMaterials.Length;

    }

    void ShieldRotation()
    {
        shield.transform.Rotate(0, 200 * Time.deltaTime, 0);
        shield.transform.localScale = new Vector3(wubb, 0.6f, wubb);
        wubb += Time.deltaTime * wubbdir * wubbspeed;
        if (wubb > 1.6)
            wubbdir *= -1;
        if (wubb < 0.9f)
            wubbdir *= -1;

    }
    private void OnFire ()
    {
        if ( !itemShot1 && slotItemNum1 == 0 )
        {
            itemSlot1.sprite = null;
            itemSlot1.color = transparentColor;
            player.movespeed = speedUp;
            itemShot1 = true;
            Invoke( "ResetMoveSpeed" , 5f );
            trail.SetActive(true);
            itemCollectedSlot1 = false;
            r2.SetActive( false );
        }
        else if ( !itemShot1 && slotItemNum1 == 1 )
        {
            var enemies = GameObject.FindGameObjectsWithTag("Player");
            itemSlot1.sprite = null;
            itemSlot1.color = transparentColor;
            itemShot1 = true;
            itemCollectedSlot1 = false;
            r2.SetActive( false );
            foreach ( var obj in enemies )
            {
                if ( obj != this.gameObject )
                {
                    
                    if( !obj.GetComponent<ItemControls>().isImmune)
                    {

                        Debug.Log( obj.name + " is freezed" );
                        obj.GetComponent<PlayerController>().GetFreezed();
                        //obj.GetComponent<PlayerController>().movespeed = 0f;
                        //obj.GetComponent<PlayerController>().Invoke("ResetSpeed", 4f);
                        obj.GetComponent<ItemControls>().normal = false;
                        obj.GetComponent<ItemControls>().InvokeNormal(obj.GetComponent<PlayerController>().startFreezeTime);
                    }
                }
            }
        }
        else if (!itemShot1 && slotItemNum1 == 2)
        {
            var enemies = GameObject.FindGameObjectsWithTag("Player");
            itemSlot1.sprite = null;
            itemSlot1.color = transparentColor;
            itemShot1 = true;
            itemCollectedSlot1 = false;
            r2.SetActive( false );
            foreach ( var obj in enemies )
            {
                if ( obj != this.gameObject )
                {
                    if ( !obj.GetComponent<ItemControls>().isImmune )
                    {
                        obj.GetComponent<PlayerController>().movespeed = 2.25f;
                        obj.GetComponent<PlayerController>().InvokeResetSpeed();
                        obj.GetComponent<ItemControls>().normal = false;
                        obj.GetComponent<ItemControls>().InvokeNormal(5);
                    }
                }
            }
        }
        else if (!itemShot1 && slotItemNum1 == 3)
        {
            //ActivateShield();
            shield.SetActive( true );
            itemSlot1.sprite = null;
            itemSlot1.color = transparentColor;
            itemShot1 = true;           
            isImmune = true;
            Invoke( "ResetImmunity" , 5f );
            itemCollectedSlot1 = false;
            r2.SetActive( false );
            if (!normal)
            {
                ResetMoveSpeed();
                player.freezeTime = 0f;
                normal = true;
            }
        }
        else if ( !itemShot1 && slotItemNum1 == 4 )
        {
            actPos = player.transform.position;
            Instantiate( trap1 , actPos , Quaternion.identity );
            itemSlot1.sprite = null;
            itemSlot1.color = transparentColor;
            itemShot1 = true;
            itemCollectedSlot1 = false;
            r2.SetActive( false );
        }
        //else if ( !itemShot1 && slotItemNum1 == 5 )
        //{
        //    actPos = player.transform.position;
        //    Instantiate( trap2 , actPos , Quaternion.identity );
        //    itemSlot1.sprite = null;
        //    itemSlot1.color = transparentColor;
        //    itemShot1 = true;
        //    itemCollectedSlot1 = false;
        //}
    }
    private void OnFireLeft ()
    {
        if ( !itemShot2 && slotItemNum2 == 0 )
        {
            Debug.Log( "Feuer" );
            itemSlot2.sprite = null;
            itemSlot2.color = transparentColor;
            player.movespeed = speedUp;
            itemShot2 = true;
            Invoke( "ResetMoveSpeed" , 5f );
            trail.SetActive(true);
            itemCollectedSlot2 = false;
            l2.SetActive( false );
        }
        else if ( !itemShot2 && slotItemNum2 == 1 )
        {
            Debug.Log( "Fire" );
            var enemies = GameObject.FindGameObjectsWithTag("Player");
            itemSlot2.sprite = null;
            itemSlot2.color = transparentColor;
            itemShot2 = true;
            itemCollectedSlot2 = false;
            l2.SetActive( false );

            foreach ( var obj in enemies )
            {
                if ( obj != this.gameObject )
                {
                    Debug.Log( obj.name + " is freezed" );
                    obj.GetComponent<PlayerController>().GetFreezed();
                    //obj.GetComponent<PlayerController>().movespeed = 0f;
                    //obj.GetComponent<PlayerController>().Invoke("ResetSpeed", 4f);
                    obj.GetComponent<ItemControls>().normal = false;
                    obj.GetComponent<ItemControls>().InvokeNormal(obj.GetComponent<PlayerController>().startFreezeTime);
                }
            }
        }
        else if ( !itemShot2 && slotItemNum2 == 2 )
        {
            var enemies = GameObject.FindGameObjectsWithTag("Player");
            itemSlot2.sprite = null;
            itemSlot2.color = transparentColor;
            itemShot2 = true;
            itemCollectedSlot2 = false;
            l2.SetActive( false );

            foreach ( var obj in enemies )
            {
                if ( obj != this.gameObject )
                {
                    if ( !obj.GetComponent<ItemControls>().isImmune )
                    {
                        obj.GetComponent<PlayerController>().movespeed = 2.25f;
                        obj.GetComponent<PlayerController>().InvokeResetSpeed();
                        obj.GetComponent<ItemControls>().normal = false;
                        obj.GetComponent<ItemControls>().InvokeNormal(5);

                    }
                }
            }
        }
        else if ( !itemShot2 && slotItemNum2 == 3 )
        {
            //ActivateShield();
            shield.SetActive( true );
            itemSlot2.sprite = null;
            itemSlot2.color = transparentColor;
            itemShot2 = true;
            isImmune = true;
            Invoke( "ResetImmunity" , 5f );
            itemCollectedSlot2 = false;
            l2.SetActive( false );

            if ( !normal )
            {
                ResetMoveSpeed();
                player.freezeTime = 0f;
                normal = true;
            }
        }
        else if ( !itemShot2 && slotItemNum2 == 4 )
        {
            actPos = player.transform.position;
            Instantiate( trap1 , actPos , Quaternion.identity );
            itemSlot2.sprite = null;
            itemSlot2.color = transparentColor;
            itemShot2 = true;
            itemCollectedSlot2 = false;
            l2.SetActive( false );

        }
        //else if ( !itemShot2 && slotItemNum2 == 5 )
        //{
        //    actPos = player.transform.position;
        //    Instantiate( trap2 , actPos , Quaternion.identity );
        //    itemSlot2.sprite = null;
        //    itemSlot2.color = transparentColor;
        //    itemShot2 = true;
        //    itemCollectedSlot2 = false;

        //}

    }
    public int AddItem()
    {
        Debug.Log("Adding Item");
        if ( !itemCollectedSlot1 )
        {
            Debug.Log("Got in the IF");
            switch (itemNumber)
            {
                case 0:
                    itemSlot1.sprite = itemMaterials[0];
                    itemSlot1.color = whiteColor1;
                    itemShot1 = false;
                    itemCollectedSlot1 = true;
                    slotItemNum1 = 0;
                    r2.SetActive( true );                    
                    break;
                case 1:
                    itemSlot1.sprite = itemMaterials[1];
                    itemSlot1.color = whiteColor1;
                    itemShot1 = false;
                    itemCollectedSlot1 = true;
                    slotItemNum1 = 1;
                    r2.SetActive( true );
                    break;
                case 2:
                    itemSlot1.sprite = itemMaterials [ 2 ];
                    itemSlot1.color = whiteColor1;
                    itemShot1 = false;
                    itemCollectedSlot1 = true;
                    slotItemNum1 = 2;
                    r2.SetActive( true );
                    break;
                case 3:
                    itemSlot1.sprite = itemMaterials [ 3 ];
                    itemSlot1.color = whiteColor1;
                    itemShot1 = false;
                    itemCollectedSlot1 = true;
                    slotItemNum1 = 3;
                    r2.SetActive( true );
                    break;
                case 4:
                    itemSlot1.sprite = itemMaterials [ 4 ];
                    itemSlot1.color = whiteColor1;
                    itemShot1 = false;
                    itemCollectedSlot1 = true;
                    slotItemNum1 = 4;
                    r2.SetActive( true );
                    break;
                //case 5:
                //    itemSlot1.sprite = itemMaterials [ 5 ];
                //    itemSlot1.color = whiteColor1;
                //    itemShot1 = false;
                //    itemCollectedSlot1 = true;
                //    slotItemNum1 = 5;
                //    break;
                default: Debug.Log("FUCKKKKK");
                    break;
            }
            return 0;
           
        }
        if ( itemCollectedSlot1 && !itemCollectedSlot2)
        {
            Debug.Log( "linker ItemSLot" );
            switch ( itemNumber )
            {
                case 0:
                    itemSlot2.sprite = itemMaterials [ 0 ];
                    itemSlot2.color = whiteColor2;
                    itemShot2 = false;
                    itemCollectedSlot2 = true;
                    slotItemNum2 = 0;
                    l2.SetActive( true );
                    break;
                case 1:
                    itemSlot2.sprite = itemMaterials [ 1 ];
                    itemSlot2.color = whiteColor2;
                    itemShot2 = false;
                    itemCollectedSlot2 = true;
                    slotItemNum2 = 1;
                    l2.SetActive( true );
                    break;
                case 2:
                    itemSlot2.sprite = itemMaterials [ 2 ];
                    itemSlot2.color = whiteColor2;
                    itemShot2 = false;
                    itemCollectedSlot2 = true;
                    slotItemNum2 = 2;
                    l2.SetActive( true );
                    break;
                case 3:
                    itemSlot2.sprite = itemMaterials [ 3 ];
                    itemSlot2.color = whiteColor2;
                    itemShot2 = false;
                    itemCollectedSlot2 = true;
                    slotItemNum2 = 3;
                    l2.SetActive( true );
                    break;
                case 4:
                    itemSlot2.sprite = itemMaterials [ 4 ];
                    itemSlot2.color = whiteColor1;
                    itemShot2 = false;
                    itemCollectedSlot2 = true;
                    slotItemNum2 = 4;
                    l2.SetActive( true );
                    break;
                //case 5:
                //    itemSlot2.sprite = itemMaterials [ 5 ];
                //    itemSlot2.color = whiteColor1;
                //    itemShot2 = false;
                //    itemCollectedSlot2 = true;
                //    slotItemNum2 = 5;
                //    break;
                default:Debug.Log("FUCKKKKK");
                    break;
            }

        }
        return 0;
    }
    private void ResetMoveSpeed()
    {
        player.movespeed = 4.0f;
        trail.SetActive(false);
    }
    private void ResetImmunity()
    {
        isImmune = false;
        DeactivateShield();
    }

    void ResetNormal()
    {
        normal = true;
    }
    public void InvokeNormal(float seconds)
    {
        Invoke("ResetNormal", seconds);
    }
    

    //public void ActivateShield()
    //{

    //    shield.SetActive( true );
    //    //shield.transform.position = player.transform.position;
    //    shield.transform.Rotate(0, 200 * Time.deltaTime, 0);

    //}
    public void DeactivateShield ()
    {
        shield.SetActive( false );
    
    }

}
