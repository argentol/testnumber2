using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;


public class LoadingLevels : MonoBehaviour
{
    public GameObject hero;
    public Scene zxc;
    
    public void GoToScene()
    {
        EditorSceneManager.MoveGameObjectToScene(hero, zxc);
        //SceneManager.LoadScene(0);
    }
}
