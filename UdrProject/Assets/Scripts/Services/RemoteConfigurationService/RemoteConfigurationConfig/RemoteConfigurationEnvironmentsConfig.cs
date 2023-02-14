using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urd.Services.RemoteConfiguration
{
    public class RemoteConfigurationEnvironmentsConfig : ScriptableObject
    {

        [field: SerializeField] public List<RemoteConfigurationEnvironmentsInfo> Environments { get; private set; }

        public bool TryGetEnvironment(RemoteConfigurationEnvironmentType environmentType, out string environmentId)
        {
            var info = Environments.Find(environmentInfo => environmentInfo.EnvironmentType == environmentType);
            environmentId = info?.EnvironmentId;
            return !string.IsNullOrEmpty(environmentId);
        }

        [System.Serializable]
        public class RemoteConfigurationEnvironmentsInfo
        {
            [field: SerializeField] public RemoteConfigurationEnvironmentType EnvironmentType { get; private set; }

            [field: SerializeField] public string EnvironmentId { get; private set; }
        }
    }


}