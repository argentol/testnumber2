using UnityEngine;

public class RotationOfSphere : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void FixedUpdate()
    {
        Quaternion rotationZ = Quaternion.AngleAxis(1f, Vector3.forward);
        player.transform.rotation *= rotationZ;
    }
}
