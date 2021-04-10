using System.IO;
using System.Linq;
using UnityEngine;

public static class SaveSystem
{
    private static string filePath_ = Path.Combine(Directory.GetCurrentDirectory(), @"Assets\Saves\nextLevel.txt");

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
        //var formatter = new BinaryFormatter();

        //var stream = new FileStream(filePath_, FileMode.Create);

        //formatter.Serialize(stream, scene);
        //stream.Close();

        File.WriteAllText(filePath_, scene.SceneCounter.ToString());
    }

    public static SceneObject LoadScene()
    {
        SceneObject sceneObject = null;

        if (File.Exists(filePath_))
        {
            var sceneNumber = GetSceneNumber();

            if (sceneNumber.HasValue) 
            {
                sceneObject = new SceneObject(sceneNumber.Value);
            }
            else
            {
                Debug.LogError("Can't parse scene number from file " + filePath_);
            }
        }
        else
        {
            Debug.LogError("Save file was not found in " + filePath_);
        }

        return sceneObject;
    }

    private static int? GetSceneNumber()
    {
        int? sceneNumber = null;
        var sceneString = File.ReadLines(filePath_).FirstOrDefault();

        if (int.TryParse(sceneString, out var sceneNumber_))
        {
            sceneNumber = sceneNumber_;
        }

        return sceneNumber;
    }


}
