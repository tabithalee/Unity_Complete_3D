using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;

    private void Start()
    {
        gameOverCanvas.enabled = false;
    }

    public void HandleDeath()
    {
        gameOverCanvas.enabled = true;

        Time.timeScale = 0f;
        FindObjectOfType<WeaponSwitcher>().enabled = false;
        // unlock the cursor and allow user to see it
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
