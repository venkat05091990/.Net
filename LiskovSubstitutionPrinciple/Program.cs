using System;

namespace DesignPatterns
{
    class Rectangle
    {
        private int _width;

        public virtual int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        private int _height;

        public virtual int Height
        {
            get { return _height; }
            set { _height = value; }
        }
 
        public Rectangle() { }

        public Rectangle(int width, int height)
        {
            this._width = width;
            this._height = height;
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    class Square : Rectangle
    {
        public override int Height { set { base.Width = base.Height = value; } }
        public override int Width { set { base.Width = base.Height = value; } }
    }
    class Demo
    {
        static public int Area(Rectangle rectangle) => rectangle.Height * rectangle.Width; 
        static void Main(string[] args)
        {
            Rectangle r = new (2, 5);
            Console.WriteLine($"Rectangle {r} has the area {Area(r)}");

            Rectangle sq = new Square();
            sq.Width = 5;
            Console.WriteLine($"Square {sq} has the area {Area(r)}");
        }
    }

}