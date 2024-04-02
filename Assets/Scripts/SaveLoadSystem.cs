using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

public class SaveLoadSystem
{
    public static string ScriptableObjectToJSON(ScriptableObject obj)
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(obj, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
    }

    public static void Save<T>(T obj, string path)
    {

        // Write json text
        File.WriteAllText(path, Newtonsoft.Json.JsonConvert.SerializeObject(obj));
    }

    public static T Load<T>(string path)
    {
        string json = File.ReadAllText(path);

        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
    }

}