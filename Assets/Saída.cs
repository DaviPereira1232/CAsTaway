using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Saída : MonoBehaviour
{
    public GameObject Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == Player)
        SceneManager.LoadScene("Asteroid_LEVEL1");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        SceneManager.LoadScene("Asteroid_LEVEL1");
    }
}
