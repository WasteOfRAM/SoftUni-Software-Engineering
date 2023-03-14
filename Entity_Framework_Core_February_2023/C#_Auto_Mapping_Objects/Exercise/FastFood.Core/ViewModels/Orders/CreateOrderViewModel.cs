namespace FastFood.Core.ViewModels.Orders;

using System.Collections.Generic;

public class CreateOrderViewModel
{
    public CreateOrderViewModel()
    {
        this.Items = new List<KeyValuePair<int, string>>();
        this.Employees = new List<KeyValuePair<int, string>>();
    }

    public IEnumerable<KeyValuePair<int,string>> Items { get; set; }

    public IEnumerable<KeyValuePair<int, string>> Employees { get; set; }
}
