using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.Tests
{
    public class TestPerson : TestDbModel<TestPerson>
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        [UI.UIHidden]
        public int Id { get; set; }

        [DatabaseColumn]
        [UI.UISingleline("Primeiro Nome", "Escreva o nome")]
        public string FirstName { get; set; }

        [DatabaseColumn]
        [UI.UISingleline]
        public string LastName { get; set; }

        [UI.UICheckbox]
        public bool IsActive { get; set; }
    }
}
