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
        var sceneObject = SaveSystem.LoadScene();
        var sceneNumber = sceneObject.GetSceneNumber();
        SceneManager.LoadScene(sceneNumber);
    }

    public void GoToEndlessLevel()
    {
        SceneManager.LoadScene(4);
    }
}
