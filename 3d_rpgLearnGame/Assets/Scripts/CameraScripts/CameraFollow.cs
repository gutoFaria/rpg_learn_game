using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 offset;

    void Awake()
    {
        if(player != null)
        {
            offset = transform.position - player.position;
        }
        else
        {
            Debug.Log("Error!");
        }
    }

    void LateUpdate()
    {
        if(player != null)
        {
            transform.position = player.position + offset;
        }
    }
}
