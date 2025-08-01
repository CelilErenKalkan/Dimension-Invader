using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class FileHandler
{
    /// <summary>
    /// Saves the list as a JSON file.
    /// </summary>
    public static void SaveListToJson<T>(List<T> toSave, string fileName)
    {
        try
        {
            string content = JsonHelper.ToJson(toSave.ToArray());
            WriteFile(GetPath(fileName), content);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error saving list to JSON: {ex.Message}");
        }
    }

    /// <summary>
    /// Saves the variable with the given type as a JSON file.
    /// </summary>
    public static void SaveToJson<T>(T toSave, string fileName)
    {
        try
        {
            string content = JsonUtility.ToJson(toSave);
            WriteFile(GetPath(fileName), content);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error saving data to JSON: {ex.Message}");
        }
    }

    /// <summary>
    /// Returns a list with the given file name.
    /// </summary>
    public static List<T> ReadListFromJson<T>(string fileName)
    {
        try
        {
            string content = ReadFile(GetPath(fileName));

            if (string.IsNullOrEmpty(content) || content == "{}")
                return new List<T>();

            return JsonHelper.FromJson<T>(content).ToList();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error reading list from JSON: {ex.Message}");
            return new List<T>();
        }
    }

    /// <summary>
    /// Returns a variable with the desired type.
    /// </summary>
    public static T ReadFromJson<T>(string fileName)
    {
        try
        {
            string content = ReadFile(GetPath(fileName));

            if (string.IsNullOrEmpty(content) || content == "{}")
                return default;

            return JsonUtility.FromJson<T>(content);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error reading data from JSON: {ex.Message}");
            return default;
        }
    }

    /// <summary>
    /// Returns the save path as a string.
    /// </summary>
    private static string GetPath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }

    /// <summary>
    /// Saves the file to the given path.
    /// </summary>
    private static void WriteFile(string path, string content)
    {
        try
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(content);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error writing file: {ex.Message}");
        }
    }

    /// <summary>
    /// Returns the file as a string from the given path.
    /// </summary>
    private static string ReadFile(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    return reader.ReadToEnd();
                }
            }

            return "";
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error reading file: {ex.Message}");
            return "";
        }
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}