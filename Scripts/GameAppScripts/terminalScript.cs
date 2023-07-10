using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class terminalScript : MonoBehaviour
{
    [SerializeField] RectTransform terminalRect, terminalMainRect, inputPivotRect, inputRect;
    [SerializeField] InputField commandInput;
    [SerializeField] Text terminalDisplayText;
    [SerializeField] EventSystem gameEventSys;
    [SerializeField] Scrollbar terminalScrollBar;
    [SerializeField] AppWindow terminalWindow;
    [SerializeField] emailAppScript emailApp;
    [SerializeField] CHAT_App_Script CHAT_App;
    [SerializeField] internetBrowser webBrowserApp;
    [SerializeField] TrueEndingEvent TrueEndScript;
    [SerializeField] EventSoundHandler soundEvent;

    string archiveString, dynamicString;
    string site1 = "=== START ===\n<!DOCTYPE html>\n <html lang='en'>\n   <head>\n     <meta charset='utf-8'>\n     <title>Issac's Pixel-rama</title>\n   </head>\n\n    <body>\n     <h1 id='title'> Issac's Pixel-rama </h1>\n     <p>Main Display</p>\n     <div id='navigation'>\n         <button>Home</button>\n         <button>Full Gallary</button>\n         <button>About Us</button>\n         <button>Contact</button>\n     </div>\n\n      <div id='gallary'>\n         <img id='art' src='images/img00.png'>\n         <img id='art' src='images/img01.png'>\n         <img id='art' src='images/img02.png'>\n         <img id='art' src='images/img03.png'>\n         <img id='art' src='images/img04.png'>\n         <img id='art' src='images/img05.png'>\n         <img id='art' src='images/img06.png'>\n         <img id='art' src='images/img07.png'>\n         <img id='art' src='images/img08.png'>\n         <img id='art' src='images/img09.png'>\n     </div>\n\n      <?php\n         // print 2-xU5s\n     ?>\n\n      <link rel='stylesheet' href='style.css'>\n   </body>\n\n    <footer>\n     <h2>&copy; Atrerious Studios 2017</h2>\n   </footer>\n </html>\n === END ===";
    string site2 = "=== START ===\n<!DOCTYPE html> <html lang='en'>\n   <head>\n     <meta charset='utf-8'>\n     <title>Cars-R-Us</title>\n   </head>\n    <body>\n     <h1 id='title'> Cars-R-Us </h1>\n     <div id='navigation'>\n         <button>Home</button>\n         <button>More Cars</button>\n         <button>About Us</button>\n         <button>Contact</button>\n     </div>\n\n      <div id='display'>\n         <img id='art' src='images/carImage.png'>\n         <img id='art' src='images/carImage2.png'>\n         <img id='art' src='images/carImage3.png'>\n         <img id='art' src='images/carImage4.png'>\n     </div>\n\n      <?php\n         // print 5-02bg\n     ?>\n\n      <link rel='stylesheet' href='style2.css'>\n   </body>\n\n    <footer>\n     <h2>&copy; Cars-R-Us 2022</h2>\n   </footer>\n </html>\n === END ===";
    string site3 = "=== START ===\n<!DOCTYPE html>\n <html lang='en'>\n\n  <head>\n     <meta charset='utf-8'>\n     <title>CHAT Home</title>\n </head>\n\n  <body>\n     <h1 id='title'> CHAT </h1>\n\n      <nav>\n         <a>Home</a> &nbsp;\n         <a>About Us</a> &nbsp;\n         <a>Contact</a>\n     </nav>\n\n      <main>\n         <p>\n             Largest chatroom service across the globe!\n             We include a promising streaming experience while you communicate with users across the world\n             No Lag, No broadband issue, & No interference guaranteed!\n         </p>\n\n          <p>\n             This application was originally targeted for the adult demographic (18+)\n             We've worked hard for many months to seamlessly integrate the best safety features so your children are\n             protected when online.\n             We at CHAT understand the recent generation demographic and their need to feel like they are included and/or\n             have a group they can be a part of;\n             This is why we at CHAT have been striving to make our chatrooms safe and secure! <br><br>\n         </p>\n\n          <img id='img1' src='images/CHAT_img.png'> <br><br>\n\n          <p id='illegal'>\n             If you believe you've encountered fraudlent activity such as: <br> Cyber-Harrassment, Cyber-Grooming,\n             Terrorism, Phishers, Traffic Rings, Digital Stalkers, or other threats not weren't disclosed here;\n         </p>\n\n          <p>\n             Please submit a report here: <a>Report Fraudlent User</a> &nbsp; We will do all in our power through our\n             trusty legal system and undo their wrongs!\n         </p>\n\n          <p id='externalChunk'>\n             <a id='ext'>CHAT Forum</a> &nbsp;\n             <a id='ext2'>Rate Us</a> &nbsp;\n             <a id='ext3'>Support</a>\n         </p>\n\n          <img id='img2' src='images/downloadImage.png'>\n     </main>\n\n      <link rel='stylesheet' href='style3.css'>\n\n      <footer>\n         <p>Copyright &copy; 2022 CHAT &nbsp; <a id='email'>chatInbox@chat.ch</a> </p>\n     </footer>\n </body>\n\n  </html>\n=== END ===";
    string site4 = "Stop being Naughty!";
    string site5 = "404 Error";
    string baseCmd, curSmbDir, curSshDir;
    bool FPT_Mode = false, SMB_Mode = false, SSH_Mode = false, takeNoInput = false, SSH_LoggedIn = false, passwordCracking = false;
    int ftpSessionID = 0, smbDirIndex = 0, sshDirIndex = 0, crackerIndex = 0, colorMode = 0;
    [SerializeField] bool[] hashFound = {false, false, false, false, false, false};
    [SerializeField] Ingame_PopUps popUpScript;
    List<string> prevCmdList = new List<string>();
    int selectedIndexInCmdList;

    bool hasLoggedIntoTheFTP = false;

    [SerializeField] GameObject executableIcon, antiVirusPopUp;

    void Start()
    {
        terminalDisplayText.text = "user@ilak:~$  \n\n";
        executableIcon.SetActive(false);
        antiVirusPopUp.SetActive(false);
    }

    void Update()
    {
        inputRect.position = inputPivotRect.position;

        SendWarningForFindingHashes();

        MoveThroughCmdList();
    }

    public int NumberOfHashesFound()
    {
        return hashFound.Count(c => c);
    }

    void SendWarningForFindingHashes()
    {
        if (NumberOfHashesFound() == 3)
        {
            //emailApp
            soundEvent.ChairMoved();
        } else if (NumberOfHashesFound() == 6)
            {
                emailApp.RecievedEmail(4);
            } else if (NumberOfHashesFound() == 1)
                {
                    popUpScript.FoundFirstHash();
                }
    }

    void MoveThroughCmdList()
    {
        if (!FPT_Mode && !SMB_Mode && !SSH_Mode)
        {
            if (!passwordCracking)
            {
                if (colorMode == 0)
                {
                    terminalDisplayText.color = Color.white;
                } else
                    {
                        terminalDisplayText.color = Color.green;
                    }

                // regular prompt : no ssh, ftp, smb, etc
                if (commandInput.isFocused && prevCmdList.ToArray().Length > 0)
                {
                    if (Input.GetKeyDown(KeyCode. UpArrow))
                    {
                        // scroll back
                        if (selectedIndexInCmdList > 0)
                        {
                            selectedIndexInCmdList--;
                            commandInput.text = prevCmdList.ToArray()[selectedIndexInCmdList];
                        }
                        commandInput.caretPosition = commandInput.text.Length;
                    } else if (Input.GetKeyDown(KeyCode. DownArrow))
                        {
                            // scroll forward
                            if (selectedIndexInCmdList < prevCmdList.ToArray().Length - 1)
                            {
                                selectedIndexInCmdList++;
                                commandInput.text = prevCmdList.ToArray()[selectedIndexInCmdList];
                            }
                            commandInput.caretPosition = commandInput.text.Length;
                        }
                }
            }
        }
    }

    void StoreInput(string cmd)
    {
        prevCmdList.Add(cmd);
        selectedIndexInCmdList = prevCmdList.ToArray().Length;
    }

    public void ReadCommand()
    {
        if (!takeNoInput)
        {
            string cmd = commandInput.text;

            if (!FPT_Mode && !SMB_Mode && !SSH_Mode)
            {
                if (!passwordCracking)
                {
                    regularPrompt(cmd);
                    if (!string.IsNullOrWhiteSpace(cmd))
                    {
                        StoreInput(cmd);
                    }
                } else
                    {
                        deagonCracker(cmd);
                    }
            } else if (FPT_Mode && !SMB_Mode && !SSH_Mode)
                {
                    string lsSessContents;

                    if (ftpSessionID == 0)
                    {
                        lsSessContents = "hash.txt  companyLogo.jpeg   TODO.txt   project.exe\n";
                        FTP_Prompt(cmd, lsSessContents);
                    } else if (ftpSessionID == 1)
                        {
                            lsSessContents = "newBusinessProtocal.pdf  r.txt  newTermsOfService.docx\n";
                            FTP_Prompt(cmd, lsSessContents);
                        } else if (ftpSessionID == 2)
                            {
                                lsSessContents = "userReadable.txt  config.ini  rootReadOnly.txt\n";
                                FTP_Prompt(cmd, lsSessContents);
                            };
                } else if (!FPT_Mode && SMB_Mode && !SSH_Mode)
                    {
                        SMB_Prompt(cmd);
                    } else if (!FPT_Mode && !SMB_Mode && SSH_Mode)
                        {
                            SSH_Prompt(cmd);
                        };

            commandInput.text = null;

            commandInput.ActivateInputField();

            snapScrollBar();
        }
    }
    
//====================================================================================================================

    IEnumerator Output_Pmap(string additionalText)
    {
        takeNoInput = true;
        terminalDisplayText.text += "Pmap Scan running. . .\n";
        yield return new WaitForSeconds(2.26f);
        terminalDisplayText.text += additionalText;
        takeNoInput = false;
    }

    IEnumerator Connect_FTP(string additionalText, string additionalText2)
    {
        takeNoInput = true;
        terminalDisplayText.text += additionalText;
        yield return new WaitForSeconds(3);
        terminalDisplayText.text += additionalText2;

        Invoke("AddIndent", 0.025f);
        takeNoInput = false;
        terminalDisplayText.color = Color.white;
    }

    IEnumerator Connect_SMB(string additionalText, string additionalText2)
    {
        takeNoInput = true;
        terminalDisplayText.text += additionalText;
        yield return new WaitForSeconds(3);
        terminalDisplayText.text += additionalText2;

        Invoke("AddIndent", 0.025f);
        takeNoInput = false;
        terminalDisplayText.color = Color.white;
    }

    IEnumerator Connect_SSH(string additionalText, string additionalText2)
    {
        takeNoInput = true;
        terminalDisplayText.text += additionalText;
        yield return new WaitForSeconds(3);
        terminalDisplayText.text += additionalText2;

        Invoke("AddIndent", 0.025f);
        takeNoInput = false;
        terminalDisplayText.color = Color.white;
    }

    IEnumerator SendPhishEmail(int phishIndex)
    {
        yield return new WaitForSeconds(10);
        emailApp.RecievedEmail(phishIndex);
    }

    IEnumerator Cracker_Mode()
    {
        takeNoInput = true;
        terminalDisplayText.text += "[*] Setting Up Deagon_Cracker. . .\n";
        yield return new WaitForSeconds(1.5f);
        terminalDisplayText.text += "[+] Instance of Deagon_Cracker Created!\n  Enter Password To Crack : ";

        Invoke("AddIndent", 0.025f);
        takeNoInput = false;
        passwordCracking = true;
        Invoke("snapScrollBar", 0.025f);
        terminalDisplayText.color = new Color32(229, 105, 0, 255);
    }

    void AddIndent()
    {
        terminalDisplayText.text += "\n\n";
        snapScrollBar();
    }

    public void EndTerminalInstance()
    {
        terminalDisplayText.color = Color.white;
        colorMode = 0;
        prevCmdList.Clear();
        terminalDisplayText.text = "user@ilak:~$  \n\n";
        FPT_Mode = false; SMB_Mode = false; SSH_Mode = false; takeNoInput = false; SSH_LoggedIn = false; passwordCracking = false;
    }

//====================================================================================================================

    void ftpStatus(bool value)
    {
        FPT_Mode = value;
    }

    void smbStatus(bool value)
    {
        SMB_Mode = value;
    }

    void sshStatus(bool value)
    {
        SSH_Mode = value;
    }

    public void snapScrollBar()
    {
        terminalScrollBar.value = 0;
    }

//====================================================================================================================
//====================================================================================================================
//====================================================================================================================

    void deagonCracker(string cmdStr)
    {
        takeNoInput = true;
        archiveString = terminalDisplayText.text;

        crackerIndex = 0;

        if (cmdStr == "exit")
        {
            terminalDisplayText.text += cmdStr + "\n [-] Session Ended\n\nuser@ilak:~$ Try help to see a list of commands!\n\n";

            takeNoInput = false;
            passwordCracking = false;
            Invoke("snapScrollBar", 0.025f);

            return;
        } else
            {
                terminalDisplayText.text += " " + cmdStr;
                StartCoroutine(CrackDelay(cmdStr));
            }
    }

    IEnumerator CrackDelay(string cmdStr)
    {
        string Ienum_hash = cmdStr;

        dynamicString = "\n" + "ATTEMPT " + crackerIndex + " of 250\n\n";

        terminalDisplayText.text = archiveString + dynamicString;

        if (crackerIndex == 168)
        {
            // Debug.Log(cmdStr.Equals("4en0983bcq2_!#$!"));

            if (cmdStr.Equals("4en0983bcq2_!#$!"))
            {
                terminalDisplayText.text += "[+] 1 Match Found! Passwd : #N3IWdH8Sw3q@h05K!^VShy!6\n  Type help for a list of commands.\n\n";

                soundEvent.FootStepsComingCloser();

                takeNoInput = false;
                passwordCracking = false;
                Invoke("snapScrollBar", 0.025f);
            }
        }
        
        yield return new WaitForSeconds(0.1f);

        if (passwordCracking)
        {
            if (crackerIndex < 250)
            {
                crackerIndex++;
                StartCoroutine(CrackDelay(Ienum_hash));
            } else
                {
                    terminalDisplayText.text += "[-] No Matches Found!\n\nEnter Password To Crack : ";

                    Invoke("AddIndent", 0.025f);
                    takeNoInput = false;
                    passwordCracking = true;
                    Invoke("snapScrollBar", 0.025f);
                }
        }
    }

//=======================================================================================================================
//=======================================================================================================================
//=======================================================================================================================
//=======================================================================================================================
//=======================================================================================================================

    void regularPrompt(string cmdStr)
    {
        switch (cmdStr)
        {
            case "":
                baseCmd="";
                terminalDisplayText.text += "user@ilak:~$  \n";
            break;
            case " ":
                baseCmd="";
                terminalDisplayText.text += "user@ilak:~$  \n";
            break;
            case "help":
                baseCmd="";
                terminalDisplayText.text += "user@ilak:~$ help\nwhoami - display current user\ncolor [green/white] - change text color\nclear - clears terminal\nexit - close terminal\nsplunk [CHAT App Active]\nskaw [email] - phishing attempt to gain more information\npmap [ip address] - port scan\nftp [ip address] - connect to ftp server\nsmb [ip address] - connect to smb server\nssh [username] - log on through ssh\nvrm - virus remover\ninspect [website open] - show source code for website\ndeagon_cracker - password cracker\nuser@ilak:~$  \n";
                // terminalMainRect.sizeDelta = new Vector2(terminalMainRect.sizeDelta.x, terminalMainRect.sizeDelta.y + 265);
            break;
            case "whoami":
                baseCmd="";
                terminalDisplayText.text += "user@ilak:~$ whoami \n user\n";
            break;
            case "color green":
                baseCmd="color";
                terminalDisplayText.color = Color.green;
                colorMode = 1;
                terminalDisplayText.text += "user@ilak:~$ color green \n";
            break;
            case "color white":
                baseCmd="color";
                terminalDisplayText.color = Color.white;
                colorMode = 0;
                terminalDisplayText.text += "user@ilak:~$ color white \n";
            break;
            case "exit":
                baseCmd="";
                terminalWindow.ClosePage();
                prevCmdList.Clear();
                terminalDisplayText.text = "user@ilak:~$  \n";
            break;
            case "clear":
                baseCmd="";
                terminalDisplayText.text = "user@ilak:~$ [Reminder] Type help for a list of commands.\n";
            break;
            case "splunk": // get current channel index and output based on index
                baseCmd="";
                if (CHAT_App.GetActiveChatChannel() == 0)
                {
                    terminalDisplayText.text += "user@ilak:~$ splunk \n user_email: swiftymarketer825@swifty.nxt\n";
                } else if (CHAT_App.GetActiveChatChannel() == 1)
                    {
                        terminalDisplayText.text += "user@ilak:~$ splunk \n user_email: davycrocket2001@wisp.slu\n";
                    } else if (CHAT_App.GetActiveChatChannel() == 2) // outputs 6-vaEb
                        {
                            terminalDisplayText.text += "user@ilak:~$ splunk \n ERROR:6-vaEb.vkm\n";
                            hashFound[5] = true;
                        }
            break;
            case "pmap 197.16.211.64":
                baseCmd="pmap";
                string resultTxt = "user@ilak:~$ pmap 197.16.211.64\nPmap scan report for 197.16.211.64\nHost is up (0.0020s latency).\n\nPort  Service\n21      FTP\n80      HTTP\nPmap done: 1 IP address (1 host up) scanned in 2.26 seconds\nuser@ilak:~$ \n\n";
                StartCoroutine(Output_Pmap(resultTxt));
            break;
            case "pmap 43.184.169.19":
                baseCmd="pmap";
                string resultTxt2 = "user@ilak:~$ pmap 43.184.169.19\nPmap scan report for 43.184.169.19\nHost is up (0.0020s latency).\n\nPort  Service\n80      HTTP\n445      SMB\nPmap done: 1 IP address (1 host up) scanned in 2.26 seconds\nuser@ilak:~$ \n\n";
                StartCoroutine(Output_Pmap(resultTxt2));
            break;
            case "pmap 64.155.13.52":
                baseCmd="pmap";
                string resultTxt4 = "user@ilak:~$ pmap 43.184.169.19\nPmap scan report for 43.184.169.19\nHost is up (0.0020s latency).\n\nPort  Service\n21      FTP\nPmap done: 1 IP address (1 host up) scanned in 2.26 seconds\nuser@ilak:~$ \n\n";
                StartCoroutine(Output_Pmap(resultTxt4));
            break;
            case "pmap 73.251.35.145": // secret
                baseCmd="pmap";
                string resultTxt3 = "user@ilak:~$ pmap 73.251.35.145\nPmap scan report for 73.251.35.145\nHost is up (0.0020s latency).\n\nPort  Service\n21      FTP\n22      SSH\n80      HTTP\nPmap done: 1 IP address (1 host up) scanned in 2.26 seconds\nuser@ilak:~$ \n\n";
                StartCoroutine(Output_Pmap(resultTxt3));
                emailApp.RecievedEmail(5);
            break;
            case "ftp 197.16.211.64": // carries hash 
                baseCmd="ftp";
                string ftp1 = "user@ilak:~$ ftp 197.16.211.64\nConnected to 197.16.211.64\n220 (vsFTPd) 6.26.7\n======\n";
                string ftp2 = "Remote system type is XION\nFTP>";
                StartCoroutine(Connect_FTP(ftp1, ftp2));
                ftpSessionID = 0; // Jacolyn
                ftpStatus(true);
            break;
            case "ftp 73.251.35.145": // warning text file
                baseCmd="ftp";
                string ftp3 = "user@ilak:~$ ftp 73.251.35.145\nConnected to 73.251.35.145\n220 (vsFTPd) 5.15.6\n======\n";
                string ftp4 = "Remote system type is VEXA\nFTP>";
                StartCoroutine(Connect_FTP(ftp3, ftp4));
                ftpSessionID = 1; // Secret IP
                ftpStatus(true);
            break;
            case "ftp 64.155.13.52": // ip given in dialogue
                baseCmd="ftp";
                string ftp5 = "user@ilak:~$ ftp 64.155.13.52\nConnected to 64.155.13.52\n220 (vsFTPd) 6.11.2\n======\n";
                string ftp6 = "Remote system type is VEXA\nFTP>";
                StartCoroutine(Connect_FTP(ftp5, ftp6));
                ftpSessionID = 2; // Nickalas
                ftpStatus(true);

                Invoke("InsideFTPSession", 10);
            break;
            case "smb 43.184.169.19":
                baseCmd="smb";
                string smb = "user@ilak:~$ smb 43.184.169.19\nUser SMB Established on 43.184.169.19\n======\n";
                string smb2 = "Using : (x86) MD_Local)\nSMB>";
                StartCoroutine(Connect_SMB(smb, smb2));
                smbDirIndex = 0; // Jeseph
                smbStatus(true);
            break;
            case "ssh D!sRupTSepi0l@73.251.35.145":
                baseCmd="ssh";
                string ssh1 = "user@ilak:~$ ssh D!sRupTSepi0l@73.251.35.145\nssh: Connect to Host 73.251.35.145 port 22: Connection accepted\n======\n";
                string ssh2 = "D!sRupTSepi0l@73.251.35.145's Password :\n";
                StartCoroutine(Connect_SSH(ssh1, ssh2));
                sshDirIndex = 0;
                sshStatus(true);
            break;
            case "inspect": // check what website is open
                baseCmd="";
                switch (webBrowserApp.GetCurrentWebsite())
                {
                    case 1:
                        terminalDisplayText.text += "user@ilak:~$ " + cmdStr + "\n" + site1 + "\n";
                        hashFound[1] = true;
                    break;
                    case 2:
                        terminalDisplayText.text += "user@ilak:~$ " + cmdStr + "\n" + site2 + "\n";
                        hashFound[4] = true;
                    break;
                    case 3:
                        terminalDisplayText.text += "user@ilak:~$ " + cmdStr + "\n" + site3 + "\n";
                    break;
                    case 4:
                        terminalDisplayText.text += "user@ilak:~$ " + cmdStr + "\n" + site4 + "\n";
                    break;
                    case 5:
                        terminalDisplayText.text += "user@ilak:~$ " + cmdStr + "\n" + site5 + "\n";
                    break;

                    default:
                    break;
                }
            break;
            case "skaw swiftymarketer825@swifty.nxt":
                baseCmd = "skaw";
                terminalDisplayText.text += "user@ilak:~$ " + cmdStr + " Spoofing Email. . .\n  [*] Check your inbox after 15 seconds\n";
                StartCoroutine(SendPhishEmail(0));
            break;
            case "skaw davycrocket2001@wisp.slu": // Jacolyn
                baseCmd = "skaw";
                terminalDisplayText.text += "user@ilak:~$ " + cmdStr + " Spoofing Email. . .\n  [*] Check your inbox after 15 seconds\n";
                StartCoroutine(SendPhishEmail(1));
                hashFound[3] = true;
            break;
            case "vrm": // Virus Cleaner Command : // ftp 197.16.211.64
                if (executableIcon != null)
                {
                    if (executableIcon.activeSelf)
                    {
                        minigame_handler minigameHandlerScript = Object.FindObjectOfType<minigame_handler>();
                        minigameHandlerScript.StopExecutable();

                        VirusEvent viEventScript = Object.FindObjectOfType<VirusEvent>();
                        viEventScript.AntiVirusDefence();

                        virusWindow[] viWindowsInScene = FindObjectsOfType<virusWindow>();
                        foreach (virusWindow viWin in viWindowsInScene)
                        {
                            viWin.ClickingOffVirusWindow();
                        }

                        foreach (AudioSource virusSound in viEventScript.GetVirusSoundList())
                        {
                            virusSound.mute = true;
                        }

                        terminalDisplayText.text += "user@ilak:~$ " + cmdStr + " [*] Scanning Volumes. . .\n  [+] Malicious Files Detected and Removed!\n";
                        Destroy(executableIcon);
                        antiVirusPopUp.SetActive(true);
                    } else
                        {
                            terminalDisplayText.text += "user@ilak:~$ " + cmdStr + " [*] Scanning Volumes. . .\n  [*] No Malicious Files Detected!\n";
                        }
                } else
                    {
                        terminalDisplayText.text += "user@ilak:~$ " + cmdStr + " [*] Scanning Volumes. . .\n  [*] No Malicious Files Detected!\n";
                    }
                baseCmd="";
            break;
            case "deagon_cracker": // ask for string and let the cracking commence but at the end if the string isnt correct it says 0 Matches Found!
                baseCmd="";
                StartCoroutine(Cracker_Mode());
                passwordCracking = true;
            break;

//============================================================================================================================================================================
//=========================== REGULAR COMMAND ERROR HELP =====================================================================================================================
//============================================================================================================================================================================

            default:
                if (cmdStr[0] == 's' && cmdStr[1] == 'k' && cmdStr[2] == 'a' && cmdStr[3] == 'w')
                {
                    terminalDisplayText.text += "user@ilak:~$ " + cmdStr + " : Email Not Valid, Check For Miss-Spells\n";
                }
                else if (cmdStr[0] == 's' && cmdStr[1] == 'm' && cmdStr[2] == 'b')
                {
                    terminalDisplayText.text += "user@ilak:~$ " + cmdStr + " : Connection Refused.\n";
                }
                else if (cmdStr[0] == 'f' && cmdStr[1] == 't' && cmdStr[2] == 'p')
                {
                    terminalDisplayText.text += "user@ilak:~$ " + cmdStr + " : Connection Refused.\n";
                }
                else if (cmdStr[0] == 's' && cmdStr[1] == 's' && cmdStr[2] == 'h')
                {
                    terminalDisplayText.text += "user@ilak:~$ " + cmdStr + " : Connection Refused.\n";
                }
                else if (cmdStr[0] == 'p' && cmdStr[1] == 'm' && cmdStr[2] == 'a' && cmdStr[3] == 'p')
                {
                    terminalDisplayText.text += "user@ilak:~$ " + cmdStr + " : Destination host unreachable\n";
                }
                else if (cmdStr[0] == 'c' && cmdStr[1] == 'o' && cmdStr[2] == 'l' && cmdStr[3] == 'o' && cmdStr[4] == 'r')
                {
                    terminalDisplayText.text += "user@ilak:~$ " + cmdStr + " : Color not Supported : try white|green \n";
                }
                else
                {
                    switch (baseCmd)
                    {
                        case "skaw":
                            terminalDisplayText.text += "user@ilak:~$ " + cmdStr + " : Email Not Valid, Check For Miss-Spells\n";
                            baseCmd="";
                            break;
                        case "pmap":
                            terminalDisplayText.text += "user@ilak:~$ " + cmdStr + " : Destination host unreachable\n";
                            baseCmd="";
                            break;
                        case "ftp":
                            terminalDisplayText.text += "user@ilak:~$ " + cmdStr + " : Connection Refused.\n";
                            baseCmd="";
                            break;
                        case "smb":
                            terminalDisplayText.text += "user@ilak:~$ " + cmdStr + " : Connection Refused.\n";
                            baseCmd="";
                            break;
                        case "ssh":
                            terminalDisplayText.text += "user@ilak:~$ " + cmdStr + " : Connection Refused.\n";
                            baseCmd="";
                            break;
                        case "color":
                            terminalDisplayText.text += "user@ilak:~$ " + cmdStr + " : Color not Supported : try white|green \n";
                            baseCmd="";
                            break;

                        default:
                            terminalDisplayText.text += "user@ilak:~$ " + cmdStr + " : Execution Failed, try help to see a list of commands\n";
                            baseCmd="";
                            break;
                    }
                }
            break;
        }

        terminalDisplayText.text += "\n";
        Invoke("snapScrollBar", 0.025f);
    }

    void InsideFTPSession()
    {
        hasLoggedIntoTheFTP = true;
    }

    public bool HasPlayerLoggedViaFTP()
    {
        return hasLoggedIntoTheFTP;
    }

    void FTP_Prompt(string cmdStr, string lsContents)
    {
        string lsCon = lsContents;
        terminalDisplayText.text = terminalDisplayText.text.Remove(terminalDisplayText.text.Length - 2);

        if (!string.IsNullOrWhiteSpace(cmdStr))
        {
            switch (cmdStr)
            {
                case "help":
                    baseCmd = "";
                    terminalDisplayText.text += cmdStr + "\nls - show files in current directory\ntac [filename] - read file content\nmget [filename] - Download File To Local Machine\nexit - exit FTP session\nFTP>";
                break;
                case "ls":
                    baseCmd = "";
                    terminalDisplayText.text += cmdStr + "\n" + lsCon + "\nFTP>";
                break;
                case "tac hash.txt":
                    baseCmd = "tac";
                    if (ftpSessionID == 0) { terminalDisplayText.text += cmdStr + "\n" + "1-o38g" + "\nFTP>"; hashFound[0] = true; } else { terminalDisplayText.text += "\n" + cmdStr + " : Execution Failed, try help to see a list of commands\nFTP>"; };
                break;
                case "tac companyLogo.jpeg":
                    baseCmd = "tac";
                    if (ftpSessionID == 0) { terminalDisplayText.text += cmdStr + "\n" + "[cannot read content]" + "\nFTP>"; } else { terminalDisplayText.text += "\n" + cmdStr + " : Execution Failed, try help to see a list of commands\nFTP>"; };
                break;
                case "tac TODO.txt": // viewing file triggers linked AI
                    baseCmd = "tac";
                    if (ftpSessionID == 0) { CHAT_App.TriggerAI_Two(); terminalDisplayText.text += cmdStr + "\n" + "1.) Fixed LinearJS\n2.) Debug Backend\n3.) reRoUTe sIMulAtiOns" + "\nFTP>"; } else { terminalDisplayText.text += "\n" + cmdStr + " : Execution Failed, try help to see a list of commands\nFTP>"; };
                break;
                case "mget project.exe": // mini-game
                    baseCmd = "mget";
                    if (ftpSessionID == 0) // ftp 197.16.211.64
                    {
                        terminalDisplayText.text += cmdStr + "\n" + "Downloading project.exe. . .\n[=================] 100%" + "\nFTP>";

                        if (executableIcon == null)
                        {
                            terminalDisplayText.text += cmdStr + "\n" + "Downloading project.exe. . .\n[=================] 100%" + "\nFTP>";
                            antiVirusPopUp.SetActive(true);
                        } else
                            {
                                if (!executableIcon.activeSelf)
                                {
                                    terminalDisplayText.text += cmdStr + "\n" + "Downloading project.exe. . .\n[=================] 100%" + "\nFTP>";
                                    executableIcon.SetActive(true);
                                } else
                                    {
                                        terminalDisplayText.text += cmdStr + "\n" + "project.exe already downloaded on Local Machine. . .\n" + "\nFTP>";
                                    }
                            }
                    } else
                        {
                            terminalDisplayText.text += "\n" + cmdStr + " : mget Not Allowed.\nFTP>";
                        };
                break;
                case "tac newBusinessProtocal.pdf":
                    baseCmd = "tac";
                    if (ftpSessionID == 1) { terminalDisplayText.text += cmdStr + "\n" + "[cannot read content]" + "\nFTP>"; } else { terminalDisplayText.text += "\n" + cmdStr + " : Execution Failed, try help to see a list of commands\nFTP>"; };
                break;
                case "tac r.txt": // viewing file triggers random event
                    baseCmd = "tac";
                    if (ftpSessionID == 1) { terminalDisplayText.text += cmdStr + "\n" + "The effort you're putting in is going to get you killed..." + "\nFTP>"; } else { terminalDisplayText.text += "\n" + cmdStr + " : Execution Failed, try help to see a list of commands\nFTP>"; };
                break;
                case "tac newTermsOfService.docx":
                    baseCmd = "tac";
                    if (ftpSessionID == 1) { terminalDisplayText.text += cmdStr + "\n" + "[cannot read content]" + "\nFTP>"; } else { terminalDisplayText.text += "\n" + cmdStr + " : Execution Failed, try help to see a list of commands\nFTP>"; };
                break;
                case "tac userReadable.txt":
                    baseCmd = "tac";
                    if (ftpSessionID == 2) { terminalDisplayText.text += cmdStr + "\n" + "Permission Denied!" + "\nFTP>"; } else { terminalDisplayText.text += "\n" + cmdStr + " : Execution Failed, try help to see a list of commands\nFTP>"; };
                break;
                case "tac config.ini": // viewing file triggers channel 3 AI
                    baseCmd = "tac";
                    if (ftpSessionID == 2) { CHAT_App.TriggerAI_Three(); terminalDisplayText.text += cmdStr + "\n" + "sUBjEct 832\n\nrEMoveD fROm sCENe\n  D::02:24:21\n  T::12:41:16" + "\nFTP>"; } else { terminalDisplayText.text += "\n" + cmdStr + " : Execution Failed, try help to see a list of commands\nFTP>"; };
                break;
                case "tac rootReadOnly.txt":
                    baseCmd = "tac";
                    if (ftpSessionID == 2) { terminalDisplayText.text += cmdStr + "\n" + "Permission Denied!" + "\nFTP>"; } else { terminalDisplayText.text += "\n" + cmdStr + " : Execution Failed, try help to see a list of commands\nFTP>"; };
                break;
                case "exit":
                    baseCmd = "";
                    terminalDisplayText.text += cmdStr + "\nDisconnected From FTP Server\n\nuser@ilak:~$ Try help to see a list of commands!\n";
                    ftpStatus(false);
                break;
    
                default:
                    if (cmdStr[0] == 't' && cmdStr[1] == 'a' && cmdStr[2] == 'c')
                    {
                        baseCmd = "tac";
                    } else if (cmdStr[0] == 'm' && cmdStr[1] == 'g' && cmdStr[2] == 'e' && cmdStr[3] == 't')
                        {
                            baseCmd = "mget";
                        }

                    switch (baseCmd)
                    {
                        case "tac":
                            terminalDisplayText.text += "\n" + cmdStr + " : File Does Not Exist, Check for Miss-Spelling\nFTP>";
                            baseCmd = "";
                            break;
                        case "mget":
                            terminalDisplayText.text += "\n" + cmdStr + " : mget Not Allowed.\nFTP>";
                            baseCmd = "";
                            break;
    
                        default:
                            terminalDisplayText.text += "\n" + cmdStr + " : Invalid Command, try help to see a list of commands\nFTP>";
                            baseCmd = "";
                            break;
                    }
                break;
            }
        } else
            {
                terminalDisplayText.text += "\nFTP>";
                baseCmd = "";
            }

        terminalDisplayText.text += "\n\n";
        Invoke("snapScrollBar", 0.025f);
    }

    void SMB_Prompt(string cmdStr)
    {
        string defaultDirList = "\nEmployees  Backup  staffNotice.txt\n", backUpList = "\nReadThis.txt  recovery.zip\n", employeeList = "\nhash.txt  employeeList.txt\n";
        terminalDisplayText.text = terminalDisplayText.text.Remove(terminalDisplayText.text.Length - 2);
        
        if (!string.IsNullOrWhiteSpace(cmdStr))
        {
            switch (cmdStr)
            {
                case "help":
                    baseCmd = "";
                    terminalDisplayText.text += cmdStr + "\nls - show files in current directory\ntac [filename] - read file content\ncd [Dir Name] - move through SMB Directories\ncd ../ - mvoe back a directory\nexit - exit SMB session\nSMB>";
                break;
                case "ls":
                    baseCmd = "";
                    switch (smbDirIndex)
                    {
                        case 0:
                            terminalDisplayText.text += cmdStr + defaultDirList + "\nSMB>";
                        break;
                        case 1: // backupDirectory
                            terminalDisplayText.text += cmdStr + backUpList + "\nSMB /Backup/>";
                        break;
                        case 2: // employeeDirectory
                            terminalDisplayText.text += cmdStr + employeeList + "\nSMB /Employees/>";
                        break;
    
                        default:
                        break;
                    }
                break;
                case "cd":
                    baseCmd = "cd";
                    terminalDisplayText.text += cmdStr + "\nUsage: cd <dir name> OR cd ../ or cd .. to move back a dir" + "\nSMB>";
                break;
                case "cd ..":
                    baseCmd = "cd";
                    switch (smbDirIndex)
                    {
                        case 0:
                            terminalDisplayText.text += cmdStr + "\nSMB>";
                        break;
                        case 1: // backupDirectory
                            terminalDisplayText.text += cmdStr + "\nSMB>";
                            smbDirIndex = 0;
                        break;
                        case 2: // employeeDirectory
                            terminalDisplayText.text += cmdStr + "\nSMB>";
                            smbDirIndex = 0;
                        break;
    
                        default:
                        break;
                    }
                break;
                case "cd ../":
                    baseCmd = "cd";
                    switch (smbDirIndex)
                    {
                        case 0:
                            terminalDisplayText.text += cmdStr + "\nSMB>";
                        break;
                        case 1: // backupDirectory
                            terminalDisplayText.text += cmdStr + "\nSMB>";
                            smbDirIndex = 0;
                        break;
                        case 2: // employeeDirectory
                            terminalDisplayText.text += cmdStr + "\nSMB>";
                            smbDirIndex = 0;
                        break;
    
                        default:
                        break;
                    }
                break;
                case "cd Employees":
                    baseCmd = "cd";
                    switch (smbDirIndex)
                    {
                        case 0:
                            terminalDisplayText.text += cmdStr + "\nSMB /Employees/>";
                            smbDirIndex = 2;
                        break;
                        case 1: // backupDirectory
                            terminalDisplayText.text += cmdStr + " : This Directory Doesn't Exist!" + "\nSMB /Backup/>";
                            smbDirIndex = 0;
                        break;
                        case 2: // employeeDirectory
                            terminalDisplayText.text += cmdStr + " : This Directory Doesn't Exist!" + "\nSMB /Employees/>";
                            smbDirIndex = 0;
                        break;
    
                        default:
                        break;
                    }
                break;
                case "cd Backup":
                    baseCmd = "cd";
                    switch (smbDirIndex)
                    {
                        case 0:
                            terminalDisplayText.text += cmdStr + "\nSMB /Backup/>";
                            smbDirIndex = 1;
                        break;
                        case 1: // backupDirectory
                            terminalDisplayText.text += cmdStr + " : This Directory Doesn't Exist!" + "\nSMB /Backup/>";
                            smbDirIndex = 0;
                        break;
                        case 2: // employeeDirectory
                            terminalDisplayText.text += cmdStr + " : This Directory Doesn't Exist!" + "\nSMB /Employees/>";
                            smbDirIndex = 0;
                        break;
    
                        default:
                        break;
                    }
                break;
                case "tac hash.txt":
                    baseCmd = "tac";
                    if (smbDirIndex == 2) { terminalDisplayText.text += cmdStr + "\n" + "3-9uvQ" + "\nSMB /Employees/>"; hashFound[2] = true; } else { terminalDisplayText.text += "\n" + cmdStr + " : Execution Failed, try help to see a list of commands\nSMB /Employees/>"; };
                break;
                case "tac recovery.zip":
                    baseCmd = "tac";
                    if (smbDirIndex == 1) { terminalDisplayText.text += cmdStr + "\n" + "[cannot read content]" + "\nSMB /Employees/>"; } else { terminalDisplayText.text += "\n" + cmdStr + " : Execution Failed, try help to see a list of commands\nSMB /Employees/>"; };
                break;
                case "tac staffNotice.txt":
                    baseCmd = "tac";
                    if (smbDirIndex == 0) { terminalDisplayText.text += cmdStr + "\n" + "WW91J3ZlIGR1ZyB0aGlzIGZhci4uLiBXaHkgc3RvcCBub3c/" + "\nSMB>"; } else { terminalDisplayText.text += "\n" + cmdStr + " : Execution Failed, try help to see a list of commands\nSMB>"; };
                break;
                case "tac ReadThis.txt":
                    baseCmd = "tac";
                    if (smbDirIndex == 1) { terminalDisplayText.text += cmdStr + "\n" + "               __\n..=====.. |==|\n||     || |= |\n_  ||     || |^*| _\n|=| o=,===,=o |__||=|\n|_|  _______)~`)  |_|\n[=======]  ()       ldb" + "\nSMB /Backup/>"; } else { terminalDisplayText.text += "\n" + cmdStr + " : Execution Failed, try help to see a list of commands\nSMB /Backup/>"; };
                break;
                case "tac employeeList.txt":
                    baseCmd = "tac";
                    if (smbDirIndex == 2) { CHAT_App.TriggerAI_One(); terminalDisplayText.text += cmdStr + "\n" + "=====================\n|Employee|Department|\n=====================\n|Jonah|CyberDivision|\n|Sarah|  Marketing  |\n|David|  Forensics  |\n=====================\n\nThe effort you're putting in is going to get you killed..." + "\nSMB /Backup/>"; } else { terminalDisplayText.text += "\n" + cmdStr + " : Execution Failed, try help to see a list of commands\nSMB /Backup/>"; };
                break;
                case "exit":
                    baseCmd = "";
                    terminalDisplayText.text += cmdStr + "\nDisconnected From SMB Server\n\nuser@ilak:~$ Try help to see a list of commands!\n";
                    smbStatus(false);
                break;
    
                default:
                    switch (smbDirIndex) // for when a player makes an error in a command the error print matches the current directory IE it doesnt output the player is in the base directory when theyre in Backup or something else
                    {
                        case 0:
                            curSmbDir = "SMB>";
                        break;
                        case 1: // backupDirectory
                            curSmbDir = "SMB /Backup/>";
                        break;
                        case 2: // employeeDirectory
                            curSmbDir = "SMB /Employees/>";
                        break;
    
                        default:
                        break;
                    }

                    if (cmdStr[0] == 'c' && cmdStr[1] == 'd')
                    {
                        baseCmd = "cd";
                    } else if (cmdStr[0] == 't' && cmdStr[1] == 'a' && cmdStr[2] == 'c')
                        {
                            baseCmd = "tac";
                        }
    
                    switch (baseCmd)
                    {
                        case "tac":
                            terminalDisplayText.text += cmdStr + " : File Does Not Exist, Check for Miss-Spelling\n" + curSmbDir;
                            baseCmd = "";
                            break;
                        case "cd":
                            terminalDisplayText.text += cmdStr + " : Directory Does Not Exist, Check for Miss-Spelling\n" + curSmbDir;
                            baseCmd = "";
                            break;
    
                        default:
                            terminalDisplayText.text += cmdStr + " : Invalid Command, try help to see a list of commands\n" + curSmbDir;
                            baseCmd = "";
                            break;
                    }
                break;
            }
        } else
            {
                switch (smbDirIndex) // for when a player makes an error in a command the error print matches the current directory IE it doesnt output the player is in the base directory when theyre in Backup or something else
                {
                    case 0:
                        curSmbDir = "SMB>";
                        break;
                    case 1: // backupDirectory
                        curSmbDir = "SMB /Backup/>";
                        break;
                    case 2: // employeeDirectory
                        curSmbDir = "SMB /Employees/>";
                        break;

                    default:
                        break;
                }

                terminalDisplayText.text += "\n" + curSmbDir;
            }

        terminalDisplayText.text += "\n\n";
        Invoke("snapScrollBar", 0.025f);
    }

    void SSH_Prompt(string cmdStr)
    {
        terminalDisplayText.color = Color.white;
        string defaultSSHDirList = "\n  youreTrappedInThe.txt  Honey  Pot\n", HoneyDir = "\n  readme.txt\n", PotDir = "\n  readme.txt\n";
        terminalDisplayText.text = terminalDisplayText.text.Remove(terminalDisplayText.text.Length - 2);

        if (SSH_LoggedIn) // ssh D!sRupTSepi0l@73.251.35.145 : #N3IWdH8Sw3q@h05K!^VShy!6
        {
            if (!string.IsNullOrWhiteSpace(cmdStr))
            {
                switch (cmdStr)
                {
                    case "help":
                        baseCmd = "";
                        terminalDisplayText.text += cmdStr + "\nls - show files in current directory\ntac [filename] - read file content\ncd [Dir Name] - move through SSH Directories\ncd ../ - move back a directory\nwhoami - display current user\ncolor [green/white] - change text color\nclear - clears terminal\nexit - exit ssh session\nD!sRupTSepi0l@73.251.35.145:~$ \n";
                    break;
                    case "whoami":
                        baseCmd = "";
                        terminalDisplayText.text += "D!sRupTSepi0l@73.251.35.145:~$ whoami \n D!sRupTSepi0l\n";
                    break;
                    case "clear":
                        baseCmd = "";
                        switch (sshDirIndex)
                        {
                            case 0:
                                terminalDisplayText.text = "D!sRupTSepi0l@73.251.35.145:~$ ";
                            break;
                            case 1: // Honey
                                terminalDisplayText.text = "D!sRupTSepi0l@73.251.35.145/Honey/:~$ ";
                            break;
                            case 2: // Pot
                                terminalDisplayText.text = "D!sRupTSepi0l@73.251.35.145/Pot/:~$ ";
                            break;
    
                            default:
                            break;
                        }
                    break;
                    case "ls":
                        baseCmd = "";
                        switch (sshDirIndex)
                        {
                            case 0:
                                terminalDisplayText.text += cmdStr + defaultSSHDirList + "\nD!sRupTSepi0l@73.251.35.145:~$ ";
                            break;
                            case 1: // Honey
                                terminalDisplayText.text += cmdStr + HoneyDir + "\nD!sRupTSepi0l@73.251.35.145/Honey/:~$ ";
                            break;
                            case 2: // Pot
                                terminalDisplayText.text += cmdStr + PotDir + "\nD!sRupTSepi0l@73.251.35.145/Pot/:~$ ";
                            break;
    
                            default:
                            break;
                        }
                    break;
                    case "cd ..":
                        baseCmd = "cd";
                        switch (sshDirIndex)
                        {
                            case 0:
                                terminalDisplayText.text += cmdStr + "\nD!sRupTSepi0l@73.251.35.145:~$ ";
                            break;
                            case 1: // Honey
                                terminalDisplayText.text += cmdStr + "\nD!sRupTSepi0l@73.251.35.145:~$ ";
                                smbDirIndex = 0;
                            break;
                            case 2: // Pot
                                terminalDisplayText.text += cmdStr + "\nD!sRupTSepi0l@73.251.35.145:~$ ";
                                smbDirIndex = 0;
                            break;
    
                            default:
                            break;
                        }
                    break;
                    case "cd ../":
                        baseCmd = "cd";
                        switch (sshDirIndex)
                        {
                            case 0:
                                terminalDisplayText.text += cmdStr + "\nD!sRupTSepi0l@73.251.35.145:~$ ";
                            break;
                            case 1: // Honey
                                terminalDisplayText.text += cmdStr + "\nD!sRupTSepi0l@73.251.35.145:~$ ";
                                sshDirIndex = 0;
                            break;
                            case 2: // Pot
                                terminalDisplayText.text += cmdStr + "\nD!sRupTSepi0l@73.251.35.145:~$ ";
                                sshDirIndex = 0;
                            break;
    
                            default:
                            break;
                        }
                    break;
                    case "cd Honey":
                        baseCmd = "cd";
                        switch (sshDirIndex)
                        {
                            case 0:
                                terminalDisplayText.text += cmdStr + "\nD!sRupTSepi0l@73.251.35.145/Honey/:~$ ";
                                sshDirIndex = 1;
                            break;
                            case 1: // Honey
                                terminalDisplayText.text += cmdStr + " : This Directory Doesn't Exist!" + "\nD!sRupTSepi0l@73.251.35.145/Honey/:~$ ";
                                sshDirIndex = 0;
                            break;
                            case 2: // Pot
                                terminalDisplayText.text += cmdStr + " : This Directory Doesn't Exist!" + "\nD!sRupTSepi0l@73.251.35.145/Pot/:~$ ";
                                sshDirIndex = 0;
                            break;
    
                            default:
                            break;
                        }
                    break;
                    case "cd Pot":
                        baseCmd = "cd";
                        switch (sshDirIndex)
                        {
                            case 0:
                                terminalDisplayText.text += cmdStr + "\nD!sRupTSepi0l@73.251.35.145/Pot/:~$ ";
                                sshDirIndex = 2;
                            break;
                            case 1: // Honey
                                terminalDisplayText.text += cmdStr + " : This Directory Doesn't Exist!" + "\nD!sRupTSepi0l@73.251.35.145/Honey/:~$ ";
                                sshDirIndex = 0;
                            break;
                            case 2: // Pot
                                terminalDisplayText.text += cmdStr + " : This Directory Doesn't Exist!" + "\nD!sRupTSepi0l@73.251.35.145/Pot/:~$ ";
                                sshDirIndex = 0;
                            break;
    
                            default:
                            break;
                        }
                    break;
                    case "tac youreTrappedInThe.txt":
                        baseCmd = "tac";
                        if (sshDirIndex == 0) { terminalDisplayText.text += cmdStr + "\n" + "You Just Had To Run Down The Rabbit Hole Didn't You. . ." + "\nD!sRupTSepi0l@73.251.35.145:~$ >"; } else { terminalDisplayText.text += "\n" + cmdStr + " : Execution Failed, try help to see a list of commands\nSMB /Employees/>"; };
                    break;
                    case "tac readme.txt":
                        baseCmd = "tac";
                        if (sshDirIndex == 1 || sshDirIndex == 2) { terminalDisplayText.text += cmdStr + "\n" + "You Got Some Nerve To Still Be Digging. . ." + "\nD!sRupTSepi0l@73.251.35.145:~$ >"; } else { terminalDisplayText.text += "\n" + cmdStr + " : Execution Failed, try help to see a list of commands\nSMB /Employees/>"; };
                    break;
                    case "exit":
                        baseCmd = "";
                        terminalDisplayText.text += cmdStr + "\nDisconnected From Host 73.251.35.145\n\nuser@ilak:~$ Try help to see a list of commands!\n";
                        sshStatus(false);
                        sshDirIndex = 0;
                    break;
    
                    default:
                        switch (sshDirIndex) // for when a player makes an error in a command the error print matches the current directory IE it doesnt output the player is in the base directory when theyre in Backup or something else
                        {
                            case 0:
                                curSshDir = "D!sRupTSepi0l@73.251.35.145:~$ ";
                            break;
                            case 1:
                                curSshDir = "D!sRupTSepi0l@73.251.35.145/Honey/:~$  ";
                            break;
                            case 2:
                                curSshDir = "D!sRupTSepi0l@73.251.35.145/Pot/:~$  ";
                            break;
    
                            default:
                            break;
                        }

                        if (cmdStr[0] == 'c' && cmdStr[1] == 'd')
                        {
                            baseCmd = "cd";
                        } else if (cmdStr[0] == 't' && cmdStr[1] == 'a' && cmdStr[2] == 'c')
                            {
                                baseCmd = "tac";
                            }
    
                        switch (baseCmd)
                        {
                            case "tac":
                                terminalDisplayText.text += cmdStr + " : File Does Not Exist, Check for Miss-Spelling\n" + curSshDir;
                                baseCmd = "";
                                break;
                            case "cd":
                                terminalDisplayText.text += cmdStr + " : Directory Does Not Exist, Check for Miss-Spelling\n" + curSshDir;
                                baseCmd = "";
                                break;
    
                            default:
                                terminalDisplayText.text += cmdStr + " : Execution Failed, try help to see a list of commands\n" + curSshDir;
                                baseCmd = "";
                                break;
                        }
                    break;
                }
            } else
                {
                    switch (sshDirIndex) // for when a player makes an error in a command the error print matches the current directory IE it doesnt output the player is in the base directory when theyre in Backup or something else
                    {
                        case 0:
                            curSshDir = "D!sRupTSepi0l@73.251.35.145:~$ ";
                            break;
                        case 1:
                            curSshDir = "D!sRupTSepi0l@73.251.35.145/Honey/:~$  ";
                            break;
                        case 2:
                            curSshDir = "D!sRupTSepi0l@73.251.35.145/Pot/:~$  ";
                            break;

                        default:
                            break;
                    }

                    terminalDisplayText.text += "\n" + curSshDir;
                    baseCmd = "";
                }
        } else
            {
                switch (cmdStr)
                {
                    case "#N3IWdH8Sw3q@h05K!^VShy!6":
                        terminalDisplayText.text += "*************************\n  Daemon SkyBox 7.3.5-red3-rdm64 #1 PCM Solariod 6.2.1-2red4 (2021-12-15) x86_64\n\nD!sRupTSepi0l@73.251.35.145:~$ ";
                        SSH_LoggedIn = true;
                        Invoke("TrueEndingTrigger", 75);
                    break;
                    case "0": // ssh D!sRupTSepi0l@73.251.35.145
                        terminalDisplayText.text += "*************************\n  Daemon SkyBox 7.3.5-red3-rdm64 #1 PCM Solariod 6.2.1-2red4 (2021-12-15) x86_64\n\nD!sRupTSepi0l@73.251.35.145:~$ ";
                        SSH_LoggedIn = true;
                        Invoke("TrueEndingTrigger", 5);
                    break;
                    case "exit":
                        SSH_LoggedIn = false;
                        sshStatus(false);
                        sshDirIndex = 0;
                    break;

                    default:
                        terminalDisplayText.text += "  Permission Denied, please try again.\n";
                    break;
                }
            }

        terminalDisplayText.text += "\n\n";
        Invoke("snapScrollBar", 0.025f);
    }

    void TrueEndingTrigger()
    {
        TrueEndScript.StartCountDown();
    }

}//EndScript