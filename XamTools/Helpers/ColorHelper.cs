using System;

#if __IOS__
using UIKit;
using CoreGraphics;
#endif

namespace eoTouchx.Helpers
{
    public struct ColorHelper
    {
        public static readonly ColorHelper Purple = 0xB455B6;
        public static readonly ColorHelper Blue = 0x3498DB;
        public static readonly ColorHelper DarkBlue = 0x3498DB;
        public static readonly ColorHelper Green = 0x77D065;
        public static readonly ColorHelper Gray = 0x738182;
        public static readonly ColorHelper LightGray = 0xB4BCBC;

        public double R, G, B;

        public static ColorHelper FromHex(int hex)
		{
			int AtHexOffSet(int offset) => (hex >> offset) & 0xFF;

			return new ColorHelper
            {
                R = AtHexOffSet(16) / 255.0,
                G = AtHexOffSet(8) / 255.0,
                B = AtHexOffSet(0) / 255.0
            };
		}

        public static implicit operator ColorHelper(int hex) => FromHex(hex);

#if __IOS__
		public UIColor ToUIColor () => UIColor.FromRGB ((float)R, (float)G, (float)B);

		public static implicit operator UIColor (ColorHelper color) => color.ToUIColor ();

		public static implicit operator CGColor (ColorHelper color) => color.ToUIColor ().CGColor;
#endif

        public Xamarin.Forms.Color ToFormsColor() => Xamarin.Forms.Color.FromRgb((int)(255 * R), (int)(255 * G), (int)(255 * B));

#if __ANDROID__
        public global::Android.Graphics.Color ToAndroidColor() => global::Android.Graphics.Color.Rgb((int)(255 * R), (int)(255 * G), (int)(255 * B));

		public static implicit operator global::Android.Graphics.Color(ColorHelper color) => color.ToAndroidColor();
#endif
    }
}