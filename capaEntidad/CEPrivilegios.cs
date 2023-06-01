using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaEntidad
{
    public class CEPrivilegios
    {
        private int _idPrivilegio;
        private string _nombrePrivilegio;

        public int IdPrivilegio { get => _idPrivilegio; set => _idPrivilegio = value; }
        public string NombrePrivilegio { get => _nombrePrivilegio; set => _nombrePrivilegio = value; }
    }

}
