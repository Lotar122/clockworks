namespace clockworks.Classes
{
    public class Mask
    {
        private bool[,] maskData;
        private int width;
        private int height;

        public Mask(Image<Rgba32> image)
        {
            using (Image<Rgba32> maskImage = image)
            {
                width = maskImage.Width;
                height = maskImage.Height;

                maskData = new bool[width, height];

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Rgba32 pixelColor = maskImage[x, y];
                        maskData[x, y] = pixelColor.A > 0; // Treat non-transparent pixels as solid
                    }
                }
            }
        }

        public void LoadMask(Image<Rgba32> image)
        {
            using (Image<Rgba32> maskImage = image)
            {
                width = maskImage.Width;
                height = maskImage.Height;

                maskData = new bool[width, height];

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Rgba32 pixelColor = maskImage[x, y];
                        maskData[x, y] = pixelColor.A > 0; // Treat non-transparent pixels as solid
                    }
                }
            }
        }

        public bool IsSolidPixel(int x, int y)
        {
            if (x < 0 || x >= width || y < 0 || y >= height)
            {
                return false; // Out of bounds, assume no collision
            }

            return maskData[x, y];
        }

        public int Width => width;
        public int Height => height;
    }
}