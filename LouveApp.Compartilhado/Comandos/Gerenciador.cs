using FluentValidator;

namespace LouveApp.Compartilhado.Comandos
{
    public abstract class Gerenciador : Notifiable
    {
        public bool ValidarComando(IComando comando)
        {
            if (!comando.Validar())
            {
                AddNotifications(comando.PegarNotificacoes());
                return false;
            }

            return true;
        }
    }
}
