﻿using UnityEngine;
using TMPro;
using System.Reflection;
using UnityEngine.UI;

namespace Splotch.Loader
{
    /// <summary>
    /// A static class containing all of the modifications Splotch makes to Bopl's GUI.
    /// </summary>
    public static class BaseGuiModifications
    {
        /// <summary>
        /// A <c>GameObject</c> covering the entire screen displayed while in the main menu.
        /// </summary>
        public static GameObject mainMenuOverlayObject;
        /// <summary>
        /// Runs all of the main menu modifications. Called by the <c>Loader</c>
        /// </summary>
        internal static void RunMainMenuModifications()
        {
            // Create a new canvas
            mainMenuOverlayObject = new GameObject();
            Canvas canvas = mainMenuOverlayObject.AddComponent<Canvas>();
            CanvasScaler scaler = mainMenuOverlayObject.AddComponent<CanvasScaler>();

            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = Camera.current;

            canvas.sortingLayerName = "behind Walls Infront of everything else";
            canvas.sortingOrder = 1;

            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

            // create the version info GameObject and set the parent to the canvas
            GameObject versionInfoText = new GameObject("Splotch_Version_info", typeof(RectTransform), typeof(TextMeshProUGUI));
            TextMeshProUGUI textComponent = versionInfoText.GetComponent<TextMeshProUGUI>();
            versionInfoText.transform.SetParent(canvas.transform);

            // set the text to the version info
            AssemblyName name = Assembly.GetExecutingAssembly().GetName();

            textComponent.text = $"{ModManager.GetLoadedModsInfoText()}\n{name.Name} version {VersionChecker.currentVersionString}";

            // change settings
            textComponent.font = LocalizedText.localizationTable.GetFont(Settings.Get().Language, false);
            textComponent.color = Color.Lerp(Color.blue, Color.black, 0.6f);

            // not sure what this is I stole it from WackyModer lol
            textComponent.raycastTarget = false;

            textComponent.fontSize = 13;
            textComponent.alignment = TextAlignmentOptions.BottomRight;

            // set the position of the text to the bottom right of the screen
            RectTransform rectTransform = versionInfoText.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(1f, 0f);
            rectTransform.anchorMax = new Vector2(1f, 0f);
            rectTransform.pivot     = new Vector2(1, 0);
            rectTransform.sizeDelta = new Vector2(1200, 0);
            rectTransform.anchoredPosition = new Vector2(-10, 30);
        }
    }
}