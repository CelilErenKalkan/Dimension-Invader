using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpaceshipRealisticFlight : MonoBehaviour
{
    public DynamicJoystick joystick;

    [Header("U�u� Ayarlar�")]
    public float forwardSpeed = 15f;
    public float rollSpeed = 50f;
    public float pitchSpeed = 40f;
    public float maxVelocity = 20f;

    [Header("Roll S�n�rlamas�")]
    public float maxRollAngle = 85f;  // Sa�a-sola maksimum d�n�� a��s� (+/- 45 derece �nerilir)

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
        // Mevcut roll a��s�n� bul
        float currentRoll = NormalizeAngle(transform.localEulerAngles.z);

        // Yeni Roll hedefini hesapla
        float desiredRollChange = -rollInput * rollSpeed * Time.fixedDeltaTime;
        float desiredRoll = Mathf.Clamp(currentRoll + desiredRollChange, -maxRollAngle, maxRollAngle);
        float rollCorrection = desiredRoll - currentRoll;

        // Roll a��s� limiti eklenmi� tork
        Vector3 torque =
            transform.forward * rollCorrection +
            transform.right * pitchInput * pitchSpeed * Time.fixedDeltaTime;

        rb.AddTorque(torque, ForceMode.VelocityChange);

        // �leri do�ru sabit h�z
        rb.AddForce(transform.forward * forwardSpeed, ForceMode.Acceleration);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
    }

    // A�� normalizasyonu i�in yard�mc� fonksiyon (-180 ile +180 aras�)
    float NormalizeAngle(float angle)
    {
        angle = angle % 360;
        if (angle > 180f) angle -= 360f;
        return angle;
    }
}
