namespace Domain.DT
{
    public class DTUsuario
    {
        public string? id { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public string? email { get; set; }
        public string? username { get; set; }
        public DateTime fechaHora { get; set; }
        public bool registro_Activo { get; set; }
        public string[] roles { get; set; } = { };
    }
}
