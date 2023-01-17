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
        if (!string.Equals(_canvasTagName.ToString(), tag))
        {
            tag = _canvasTagName.ToString();
        }

        if (_canvasesGamesConfig != null)
        {
            GetComponent<CanvasScaler>().referenceResolution = _canvasesGamesConfig.CanvasResolution;
        }
    }
}
