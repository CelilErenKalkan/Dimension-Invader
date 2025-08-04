using UnityEngine;
using Cinemachine; // Kamera geçişi için Cinemachine kütüphanesi
using System.Collections;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [Header("Cinemachine Kameralar")]
    public CinemachineVirtualCamera main3DCamera;
    public CinemachineVirtualCamera topDownCamera;

    [Header("Kamera Geçiş Ayarları")]
    public int obstaclesToPassForCameraChange = 10;
    private int obstaclesPassedCount = 0;
    private bool hasSwitched = false;
    [Header("Spawner Referansı")]
    public PoolTest obstacleSpawner;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Oyun başladığında 3D kameranın önceliği yüksek, top-down düşük.
        main3DCamera.Priority = 15;
        topDownCamera.Priority = 5;
    }

    // Bu metot, her bir engel geçtiğinde ObstacleTrigger script'i tarafından çağrılacak.
    public void ObstaclePassed()
    {
        if (!hasSwitched)
        {
            obstaclesPassedCount++;
            Debug.Log("Geçilen engel sayısı: " + obstaclesPassedCount);

            if (obstaclesPassedCount >= obstaclesToPassForCameraChange)
            {
                SwitchToTopDownCamera();
            }
        }
    }

    private void SwitchToTopDownCamera()
    {
        Debug.Log(obstaclesToPassForCameraChange + " engel geçti, kamera geçişi tetiklendi!");

        // 3D kameranın önceliğini düşür
        main3DCamera.Priority = 5;

        // Top-down kameranın önceliğini yükselterek aktif hale getir
        topDownCamera.Priority = 15;

        if (obstacleSpawner != null)
    {
        obstacleSpawner.StopSpawning();
    }

        hasSwitched = true;
    }
}