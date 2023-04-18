using MyBox;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Urd.Utils;

[RequireComponent(typeof(CanvasScaler))]
public class CanvasConfiguration : MonoBehaviour
{
    [SerializeField] 
    private CanvasesGamesConfig _canvasesGamesConfig;
    
    [SerializeField, Tag] 
    private string _canvasTag;

    private void Awake()
    {
        SetTag();
        SetResolution();
    }

    private void SetTag()
    {
        if (!string.Equals(_canvasTag, tag))
        {
            tag = _canvasTag;
        }
    }
    private void SetResolution()
    {
        if (_canvasesGamesConfig != null)
        {
            GetComponent<CanvasScaler>().referenceResolution = _canvasesGamesConfig.CanvasResolution;
        }
    }
}
