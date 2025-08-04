using UnityEngine;
using UnityEngine.SceneManagement; // Bu satırı eklemeyi unutmayın!

public class MainMenuController : MonoBehaviour
{
    // Bu metot, "START" butonuna tıklandığında çalışacak.
    public void StartGame()
    {
        // "GameScene" adındaki sahneyi yükle.
        // Sahnenizin adını "Build Settings" penceresinde nasıl göründüğüne göre değiştirmeyi unutmayın.
        SceneManager.LoadScene("EmirhanDevelopmentScene");
    }

    // İsteğe bağlı olarak, oyundan çıkmak için bu metodu da ekleyebilirsiniz.
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Oyundan çıkıldı.");
    }
}