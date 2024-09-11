using System;
using System.Collections.Generic;

namespace interfacesAPI.Models;

public partial class Manga
{
    public int IdManga { get; set; }

    public string Nombre { get; set; } = null!;

    public string Portada { get; set; } = null!;

    public string Tomo { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string AñoSalida { get; set; } = null!;

    public string Sinopsis { get; set; } = null!;

    public int? Likes { get; set; }

    public int? Dislikes { get; set; }

    public virtual ICollection<Calificacione> Calificaciones { get; set; } = new List<Calificacione>();

    public virtual ICollection<MangaGenero> MangaGeneros { get; set; } = new List<MangaGenero>();

    public virtual ICollection<Resenium> Resenia { get; set; } = new List<Resenium>();
}
