using UnityEngine;

namespace Urd.Utils
{
    public class ResourceHelper<T> where T : UnityEngine.Object
    {
        public T FileLoaded
        {
            get
            {
                if (_fileLoaded == null)
                {
                    LoadFile();
                }

                return _fileLoaded;
            }
        }

        private T _fileLoaded;
        private string _path;

        public ResourceHelper(string path)
        {
            _path = path;
            LoadFile();
        }

        private void LoadFile()
        {
            _fileLoaded = Resources.Load<T>(_path);
        }
    }
}