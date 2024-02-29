using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goalTrigger : MonoBehaviour
{
    private GameManager g;
    // Start is called before the first frame update
    void Start()
    {
        g = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        g.NextLevel();
    }
}
