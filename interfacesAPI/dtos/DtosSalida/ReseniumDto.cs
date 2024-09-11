namespace interfacesAPI.NewFolder.dtoSalida
{
    public class ReseniumDto
    {
        public int IdResenia { get; set; }
        public string? Reseña { get; set; }
        public int? Likes { get; set; }
        public int? Dislike { get; set; }
        public int? UsuarioId { get; set; }
        public int? MangaId { get; set; }
        public MangaDto? Manga { get; set; }
        public UsuarioDto? Usuario { get; set; }
    }

    public class UsuarioDto
    {
        public int IdUsuarios { get; set; }
        public string Email { get; set; } = null!;
        public string Img { get; set; } = null!;
    }

    public class MangaDto
    {
        public int IdManga { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
