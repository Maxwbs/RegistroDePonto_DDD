using Domain.Interfaces.Generics;
using Domain.Interfaces.IRegistroDePonto;
using Entities.Entidades;
using Infra.Repositorio.Genericos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repositorio
{
    public class RepositorioRegistroDePonto : RepositorioGenerico<RegistroDePonto>, InterfaceRegistroDePonto
    {
    }
}
