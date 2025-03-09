using ComercioCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComercioCore.Application.Interfaces.Services
{
    public interface IMunicipioService
    {
        Task<IEnumerable<Municipio>?> ObtenerTodos();
    }
}
