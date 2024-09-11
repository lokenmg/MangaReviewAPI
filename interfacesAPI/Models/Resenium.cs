using System;
using System.Collections.Generic;

namespace interfacesAPI.Models;

public partial class Resenium
{
    public int IdResenia { get; set; }

    public string? Reseña { get; set; }

    public int? Likes { get; set; }

    public int? Dislike { get; set; }

    public int? Usuario { get; set; }

    public int? Manga { get; set; }

    public virtual Manga? MangaNavigation { get; set; }

    public virtual Usuario? UsuarioNavigation { get; set; }
}
