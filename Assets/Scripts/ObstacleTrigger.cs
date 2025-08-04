using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    private bool hasBeenPassed = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasBeenPassed)
        {
            hasBeenPassed = true;
            // GameManager'a bir engelin geçildiğini bildir
            CameraController.Instance.ObstaclePassed();
        }
    }
}