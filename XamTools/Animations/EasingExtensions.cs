﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamTools.Animations
{
    public static class EasingExtensions
    {
        public static Easing GetEasing(EasingType type)
        {
            switch (type)
            {
                case EasingType.BounceIn:
                    return Easing.BounceIn;
                case EasingType.BounceOut:
                    return Easing.BounceOut;
                case EasingType.CubicIn:
                    return Easing.CubicIn;
                case EasingType.CubicInOut:
                    return Easing.CubicInOut;
                case EasingType.CubicOut:
                    return Easing.CubicOut;
                case EasingType.Linear:
                    return Easing.Linear;
                case EasingType.SinIn:
                    return Easing.SinIn;
                case EasingType.SinInOut:
                    return Easing.SinInOut;
                case EasingType.SinOut:
                    return Easing.SinOut;
                case EasingType.SpringIn:
                    return Easing.SpringIn;
                case EasingType.SpringOut:
                    return Easing.SpringOut;
            }

            return null;
        }
    }
}
