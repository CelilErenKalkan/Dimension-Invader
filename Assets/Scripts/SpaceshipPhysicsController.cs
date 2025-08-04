using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpaceshipRealisticFlight : MonoBehaviour
{
    public DynamicJoystick joystick;

    [Header("Uçuþ Ayarlarý")]
    public float forwardSpeed = 15f;
    public float rollSpeed = 50f;
    public float pitchSpeed = 40f;
    public float maxVelocity = 20f;

    [Header("Roll Sýnýrlamasý")]
    public float maxRollAngle = 85f;  // Saða-sola maksimum dönüþ açýsý (+/- 45 derece önerilir)

    Rigidbody rb;
    float rollInput;
    float pitchInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        rollInput = joystick.Horizontal;
        pitchInput = joystick.Vertical;
    }

    void FixedUpdate()
    {
        // Mevcut roll açýsýný bul
        float currentRoll = NormalizeAngle(transform.localEulerAngles.z);

        // Yeni Roll hedefini hesapla
        float desiredRollChange = -rollInput * rollSpeed * Time.fixedDeltaTime;
        float desiredRoll = Mathf.Clamp(currentRoll + desiredRollChange, -maxRollAngle, maxRollAngle);
        float rollCorrection = desiredRoll - currentRoll;

        // Roll açýsý limiti eklenmiþ tork
        Vector3 torque =
            transform.forward * rollCorrection +
            transform.right * pitchInput * pitchSpeed * Time.fixedDeltaTime;

        rb.AddTorque(torque, ForceMode.VelocityChange);

        // Ýleri doðru sabit hýz
        rb.AddForce(transform.forward * forwardSpeed, ForceMode.Acceleration);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
    }

    // Açý normalizasyonu için yardýmcý fonksiyon (-180 ile +180 arasý)
    float NormalizeAngle(float angle)
    {
        angle = angle % 360;
        if (angle > 180f) angle -= 360f;
        return angle;
    }
}
