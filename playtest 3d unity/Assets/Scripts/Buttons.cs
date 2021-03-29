using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
   
    public KeyCode _Key;
    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Material material1;
    public Material material2;
    public Material material3;
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Button1.onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Button2.onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Button3.onClick.Invoke();
        }
    }

    public void OnButtonPress()
    {
        Debug.Log("zxc");
    }


    //void update()
    //{
    //    if (Input.GetKeyDown("z"))
    //    {
    //        print("df");
    //    }
    //}
}
