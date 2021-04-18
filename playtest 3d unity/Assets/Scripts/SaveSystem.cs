using System.IO;
using System.Linq;
using UnityEngine;

public static class SaveSystem
{
    private static string filePath_ = Path.Combine(Application.persistentDataPath, "nextLevel.txt");

    public static void UpdateLevel()
    {
        var sceneObject = LoadScene();

        if (sceneObject != null)
        {
            sceneObject.SceneCounter++;

            SaveScene(sceneObject);
        }
    }

    public static void SaveScene(SceneObject scene)
    {
        File.WriteAllText(filePath_, scene.SceneCounter.ToString());
    }

    public static SceneObject LoadScene()
    {
        SceneObject sceneObject = null;

        var sceneNumber = GetSceneNumber();

        if (sceneNumber.HasValue) 
        {
            sceneObject = new SceneObject(sceneNumber.Value);
        }
        else
        {
            Debug.LogError("Can't parse scene number from file " + filePath_);
        }


        return sceneObject;
    }

    private static int? GetSceneNumber()
    {
        int? sceneNumber = null;

        if (File.Exists(filePath_))
        {
            var sceneString = File.ReadLines(filePath_).FirstOrDefault();
            if (int.TryParse(sceneString, out var sceneNumber_))
            {
                sceneNumber = sceneNumber_;
            }
        }
        else
        {
            sceneNumber = 0;
            var scene = new SceneObject(sceneNumber.Value);
            SaveScene(scene);
        }


        return sceneNumber;
    }


}
