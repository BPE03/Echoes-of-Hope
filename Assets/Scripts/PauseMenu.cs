using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseButton;       // Tombol pause
    public GameObject pauseMenuPanel;    // Panel pop-up window

    private bool isPaused = false;       // Status pause game

    void Start()
    {
        // Pastikan pop-up window nonaktif di awal
        pauseMenuPanel.SetActive(false);
        pauseButton.SetActive(true);
    }

    // Fungsi saat tombol Pause ditekan
    public void OnPauseButton()
    {
        isPaused = true;
        Time.timeScale = 0; // Pause game
        pauseMenuPanel.SetActive(true);  // Tampilkan pop-up window
        pauseButton.SetActive(false);    // Sembunyikan tombol pause
    }

    // Fungsi saat tombol Back ditekan
    public void OnBackButton()
    {
        isPaused = false;
        Time.timeScale = 1; // Resume game
        pauseMenuPanel.SetActive(false); // Sembunyikan pop-up window
        pauseButton.SetActive(true);     // Tampilkan tombol pause kembali
    }

    public void OnQuitButton()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false; // Stop play mode di Editor
        #else
        Application.Quit(); // Keluar saat di build
        #endif
    }
    
}
