using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.IO.Enumeration;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;
using System.Data.Common;
//using UnityEditor.Build.Content;

[CreateAssetMenu(
    fileName = "Player Progress",
    menuName = "Game Kuis/Player Progress"
)
]
public class PlayerProgress : ScriptableObject
{
    [System.Serializable]
    public struct MainData
    {
        public int koin;
        public Dictionary<string, int> progresLevel;
    }

    [SerializeField]
    private string _filename = "contoh.txt";

    [SerializeField]
    private string _startingLevelPackName = string.Empty;

    public MainData progresData = new MainData();

    public void SimpanProgres()
{
    // Initialize starting data if progresLevel is null
    if (progresData.progresLevel == null)
    {
        progresData.progresLevel = new();
        progresData.koin = 0;
        progresData.progresLevel.Add(_startingLevelPackName, 1);
    }

    // Determine the directory and file path
#if UNITY_EDITOR
    string directory = Application.dataPath + "/Temporary/";
#elif (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
    string directory = Application.persistentDataPath + "/ProgresLokal/";
#endif
    var path = Path.Combine(directory, _filename);

    // Create directory if it doesn't exist
    if (!Directory.Exists(directory))
    {
        Directory.CreateDirectory(directory);
        Debug.Log("Directory has been created: " + directory);
    }

    try
    {
        // Create or overwrite the file
        using (var fileStream = File.Open(path, FileMode.Create))
        using (var writer = new BinaryWriter(fileStream))
        {
            writer.Write(progresData.koin); // Save koin first
            writer.Write(progresData.progresLevel.Count); // Save the number of elements in the dictionary
            foreach (var entry in progresData.progresLevel)
            {
                writer.Write(entry.Key);
                writer.Write(entry.Value);
            }

            Debug.Log("File has been saved to: " + path);
        }
    }
    catch (System.Exception e)
    {
        Debug.LogError("Error while saving file: " + e.Message);
    }

    Debug.Log($"{_filename} successfully saved");
}
    public bool MuatProgres()
{
    // Determine the directory and file path
#if UNITY_EDITOR
    string directory = Application.dataPath + "/Temporary/";
#elif (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
    string directory = Application.persistentDataPath + "/ProgresLokal/";
#endif
    var path = Path.Combine(directory, _filename);

    if (!File.Exists(path))
    {
        Debug.LogError($"File not found: {path}");
        return false;
    }

    try
    {
        using (var fileStream = File.Open(path, FileMode.Open))
        using (var reader = new BinaryReader(fileStream))
        {
            progresData.koin = reader.ReadInt32(); // Load koin first
            int count = reader.ReadInt32(); // Read the number of elements in the dictionary
            progresData.progresLevel = new Dictionary<string, int>(count);

            for (int i = 0; i < count; i++)
            {
                var namaLevelPack = reader.ReadString();
                var levelKe = reader.ReadInt32();
                progresData.progresLevel[namaLevelPack] = levelKe;
                Debug.Log($"{namaLevelPack}:{levelKe}");
            }

            Debug.Log($"{progresData.koin}; {progresData.progresLevel.Count}");
        }

        return true;
    }
    catch (System.Exception e)
    {
        Debug.LogError($"ERROR: Terjadi kesalahan saat memuat progress binari\n {e.Message}");
        return false;
    }
}
}