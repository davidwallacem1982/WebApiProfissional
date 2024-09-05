namespace WebApiProfissional.Domain.ViewModels.DropDown
{
    /// <summary>
    /// Representa um modelo de visão para exibição de dados em um dropdown, contendo um identificador e uma descrição.
    /// </summary>
    /// <remarks>
    /// Esta classe é utilizada para encapsular as informações que serão exibidas em uma lista suspensa (dropdown). 
    /// Ela inclui um identificador de tipo short e uma descrição associada.
    /// </remarks>
    /// <remarks>
    /// Inicializa uma nova instância da classe <see cref="DropIntViewModel"/> com o identificador e a descrição fornecidos.
    /// </remarks>
    /// <param name="id">O identificador associado ao item do dropdown.</param>
    /// <param name="descricao">A descrição associada ao item do dropdown.</param>
    public class DropShortViewModel(short id, string descricao)
    {
        /// <summary>
        /// Obtém ou define o identificador associado ao item do dropdown.
        /// </summary>
        public short Id { get; set; } = id;

        /// <summary>
        /// Obtém ou define a descrição associada ao item do dropdown.
        /// </summary>
        public string Descricao { get; set; } = descricao;
    }
}
