﻿using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace WPF;

public static class DataTransaction
{
    public static async Task<Node[,]?> LoadAsync()
    {
        OpenFileDialog openFileDialog = new()
        {
            InitialDirectory = System.Windows.Forms.Application.StartupPath,
            Title = "Load Json Files",
            CheckFileExists = true,
            CheckPathExists = true,
            DefaultExt = "Json",
            Filter = "Json files (*.Json)|*.json|All files (*.*)|*.*",
            FilterIndex = 1,
            RestoreDirectory = true
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            return await Task.Run(() =>
            {
                using StreamReader r = new(openFileDialog.FileName);
                string json = r.ReadToEnd();
                Node[,] items = JsonConvert.DeserializeObject<Node[,]>(json);
                return items;
            });
        }
        return default;
    }

    public static async Task SaveAsync(Node[,] NodeMap)
    {
        SaveFileDialog saveFileDialog = new()
        {
            InitialDirectory = System.Windows.Forms.Application.StartupPath,
            Title = "Save Json Files",
            CheckPathExists = true,
            DefaultExt = "Json",
            Filter = "Json files (*.Json)|*.json|All files (*.*)|*.*",
            FilterIndex = 1,
            RestoreDirectory = true
        };

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            await Task.Run(() =>
            {
                var json = JsonConvert.SerializeObject(NodeMap);
                File.WriteAllText(saveFileDialog.FileName, json);
            });
        }
    }
}