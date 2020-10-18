using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using TMPro;

[System.Serializable]
public class ExportToCSV : MonoBehaviour
{
    private List<string[]> rowData = new List<string[]>();
    [SerializeField]
    public ScoutingDataList scoutingDataList = new ScoutingDataList();
    public NotificationSystem notificationSystem;

    private string importDataPath;
    private TextAsset jsonFile;

    string[] rowDataTemp = new string[12];

    void Start()
    {
        CheckFolderExistence("/ImportData");
        CheckFolderExistence("/Spreadsheets");

        importDataPath = Application.persistentDataPath + "/ImportData/";
        // Debug.Log("Data Path: " + Application.dataPath);
        // Debug.Log("Persistent Data Path: " + Application.persistentDataPath);
    }

    public void Save()
    {
        CheckFolderExistence("/Spreadsheets"); //Check if necessary folder exists

        if (!File.Exists(Application.persistentDataPath + "/Spreadsheets/" + "Saved_data.csv")) //Include titles if Saved_data.csv does not exist
        {
            AddTitles();
        }
        else
        {
            // Debug.Log("File exists.. not adding titles");
        }

        foreach (string filePath in Directory.GetFiles(importDataPath))
        {
            jsonFile = new TextAsset(File.ReadAllText(filePath));

            if (jsonFile != null)
            {
                scoutingDataList = JsonUtility.FromJson<ScoutingDataList>(jsonFile.text);

                int x = 0;

                foreach (ScoutingData scoutingData in scoutingDataList.ScoutingData)
                {
                    rowDataTemp = new string[12];
                    rowDataTemp[0] = scoutingDataList.ScoutingData[x].name.ToString();
                    rowDataTemp[1] = scoutingData.matchNumber;
                    rowDataTemp[2] = scoutingData.teamName;
                    rowDataTemp[3] = scoutingData.autonomousInnerCount;
                    rowDataTemp[4] = scoutingData.autonomousInnerInnerCount;
                    rowDataTemp[5] = scoutingData.autonomousOuterCount;
                    rowDataTemp[6] = scoutingData.teleOpInnerCount;
                    rowDataTemp[7] = scoutingData.teleOpInnerInnerCount;
                    rowDataTemp[8] = scoutingData.teleOpOuterCount;
                    rowDataTemp[9] = scoutingData.drivingEffectiveness;
                    rowDataTemp[10] = scoutingData.defenseEffectiveness;
                    rowDataTemp[11] = scoutingData.additionalNotes;
                    rowData.Add(rowDataTemp);
                    // Debug.Log("Test: " + scoutingDataList.ScoutingData[x].name.ToString());
                    x++;
                }
            }
            else
            {
                notificationSystem.ErrorNullAsset();
            }
        }

        StreamWriter outStream = System.IO.File.CreateText(Application.persistentDataPath + "/Spreadsheets/" + "Scouting_Data.csv");

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        // Debug.Log("Exporting to CSV!");

        outStream.WriteLine(sb);
        outStream.Close();

        notificationSystem.FinishedExportingData();

        OpenInFiles("/Spreadsheets/");
    }

    private void CheckFolderExistence(string folderLocation)
    {
        if (!Directory.Exists(Application.persistentDataPath + folderLocation))
        {
            Directory.CreateDirectory(Application.persistentDataPath + folderLocation);
        }
    }


    private void AddTitles() // Add titles to the CSV file
    {
        rowDataTemp = new string[12];
        rowDataTemp[0] = "Name";
        rowDataTemp[1] = "Match Number";
        rowDataTemp[2] = "Team Number";
        rowDataTemp[3] = "Autonomous Inner Count";
        rowDataTemp[4] = "Autonomous Inner Inner Count";
        rowDataTemp[5] = "Autonomous Outer Count";
        rowDataTemp[6] = "TeleOp Inner Count";
        rowDataTemp[7] = "TeleOp Inner Inner Count";
        rowDataTemp[8] = "TeleOp Outer Count";
        rowDataTemp[9] = "Driving Effectiveness";
        rowDataTemp[10] = "Defense Effectiveness";
        rowDataTemp[11] = "Additional Notes";
        rowData.Add(rowDataTemp);
    }

    public void OpenInFiles(string folderPathToOpen)
    {
        string folderPath = Application.persistentDataPath + folderPathToOpen;
        // System.Diagnostics.Process.Start("explorer.exe", "/select," + folderPathToOpen);
        // System.Diagnostics.Process.Start("explorer.exe", "/select," + Application.persistentDataPath);

        Application.OpenURL("file://" + folderPath);
    }
}