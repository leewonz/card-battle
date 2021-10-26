using UnityEngine;

namespace Con
{
    public static class Tags
    {
        public const string gameController = "GameController";
        public const string cameraParent = "CameraParent";
        public const string mainCamera = "MainCamera";
        public const string uiCamera = "UICamera";
        public const string canvas = "Canvas";
        public const string player = "Player";
        public const string enemy = "Enemy";
        public const string slot = "Slot";
    }

    public static class Layers
    {
        public static string ui = "UI";
        public static string card = "Card";
        public static string clickable = "Clickable";
    }

    public static class RaycastDefaults
    {
        public const float maxDistance = 100.0f;
    }
}