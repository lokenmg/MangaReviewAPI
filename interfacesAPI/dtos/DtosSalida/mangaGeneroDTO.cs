namespace interfacesAPI.dtos.DtosSalida
{
    public class MangaGeneroDto
    {
        public int? GeneroId { get; set; }
        public int? MangaId { get; set; }
        public GeneroDto? Genero { get; set; }
        public MangaByGeneroDto? Manga { get; set; }
    }

    public class GeneroDto
    {
        public int IdGenero { get; set; }
        public string Genero1 { get; set; } = null!;
    }

    public class MangaByGeneroDto
    {
        public int IdManga { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
