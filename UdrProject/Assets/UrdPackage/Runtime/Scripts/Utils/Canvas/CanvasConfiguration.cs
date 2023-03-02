using UnityEngine;
using UnityEngine.UI;
using Urd.Utils;

[RequireComponent(typeof(CanvasScaler))]
public class CanvasConfiguration : MonoBehaviour
{
    [SerializeField] 
    private CanvasesGamesConfig _canvasesGamesConfig;
    
    [SerializeField] 
    private CanvasTagNames _canvasTagName;

    private void Awake()
    {
        SetTag();
        SetResolution();
    }

    private void SetTag()
    {
        if (!string.Equals(_canvasTagName.ToString(), tag))
        {
            tag = _canvasTagName.ToString();
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
