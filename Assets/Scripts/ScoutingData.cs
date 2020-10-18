using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoutingData
{
    public string name;
    public string matchNumber;
    public string teamName;
    public string autonomousInnerCount;
    public string autonomousInnerInnerCount;
    public string autonomousOuterCount;
    public string teleOpInnerCount;
    public string teleOpInnerInnerCount;
    public string teleOpOuterCount;
    public string drivingEffectiveness;
    public string defenseEffectiveness;
    public string additionalNotes;
}

[System.Serializable]
public class ScoutingDataList
{
    public List<ScoutingData> ScoutingData = new List<ScoutingData>();

    // public Container(List<ScoutingData> _dataList)
    // {
    //     scoutingData = _dataList;
    // }
}