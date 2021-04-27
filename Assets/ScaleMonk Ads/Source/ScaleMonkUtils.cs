using System;
using System.Collections.Generic;

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

        public int Width => width;

        public int Height => height;
    }
}