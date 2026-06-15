using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;
    public Vector3 offset;


    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,Player.transform.position + offset, Time.deltaTime * 4);
    }
}
