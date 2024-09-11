namespace interfacesAPI.dtos.DtosSalida
{
    public class MangasLDto
    {
        public int IdManga { get; set; }
        public string Nombre { get; set; }
        public string Portada { get; set; }
        public string Tomo { get; set; }
        public string Estado { get; set; }
        public string AnioSalida { get; set; }
        public string Sinopsis { get; set; }
        public int? Likes { get; set; }
        public int? Dislikes { get; set; }
    }
}
