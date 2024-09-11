using System;
using System.Collections.Generic;

namespace interfacesAPI.Models;

public partial class Genero
{
    public int IdGenero { get; set; }

    public string Genero1 { get; set; } = null!;

    public virtual ICollection<MangaGenero> MangaGeneros { get; set; } = new List<MangaGenero>();
}
