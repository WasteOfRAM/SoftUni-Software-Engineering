namespace Cars
{
    public interface IElectricCar
    {
        int Batteries { get; }
    }
}

// Can be made to extend ICar so tesla can inherit only IElectricCar
//public interface IElectricCar : ICar
//{
//    int Batteries { get; }
//}
