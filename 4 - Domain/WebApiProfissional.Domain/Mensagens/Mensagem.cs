namespace WebApiProfissional.Domain.Mensagens
{
    public static class Mensagem
    {
        public static string CampoObrigatorio(string dado)
        {
            return string.Format(Resources.Erros.CampoObrigatorio, dado);
        }

        public static string EmailValido()
        {
            return string.Format(Resources.Erros.EmailValido);
        }

        public static string MinimoCaracteres(string dado, string minimo)
        {
            return string.Format(Resources.Erros.MinimoCaracteres, dado, minimo);
        }

        public static string TamanhoCampo(string dado, int minimo, int maximo)
        {
            return string.Format(Resources.Erros.TamanhoCampo, dado, minimo, maximo);
        }
    }
}
