using Microsoft.AspNetCore.Identity;
using SistemaAC.Data;
using SistemaAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAC.ModelsClass
{
    public class EstudiantesModels
    {
        private ApplicationDbContext context;
        private List<IdentityError> identityError;
        private string code = "", des = "";
        public EstudiantesModels(ApplicationDbContext context)
        {
            this.context = context;
            identityError = new List<IdentityError>();

        }
        public List<IdentityError> guardarEstudiante(int id, string codigo, string nombre, string apellido, DateTime fecha, string documento, string email, string telefono, string direccion, Boolean estado, int funcion)
        {
            var estudiante = new Estudiante
            {
                ID = id,
                Codigo = codigo,
                Apellidos = apellido,
                Nombres = nombre,
                FechaNacimiento = fecha,
                Documento = documento,
                Email = email,
                Telefono = telefono,
                Direccion = direccion,
                Estado = estado
            };
            try
            {
                context.Update(estudiante);
                context.SaveChanges();
                code = "1";
                des = "Save";
            }
            catch (Exception ex)
            {
                code = "0";
                des = ex.Message;
            }
            identityError.Add(new IdentityError
            {
                Code = code,
                Description = des
            });
            return identityError;

        }

        public List<object[]> filtrarEstudiantes(int numPagina, string valor, string order)
        {
            int cant, numRegistros = 0, inicio = 0, reg_por_pagina = 6;
            int can_paginas, pagina = 0, count = 1;
            string dataFilter = "", paginador = "", Estado = null;
            List<object[]> data = new List<object[]>();
            IEnumerable<Estudiante> query;
            List<Estudiante> estudiantes = null;

            // solo va a validar si viene nombre en el parametro si se quiere que valide por todos se debe agregar en el index y || en la funcion landa
            estudiantes = context.Estudiante.OrderBy(p => p.Nombres).ToList();
            numRegistros = estudiantes.Count;
            inicio = (numPagina - 1) * reg_por_pagina;
            can_paginas = (numRegistros / reg_por_pagina);
            if (valor == "null")
                query = estudiantes.Skip(inicio).Take(reg_por_pagina);
            else
                query = estudiantes.Where(p => p.Documento.StartsWith(valor) || p.Nombres.StartsWith(valor) || p.Apellidos.StartsWith(valor)).Skip(inicio).Take(reg_por_pagina);
            cant = query.Count();
            foreach (var item in query)
            {
                if (item.Estado == true)
                    Estado = "<a onclick='editarEstudiante(" + item.ID + ',' + 0 + ")' class='btn btn-success'>Activo</a>";
                else
                    Estado = "<a onclick='editarEstudiante(" + item.ID + ',' + 0 + ")' class='btn btn-danger'>No activo</a>";

                dataFilter += "<tr>" +
                   "<td>" + item.Codigo + "</td>" +
                   "<td>" + item.Documento + "</td>" +
                   "<td>" + item.Nombres + "</td>" +
                   "<td>" + item.Apellidos + "</td>" +
                   "<td>" + item.FechaNacimiento + "</td>" +
                   "<td>" + item.Telefono + "</td>" +
                   "<td>" + item.Email + "</td>" +
                   "<td>" + item.Direccion + "</td>" +
                   "<td>" + Estado + " </td>" +
                   "<td>" +
                   "<a data-toggle='modal' data-target='#modalAS' onclick='editarEstudiante(" + item.ID + ',' + 1 + ")'  class='btn btn-success'>Edit</a>" +
                   "</td>" +
                   "<td>" +
                   "<a data-toggle='modal' data-target='#modalCS' onclick='editarEstudiante(" + item.ID + ',' + 1 + ")'  class='btn btn-danger'>Delete</a>" +
                   "</td>" +
               "</tr>";
            }
            object[] dataObj = { dataFilter, paginador };
            data.Add(dataObj);
            return data;
        }

    }
}
