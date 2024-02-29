using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakJump : MonoBehaviour
{
    public float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("breakable"))
        {
            if (timer < 0)
            {
                BlocksType b = other.gameObject.GetComponent<BlocksType>();
                GameManager g = b.GetManager();
                if (b.type == "?")
                {
                    g.AddCoins(1);
                    g.AddScore(100);
                    g.CoinPlay();
                    other.gameObject.SetActive(false);
                    timer = 0.2f;
                }
                if (b.type == "b")
                {
                    Vector3 pos = gameObject.transform.position;
                    pos.z -= 0.1f;
                    g.brickeffect.gameObject.transform.position = pos;
                    g.AddScore(100);
                    g.BrickPlay();
                    other.gameObject.SetActive(false);
                    timer = 0.2f;
                }
            }
        }
    }
}
