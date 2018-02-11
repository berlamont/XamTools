using System.Drawing;

#if __IOS__
using CoreGraphics;
using UIKit;
#endif
namespace eoTouchx.Extensions
{
    public static class ImageExtensions
    {
        
#if __IOS__
        /// <summary>
        /// Gets a recolored version of the image you are requesting
        /// </summary>
        /// <param name="image">the Image you wish to recolor</param>
        /// <param name="color">the color you want the image to be</param>
        /// <returns>Your requested image, recolored</returns>
        public static UIImage Colored(this UIImage image, UIColor color)
        {
            if (image == null || color == null)
                return image;

            UIImage coloredImage;

            UIGraphics.BeginImageContextWithOptions(image.Size, false, UIScreen.MainScreen.Scale);
            using (var context = UIGraphics.GetCurrentContext())
            {
                context.TranslateCTM(0, image.Size.Height);
                context.ScaleCTM(1.0f, -1.0f);

                var rect = new CGRect(0, 0, image.Size.Width, image.Size.Height);

                //draw image to get transparency mask
                context.SetBlendMode(CGBlendMode.Normal);
                context.DrawImage(rect, image.CGImage);

                context.SetBlendMode(CGBlendMode.SourceIn);
                context.SetFillColor(color.CGColor);
                context.FillRect(rect);

                coloredImage = UIGraphics.GetImageFromCurrentImageContext();
                UIGraphics.EndImageContext();
            }
            return coloredImage;
        }

        /// <summary>
        /// Orientates the image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="orientation">The orientation.</param>
        public static UIImage WithOrientation(this UIImage image, UIImageOrientation orientation) => UIImage.FromImage(image.CGImage, 1, orientation);

        /// <summary>
        /// Resizes the Image while trying to keep aspect ratio
        /// </summary>
        /// <param name="sourceImage">The source image</param>
        /// <param name="maxWidth">The maximum width</param>
        /// <param name="maxHeight">the maximum height</param>
        /// <returns></returns>
        public static UIImage ResizeAndKeepAspect(this UIImage sourceImage, float maxWidth, float maxHeight)
        {
            var sourceSize = sourceImage.Size;
            var widthIsGreaterOrEqualToHeight = sourceSize.Width >= sourceSize.Height;
            var widthIsGreaterOrEqualToMax = sourceSize.Width >= maxWidth;
            var heightIsGreaterOrEqualToMax = sourceSize.Height >= maxHeight;
            var sizeFactor = widthIsGreaterOrEqualToHeight && widthIsGreaterOrEqualToMax ? maxWidth / sourceSize.Width : heightIsGreaterOrEqualToMax ? maxHeight / sourceSize.Height : 1;

            var width =
                (float)
                    (!widthIsGreaterOrEqualToMax
                        ? (!heightIsGreaterOrEqualToMax) ? sourceSize.Width : sourceSize.Width * sizeFactor
                        : maxWidth);
            var height =
                (float)
                    (!heightIsGreaterOrEqualToMax
                        ? (!widthIsGreaterOrEqualToMax) ? sourceSize.Height : sourceSize.Height * sizeFactor
                        : maxHeight);

            UIGraphics.BeginImageContext(new SizeF(width, height));
            sourceImage.Draw(new RectangleF(0, 0, width, height));
            var resultImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return resultImage;
        }

        /// <summary>
        /// Resizes the image without trying to keep aspect ratio
        /// </summary>
        /// <param name="sourceImage">The source image.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public static UIImage Resize(this UIImage sourceImage, float width, float height)
        {
            UIGraphics.BeginImageContext(new SizeF(width, height));
            sourceImage.Draw(new RectangleF(0, 0, width, height));
            var resultImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return resultImage;
        }

        /// <summary>
        /// Crops the specified Image
        /// </summary>
        /// <param name="sourceImage">The source image.</param>
        /// <param name="cropX">The crop x.</param>
        /// <param name="cropY">The crop y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public static UIImage Cropped(this UIImage sourceImage, int cropX, int cropY, int width, int height)
        {
            var imgSize = sourceImage.Size;
            UIGraphics.BeginImageContext(new SizeF(width, height));
            var context = UIGraphics.GetCurrentContext();
            var clippedRect = new RectangleF(0, 0, width, height);
            context.ClipToRect(clippedRect);
            var drawRect = new RectangleF(-cropX, -cropY, (float)imgSize.Width, (float)imgSize.Height);
            sourceImage.Draw(drawRect);
            var modifiedImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return modifiedImage;
        }
#endif
    }
}
