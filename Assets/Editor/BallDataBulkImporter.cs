using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class BallDataBulkImporter : EditorWindow
{
    private BallDataBase database;
    private string folderPath = "Assets/Resources/Balls"; // ←画像があるフォルダ

    [MenuItem("Tools/Ball Data Bulk Importer")]
    public static void ShowWindow()
    {
        GetWindow<BallDataBulkImporter>("Ball Data Bulk Importer");
    }

    void OnGUI()
    {
        GUILayout.Label("Ball Data Bulk Generator", EditorStyles.boldLabel);

        database = (BallDataBase)EditorGUILayout.ObjectField("Ball Data Base", database, typeof(BallDataBase), false);
        folderPath = EditorGUILayout.TextField("Sprite Folder Path", folderPath);

        if (GUILayout.Button("Generate BallData from Folder"))
        {
            ImportFromFolder();
        }
    }

    void ImportFromFolder()
    {
        if (database == null)
        {
            Debug.LogError("BallDataBase を指定してください。");
            return;
        }

        string[] guids = AssetDatabase.FindAssets("t:Sprite", new[] { folderPath });
        List<BallData> newDataList = new List<BallData>();

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
            if (sprite == null) continue;

            string filename = Path.GetFileNameWithoutExtension(assetPath);

            BallData ballData = ScriptableObject.CreateInstance<BallData>();
            ballData.ballID = database.ballDataList.Count + newDataList.Count;
            ballData.ballImage = sprite;
            ballData.isGet = false;
            ballData.description = filename;

            string savePath = $"Assets/BallDataAssets/{filename}.asset";
            AssetDatabase.CreateAsset(ballData, savePath);

            newDataList.Add(ballData);
        }

        database.ballDataList.AddRange(newDataList);
        EditorUtility.SetDirty(database);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"生成完了：{newDataList.Count}個の BallData を追加しました。");
    }
}
