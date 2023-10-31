using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourismMVC.Models;

public partial class Visit
{
    public int Id { get; set; }

    public int PlaceId { get; set; }

    public int TouristId { get; set; }

    [DataType(DataType.Date)]
    public DateTime VisitDate { get; set; }

    public virtual Place Place { get; set; } = null!;

    public virtual Tourist Tourist { get; set; } = null!;
}
