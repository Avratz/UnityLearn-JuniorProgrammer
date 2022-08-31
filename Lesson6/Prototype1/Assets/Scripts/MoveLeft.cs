using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 10.0f;

    private void Update() {
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
    }
}
