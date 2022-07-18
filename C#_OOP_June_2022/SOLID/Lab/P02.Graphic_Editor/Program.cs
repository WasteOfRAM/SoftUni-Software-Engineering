using System;

namespace P02.Graphic_Editor
{
    class Program
    {
        static void Main()
        {
            var square = new Square();
            var rectangle = new Rectangle();
            var circle = new Circle();

            var editor = new GraphicEditor();

            editor.DrawShape(square);
            editor.DrawShape(rectangle);
            editor.DrawShape(circle);
        }
    }
}
