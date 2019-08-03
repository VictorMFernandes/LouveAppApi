namespace LouveApp.Dominio.Entidades.Juncao
{
    public class EntidadeJuncaoComUsuario
    {
        public string UsuarioId { get; protected set; }
        private Usuario _usuario;
        public Usuario Usuario
        {
            get => _usuario;
            protected set
            {
                if (value != null) UsuarioId = value.Id;
                _usuario = value;
            }
        }
    }
}
