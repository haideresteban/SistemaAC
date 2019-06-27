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
        private Boolean estados;
        public EstudiantesModels(ApplicationDbContext context)
        {
            this.context = context;
            identityError = new List<IdentityError>();

        }
        public List<Estudiante> getEstudiante(int id)
        {
            return context.Estudiante.Where(c => c.ID == id).ToList();

        }
        public List<IdentityError> guardarEstudiante(List<Estudiante> response, int funcion)
        {
            switch (funcion)
            {
                case 0:
                    if (response[0].Estado)
                    {
                        estados = false;
                    }
                    else
                    {
                        estados = true;
                    }
                    break;
                case 1:
                    //para registrar o editar un estduiante
                    estados = response[0].Estado;                
                    break;
            }

            var estudiante = new Estudiante
            {
                ID = response[0].ID,
                Codigo =response[0].Codigo,
                Apellidos = response[0].Apellidos,
                Nombres = response[0].Nombres,
                FechaNacimiento = response[0].FechaNacimiento,
                Documento = response[0].Documento,
                Email = response[0].Email,
                Telefono = response[0].Telefono,
                Direccion = response[0].Direccion,
                Estado = estados
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
