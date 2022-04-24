using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuDisplayManager : MonoBehaviour
{
    [SerializeField] Image[] lstCurtains;
    bool transition1;
    float timer1;

    [SerializeField] AudioSource sndChoice;
    [SerializeField] AudioSource click;
    [SerializeField] AudioSource seaVibe;
    [SerializeField] AudioSource boubi;
    bool boubidone;

    bool choice;

    [SerializeField] GameObject controlsPanel;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        lstCurtains[0].rectTransform.sizeDelta = new Vector2(Screen.width*2, Screen.height/2 + Screen.height/3);
        lstCurtains[1].rectTransform.sizeDelta = new Vector2(Screen.width*2, Screen.height/2 + Screen.height/3);
        print(Screen.width + ".." + Screen.height);
        transition1 = true;
        choice = false;
        timer1 = 0;
        boubidone = false;
    }

    void Update()
    {
        if (transition1)
        {
            timer1 += Time.deltaTime;
        }
        if (timer1 >= 2 && transition1)
        {
            if (!boubidone) { boubi.Play(); boubidone = true; }
            lstCurtains[0].rectTransform.sizeDelta -= new Vector2(0, Time.deltaTime * 200);
            lstCurtains[1].rectTransform.sizeDelta -= new Vector2(0, Time.deltaTime * 200);
            if (lstCurtains[0].rectTransform.sizeDelta.y <= 1 || lstCurtains[1].rectTransform.sizeDelta.y <= 1)
            {
                lstCurtains[0].rectTransform.sizeDelta = new Vector2(Screen.width*2, 0);
                lstCurtains[1].rectTransform.sizeDelta = new Vector2(Screen.width*2, 0);
                transition1 = false;
                timer1 = 0;
            }
        }

        if (choice)
        {
            lstCurtains[0].rectTransform.sizeDelta += new Vector2(0, Time.deltaTime * 300);
            lstCurtains[1].rectTransform.sizeDelta += new Vector2(0, Time.deltaTime * 300);
            if (lstCurtains[0].rectTransform.sizeDelta.y >= Screen.height / 2 + Screen.height / 3 || lstCurtains[1].rectTransform.sizeDelta.y >= Screen.height / 2 + Screen.height / 3)
            {
                boubi.volume -= Time.deltaTime / 10;
                seaVibe.volume -= Time.deltaTime / 10;
                if (boubi.volume <= 0)
                {
                    SceneManager.LoadScene(1);
                }
            }
        }
    }

    public void PlayButton()
    {
        choice = true;
        sndChoice.Play();
    }

    public void ControlsButton()
    {
        if (controlsPanel.activeInHierarchy)
        {
            controlsPanel.SetActive(false);
        }
        else
        {
            controlsPanel.SetActive(true);
        }
        click.Play();
    }

    public void ExitButton()
    {
        sndChoice.Play();
        Application.Quit();
    }
}
