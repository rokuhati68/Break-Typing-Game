using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BallDataBase", menuName = "Scriptable Objects/BallDataBase")]
public class BallDataBase : ScriptableObject
{
    public List<BallData> ballDataList;
    public BallData GetBall(int id)
    {
        if (id >= 0 && id < ballDataList.Count)
        {
            Debug.Log($"Getting BallData with ID: {id}");
            return ballDataList[id];
        }
        Debug.LogWarning($"BallData with ID: {id} not found");
        return null;
    }
}
