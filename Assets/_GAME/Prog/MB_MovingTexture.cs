using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_MovingTexture : MonoBehaviour
{
    private Renderer _renderer;

    [SerializeField]
    [Tooltip("Direction that will be applied to the script")]
    private Vector2 _directionToMove = Vector2.one;

    [SerializeField]
    [Tooltip("Speed of the textureMovement")]
    private float _speed = 1f;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        _renderer.material.mainTextureOffset += _directionToMove.normalized * _speed * Time.fixedDeltaTime;
    }


}
