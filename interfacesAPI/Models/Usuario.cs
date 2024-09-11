using System;
using System.Collections.Generic;

namespace interfacesAPI.Models;

public partial class Usuario
{
    public int IdUsuarios { get; set; }

    public string Email { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public string Img { get; set; } = null!;

    public virtual ICollection<Resenium> Resenia { get; set; } = new List<Resenium>();
}
