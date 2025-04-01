using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;

public class CsvToJsonEditor : EditorWindow
{
    // 添加菜单项
    [MenuItem("Tools/Convert CSV to JSON")]
    public static void ShowWindow()
    {
        // 打开窗口
        EditorWindow.GetWindow<CsvToJsonEditor>("CSV to JSON Converter");
    }

    // 按钮点击时执行转换
    private void OnGUI()
    {
        if (GUILayout.Button("Convert All CSVs to JSON"))
        {
            ConvertAllCsvToJson();
        }
    }

    // 转换文件夹中的所有CSV文件
    private void ConvertAllCsvToJson()
    {
        string excelDirectory = "Assets/Data/Excel"; // Excel文件夹路径
        string jsonDirectory = "Assets/Data/Json";   // 输出JSON文件夹路径

        // 如果输出路径不存在，创建文件夹
        if (!Directory.Exists(jsonDirectory))
        {
            Directory.CreateDirectory(jsonDirectory);
        }

        // 获取所有CSV文件
        string[] csvFiles = Directory.GetFiles(excelDirectory, "*.csv");

        foreach (string csvFilePath in csvFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(csvFilePath);
            string jsonFilePath = Path.Combine(jsonDirectory, fileName + ".json");

            // 转换CSV到JSON
            ConvertCsvToJson(csvFilePath, jsonFilePath);

            // 在控制台输出转换完成信息
            Debug.Log($"Converted {csvFilePath} to {jsonFilePath}");
        }

        // 刷新Unity编辑器资源
        AssetDatabase.Refresh();
    }

    // 转换单个CSV文件到JSON
    private void ConvertCsvToJson(string csvFilePath, string jsonFilePath)
    {
        var lines = File.ReadAllLines(csvFilePath);
        var headers = lines[0].Split(',');
        var data = new List<Dictionary<string, string>>();

        for (int i = 1; i < lines.Length; i++)
        {
            var values = lines[i].Split(',');
            var row = new Dictionary<string, string>();
            for (int j = 0; j < headers.Length; j++)
            {
                row[headers[j]] = values[j];
            }
            data.Add(row);
        }

        // 将数据转换为JSON格式并写入文件
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(jsonFilePath, json);
    }
}
