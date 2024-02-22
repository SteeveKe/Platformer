using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksType : MonoBehaviour
{
    public string type;
    private GameManager _gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnDestroy()
    {
        if (type == "?")
        {
            _gameManager.AddCoins(1);
            _gameManager.AddScore(200);
            _gameManager.CoinPlay();
        }
        if (type == "b")
        {
            Vector3 pos = gameObject.transform.position;
            pos.z -= 0.1f;
            if (_gameManager.brickeffect != null)
            {
                _gameManager.brickeffect.gameObject.transform.position = pos;
                _gameManager.BrickPlay();
            }
        }
    }
}
