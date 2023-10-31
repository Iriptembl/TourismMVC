using System;
using System.Collections.Generic;

namespace TourismMVC.Models;

public partial class Tourist
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public int CountryId { get; set; }

    public virtual Country Country { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
