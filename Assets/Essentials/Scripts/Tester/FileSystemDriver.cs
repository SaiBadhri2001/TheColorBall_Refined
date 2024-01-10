using UnityEngine;
namespace Essentails
{
    public class FileSystemDriver : MonoBehaviour
    {
        [Header("Path details")]
        public string Path = string.Empty;
        [Header("File details")]
        public bool IsTxtFile;
        public string FileName;
        [TextArea(5, 30)]
        public string TextForFile;

        public string FileFormat;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                DeleteFile();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                CreateFile();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                ReadFile();
            }
        }
        [ContextMenu("Delete File")]
        private void DeleteFile()
        {
            if (IsTxtFile)
            {
                FileSystem.DeleteTextFile(FileName, Application.persistentDataPath);
            }
            else
            {
                FileSystem.DeleteBinaryFile(FileName, Application.persistentDataPath, ".bbb");
            }
        }
        [ContextMenu("Read File")]
        private void ReadFile()
        {
            string _readText = string.Empty;

            if (IsTxtFile)
            {
                _readText = FileSystem.ReadFromTextFile(FileName, Application.persistentDataPath);
            }
            else
            {
                _readText = FileSystem.ReadFromBinaryFile(FileName, Application.persistentDataPath, ".bbb");
            }

            if (!string.IsNullOrEmpty(_readText))
            {
                Debug.Log("Data from the file is : '" + _readText + "'");
            }
        }
        [ContextMenu("Create File")]
        private void CreateFile()
        {
            if (IsTxtFile)
            {
                FileSystem.CreateTextFile(FileName, Application.persistentDataPath, TextForFile);
            }
            else
            {
                FileSystem.CreateBinaryFile(FileName, ".bbb", Application.persistentDataPath, TextForFile);
            }
        }
    }
}
