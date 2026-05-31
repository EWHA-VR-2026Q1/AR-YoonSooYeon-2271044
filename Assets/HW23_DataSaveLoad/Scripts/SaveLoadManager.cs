using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SaveLoadManager : MonoBehaviour
{
    public List<Transform> targets;

    string SavePath =>
        Path.Combine(
            Application.persistentDataPath,
            "save.json");

    public void Save()
    {
        WorldData worldData = new WorldData();

        foreach (Transform t in targets)
        {
            TransformData data =
                new TransformData();

            data.objectName = t.name;

            data.posX = t.position.x;
            data.posY = t.position.y;
            data.posZ = t.position.z;

            data.rotX = t.rotation.x;
            data.rotY = t.rotation.y;
            data.rotZ = t.rotation.z;
            data.rotW = t.rotation.w;

            worldData.transforms.Add(data);
        }

        string json =
            JsonUtility.ToJson(worldData, true);

        File.WriteAllText(SavePath, json);

        Debug.Log("저장 완료");
    }

    public void Load()
    {
        if (!File.Exists(SavePath))
            return;

        string json =
            File.ReadAllText(SavePath);

        WorldData worldData =
            JsonUtility.FromJson<WorldData>(json);

        foreach (TransformData data in worldData.transforms)
        {
            Transform target =
                targets.Find(t =>
                    t.name == data.objectName);

            if (target == null)
                continue;

            target.position = new Vector3(
                data.posX,
                data.posY,
                data.posZ);

            target.rotation = new Quaternion(
                data.rotX,
                data.rotY,
                data.rotZ,
                data.rotW);
        }

        Debug.Log("불러오기 완료");
    }

    private void Start()
    {
        Load();
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}