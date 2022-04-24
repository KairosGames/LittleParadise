using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayManager : MonoBehaviour
{
    [SerializeField] CoinsManager coinsManager;
    [SerializeField] Image[] lstCurtains;

    [SerializeField] Text winText;
    Vector3 factory;

    [SerializeField] Text startText;
    Vector3 startfacto;

    [SerializeField] AudioSource kaamelott;
    bool kamdone;
    [SerializeField] AudioSource bigApplause;
    bool bigApp;
    [SerializeField] AudioSource playa;
    bool playadone;
    [SerializeField] AudioSource seaVibe;

    private float timer1;
    private float timer2;
    private bool transition1;
    private bool transition2;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        lstCurtains[0].rectTransform.sizeDelta = new Vector2(Screen.width*2, Screen.height / 2 + Screen.height / 3);
        lstCurtains[1].rectTransform.sizeDelta = new Vector2(Screen.width*2, Screen.height / 2 + Screen.height / 3);
        timer1 = 0;
        timer2 = 0;
        transition1 = true;
        transition2 = false;
        kamdone = false;
        bigApp = false;
        playadone = false;

        startfacto = new Vector3(0.001f, 0.001f, 0.001f);
        startText.rectTransform.localScale = new Vector3(0, 0, 0);

        factory = new Vector3(0.001f, 0.001f, 0.001f);
        winText.rectTransform.localScale = new Vector3(0, 0, 0);
    }

    void Update()
    {
        if (transition1)
        {
            timer1 += Time.deltaTime;
        }
        if (timer1 >= 1 && transition1)
        {
            lstCurtains[0].rectTransform.sizeDelta -= new Vector2(0, Time.deltaTime * 100);
            lstCurtains[1].rectTransform.sizeDelta -= new Vector2(0, Time.deltaTime * 100);
            if (lstCurtains[0].rectTransform.sizeDelta.y <= 1 || lstCurtains[1].rectTransform.sizeDelta.y <= 1)
            {
                lstCurtains[0].rectTransform.sizeDelta = new Vector2(Screen.width*2, 0);
                lstCurtains[1].rectTransform.sizeDelta = new Vector2(Screen.width*2, 0);
                startText.rectTransform.localScale = startfacto;
                if (!transition2)
                {
                    startfacto *= 1.2f;
                    if(!kamdone && timer1 >= 4.2) { kaamelott.Play(); kamdone = true; }
                    if (startfacto.x >= 1)
                    {
                        startfacto = new Vector3(1, 1, 1);
                        timer2 += Time.deltaTime;
                        if (timer2 >= 3)
                        {
                            transition2 = true;
                        }
                    }
                }
                else
                {
                    startfacto *= 0.9f;
                    if(!playadone) { playa.Play(); playadone = true; }
                    if (startfacto.x <= 0.001)
                    {
                        startfacto = new Vector3(0, 0, 0);
                        transition1 = false;
                        transition2 = false;
                        timer1 = 0;
                        timer2 = 0;
                        kamdone = false;
                    }
                }
            }
        }

        if (coinsManager.won)
        {
            timer1 += Time.deltaTime;
            if(!bigApp) { bigApplause.Play(); bigApp = true; }
            if(playadone) { playa.volume -= Time.deltaTime/10; if(playa.volume <= 0) { playa.volume = 0.2f; playadone = false; playa.Stop(); } }
            if (timer1>= 1)
            {
                winText.rectTransform.localScale = factory;
                factory *= 1.1f;
                if (factory.x >= 1)
                {
                    factory = new Vector3(1,1,1);
                }
            }
            if (timer1 >= 10)
            {
                lstCurtains[0].rectTransform.sizeDelta += new Vector2(0, Time.deltaTime * 200);
                lstCurtains[1].rectTransform.sizeDelta += new Vector2(0, Time.deltaTime * 200);
                if (lstCurtains[0].rectTransform.sizeDelta.y >= Screen.height / 2 + Screen.height / 3 || lstCurtains[1].rectTransform.sizeDelta.y >= Screen.height / 2 + Screen.height / 3)
                {
                    seaVibe.volume -= Time.deltaTime / 10;
                    if(seaVibe.volume <=0)
                    {
                        SceneManager.LoadScene(0);
                    }
                }
            }
        }
    }

}
