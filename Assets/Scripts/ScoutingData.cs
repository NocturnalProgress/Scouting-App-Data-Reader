using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoutingData
{
    public string name;
    public string matchNumber;
    public string teamName;
    public string initiationLine;
    public string autonomousUpperCount;
    public string autonomousInnerCount;
    public string autonomousLowerCount;
    public string teleOpUpperCount;
    public string teleOpInnerCount;
    public string teleOpLowerCount;
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