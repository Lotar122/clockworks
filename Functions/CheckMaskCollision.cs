using System.Numerics;
using clockworks.Classes;

namespace clockworks.Methods
{
    public partial class Functions
    {
        public static bool CheckMaskCollision(Mask mask1, Vector2 position1, Mask mask2, Vector2 position2)
        {
            // Calculate the bounding box of each object in screen coordinates
            int left1 = (int)position1.X;
            int top1 = (int)position1.Y;
            int right1 = (int)(position1.X + mask1.Width);
            int bottom1 = (int)(position1.Y + mask1.Height);

            int left2 = (int)position2.X;
            int top2 = (int)position2.Y;
            int right2 = (int)(position2.X + mask2.Width);
            int bottom2 = (int)(position2.Y + mask2.Height);

            // Calculate the overlapping area
            int overlapLeft = Math.Max(left1, left2);
            int overlapTop = Math.Max(top1, top2);
            int overlapRight = Math.Min(right1, right2);
            int overlapBottom = Math.Min(bottom1, bottom2);

            // Check for overlapping pixels
            for (int y = overlapTop; y < overlapBottom; y++)
            {
                for (int x = overlapLeft; x < overlapRight; x++)
                {
                    Vector2 mask1Position = new Vector2(x, y) - position1;
                    Vector2 mask2Position = new Vector2(x, y) - position2;

                    int mask1X = (int)mask1Position.X;
                    int mask1Y = (int)mask1Position.Y;
                    int mask2X = (int)mask2Position.X;
                    int mask2Y = (int)mask2Position.Y;

                    if (mask1.IsSolidPixel(mask1X, mask1Y) && mask2.IsSolidPixel(mask2X, mask2Y))
                    {
                        return true; // Collision detected
                    }
                }
            }

            return false; // No collision detected
        }
    }
}