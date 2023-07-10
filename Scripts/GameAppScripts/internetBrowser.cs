using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class internetBrowser : MonoBehaviour
{
    [SerializeField] GameObject WebsiteOne, WebsiteTwo, CHAT_Website, SecretSite, FourOFour_ErrorPage, jokeSite;
    [SerializeField] Text searchBarText;
    [SerializeField] UI_Interface pauseScript;
    int websiteIndex = 0;
    [SerializeField] Ingame_PopUps popUpScript;

    public void GetSearchInput()
    {
        SearchURL(searchBarText.text);
    }

    public int GetCurrentWebsite()
    {
        return websiteIndex;
    }

    void SearchURL(string url)
    {
        bool[] vals_0 = {false, false, false, false, false, true};

        switch (url)
        {
            case "43.184.169.19": // website 1
                bool[] vals = {true, false, false, false, false, false};
                SiteSwitch(vals);
                websiteIndex = 1;
            break;
            case "197.16.211.64": // website 2
                bool[] vals2 = {false, true, false, false, false, false};
                SiteSwitch(vals2);
                websiteIndex = 2;
            break;
            case "chat.org": // CHAT website alt input
                bool[] vals3 = {false, false, true, false, false, false};
                SiteSwitch(vals3);
                websiteIndex = 3;
            break;
            case "https://chat.org/": // CHAT website
                bool[] vals4 = {false, false, true, false, false, false};
                SiteSwitch(vals4);
                websiteIndex = 3;
            break;                            //                                      FTP   website  smb  email  website CHAT   <== Locations of the hashes
            case "http://o38gxU5s9uvQ937t02bgvaEb.vkm": // hash website (true ending) { <-start 1-o38g 2-xU5s 3-9uvQ 4-937t 5-02bg 6-vaEb end-/> }
                bool[] vals5 = {false, false, false, true, false, false};
                SiteSwitch(vals5);
                websiteIndex = 4;
                popUpScript.FoundEncrypted_SSH_Hash();
            break;
            case "https://o38gxU5s9uvQ937t02bgvaEb.vkm": // secret site varient 1
                bool[] vals5_1 = {false, false, false, true, false, false};
                SiteSwitch(vals5_1);
                websiteIndex = 4;
                popUpScript.FoundEncrypted_SSH_Hash();
            break;
            case "o38gxU5s9uvQ937t02bgvaEb.vkm": // secret site varient 2
                bool[] vals5_2 = {false, false, false, true, false, false};
                SiteSwitch(vals5_2);
                websiteIndex = 4;
                popUpScript.FoundEncrypted_SSH_Hash();
            break;
            case "pornhub.com":
                SiteSwitch(vals_0);
                websiteIndex = 5;
            break;
            case "pornhub":
                SiteSwitch(vals_0);
                websiteIndex = 5;
            break;
            case "https://pornhub.com":
                SiteSwitch(vals_0);
                websiteIndex = 5;
            break;
            case "rule34":
                SiteSwitch(vals_0);
                websiteIndex = 5;
            break;
            case "https://rule34.com":
                SiteSwitch(vals_0);
                websiteIndex = 5;
            break;
            case "https://xxvideos.com":
                SiteSwitch(vals_0);
                websiteIndex = 5;
            break;
            case "xxvideos":
                SiteSwitch(vals_0);
                websiteIndex = 5;
            break;
            
            default:
                // url invalid (404 error)
                bool[] vals0 = {false, false, false, false, true, false};
                SiteSwitch(vals0);
                websiteIndex = 0;
            break;
        }
    }

    void SiteSwitch(bool[] values)
    {
        WebsiteOne.SetActive(values[0]);
        WebsiteTwo.SetActive(values[1]);
        CHAT_Website.SetActive(values[2]);
        SecretSite.SetActive(values[3]);
        FourOFour_ErrorPage.SetActive(values[4]);
        jokeSite.SetActive(values[5]);
    }

    public void FunnyOutput() // button on joke website
    {
        pauseScript.ExternalPause();

        string[] holyURLs = {
            "https://www.firstchristian.com/",
            "https://greenfordchristian.org/",
            "https://www.orrville.church/",
            "https://cliftonmosque.org/",
            "https://raleighmasjid.org/",
            "https://icgt.org/",
            "https://www.myjewishlearning.com/",
            "https://www.youtube.com/watch?v=9Deg7VrpHbM",
            "https://www.youtube.com/watch?v=bN1tW2o23Qk"
        };

        Process.Start(holyURLs[UnityEngine.Random.Range(0, holyURLs.Length)]);

        MinimizeGame();
    }

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    void MinimizeGame()
    {
        ShowWindow(GetActiveWindow(), 2);
    }
}//EndScript