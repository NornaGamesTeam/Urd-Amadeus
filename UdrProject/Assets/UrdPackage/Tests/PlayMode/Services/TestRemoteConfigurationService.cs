using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using Urd.Services;
using Urd.Services.RemoteConfiguration;

namespace Urd.Test
{
    public class TestRemoteConfigurationService
    {
        private const string TestBoolKey = "TestBool";
        private const bool TestBoolValue = true;
        private const string TestIntKey = "TestInt";
        private const int TestIntValue = 1;
        private const string TestFloatKey = "TestFloat";
        private const float TestFloatValue = 1.1f;
        private const string TestStringKey = "TestString";
        private const string TestStringValue = "TestString";
        private const string TestDictionaryKey = "TestDictionary";
        private Dictionary<string, string> TestDictionaryValue = new Dictionary<string, string>()
        {
            { "Ok", "Ok" },
        };


        private IRemoteConfigurationService _remoteConfigurationService;

        private bool _dataFetched;

        [SetUp]
        public void SetUp()
        {
            var remoteConfigurationService = new RemoteConfigurationService();
            remoteConfigurationService.Init();
            remoteConfigurationService.SetProvider(new TestRemoteConfigurationProvider());
            _remoteConfigurationService = remoteConfigurationService;

            _dataFetched = false;
        }

        [UnityTest]
        public IEnumerator RemoteConfigurationService_FetchData_Success()
        {
            _remoteConfigurationService.FetchData(OnFetchData);
            
            yield return new WaitUntil(() => _dataFetched);

            Assert.That(_dataFetched, Is.True);
        }

        [UnityTest]
        public IEnumerator RemoteConfigurationService_GetDataBool_Success()
        {
            _remoteConfigurationService.FetchData(OnFetchData);

            yield return new WaitUntil(() => _dataFetched);
            _remoteConfigurationService.TryGetDataAs(TestBoolKey, out bool boolValue);

            Assert.That(boolValue, Is.EqualTo(TestBoolValue));
        }

        [UnityTest]
        public IEnumerator RemoteConfigurationService_GetDataInt_Success()
        {
            _remoteConfigurationService.FetchData(OnFetchData);

            yield return new WaitUntil(() => _dataFetched);
            _remoteConfigurationService.TryGetDataAs(TestIntKey, out int intValue);

            Assert.That(intValue, Is.EqualTo(TestIntValue));
        }

        [UnityTest]
        public IEnumerator RemoteConfigurationService_GetDataFloat_Success()
        {
            _remoteConfigurationService.FetchData(OnFetchData);

            yield return new WaitUntil(() => _dataFetched);
            _remoteConfigurationService.TryGetDataAs(TestFloatKey, out float floatValue);

            Assert.That(floatValue, Is.EqualTo(TestFloatValue));
        }
        
        [UnityTest]
        public IEnumerator RemoteConfigurationService_GetDataString_Success()
        {
            _remoteConfigurationService.FetchData(OnFetchData);

            yield return new WaitUntil(() => _dataFetched);
            _remoteConfigurationService.TryGetDataAs(TestStringKey, out string stringValue);

            Assert.That(stringValue, Is.EqualTo(TestStringValue));
        }

        [UnityTest]
        public IEnumerator RemoteConfigurationService_GetDataDictionary_Success()
        {
            _remoteConfigurationService.FetchData(OnFetchData);

            yield return new WaitUntil(() => _dataFetched);
            _remoteConfigurationService.TryGetDataAs(TestDictionaryKey, out Dictionary<string, string> dictionaryValue);

            Assert.That(dictionaryValue, Is.EqualTo(TestDictionaryValue));
        }

        private void OnFetchData()
        {
            _dataFetched = true;
        }

        public class TestRemoteConfigurationProvider : IRemoteConfigurationProvider
        {
            public event IRemoteConfigurationProvider.OnGetRemoteConfigurationDataDelegate OnGetRemoteConfigurationData;
            public void FetchData(Action onFetchData)
            {
                var dictionary = new Dictionary<string, string>();
                dictionary.Add(TestBoolKey, TestBoolValue.ToString().ToLower());
                dictionary.Add(TestIntKey, TestIntValue.ToString());
                dictionary.Add(TestFloatKey, TestFloatValue.ToString());
                dictionary.Add(TestStringKey, TestStringValue);
                dictionary.Add(TestDictionaryKey, "{\"Ok\":\"Ok\"}");

                OnGetRemoteConfigurationData?.Invoke(dictionary);
                onFetchData?.Invoke();
            }

            
            public void SetEnvironment(RemoteConfigurationEnvironmentType environment) { }
            public void Dispose()
            {
                
            }

            public void Init()
            {
                
            }
        }
    }
}