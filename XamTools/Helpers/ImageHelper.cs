using System.Collections.Generic;
using Xamarin.Forms;

namespace eoTouchx.Helpers
{
    public static class ImageHelper
    {
        public static class eoImages
        {
            const string ImagePlaceholderName = "imageplaceholder.png";

            static Dictionary<string, ImageSource> _loadedImages;

            public static ImageSource ImagePlaceholder => GetImage(ImagePlaceholderName);

            public static ImageSource Loopback => GetImage("_3_loopback.png");
            public static ImageSource WhiteBarcode = GetImage("white_195_barcode.png");
            public static ImageSource ArrowEast => GetImage("_2_arrow_east.png");
            public static ImageSource ArrowWest => GetImage("_9_arrow_west.png");
            public static ImageSource Barcode => GetImage("_195_barcode.png");
            public static ImageSource BetaWarning => GetImage("_184_warning.png");
            public static ImageSource Box => GetImage("_256_box2.png");
            public static ImageSource Truck => GetImage("_15_bus.png");
            public static ImageSource Calculator => GetImage("_161_calculator.png");
            public static ImageSource Calendar => GetImage("_83_calendar.png");
            public static ImageSource Camera => GetImage("_86_camera.png");
            public static ImageSource CircleCheckmark => GetImage("circle_checkmark.png");
            public static ImageSource CircleExclamation => GetImage("circle_exclamation.png");
            public static ImageSource CircleX => GetImage("circle_x.png");
            public static ImageSource Clock => GetImage("_11_clock.png");
            public static ImageSource Cloud => GetImage("_234_cloud.png");
            public static ImageSource Compass => GetImage("compass.png");
            public static ImageSource Compose => GetImage("_216_compose.png");
            public static ImageSource DialPad => GetImage("_40_dialpad.png");
            public static ImageSource Download => GetImage("_265_download.png");
            public static ImageSource EmptyCircle => GetImage("empty_circle.png");
            public static ImageSource Eye => GetImage("_12_eye.png");
            public static ImageSource Gear => GetImage("_19_gear.png");
            public static ImageSource Gears => GetImage("_20_gear_2.png");
            public static ImageSource Glasses => GetImage("_164_glasses_2.png");
            public static ImageSource HollowStar => GetImage("hollow_star.png");
            public static ImageSource LineChart => GetImage("_16linechart.png");
            public static ImageSource List => GetImage("_259_list.png");
            public static ImageSource Location => GetImage("location.png");
            public static ImageSource LocationArrow => GetImage("location_arrow.png");
            public static ImageSource Lock => GetImage("_54_lock.png");
            public static ImageSource MagnifyingGlass => GetImage("_6_magnify.png");
            public static ImageSource Navigation => GetImage("navigation.png");
            public static ImageSource Notepad => GetImage("_179_notepad.png");
            public static ImageSource Objectives => GetImage("objectives.png");
            public static ImageSource OrderInfo => GetImage("order_info.png");
            public static ImageSource OrderInfo_2 => GetImage("order_info2.png");
            public static ImageSource Pencil => GetImage("_187_pencil.png");
            public static ImageSource Person => GetImage("_253person.png");
            public static ImageSource Phone => GetImage("phone.png");
            public static ImageSource PhotoRoll => GetImage("_43_film_roll.png");
            public static ImageSource Photos => GetImage("_42_photos.png");
            public static ImageSource PictureFrame => GetImage("_41_picture_frame.png");
            public static ImageSource Preorder => GetImage("pre_order.png");
            public static ImageSource Presentation => GetImage("_137presentation.png");
            public static ImageSource Printer => GetImage("_185_printer");
            public static ImageSource QuickEntry => GetImage("quickEntry.png");
            public static ImageSource Redo => GetImage("_2_redo.png");
            public static ImageSource Replenish => GetImage("replenish.png");
            public static ImageSource RetailInitiatives => GetImage("retail_initiatives.png");
            public static ImageSource Route => GetImage("_246_route.png");
            public static ImageSource Star => GetImage("_28_star.png");
            public static ImageSource Stats => GetImage("_122stats.png");
            public static ImageSource Target => GetImage("_13target.png");
            public static ImageSource Todo => GetImage("_117_todo.png");
            public static ImageSource Ufo => GetImage("_133ufo.png");
            public static ImageSource Upload => GetImage("_266_upload.png");
            public static ImageSource Warning => GetImage("red_warning.png");
            public static ImageSource Windows => GetImage("_272_windows.png");
            public static ImageSource Wrench => GetImage("wrench.png");

            public static ImageSource GetImage(string name)
            {
                if (_loadedImages == null)
                    _loadedImages = new Dictionary<string, ImageSource>();

                if (_loadedImages.ContainsKey(name))
                    return _loadedImages[name];

                ImageSource imageSource;

                switch (Device.RuntimePlatform)
                {
                    case "iOS":
                        imageSource = ImageSource.FromFile($"Images/{name}");
                        break;
                    case "Android":
                        imageSource = ImageSource.FromFile(name);
                        break;
                    default:
                        imageSource = ImageSource.FromFile(name);
                        break;
                }

                if (imageSource == null)
                    return ImagePlaceholder;

                _loadedImages[name] = imageSource;
                return imageSource;
            }
        }
    }
}