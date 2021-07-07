// Code by Paul Woerner - 2021
// info@paulwoerner.com

#region Info
// Shows a UI console for debugging on Android
#endregion

#region Libraries
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System;
#endregion

#region Classes
[System.Serializable]
public class LogStyle
{
    public string tag;
    public Color txtColor;
}
#endregion

public class UIConsole : MonoBehaviour
{
    #region Variables
    [SerializeField] LogStyle[] logStyles;

    [SerializeField] int maxCharacters = 4098;

    string clipboard = "";
    static string myLog = "";
    string myType;

    [SerializeField] Transform content;
    [SerializeField] GameObject logPrefab;
    [SerializeField] List<GameObject> logObjects;
    [SerializeField] ScrollRect scrollRect;
    #endregion

    #region Update Log
    void UpdateLog(string logTxt, string stackTrace, LogType logType)
    {
        string log = logTxt;

        myLog = logTxt + "\n" + myLog;

        #region Set TimeStamp
        DateTime dateTime = DateTime.Now;
        string timeStamp = "[" + dateTime.Hour.ToString() + ":" + dateTime.Minute.ToString() + ":" + dateTime.Second.ToString() + "] ";
        #endregion

        GameObject txtObj = Instantiate(logPrefab, content);
        TextMeshProUGUI tm = txtObj.GetComponent<TextMeshProUGUI>();
        tm.color = logStyles[(int)logType].txtColor;

        myType = timeStamp + logStyles[(int)logType].tag;
        tm.SetText(myType + ": " + log);

        logObjects.Add(txtObj);

        clipboard += myType + log + "\n";

        ScrollToBottom();
    }
    #endregion

    #region Clipboard
    public void GetClipBoard()
    {
        string copyString = "Console Clipboard: \n";

        string[] subLines = clipboard.Split('\n');

        for (int i = subLines.Length - 1; i > 0; i--)
        {
            copyString += subLines[i];
            copyString += "\n";
            if (copyString.Length >= maxCharacters)
            {
                break;
            }
        }

        GUIUtility.systemCopyBuffer = copyString;
    }
    #endregion

    #region Scroll
    public void ScrollToBottom()
    {
        scrollRect.normalizedPosition = new Vector2(0, 0);
    }
    #endregion

    #region Setup
    void OnEnable()
    {
        Application.logMessageReceived += UpdateLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= UpdateLog;
    }
    #endregion
}
