using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Rigidbody _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Moveplatform();
    }

    private void Moveplatform()
    {
        _rb.MovePosition(_rb.position + Vector3.forward * Time.deltaTime);
    }
}
