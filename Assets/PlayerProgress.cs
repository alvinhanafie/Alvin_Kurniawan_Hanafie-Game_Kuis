using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.IO.Enumeration;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;

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


    public MainData progresData = new MainData();

    public void SimpanProgres()
    {
        // Sample Data
        progresData.koin = 200;
        if (progresData.progresLevel == null)
            progresData.progresLevel = new();
        progresData.progresLevel.Add("Level Pack 1", 3);
        progresData.progresLevel.Add("Level Pack 3", 5);

        // data save information
        var directory = Application.dataPath + "/Temporary";
        var path = directory + "/" + _filename;

        // creating directory temporary
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Debug.Log("Directory has been created: " + directory);
        }
        try
        {
            // Write to file
            System.IO.File.WriteAllText(path, "Sample data to write");
            Debug.Log("File has been saved to: " + path);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error while saving file: " + e.Message);
        }
    // creating new file
    if (File.Exists(path))
    {
        File.Create(path).Dispose();
        Debug.Log("File created: " + path);
    }

    //var konten = $"{progresData.koin}\n";
    var fileStream = File.Open(path, FileMode.Open);
    //var formatter = new BinaryFormatter();

    fileStream.Flush();
    //formatter.Serialize(fileStream, progresData);
    
    // Save using binary writer
    var writer = new BinaryWriter(fileStream);

    writer.Write(progresData.koin);
    foreach (var i in progresData.progresLevel)
    {
        writer.Write(i.Key);
        writer.Write(i.Value);
    }

    // cut memory with file
    writer.Dispose();
    fileStream.Dispose();

    //foreach (var i in progresData.progresLevel)
    //{
    //    konten += $"{i.Key} {i.Value}\n";
    //}

    //File.WriteAllText(path, konten);

    Debug.Log($"{_filename} Berhasil Disimpan");

    }

    public bool MuatProgres()
    {
        // data save information
        string directory = Application.dataPath + "/Temporary/";
        string path = directory + _filename;

        var fileStream = File.Open(path, FileMode.OpenOrCreate);

    try
    {
        var reader = new BinaryReader(fileStream);

        try
        {
            progresData.koin = reader.ReadInt32();
            if (progresData.progresLevel == null)
                progresData.progresLevel = new();
            while (reader.PeekChar() != -1)
            {
                var namaLevelPack = reader.ReadString();
                var levelKe = reader.ReadInt32();
                progresData.progresLevel.Add(namaLevelPack, levelKe);
                Debug.Log($"{namaLevelPack}:{levelKe}");
            }

            reader.Dispose();

        }
        catch (System.Exception e)
        {
            Debug.LogError($"ERROR: Terjadi kesalahan saat memuat progress binari\n {e.Message}");
            
            reader.Dispose();
            fileStream.Dispose();

            return false;
        }
        //var formatter = new BinaryFormatter();

        //progresData = (MainData)formatter.Deserialize(fileStream);

        fileStream.Dispose();

        Debug.Log($"{progresData.koin}; {progresData.progresLevel.Count}");

        return true;
    }
    
    catch (System.Exception e)
    {
        fileStream.Dispose();

        Debug.LogError($"ERROR: Terjadi kesalahan saat memuat progress\n {e.Message}");

        return false;
    }

    }
}
