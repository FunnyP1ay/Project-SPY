using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDB : MonoBehaviour
{
    
    private static GameDB instance;
    public static GameDB Instance { get { if (instance == null) return null; return instance; }  }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
