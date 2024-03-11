using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingState : MonoBehaviour
{
    private static BuildingState instance;
    public static BuildingState Instance { get { if (instance == null) return null; return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
   
}
