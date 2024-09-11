namespace interfacesAPI.dtos.DtosEntrada
{
    public class NuevoManga
    {
        public string Nombre { get; set; } = null!;

        public string Portada { get; set; } = null!;

        public string Tomo { get; set; } = null!;

        public string Estado { get; set; } = null!;

        public string AnioSalida { get; set; } = null!;

        public string Sinopsis { get; set; } = null!;

        public int? Likes { get; set; }

        public int? Dislikes { get; set; }
    }
}
