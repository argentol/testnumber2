using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{

    #region Fields

    public KeyCode _Key;

    public Button Button1;
    public Button Button2;
    public Button Button3;

    public Material material1;
    public Material material2;
    public Material material3;

    #endregion

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
}
