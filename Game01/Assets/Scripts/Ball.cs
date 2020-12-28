using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    float _speed = 25f;
    Rigidbody _rigidbody;
    Vector3 _velocity;
    Renderer _render;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _render = GetComponent<Renderer>();
        _rigidbody.velocity = Vector3.up * _speed;

    }

    void FixedUpdate()
    {
        _rigidbody.velocity = _rigidbody.velocity.normalized * _speed;
        _velocity = _rigidbody.velocity;
        if (!_render.isVisible)
        {
            GameManager.Instance.Balls--;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _rigidbody.velocity = Vector3.Reflect(_velocity, collision.contacts[0].normal);
    }
}
