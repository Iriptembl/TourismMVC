using System;
using System.Collections.Generic;

namespace TourismMVC.Models;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Tourist> Tourists { get; set; } = new List<Tourist>();
}
