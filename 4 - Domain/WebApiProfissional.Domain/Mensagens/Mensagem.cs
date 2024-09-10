namespace WebApiProfissional.Domain.Mensagens
{
    public static class Mensagem
    {
        public static string CampoObrigatorio(string dado)
        {
            return string.Format(Resources.Erros.CampoObrigatorio, dado);
        }
    }
}
