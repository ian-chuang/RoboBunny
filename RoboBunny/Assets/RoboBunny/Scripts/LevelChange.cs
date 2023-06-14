using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// https://www.youtube.com/watch?v=r37YoRWYjX0
public class LevelChange : MonoBehaviour
{
    [SerializeField] private LevelConnection connection;
    [SerializeField] private string targetSceneName;
    [SerializeField] private Transform spawnPoint;
    void Start()
    {
        if (connection == LevelConnection.ActiveConnection)
        {
            FindObjectOfType<PlayerController>().transform.position = spawnPoint.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.collider.GetComponent<PlayerController>();
        if (player != null)
        {
            LevelConnection.ActiveConnection = connection;
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
