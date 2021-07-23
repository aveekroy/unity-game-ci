using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Let us define a variable _speed of type float
    [SerializeField]
    private float _speed = 8f;

    // Update is called once per frame
    void Update()
    {
        // The below code lets the laser go up
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        // Destroy the Laser if it goes up by 8f
        if(transform.position.y > 8f)
        {
            Destroy(this.gameObject);
        }

    }
}
