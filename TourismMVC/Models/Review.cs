using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourismMVC.Models;

public partial class Review
{
    public int Id { get; set; }

    public int PlaceId { get; set; }

    public int TouristId { get; set; }

    [Range(1, 10)]
    public int Rating { get; set; }

    public string Title { get; set; }

    public string Text { get; set; }

    public virtual Place Place { get; set; }

    public virtual Tourist Tourist { get; set; }
}
