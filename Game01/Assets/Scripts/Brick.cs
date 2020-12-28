using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hits = 1;
    public int points = 100;
    public Vector3 rotator;
    
    void Start()
    {
        //movment of the bricks
        transform.Rotate(rotator * (transform.position.y + transform.position.x) * 0.2f);
    }


    private void Update()
    {
        transform.Rotate(rotator * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
          hits--;
        //Score points
        GameManager.Instance.Score += points;
        if(hits <= 0)
        {
            Destroy(gameObject);
        }
    
    }

  
}
