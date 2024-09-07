using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiProfissional.Domain.ViewModels
{
    public class UserAuthorizedViewModel(int Id, string Login, bool IsAdmin)
    {
        public int Id { get; set; } = Id;

        public string Login { get; set; } = Login;

        public bool IsAdmin { get; set; } = IsAdmin;
    }
}
