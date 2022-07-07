namespace Shapes
{
    public class Rectangle : IDrawable
    {
        private int width;
        private int height;

        public Rectangle(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void Draw()
        {
            DrawLine(this.width, '*', '*');

            for (int i = 1; i < this.height; ++i)
            {
                DrawLine(this.width, '*', ' ');
            }

            DrawLine(this.width, '*', '*');
        }

        private void DrawLine(int width, char end, char mid)
        {
            System.Console.Write(end);

            for (int i = 1; i < width; ++i)
            {
                System.Console.Write(mid);
            }
            System.Console.WriteLine(end);
        }
    }
}
