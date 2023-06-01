namespace capaEntidad
{
    public class CEUsuarios
    {
        private int _idUsuarios;
        private string _nombres;
        private string _apellidoP;
        private string _apellidoM;
        private int _dni;
        private string _email;
        private int _telefono;
        private int _idPrivilegio;
        private byte[] _img;
        private string _usuario;
        private string _contrasenia;
        private string _patron;

        public int IdUsuarios { get => _idUsuarios; set => _idUsuarios = value; }
        public string Nombres { get => _nombres; set => _nombres = value; }
        public string ApellidoP { get => _apellidoP; set => _apellidoP = value; }
        public string ApellidoM { get => _apellidoM; set =>_apellidoM = value; }
        public int Dni { get => _dni; set => _dni = value; }
        public string Email { get => _email; set => _email = value; }
        public int Telefono { get => _telefono; set => _telefono = value; }
        public int IdPrivilegio { get => _idPrivilegio; set => _idPrivilegio = value; }
        public byte[] Img { get => _img; set => _img = value; }
        public string Usuario { get => _usuario; set => _usuario = value; }
        public string Contrasenia { get => _contrasenia; set => _contrasenia = value; }
        public string Patron { get => _patron; set => _patron = value; }
    }
}