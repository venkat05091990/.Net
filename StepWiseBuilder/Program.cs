using System;
using System.Runtime.InteropServices;

namespace DesignPatterns
{
    public enum CarType
    {
        Sedan,
        Crossover,
        Sports
    }

    public enum WheelType
    {
        AllWeathered,
        Wet
    }

    public class Car
    {
        public Car() { }

        public CarType _ctype;
        public WheelType _wtype;

        public override string? ToString()
        {
            return $"CarType - {_ctype} WheelType - {_wtype}";
        }
    }

    public interface ICarTypeBuilder
    {
        public IWheelTypeBuilder SetCarType(CarType type);
    }

    public interface IWheelTypeBuilder
    {
        public ICarBuilder SetWheelType(WheelType type);
    }

    public interface ICarBuilder
    {
        public Car Build();
    }


    public class CarBuilder
    {
        private class Impl : ICarTypeBuilder, IWheelTypeBuilder, ICarBuilder
        {
            private readonly Car _car = new();

            public IWheelTypeBuilder SetCarType(CarType type)
            {
                _car._ctype = type;
                return this;
            }

            public ICarBuilder SetWheelType(WheelType type)
            {
                _car._wtype = type;
                return this;
            }

            public Car Build()
            {
                return _car;
            }
        }

        public static ICarTypeBuilder Create()
        {
            return new Impl();
        }
    }

    class Demo
    {
        static void Main()
        {
            Car car = CarBuilder.Create()
                                .SetCarType(CarType.Sedan)
                                .SetWheelType(WheelType.Wet)
                                .Build();
            Console.WriteLine(car);
        }
    }

}