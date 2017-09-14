using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DLLLibrary
{
    public class Utility
    {
        public static GameObject RandomSelectFromList(List<GameObject> list)
        {
            System.Random rnd = new System.Random();
            return list[rnd.Next(0, list.Count)];
        }

        public static void RestartLevel()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        public static void WriteToFile(string path, string words)
        {
            //Debug.Log("Written to file");
            StreamWriter write = new StreamWriter(path, true);
            write.WriteLine(words);
            write.Close();
        }
        public static string VectorToString(Vector3 vec)
        {
            string words = vec.x + "," + vec.y + "," + vec.z;

            return words;
        }
        public static Vector3 StringToVector(string str)
        {
            Vector3 vec = new Vector3();
            string[] split = str.Split('(', ',');
            vec.x = Convert.ToSingle(split[0]);
            vec.y = Convert.ToSingle(split[1]);
            vec.z = Convert.ToSingle(split[2]);
            return vec;
        }

        public static void ReplaceLineFromFile(string path, string words, string stringToReplace)
        {

            StreamReader reader = new StreamReader(path);

            //int lineNumber=0;
            string[] file = File.ReadAllText(path).Split('\n', '\r');
            string[] toWrite = new string[file.Length];
            int lenght = 0;
            /**/
            foreach (string s in file)
            {
                if (s != "")
                {
                    toWrite[lenght] = s;
                    lenght++;
                }
            }
            /**/
            reader.Close();


            for (int i = 0; i < lenght; i++)
            {
                //Debug.Log(toWrite[i] + "|" + stringToReplace+"|");
                if (String.Compare(toWrite[i], stringToReplace) == 0)
                {
                    //Debug.Log("replacing");
                    toWrite[i] = words;
                }
            }
            File.WriteAllText(path, String.Empty);


            StreamWriter writer = new StreamWriter(path);
            for (int i = 0; i < lenght; i++)
            {
                writer.WriteLine(toWrite[i]);
            }
            writer.Close();
        }

        public static int GetLastNumberFromFile(string path)
        {
            string text = ReadFromFile(path);

            StreamReader reader = new StreamReader(path);
            string[] file = reader.ReadToEnd().Split('\n', '\r', ' ');
            int latestNumber = 0;
            int tempNr;
            for (int i = 0; i < file.Length; i++)
            {
                //Debug.Log(file[i]);
                if (IsNumeric(file[i]))
                {
                    latestNumber = Convert.ToInt32(file[i]);
                }
            }
            reader.Close();

            return latestNumber;
        }

        public static bool IsNumeric(string text)
        {
            float value;
            return float.TryParse(text, out value);
        }

        public static string ReadFromFile(string path)
        {
            StreamReader read = new StreamReader(path);
            string file = read.ReadToEnd();
            read.Close();
            return file;
        }


        public static void SaveStats(string path, string info)
        {
            if (ReadFromFile(path).Length > 1)
            {
                File.WriteAllText(path, String.Empty);
            }

            WriteToFile(path, info);
        }

        public static int GetValueAfterString(string path, string pString)
        {
            string text = ReadFromFile(path);

            StreamReader reader = new StreamReader(path);
            string[] file = reader.ReadToEnd().Split('\n', '\r', ' ');
            int latestNumber = 0;
            for (int i = 0; i < file.Length; i++)
            {
                if (string.Compare(file[i], pString) == 0)
                {
                    if (IsNumeric(file[i + 1]))
                    {
                        latestNumber = Convert.ToInt32(file[i + 1]);
                        i++;
                    }
                }
            }
            reader.Close();

            return latestNumber;
        }

        public static void SetValueAfterString(string path, string pString, int nextValue)
        {
            int lastValue = GetValueAfterString(path, pString);
            string before = pString + " " + lastValue;
            string after = pString + " " + nextValue;
            //Debug.Log(before + "|" + after);
            //Debug.Log("|" + before +"|"+ after + "|");
            ReplaceLineFromFile(path, after, before);
        }
        
        public static string[] AllFilesInPath(string path)
        {
            string[] files = Directory.GetFiles(path);

            return files;
        }
    }
}
