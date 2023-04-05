using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 _offset = new Vector3(0, 10, -10);
    private Quaternion _rotateOffset = new Quaternion();

    private void Start()
    {
        //_rotateOffset = Quaternion.Euler(25,0,0);
    }

    private void LateUpdate()
    {
        // var position = player.transform.position;
        // transform.position = position + _offset;
        // transform.rotation = Quaternion.Lerp(_rotateOffset, player.transform.rotation, 0.5f);
    }
}
