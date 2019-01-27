using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public bool isRight = false;

    void Start()
    {
        Destroy(gameObject, 3);
    }

    void Update()
    {
        transform.position = new Vector2(transform.position.x + (isRight ?  10 : -10) * Time.deltaTime, transform.position.y);
    }

}
