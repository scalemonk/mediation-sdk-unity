using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScaleMonk.Ads
{
    public enum BannerPosition
    {
        TopLeft,
        TopCenter,
        TopRight,
        Centered,
        CenterLeft,
        CenterRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    public static class BannerPositionExtension
    {
        private static readonly Dictionary<BannerPosition, string> Positions = new Dictionary<BannerPosition, string>()
        {
            {BannerPosition.TopCenter, "top_center"},
            {BannerPosition.TopLeft, "top_left"},
            {BannerPosition.TopRight, "top_right"},
            {BannerPosition.Centered, "centered"},
            {BannerPosition.CenterLeft, "center_left"},
            {BannerPosition.CenterRight, "center_right"},
            {BannerPosition.BottomCenter, "bottom_center"},
            {BannerPosition.BottomLeft, "bottom_left"},
            {BannerPosition.BottomRight, "bottom_right"}
        };

        public static EditorPosition toEditorPosition(this BannerPosition position)
        {
            Vector2 anchorMin;
            Vector2 anchorMax;
            Vector2 pivot;

            switch (position)
            {
                case BannerPosition.TopCenter:
                    anchorMin = new Vector2(0.5f, 1);
                    anchorMax = new Vector2(0.5f, 1);
                    pivot = new Vector2(0.5f, 1);
                    break;
                case BannerPosition.TopLeft:
                    anchorMin = new Vector2(0, 1);
                    anchorMax = new Vector2(0, 1);
                    pivot = new Vector2(0, 1);
                    break;
                case BannerPosition.TopRight:
                    anchorMin = new Vector2(1, 1);
                    anchorMax = new Vector2(1, 1);
                    pivot = new Vector2(1, 1);
                    break;
                case BannerPosition.Centered:
                    anchorMin = new Vector2(0.5f, 0.5f);
                    anchorMax = new Vector2(0.5f, 0.5f);
                    pivot = new Vector2(0.5f, 0.5f);
                    break;
                case BannerPosition.CenterLeft:
                    anchorMin = new Vector2(0, 0.5f);
                    anchorMax = new Vector2(0, 0.5f);
                    pivot = new Vector2(0, 0.5f);
                    break;
                case BannerPosition.CenterRight:
                    anchorMin = new Vector2(1, 0.5f);
                    anchorMax = new Vector2(1, 0.5f);
                    pivot = new Vector2(1, 0.5f);
                    break;
                case BannerPosition.BottomLeft:
                    anchorMin = new Vector2(0, 0);
                    anchorMax = new Vector2(0, 0);
                    pivot = new Vector2(0, 0);
                    break;
                case BannerPosition.BottomCenter:
                    anchorMin = new Vector2(0.5f, 0);
                    anchorMax = new Vector2(0.5f, 0);
                    pivot = new Vector2(0.5f, 0);
                    break;
                case BannerPosition.BottomRight:
                    anchorMin = new Vector2(1, 0);
                    anchorMax = new Vector2(1, 0);
                    pivot = new Vector2(1, 0);
                    break;
                default:
                    AdsLogger.LogError("Position is not valid");
                    throw new Exception("Position is not valid");
            }

            return new EditorPosition(anchorMin, anchorMax, pivot);
        }

        public static string ToSnakeCaseString(this BannerPosition position)
        {
            return Positions[position];
        }
    }

    public class BannerSize
    {
        private int width;
        private int height;

        public static readonly BannerSize Small = new BannerSize(320, 50);
        public static readonly BannerSize Large = new BannerSize(320, 100);
        public static readonly BannerSize Rectangle = new BannerSize(300, 250);
        public static readonly BannerSize Full = new BannerSize(468, 60);
        public static readonly BannerSize Leaderboard = new BannerSize(728, 90);

        public BannerSize(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }
    }

    public class EditorPosition
    {
        private Vector2 anchorMin;
        private Vector2 anchorMax;
        private Vector2 pivot;

        public EditorPosition(Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot)
        {
            this.anchorMin = anchorMin;
            this.anchorMax = anchorMax;
            this.pivot = pivot;
        }

        public Vector2 AnchorMin
        {
            get { return anchorMin; }
        }

        public Vector2 AnchorMax
        {
            get { return anchorMax; }
        }

        public Vector2 Pivot
        {
            get { return pivot; }
        }
    }

    [Serializable]
    public class EventWrapper
    {
        public string eventName;
        public string[] eventKeys;
        public string[] eventValues;

        public Dictionary<string, string> GetEventParams()
        {
            var eventDictionary = new Dictionary<string, string>();

            for (int i = 0; i < eventKeys.Length; i++)
            {
                eventDictionary.Add(eventKeys[i], eventValues[i]);
            }

            return eventDictionary;
        }

        public bool HasEventParams()
        {
            return eventKeys.Length != 0 && eventValues.Length != 0;
        }
    }

    public interface IAnalytics
    {
        void SendEvent(string eventName, Dictionary<string, string> eventParams);
    }
}