using UnityEngine;

public class CameraMotion : MonoBehaviour
{
    public Vector3 EndPoint;
    private float speed = 2.5f;

    void Start()
    {
        EndPoint= new Vector3(112, 0, -10);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, EndPoint, speed * Time.deltaTime);
    }
}
