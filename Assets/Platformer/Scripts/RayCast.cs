using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    private Camera cam;
    public LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position, Color.blue);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, mask))
            {
                BlocksType b = hit.collider.gameObject.GetComponent<BlocksType>();
                GameManager g = b.GetManager();
                if (b.type == "?")
                {
                    g.AddCoins(1);
                    g.AddScore(100);
                    g.CoinPlay();
                    hit.collider.gameObject.SetActive(false);
                }
                if (b.type == "b")
                {
                    Vector3 pos = gameObject.transform.position;
                    pos.z -= 0.1f;
                    g.brickeffect.gameObject.transform.position = pos;
                    g.AddScore(100);
                    g.BrickPlay();
                    hit.collider.gameObject.SetActive(false);
                }
                
            }
        }
        
    }
}
