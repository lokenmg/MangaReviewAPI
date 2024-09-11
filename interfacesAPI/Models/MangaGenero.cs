using System;
using System.Collections.Generic;

namespace interfacesAPI.Models;

public partial class MangaGenero
{
    public int IdMangagenero { get; set; }

    public int? Genero { get; set; }

    public int? Manga { get; set; }

    public virtual Genero? GeneroNavigation { get; set; }

    public virtual Manga? MangaNavigation { get; set; }
}
