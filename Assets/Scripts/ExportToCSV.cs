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

    private string[] rowDataTemp = new string[12];

    private void Start()
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
            // AddTitles();
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
                    rowDataTemp = new string[13];
                    rowDataTemp[0] = scoutingDataList.ScoutingData[x].name.ToString();
                    rowDataTemp[1] = scoutingData.matchNumber;
                    rowDataTemp[2] = scoutingData.teamName;
                    rowDataTemp[3] = scoutingData.initiationLine;
                    rowDataTemp[4] = scoutingData.autonomousUpperCount;
                    rowDataTemp[5] = scoutingData.autonomousInnerCount;
                    rowDataTemp[6] = scoutingData.autonomousLowerCount;
                    rowDataTemp[7] = scoutingData.teleOpUpperCount;
                    rowDataTemp[8] = scoutingData.teleOpInnerCount;
                    rowDataTemp[9] = scoutingData.teleOpLowerCount;
                    rowDataTemp[10] = scoutingData.drivingEffectiveness;
                    rowDataTemp[11] = scoutingData.defenseEffectiveness;
                    rowDataTemp[12] = scoutingData.additionalNotes;
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
        rowDataTemp = new string[13];
        rowDataTemp[0] = "Name";
        rowDataTemp[1] = "Match Number";
        rowDataTemp[2] = "Team Number";
        rowDataTemp[3] = "Initiation Line";
        rowDataTemp[4] = "Autonomous Upper Count";
        rowDataTemp[5] = "Autonomous Inner Inner Count";
        rowDataTemp[6] = "Autonomous Lower Count";
        rowDataTemp[7] = "TeleOp Upper Count";
        rowDataTemp[8] = "TeleOp Inner Inner Count";
        rowDataTemp[9] = "TeleOp Lower Count";
        rowDataTemp[10] = "Driving Effectiveness";
        rowDataTemp[11] = "Defense Effectiveness";
        rowDataTemp[12] = "Additional Notes";
        rowData.Add(rowDataTemp);
    }

    public void OpenInFiles(string folderPathToOpen)
    {
        Debug.Log("Opening in Finder");
        string folderPath = Application.persistentDataPath + folderPathToOpen;
        // System.Diagnostics.Process.Start("explorer.exe", "/select," + folderPathToOpen);
        // System.Diagnostics.Process.Start("explorer.exe", "/select," + Application.persistentDataPath);

        Application.OpenURL("file:\\" + folderPath);
    }
}