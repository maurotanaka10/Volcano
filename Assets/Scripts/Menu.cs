using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject instruction;

    public void Play()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void Instruction()
    {
        instruction.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void CloseInstruction()
    {
        instruction.SetActive(false);
    }
}
