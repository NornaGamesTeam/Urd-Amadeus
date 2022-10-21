using NSubstitute;
using NUnit.Framework;
using System;
using Urd.Error;
using Urd.Services;
using Urd.Services.Cache;

namespace Urd.Test
{
    public class TestCacheService
    {
        private ICacheService _cacheService;

        private CacheModelRaw _cacheModelRaw;
        private CacheModel<ArbitrarySerializableClass> _cacheModel;
        private ArbitrarySerializableClass _arbitrarySerializableClass;

        private bool _cacheSaved;
        private bool _cacheLoaded;
        private string _cacheLoadedString;
        private ArbitrarySerializableClass _cacheLoadedClass;

        private string arbitraryKey = "ArbitraryKey";
        private string arbitraryCacheValue = "arbitrary value";

        [SetUp]
        public void SetUp()
        {
            IServiceLocator serviceLocator = new ServiceLocator();
            _cacheService = new CacheService();
            serviceLocator.Register<ICacheService>(_cacheService);

            _cacheModelRaw = new CacheModelRaw(arbitraryKey, arbitraryCacheValue);
            _arbitrarySerializableClass = new ArbitrarySerializableClass();
            _cacheModel = new CacheModel<ArbitrarySerializableClass>(arbitraryKey, _arbitrarySerializableClass);

            _cacheSaved = false;
            _cacheLoaded = false;
            _cacheLoadedString = null;
            _cacheLoadedClass = null;
        }

        [Test]
        public void CacheService_SaveCacheRaw_Success()
        {
            _cacheService.SaveCache(_cacheModelRaw, OnCacheSavedSuccess, OnCacheSavedFailed);

            Assert.That(_cacheSaved, Is.True);
        }

        [Test]
        public void CacheService_SaveCacheRaw_Failed()
        {
            _cacheModelRaw = new CacheModelRaw("\n");
            _cacheService.SaveCache(_cacheModelRaw, OnCacheSavedSuccess, OnCacheSavedFailed);

            Assert.That(_cacheSaved, Is.False);
        }

        [Test]
        public void CacheService_LoadCacheRaw_Success()
        {
            _cacheModelRaw = new CacheModelRaw(arbitraryKey);
            _cacheService.SaveCache(_cacheModelRaw, OnCacheLoadSuccess, OnCacheLoadFailed);
            
            _cacheService.LoadCache(_cacheModelRaw, OnCacheLoadSuccess, OnCacheLoadFailed);

            Assert.That(_cacheLoadedString, Is.EqualTo(_cacheModelRaw.RawValue));
        }

        [Test]
        public void CacheService_LoadCacheRaw_Failed()
        {
            _cacheModelRaw = new CacheModelRaw("\n");
            _cacheService.SaveCache(_cacheModelRaw, OnCacheLoadSuccess, OnCacheLoadFailed);

            Assert.That(_cacheLoaded, Is.False);
        }

        [Test]
        public void CacheService_SaveCache_Success()
        {
            _cacheService.SaveCache(_cacheModel, OnCacheSavedSuccess, OnCacheSavedFailed);

            Assert.That(_cacheSaved, Is.True);
        }

        [Test]
        public void CacheService_SaveCache_Failed()
        {
            _cacheModel = new CacheModel<ArbitrarySerializableClass>("\n", _arbitrarySerializableClass);
            _cacheService.SaveCache(_cacheModel, OnCacheSavedSuccess, OnCacheSavedFailed);

            Assert.That(_cacheSaved, Is.False);
        }

        [Test]
        public void CacheService_LoadCache_Success()
        {
            _cacheService.SaveCache(_cacheModel, OnCacheLoadSuccess, OnCacheLoadFailed);

            _cacheService.LoadCache(_cacheModel, OnCacheLoadSuccess, OnCacheLoadFailed);

            Assert.That(_cacheModel.Value, Is.EqualTo(_cacheLoadedClass));
        }

        [Test]
        public void CacheService_LoadCache_Failed()
        {
            _cacheService.SaveCache(_cacheModel, OnCacheSavedSuccess, OnCacheSavedFailed);
            var cacheModel = new CacheModel<UnityEngine.Transform>(arbitraryKey);

            _cacheService.SaveCache(cacheModel, OnCacheSavedSuccess, OnCacheSavedFailed);

            Assert.That(_cacheLoadedClass, Is.Null);
        }

        private void OnCacheSavedSuccess(CacheModelRaw cacheModel)
        {
            _cacheSaved = true;
            
        }
        private void OnCacheSavedFailed(ErrorModel errorModel)
        {
            _cacheSaved = false;
        }

        private void OnCacheLoadSuccess(CacheModelRaw cacheModel)
        {
            _cacheLoaded = true;
            _cacheLoadedString = cacheModel.RawValue;
            cacheModel.TryGetValue<ArbitrarySerializableClass>(out _cacheLoadedClass);
        }

        private void OnCacheLoadFailed(ErrorModel errorModel)
        {
            _cacheLoaded = false;
        }

        private class ArbitrarySerializableClass : IEquatable<ArbitrarySerializableClass>
        {
            public int arbitraryInt = 5;
            public string arbitraryString = "string";

            public bool Equals(ArbitrarySerializableClass other)
            {
                return arbitraryInt == other.arbitraryInt
                    && arbitraryString == other.arbitraryString;
            }
        }
    }
}