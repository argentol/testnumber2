using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadingLevels : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToNextLevel()
    {
        var sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings; 
        Debug.Log(sceneCount);

        var sceneObject = SaveSystem.LoadScene();
        var sceneNumber = sceneObject.GetSceneNumber();
        Debug.Log(sceneNumber);
        SceneManager.LoadScene(sceneNumber);
        Debug.Log(sceneNumber);

    }
}
