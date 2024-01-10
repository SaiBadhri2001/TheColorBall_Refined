using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Essentails
{
    public static class FileSystem
    {
        static string overallTextPath;
        #region Text File
        public static void CheckIfTextFileExistThenWrite(string fileName, string path, string text, bool deleteTheExistingFile = false)
        {
            overallTextPath = CheckTextFileFormatPattern(fileName, path);

            if (File.Exists(overallTextPath))
            {
                if (!deleteTheExistingFile)
                {
                    using (FileStream fileStream = new(overallTextPath, FileMode.Open))
                    {
                        byte[] textToWrite = System.Text.Encoding.UTF8.GetBytes(text);
                        fileStream.Write(textToWrite);
                        fileStream.Close();

                        Debug.Log("written to file successfull : " + overallTextPath);
                    }
                }
                else
                {
                    File.Delete(overallTextPath);

                    if (File.Exists(overallTextPath))
                    {
                        Debug.LogError("The file is still not deleted for some reason");
                    }

                    using (FileStream fileStream = new(overallTextPath, FileMode.Create))
                    {
                        byte[] textToWrite = System.Text.Encoding.UTF8.GetBytes(text);
                        fileStream.Write(textToWrite);
                        fileStream.Close();

                        Debug.Log("Created file successfully : " + overallTextPath);
                    }
                }
            }
            else
            {
                using (FileStream fileStream = new(overallTextPath, FileMode.Create))
                {
                    byte[] textToWrite = System.Text.Encoding.UTF8.GetBytes(text);
                    fileStream.Write(textToWrite);
                    fileStream.Close();

                    Debug.Log("Created file successfully : " + overallTextPath);
                }
            }
        }
        public static void CreateTextFile(string fileName, string path, string text)
        {
            overallTextPath = CheckTextFileFormatPattern(fileName, path);

            if (!File.Exists(overallTextPath))
            {
                using (FileStream fileStream = new(overallTextPath, FileMode.OpenOrCreate))
                {
                    byte[] textToWrite = System.Text.Encoding.UTF8.GetBytes(text);
                    fileStream.Write(textToWrite);
                    fileStream.Close();

                    Debug.Log("Created file successfully : " + overallTextPath);
                }
            }
            else
            {
                Debug.LogError("Mentioned file already exist in the path sepcified");
            }
        }
        public static void WriteToFile(string fileName, string path, string text)
        {
            overallTextPath = CheckTextFileFormatPattern(fileName, path);

            if (File.Exists(overallTextPath))
            {
                using (FileStream fileStream = new(overallTextPath, FileMode.Open))
                {
                    byte[] textToWrite = System.Text.Encoding.UTF8.GetBytes(text);
                    fileStream.Write(textToWrite);
                    fileStream.Close();
                }
            }
            else
            {
                Debug.LogError("Cannot find the file. try creating them first");
            }
        }
        public static string ReadFromTextFile(string fileName, string path)
        {
            overallTextPath = CheckTextFileFormatPattern(fileName, path);

            if (File.Exists(overallTextPath))
            {
                using (FileStream fileStream = new(overallTextPath, FileMode.Open))
                {
                    byte[] textFromFile = new byte[fileStream.Length];
                    fileStream.Read(textFromFile);
                    fileStream.Close();

                    return System.Text.Encoding.UTF8.GetString(textFromFile);
                }
            }

            Debug.LogError("File doesn't exist in the current path specified, cannot read the file");
            return string.Empty;
        }
        public static bool DeleteTextFile(string fileName, string path)
        {
            overallTextPath = CheckTextFileFormatPattern(fileName, path);

            if (File.Exists(overallTextPath))
            {
                File.Delete(overallTextPath);

                if (File.Exists(overallTextPath))
                {
                    Debug.LogError("The file is still not deleted for some reason");
                    return false;
                }
            }
            else
            {
                Debug.LogError("There is no such file found in the path you mentioned, cannot delete file");
                return false;
            }

            Debug.Log("File successfully deleted from path :" + overallTextPath);
            return true;
        }
        #endregion
        #region Binary File
        static string binaryFileOverallPath;
        public static void CreateBinaryFile(string fileName, string fileFormat, string path, string contant)
        {
            BinaryFormatter binaryFormatter = new();

            binaryFileOverallPath = CheckBinaryFileFormatPattern(fileName, path, fileFormat);

            if (!File.Exists(binaryFileOverallPath))
            {
                using (FileStream fileStream = new(binaryFileOverallPath, FileMode.OpenOrCreate))
                {
                    binaryFormatter.Serialize(fileStream, contant);
                    fileStream.Close();

                    Debug.Log("File created successfully at the path : " + binaryFileOverallPath);
                }
            }
            else
            {
                Debug.LogError("File with the same name and format already exists in the given directory");
            }
        }
        public static void WriteToBinaryFile(string fileName, string path, string text, string fileFormat)
        {
            binaryFileOverallPath = CheckBinaryFileFormatPattern(fileName, path, fileFormat);

            BinaryFormatter binaryFormatter = new();

            if (File.Exists(binaryFileOverallPath))
            {
                using (FileStream fileStream = new(binaryFileOverallPath, FileMode.Open))
                {
                    binaryFormatter.Serialize(fileStream, text);

                    fileStream.Close();

                    Debug.Log("Writtern to file successful");
                }
            }
            else
            {
                Debug.LogError("Cannot find the file, cannot write to file");
            }
        }
        public static string ReadFromBinaryFile(string fileName, string path, string fileFormat)
        {
            string retrivedData = string.Empty;

            BinaryFormatter binaryFormatter = new();

            binaryFileOverallPath = CheckBinaryFileFormatPattern(fileName, path, fileFormat);

            if (File.Exists(binaryFileOverallPath))
            {
                using (FileStream fileStream = new(binaryFileOverallPath, FileMode.Open))
                {
                    retrivedData = binaryFormatter.Deserialize(fileStream) as string;
                    fileStream.Close();
                }
            }
            else
            {
                Debug.LogError("Cannot find the file you mentioned in the directory");

                return string.Empty;
            }

            return retrivedData;
        }
        public static bool DeleteBinaryFile(string fileName, string path, string fileFormat)
        {
            binaryFileOverallPath = CheckBinaryFileFormatPattern(fileName, path, fileFormat);

            if (File.Exists(binaryFileOverallPath))
            {
                File.Delete(binaryFileOverallPath);

                Debug.Log("Delete file successfull");
            }
            else
            {
                Debug.LogError("File does not exist in the directory, cannot delete file");

                return false;
            }

            return true;
        }
        #endregion
        private static string CheckBinaryFileFormatPattern(string fileName, string path, string fileFormat)
        {
            string _tempText;

            if (!fileFormat.Contains("."))
            {
                fileFormat = "." + fileFormat;
            }
            if (!path.Contains(fileName))
            {
                if (!fileName.Contains("." + fileFormat))
                {
                    _tempText = path + "/" + fileName + fileFormat;

                    Debug.Log(_tempText);
                }
                else
                {
                    _tempText = path + "/" + fileName;
                }
            }
            else
            {
                if (!path.Contains("." + fileFormat))
                {
                    _tempText = path + "." + fileFormat;
                }
                else
                {
                    _tempText = path;
                }
            }

            return _tempText;
        }
        private static string CheckTextFileFormatPattern(string fileName, string path)
        {
            string _tempText;
            if (!path.Contains(fileName))
            {
                if (!fileName.Contains(".txt"))
                {
                    _tempText = path + "/" + fileName + ".txt";
                }
                else
                {
                    _tempText = path + "/" + fileName;
                }
            }
            else
            {
                if (!path.Contains(".txt"))
                {
                    _tempText = path + ".txt";
                }
                else
                {
                    _tempText = path;
                }
            }

            return _tempText;
        }
    }
}