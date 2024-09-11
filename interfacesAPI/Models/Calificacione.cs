using System;
using System.Collections.Generic;

namespace interfacesAPI.Models;

public partial class Calificacione
{
    public int IdCalificacion { get; set; }

    public int Calificacion { get; set; }

    public int? Manga { get; set; }

    public virtual Manga? MangaNavigation { get; set; }
}
