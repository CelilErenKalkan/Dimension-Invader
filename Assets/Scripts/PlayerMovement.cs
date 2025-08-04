using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Referanslar")]
    public Joystick joystick;
    public Transform shipVisuals; // Geminin görsel modelini tutan Transform

    [Header("Hareket Ayarları")]
    public float moveSpeed = 10f;

    [Header("Yatay Limitler")]
    public float horizontalLimit = 5f;

    [Header("Dikey Limitler")]
    public float minYLimit = -3f;
    public float maxYLimit = 5f;

    [Header("Dönüş (Rotation) Ayarları")]
    [Tooltip("Gemi ne kadar fazla yana yatacak?")]
    public float tiltAmount = 15f;
    [Tooltip("Gemi ne kadar hızlı yana yatacak ve düzelecek?")]
    public float tiltSpeed = 5f;


    void Update()
    {
        // Joystick'ten HEM yatay HEM DE dikey girdiyi al
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        // --- HAREKET BÖLÜMÜ ---
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0);
        Vector3 newPosition = transform.position + movement * moveSpeed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -horizontalLimit, horizontalLimit);
        newPosition.y = Mathf.Clamp(newPosition.y, minYLimit, maxYLimit);
        transform.position = newPosition;


        // --- YANA YATMA (TILT) BÖLÜMÜ ---
        
        // 1. Hedef rotasyonu hesapla
        // Gemi sağa giderken (-1 * tiltAmount), sola giderken (+1 * tiltAmount) olacak şekilde Z ekseninde bir hedef belirle.
        Quaternion targetRotation = Quaternion.Euler(0, 0, -horizontalInput * tiltAmount);

        // 2. Mevcut rotasyondan hedef rotasyona doğru yumuşak bir geçiş yap
        // Quaternion.Slerp, iki rotasyon arasında yumuşak, küresel bir geçiş sağlar.
        shipVisuals.localRotation = Quaternion.Slerp(shipVisuals.localRotation, targetRotation, tiltSpeed * Time.deltaTime);
    }
}