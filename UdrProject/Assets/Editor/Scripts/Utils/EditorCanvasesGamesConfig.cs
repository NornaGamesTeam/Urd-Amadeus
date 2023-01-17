using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Urd.UrdEditor;

public class EditorCanvasesGamesConfig
{
    private const string CANVAS_GAME_CONFIG = "CanvasesGamesConfig";

    [MenuItem("Urd/GameConfig/CanvasConfig")]
    public static void OpenConfigFile() => UrdEditorUtils.GetConfigFile(CANVAS_GAME_CONFIG);

}
