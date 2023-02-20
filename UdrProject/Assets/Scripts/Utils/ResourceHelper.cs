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

        public ResourceHelper(string path, bool loadOnAwake = false)
        {
            _path = path;

            if (loadOnAwake)
            {
                LoadFile();
            }
        }

        private void LoadFile()
        {
            _fileLoaded = Resources.Load<T>(_path);
        }
    }
}