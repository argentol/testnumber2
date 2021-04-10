using UnityEngine.SceneManagement;

public class SceneObject
{
    public int SceneCounter { get; set; }

    public SceneObject(int scene)
    {
        SceneCounter = scene;
    }

    public int GetSceneNumber()
    {
        var sceneCount = SceneManager.sceneCountInBuildSettings;
        return SceneCounter % (sceneCount - 1);
    }
}


