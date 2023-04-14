using System.Text.Json;
using System.Text.Json.Serialization;


namespace DesignPatters
{
    public class Product
    {
        private readonly int _id;
        private readonly Size _size;
        private readonly Color _color;
        private readonly int _age;
        private readonly Gender _gender;
        private readonly decimal _price;

        public Product(int id, Size size, Color color, int age, Gender gender, decimal price)
        {
            _id = id;
            _size = size;
            _color = color;
            _age = age;
            _gender = gender;
            _price = price;
        }

        public int Id
        {
            get { return _id; }
        }

        public Size Size
        {
            get { return _size; }
        }

        public Color Color
        {
            get { return _color; }
        }

        public int Age
        {
            get { return _age; }
        }

        public Gender Gender
        {
            get { return _gender; }
        }

        public decimal Price
        {
            get { return _price; }
        }

        public override string ToString()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Converters ={new JsonStringEnumConverter( JsonNamingPolicy.CamelCase)},
            };
            return JsonSerializer.Serialize(this, options);
        }
    }

    public enum Gender
    {
        F, M, O
    }
    public enum Color
    {
        Violet, Indigo, Blue, Green, Yellow, Orange, Red
    }

    public enum Size
    {
        XS, S, M, L, XL, XXL
    }

    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class ColorSpecification : ISpecification<Product>
    {
        private readonly Color _color;

        public ColorSpecification(Color color)
        {
            this._color = color;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Color == _color;
        }
    }
    public class SizeSpecification : ISpecification<Product>
    {
        private readonly Size _size;

        public SizeSpecification(Size size)
        {
            this._size = size;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Size == _size;
        }
    }

    public class GenderSpecification : ISpecification<Product>
    {
        private readonly Gender _gender;

        public GenderSpecification(Gender gender)
        {
            this._gender = gender;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Gender == _gender;
        }
    }

    public class AgeSpecification : ISpecification<Product>
    {
        private readonly int _age;

        public AgeSpecification(int age)
        {
            this._age = age;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Age == _age;
        }
    }

    public class PriceSpecification : ISpecification<Product>
    {
        private readonly decimal _startPrice, _endPrice;

        public PriceSpecification(decimal startPrice, decimal endPrice)
        {
            this._startPrice = startPrice;
            this._endPrice = endPrice;
        }
        public bool IsSatisfied(Product t)
        {
            return _startPrice <= t.Price && t.Price <= _endPrice;
        }
    }

    public class AddSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T>[] specifications;

        public AddSpecification(params ISpecification<T>[] specifications)
        {

            this.specifications = specifications;
        }

        public bool IsSatisfied(T t)
        {
            bool satisfied = specifications[0].IsSatisfied(t);

            foreach (var specification in specifications)
            {
                satisfied = satisfied && specification.IsSatisfied(t);

                if (!satisfied)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class ProductFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (Product p in items)
            {
                if (spec.IsSatisfied(p))
                {
                    yield return p;
                }
            }
        }
    }

    public class Demo
    {
        public static void Main(string[] args)
        {
            var p1 = new Product(1, Size.S, Color.Red, 10, Gender.M, 100m);
            var p2 = new Product(2, Size.L, Color.Red, 20, Gender.F, 100.67m);
            var p3 = new Product(3, Size.S, Color.Red, 10, Gender.M, 300m);
            var p4 = new Product(4, Size.XL, Color.Red, 34, Gender.F, 500.8905m);
            var p5 = new Product(5, Size.M, Color.Green, 1, Gender.M, 4555m);
            var p6 = new Product(6, Size.L, Color.Green, 20, Gender.F, 5654.8964m);
            var p7 = new Product(7, Size.XL, Color.Green, 35, Gender.M, 179m);
            var p8 = new Product(8, Size.L, Color.Blue, 40, Gender.O, 3423m);
            var p9 = new Product(9, Size.S, Color.Red, 10, Gender.F, 323.30m);

            Product[] products = { p1, p2, p3, p4, p5, p6, p7, p8, p9 };

            ProductFilter productfilter = new ProductFilter();

            Console.WriteLine($"Red Small Female Products");
            ColorSpecification colorSpecification = new ColorSpecification(Color.Red);
            SizeSpecification sizeSpecification = new SizeSpecification(Size.S);
            GenderSpecification  genderSpecification = new GenderSpecification(Gender.F);
            AddSpecification<Product> colorSizeSpecification = new(colorSpecification, sizeSpecification, genderSpecification);
            foreach (Product p in productfilter.Filter(products, colorSizeSpecification))
            {
                Console.WriteLine(p);
            }
        }
    }
}