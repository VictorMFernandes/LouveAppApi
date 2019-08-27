using LouveApp.Compartilhado.Util;

namespace LouveApp.Servicos.Email.Enums
{
    public enum EConfigEmailSecao
    {
        [EnumTextos("EmailServico:ApiKey")]
        ApiKey,
        [EnumTextos("EmailServico:EmailRemetente")]
        EmailRemetente,
        [EnumTextos("EmailServico:NomeRemetente")]
        NomeRemetente,
    }
}
