using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
[System.Serializable]
public class ImportData : MonoBehaviour
{
    private string path;
    private TextAsset jsonFile;
    public TMP_InputField fileLocationInputField;
    [SerializeField]
    public ScoutingDataList scoutingDataList = new ScoutingDataList();

    void Start()
    {
        // fileLocationInputField.text = Application.persistentDataPath;

        // path = Application.persistentDataPath + "/ImportData/data.json";
        path = Application.persistentDataPath + "/ImportData/";
    }

    public void ImportJsonFile()
    {
        // jsonFile = Resources.Load(path) as TextAsset;
        jsonFile = new TextAsset(File.ReadAllText(path));

        if (jsonFile != null)
        {
            scoutingDataList = JsonUtility.FromJson<ScoutingDataList>(jsonFile.text);
            foreach (ScoutingData importedScoutingData in scoutingDataList.ScoutingData)
            {
                // Debug.Log(importedScoutingData.name);
                // ScoutingData scoutingData = new ScoutingData();
                // scoutingData.name = importedScoutingData.name;
                // scoutingData.teamName = importedScoutingData.teamName;
                // scoutingData.matchNumber = importedScoutingData.matchNumber;

                // scoutingData.autonomousInnerCount = importedScoutingData.autonomousInnerCount;
                // scoutingData.autonomousInnerInnerCount = importedScoutingData.autonomousInnerInnerCount;
                // scoutingData.autonomousOuterCount = importedScoutingData.autonomousOuterCount;

                // scoutingData.teleOpInnerCount = importedScoutingData.teleOpInnerCount;
                // scoutingData.teleOpInnerInnerCount = importedScoutingData.teleOpInnerInnerCount;
                // scoutingData.teleOpOuterCount = importedScoutingData.teleOpOuterCount;

                // scoutingData.drivingEffectiveness = importedScoutingData.drivingEffectiveness;
                // scoutingData.defenseEffectiveness = importedScoutingData.defenseEffectiveness;
                // scoutingData.additionalNotes = importedScoutingData.additionalNotes;
                // scoutingDataList.ScoutingData.Add(scoutingData);

            }
        }
        else
        {
            Debug.Log("Asset is null");
        }


        // Data data = JsonUtility.FromJson<Data>(jsonFile);
        // Debug.Log(data.name);
        // Debug.Log(data.teamName);

        // scoutingDataContainer.Add(JsonUtility.FromJson<List<ScoutingData>>(jsonFile));

        // Debug.Log(scoutingData.name);
    }


    public void GetAllFiles()
    {
        foreach (string filePath in Directory.GetFiles(path))
        {
            jsonFile = new TextAsset(File.ReadAllText(filePath));

            Debug.Log(jsonFile);
        }
    }
}
