using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class CharacterDataBulkImporter : EditorWindow
{
    private CharacterDataBase database;
    private string folderPath = "Assets/Resources/characters"; // ←画像があるフォルダ

    [MenuItem("Tools/Character Data Bulk Importer")]
    public static void ShowWindow()
    {
        GetWindow<CharacterDataBulkImporter>("Character Data Bulk Importer");
    }

    void OnGUI()
    {
        GUILayout.Label("Character Data Bulk Generator", EditorStyles.boldLabel);

        database = (CharacterDataBase)EditorGUILayout.ObjectField("Character Data Base", database, typeof(CharacterDataBase), false);
        folderPath = EditorGUILayout.TextField("Sprite Folder Path", folderPath);

        if (GUILayout.Button("Generate CharacterData from Folder"))
        {
            ImportFromFolder();
        }
    }

    void ImportFromFolder()
    {
        if (database == null)
        {
            Debug.LogError("CharacterDataBase を指定してください。");
            return;
        }

        string[] guids = AssetDatabase.FindAssets("t:Sprite", new[] { folderPath });
        List<CharacterData> newDataList = new List<CharacterData>();

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
            if (sprite == null) continue;

            string filename = Path.GetFileNameWithoutExtension(assetPath);

            CharacterData charData = ScriptableObject.CreateInstance<CharacterData>();
            charData.characterImage = sprite;
            charData.hiragana = filename;
            charData.romaji = filename; // 必要に応じて変換してもよい
            charData.isGet = false;
            charData.description = "";

            string savePath = $"Assets/CharacterDataAssets/{filename}.asset";
            AssetDatabase.CreateAsset(charData, savePath);

            newDataList.Add(charData);
        }

        database.characterDataList.AddRange(newDataList);
        EditorUtility.SetDirty(database);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"生成完了：{newDataList.Count}個の CharacterData を追加しました。");
    }
}
