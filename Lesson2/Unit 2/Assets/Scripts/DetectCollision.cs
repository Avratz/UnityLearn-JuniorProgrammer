using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}
