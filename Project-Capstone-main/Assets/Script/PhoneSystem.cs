using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PhoneSystem : MonoBehaviour
{
    public static PhoneSystem Instance; 

    private Animator anim;
    public CinemachineBrain cameraPlayer;
    public GameObject buttonAccept;
    private bool isOpen = false;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && isOpen == false)
        {
            SoundManager.instance.openSfx();
            isOpen = true;
            //cameraPlayer.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            anim.SetBool("IsOpen", true);
            anim.SetBool("IsClose", false);
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && isOpen == true)
        {
            SoundManager.instance.closeSfx();
            isOpen = false;
            //cameraPlayer.enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            anim.SetBool("IsOpen", false);
            anim.SetBool("IsClose", true);
        }
    }

    public GameObject GetButton()
    {
        return buttonAccept;
    }
}
