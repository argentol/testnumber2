using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public KeyCode _Key;
    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Material material1;
    public Material material2;
    public Material material3;
    public Player Player;

    //void FixedUpdate()
    //{
    //    if (Player.GameIsOver == false)
    //    {
    //        //var v = Button1.transform.position;
    //        //Debug.Log($"{v.x} {v.y} {v.z}");

    //        if (Input.GetKeyDown(KeyCode.Z))
    //        {
    //            Button1.onClick.Invoke();
    //        }
    //        if (Input.GetKeyDown(KeyCode.X))
    //        {
    //            Button2.onClick.Invoke();
    //        }
    //        if (Input.GetKeyDown(KeyCode.C))
    //        {
    //            Button3.onClick.Invoke();
    //        }


    //    }
    //}

    public void OnButtonPress()
    {
        Debug.Log("zxc");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    //void update()
    //{
    //    if (Input.GetKeyDown("z"))
    //    {
    //        print("df");
    //    }
    //}
}
