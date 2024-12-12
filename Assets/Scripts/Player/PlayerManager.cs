using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;

    private void Awake()
    {
        // Make this a Singleton to avoid duplicates when reloading the scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevent this GameObject from being destroyed
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate PlayerManagers
        }
    }
}
