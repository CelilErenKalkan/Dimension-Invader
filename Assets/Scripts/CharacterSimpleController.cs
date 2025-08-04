using UnityEngine;
using DG.Tweening;

public class CharacterSimpleController : MonoBehaviour
{
    public DynamicJoystick joystick;
    public float moveSpeed = 5f;
    Vector3 moveDirection;

    void Update()
    {
        moveDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;

        if (moveDirection != Vector3.zero)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
            RotateSmoothly(moveDirection);
        }
    }

    void RotateSmoothly(Vector3 direction)
    {
        Quaternion toRot = Quaternion.LookRotation(direction);
        transform.DORotateQuaternion(toRot, 0.15f);
    }
}

