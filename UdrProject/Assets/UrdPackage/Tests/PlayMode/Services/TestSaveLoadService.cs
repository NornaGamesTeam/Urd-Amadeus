using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.TestTools;
using Urd.Error;
using Urd.SaveLoad;
using Urd.Services;

namespace Urd.Test
{
    public class TestSaveLoadService
    {
        private ISaveLoadDataService _saveLoadDataService;

        public const string ArbitraryKey = "KEY";
        public const bool ArbitraryValueBool = true;
        public const int ArbitraryValueInt = 1;
        public const float ArbitraryValueFloat = 1.0f;
        public const string ArbitraryValueString = "Value";
        public DummyClass ArbitraryValueClass;
        
        public bool _resultValueBool;
        public int _resultValueInt;
        public float _resultValueFloat;
        public string _resultValueString;
        public DummyClass _resultValueClass;

        private ErrorModel _errorModel;

        [SetUp]
        public void SetUp()
        {
            _saveLoadDataService = new SaveLoadDataService();
            _saveLoadDataService.AddProvider(new SaveLoadDataServicePlayerPrefProvider());

            _errorModel = null;

            _resultValueBool = false;
            _resultValueInt = 0;
            _resultValueFloat = 0.0f;
            _resultValueString = string.Empty;
            _resultValueClass = null;
        }

        [UnityTest]
        public IEnumerator SaveLoadService_Save_Int()
        {
            _saveLoadDataService.SaveData(ArbitraryKey, ArbitraryValueInt, OnDataSavedCallback);
            
            yield return new WaitUntil(() => _errorModel != null);
            
            Assert.That(_errorModel?.IsSuccess, Is.EqualTo(true));
        }

        [UnityTest]
        public IEnumerator SaveLoadService_Load_Int()
        {
            _saveLoadDataService.SaveData(ArbitraryKey, ArbitraryValueInt, OnDataSavedCallback);
            yield return new WaitUntil(() => _errorModel != null);
            if (_errorModel.IsSuccess)
            {
                _saveLoadDataService.LoadData(ArbitraryKey, ArbitraryValueInt, OnDataLoadIntCallback);
            
                yield return new WaitUntil(() => _errorModel != null);
            }
            
            Assert.That(_errorModel?.IsSuccess == true 
                        && ArbitraryValueInt == _resultValueInt, 
                        Is.EqualTo(true));
        }
        
        [UnityTest]
        public IEnumerator SaveLoadService_Save_Float()
        {
            _saveLoadDataService.SaveData(ArbitraryKey, ArbitraryValueFloat, OnDataSavedCallback);
            
            yield return new WaitUntil(() => _errorModel != null);
            
            Assert.That(_errorModel?.IsSuccess, Is.EqualTo(true));
        }

        [UnityTest]
        public IEnumerator SaveLoadService_Load_Float()
        {
            _saveLoadDataService.SaveData(ArbitraryKey, ArbitraryValueFloat, OnDataSavedCallback);
            yield return new WaitUntil(() => _errorModel != null);
            if (_errorModel.IsSuccess)
            {
                _saveLoadDataService.LoadData(ArbitraryKey, ArbitraryValueFloat, OnDataLoadFloatCallback);
            
                yield return new WaitUntil(() => _errorModel != null);
            }
            
            Assert.That(_errorModel?.IsSuccess == true 
                        && ArbitraryValueFloat == _resultValueFloat, 
                        Is.EqualTo(true));
        }
        
        [UnityTest]
        public IEnumerator SaveLoadService_Save_String()
        {
            _saveLoadDataService.SaveData(ArbitraryKey, ArbitraryValueString, OnDataSavedCallback);
            
            yield return new WaitUntil(() => _errorModel != null);
            
            Assert.That(_errorModel?.IsSuccess, Is.EqualTo(true));
        }

        [UnityTest]
        public IEnumerator SaveLoadService_Load_String()
        {
            _saveLoadDataService.SaveData(ArbitraryKey, ArbitraryValueString, OnDataSavedCallback);
            yield return new WaitUntil(() => _errorModel != null);
            if (_errorModel.IsSuccess)
            {
                _saveLoadDataService.LoadData(ArbitraryKey, ArbitraryValueString, OnDataLoadStringCallback);
            
                yield return new WaitUntil(() => _errorModel != null);
            }
            
            Assert.That(_errorModel?.IsSuccess == true 
                        && ArbitraryValueString == _resultValueString, 
                        Is.EqualTo(true));
        }
        
        [UnityTest]
        public IEnumerator SaveLoadService_Save_Bool()
        {
            _saveLoadDataService.SaveData(ArbitraryKey, ArbitraryValueBool, OnDataSavedCallback);
            
            yield return new WaitUntil(() => _errorModel != null);
            
            Assert.That(_errorModel?.IsSuccess, Is.EqualTo(true));
        }

        [UnityTest]
        public IEnumerator SaveLoadService_Load_Bool()
        {
            _saveLoadDataService.SaveData(ArbitraryKey, ArbitraryValueBool, OnDataSavedCallback);
            yield return new WaitUntil(() => _errorModel != null);
            if (_errorModel.IsSuccess)
            {
                _saveLoadDataService.LoadData(ArbitraryKey, ArbitraryValueBool, OnDataLoadBoolCallback);
            
                yield return new WaitUntil(() => _errorModel != null);
            }
            
            Assert.That(_errorModel?.IsSuccess == true 
                        && ArbitraryValueBool == _resultValueBool, 
                        Is.EqualTo(true));
        }
        
        [UnityTest]
        public IEnumerator SaveLoadService_Save_Class()
        {
            _saveLoadDataService.SaveData(ArbitraryKey, ArbitraryValueClass, OnDataSavedCallback);
            
            yield return new WaitUntil(() => _errorModel != null);
            
            Assert.That(_errorModel?.IsSuccess, Is.EqualTo(true));
        }

        [UnityTest]
        public IEnumerator SaveLoadService_Load_Class()
        {
            ArbitraryValueClass = new DummyClass(ArbitraryValueFloat);
            _saveLoadDataService.SaveData(ArbitraryKey, ArbitraryValueClass, OnDataSavedCallback);
            yield return new WaitUntil(() => _errorModel != null);
            if (_errorModel.IsSuccess)
            {
                var defaultClass = new DummyClass(20);
                _saveLoadDataService.LoadData(ArbitraryKey, defaultClass, OnDataLoadClassCallback);
            
                yield return new WaitUntil(() => _errorModel != null);
            }
            
            Assert.That(_errorModel?.IsSuccess == true 
                        && ArbitraryValueClass.FloatValue == _resultValueClass.FloatValue, 
                        Is.EqualTo(true));
        }

        private void OnDataSavedCallback(ErrorModel errorModel)
        {
            _errorModel = errorModel;
        }
        
        private void OnDataLoadIntCallback(ErrorModel errorModel, int value)
        {
            _errorModel = errorModel;
            _resultValueInt = value;
        }
        
        private void OnDataLoadFloatCallback(ErrorModel errorModel, float value)
        {
            _errorModel = errorModel;
            _resultValueFloat = value;
        }
        
        private void OnDataLoadStringCallback(ErrorModel errorModel, string value)
        {
            _errorModel = errorModel;
            _resultValueString = value;
        }
        
        private void OnDataLoadBoolCallback(ErrorModel errorModel, bool value)
        {
            _errorModel = errorModel;
            _resultValueBool = value;
        }
        
        private void OnDataLoadClassCallback(ErrorModel errorModel, DummyClass value)
        {
            _errorModel = errorModel;
            _resultValueClass = value;
        }
    
        public class DummyClass
        {
            [JsonProperty]
            public float FloatValue { get; private set; }

            public DummyClass(float floatValue)
            {
                FloatValue = floatValue;
            }
        }
    }

}