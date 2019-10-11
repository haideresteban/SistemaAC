using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaAC.Data;
using SistemaAC.Models;

namespace SistemaAC.ModelsClass
{
    public class InscripcionesModels
    {
        private ApplicationDbContext context;

        public InscripcionesModels(ApplicationDbContext context)
        {
            this.context = context;
        }

        public string filtrarEstudiantes(string valor)
        {
            string dataFilter = "";
            if (valor != "null")
            {
                var estudiantes = context.Estudiante.OrderBy(p => p.Nombres).ToList();
                //para filtrar por identificacion, nombres, apellidos
                var query = estudiantes.Where(p => p.Documento.StartsWith(valor) || p.Nombres.StartsWith(valor) || p.Apellidos.StartsWith(valor));
                foreach (var item in query)
                {
                    dataFilter += "<tr>" +
                        "<td>" + "<input type='checkbox' name='cboxEstudiante[]' id='cboxEstudiante' value='" + item.ID + "'>" + "</td>" +
                        "<td>" + item.Apellidos + " " + item.Nombres + "</td>" +
                        "<td>" + item.Documento + "</td>" +
                        "<td>" + item.Email + "</td>" +
                        "<td>" + item.Telefono + "</td>" +
                   "</tr>";
                }
            }
            return dataFilter;
        }
        public string filtrarCurso(string valor)
        {
            string dataFilter = "";
            if (valor != "null")
            {
                var curso = context.Curso.OrderBy(p => p.Nombre).ToList();
                //para filtrar por identificacion, nombres, apellidos
                var query = curso.Where(p => p.Nombre.StartsWith(valor));
                foreach (var item in query)
                {
                    dataFilter += "<tr>" +
                        "<td>" + "<input type='checkbox' name='cboxCurso[]' id='cboxCurso' value='" + item.CursoID + "'>" + "</td>" +
                        "<td>" + item.Nombre + "</td>" +
                        "<td>" + getCategorias(item.CategoriaID) + "</td>" +
                        "<td>" + item.Creditos + "</td>" +
                        "<td>" + item.Horas + "</td>" +
                        "<td>" + item.Costo + "</td>" +
                   "</tr>";
                }
            }
            return dataFilter;
        }
        public String getCategorias(int id)
        {

            var data = context.Categoria.Where(c => c.CategoriaID == id).ToList();
            return data[0].Nombre;

        }
        public List<Estudiante> getEstudiante(int id)
        {

            return context.Estudiante.Where(c => c.ID == id).ToList();

        }

    }
}
