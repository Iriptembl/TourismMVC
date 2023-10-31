using System;
using System.Collections.Generic;

namespace TourismMVC.Models;

public partial class Place
{
    public int Id { get; set; }

    public int TypeId { get; set; }

    public string Name { get; set; }

    public string OpenTime { get; set; }

    public string CloseTime { get; set; }

    public int TicketPrice { get; set; }

    public string Location { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Type Type { get; set; }

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
