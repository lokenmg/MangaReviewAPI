public class ReseniaConUsuarioDto
{
    public int IdResenia { get; set; }
    public string Resenia { get; set; }
    public int Likes { get; set; }
    public int Dislike { get; set; }
    public int UsuarioId { get; set; }
    public string UsuarioEmail { get; set; }
    public string UsuarioImg { get; set; }
}