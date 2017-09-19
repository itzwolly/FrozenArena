using System;
using System.IO;
using UnityEngine;

public static class FileHandler {
    public static readonly string TEMP_PATH = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FrozenArena\\Temp";
    
    public static void CreateTempFolder() {
        if (!Directory.Exists(TEMP_PATH)) {
            Directory.CreateDirectory(TEMP_PATH);
        }
    }

    public static void DeleteTempFolder() {
        if (File.Exists(TEMP_PATH)) {
            Directory.Delete(TEMP_PATH, true);
        }
    }

    public static void CreateTempPlayerFile(int pPlayerAmount) {
        if (!File.Exists(TEMP_PATH)) {
            string pathString = Path.Combine(TEMP_PATH, "player_" + pPlayerAmount);
            FileStream fs = File.Create(pathString + ".txt");
            fs.Close();
        }
    }

    public static void WriteToFile(int pPlayerNr, string pMessage) {
        StreamWriter file = new StreamWriter(TEMP_PATH + "\\player_" + pPlayerNr + ".txt");
        file.WriteLine(pMessage + "\n");
        file.Close();
    }
}
