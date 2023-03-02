using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urd.Asset
{
    public interface IShowableModel
    {
        string Addressable { get; }
        
        event System.Action OnChanged;
    }
}