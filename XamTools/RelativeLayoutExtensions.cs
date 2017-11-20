using Xamarin.Forms;

namespace XamTools
{
   public static class RelativeLayoutExtensions
        {
                
        public static void AddLeft(this RelativeLayout.IRelativeList<View> layout, View view, int width, int height, int x, int y) => layout.Add(view,
                xConstraint: Constraint.RelativeToParent(arg => x),
                yConstraint: Constraint.RelativeToParent(arg => y),
                widthConstraint: Constraint.RelativeToParent(arg => width >= 0 ? (width > 0 ? width : arg.Width - (x * 2)) : arg.Width + width),
                heightConstraint: Constraint.RelativeToParent(arg => height >= 0 ? (height > 0 ? height : arg.Height - (y * 2)) : arg.Height + height));

        public static void AddLeft(this RelativeLayout.IRelativeList<View> layout, View view, int width, int x, int y) => layout.Add(view,
                xConstraint: Constraint.RelativeToParent(arg => x > 0 ? x : (arg.Width - width) + x),
                yConstraint: Constraint.RelativeToParent(arg => y),
                widthConstraint: Constraint.RelativeToParent(arg => width >= 0 ? (width > 0 ? width : arg.Width - (x * 2)) : arg.Width + width));

        public static void AddRight(this RelativeLayout.IRelativeList<View> layout, View view, int width, int height, int x, int y) => layout.Add(view,
                xConstraint: Constraint.RelativeToParent(arg => arg.Width - width - x),
                yConstraint: Constraint.RelativeToParent(arg => y),
                widthConstraint: Constraint.RelativeToParent(arg => width >= 0 ? (width > 0 ? width : arg.Width - (x * 2)) : arg.Width + width),
                heightConstraint: Constraint.RelativeToParent(arg => height >= 0 ? (height > 0 ? height : arg.Height - (y * 2)) : arg.Height + height));

        public static void AddRight(this RelativeLayout.IRelativeList<View> layout, View view, int width, int x, int y) => layout.Add(view,
                xConstraint: Constraint.RelativeToParent(arg => arg.Width - width - x),
                yConstraint: Constraint.RelativeToParent(arg => y),
                widthConstraint: Constraint.RelativeToParent(arg => width >= 0 ? (width > 0 ? width : arg.Width - (x * 2)) : arg.Width + width));

        public static void AddLeftAfter(this RelativeLayout.IRelativeList<View> layout, View view, View prev, int width, int height, int x, int y) => layout.Add(view,
                xConstraint: Constraint.RelativeToParent(arg => x > 0 ? x : arg.Width + x),
                yConstraint: Constraint.RelativeToView(prev, (parent, arg) => arg.Y + arg.Height + y),
                widthConstraint: Constraint.RelativeToParent(arg => width >= 0 ? (width > 0 ? width : arg.Width - (x * 2)) : arg.Width + width),
                heightConstraint: Constraint.RelativeToParent(arg => height >= 0 ? (height > 0 ? height : arg.Height - (y * 2)) : arg.Height + height));

        public static void AddLeftAfter(this RelativeLayout.IRelativeList<View> layout, View view, View prev, int width, int x, int y) => layout.Add(view,
                xConstraint: Constraint.RelativeToParent(arg => x),
                yConstraint: Constraint.RelativeToView(prev, (parent, arg) => arg.Y + arg.Height + y),
                widthConstraint: Constraint.RelativeToParent(arg => width >= 0 ? (width > 0 ? width : arg.Width - (x * 2)) : arg.Width + width));

        public static void AddRightAfter(this RelativeLayout.IRelativeList<View> layout, View view, View prev, int width, int height, int x, int y) => layout.Add(view,
                xConstraint: Constraint.RelativeToParent(arg => arg.Width - width - x),
                yConstraint: Constraint.RelativeToView(prev, (parent, arg) => arg.Y + arg.Height + y),
                widthConstraint: Constraint.RelativeToParent(arg => width >= 0 ? (width > 0 ? width : arg.Width - (x * 2)) : arg.Width + width),
                heightConstraint: Constraint.RelativeToParent(arg => height >= 0 ? (height > 0 ? height : arg.Height - (y * 2)) : arg.Height + height));

        public static void AddRightAfter(this RelativeLayout.IRelativeList<View> layout, View view, View prev, int width, int x, int y) => layout.Add(view,
                xConstraint: Constraint.RelativeToParent(arg => arg.Width - width - x),
                yConstraint: Constraint.RelativeToView(prev, (parent, arg) => arg.Y + arg.Height + y),
                widthConstraint: Constraint.RelativeToParent(arg => width >= 0 ? (width > 0 ? width : arg.Width - (x * 2)) : arg.Width + width));

        public static void AddCentered(this RelativeLayout.IRelativeList<View> layout, View view, int width, int height, int y) => layout.Add(view,
                xConstraint: Constraint.RelativeToParent(arg => width > 0 ? (arg.Width - width) / 2 : 0 - (width / 2)),
                yConstraint: Constraint.RelativeToParent(arg => y),
                widthConstraint: Constraint.RelativeToParent(arg => width > 0 ? width : arg.Width + width),
                heightConstraint: Constraint.RelativeToParent(arg => height > 0 ? height : arg.Height));

        public static void AddCentered(this RelativeLayout.IRelativeList<View> layout, View view, int width, int y) => layout.Add(view,
                xConstraint: Constraint.RelativeToParent(arg => width > 0 ? (arg.Width - width) / 2 : 0 - (width / 2)),
                yConstraint: Constraint.RelativeToParent(arg => y),
                widthConstraint: Constraint.RelativeToParent(arg => width > 0 ? width : arg.Width + width));

        public static void AddAfter(this RelativeLayout.IRelativeList<View> layout, View view, View prev, int width, int y) => layout.Add(view,
                xConstraint: Constraint.RelativeToParent(arg => width > 0 ? (arg.Width - width) / 2 : 0 - (width / 2)),
                yConstraint: Constraint.RelativeToView(prev, (parent, arg) => arg.Y + arg.Height + y),
                widthConstraint: Constraint.RelativeToParent(arg => width > 0 ? width : arg.Width + width));

        public static void AddInOrigo(this RelativeLayout.IRelativeList<View> layout, View view, int width, int height) => layout.Add(view,
                xConstraint: Constraint.RelativeToParent(arg => width > 0 ? (arg.Width - width) / 2 : 0 - (width / 2)),
                yConstraint: Constraint.RelativeToParent(arg => height > 0 ? (arg.Height - height) / 2 : 0 - (height / 2)),
                widthConstraint: Constraint.RelativeToParent(arg => width > 0 ? width : arg.Width + width),
                heightConstraint: Constraint.RelativeToParent(arg => height > 0 ? height : arg.Height));
    }
}
