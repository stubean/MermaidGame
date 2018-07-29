using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHelp_Controller : MonoBehaviour {

    public enum HelpType { EKey, GKey, FKey };

    Text fKey, eKey, gKey;
    float ShowTime = 4f;

    private void Awake()
    {
        eKey = GameObject.Find("Help E Text").GetComponent<Text>();
        gKey = GameObject.Find("Help G Text").GetComponent<Text>();
        fKey = GameObject.Find("Help F Text").GetComponent<Text>();

    }

    public void ShowHelp(HelpType helpType)
    {
        switch (helpType)
        {
            case HelpType.EKey:
                eKey.color = new Color(1, 1, 1, 1);
                gKey.color = new Color(1, 1, 1, 0);
                fKey.color = new Color(1, 1, 1, 0);
                return;
            case HelpType.GKey:
                eKey.color = new Color(1, 1, 1, 0);
                gKey.color = new Color(1, 1, 1, 1);
                fKey.color = new Color(1, 1, 1, 0);
                return;
            case HelpType.FKey:
                eKey.color = new Color(1, 1, 1, 0);
                gKey.color = new Color(1, 1, 1, 0);
                fKey.color = new Color(1, 1, 1, 1);
                return;
        }

        Invoke("HideHelp", ShowTime);
    }

    void HideHelp()
    {
        eKey.color = new Color(1, 1, 1, 0);
        gKey.color = new Color(1, 1, 1, 0);
        fKey.color = new Color(1, 1, 1, 0);
    }

    

}
