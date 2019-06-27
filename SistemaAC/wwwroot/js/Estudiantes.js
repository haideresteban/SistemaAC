var idEstudiante = 0;
class Estudiantes {
    constructor() {

    }

    /*
     ...data representa un arreglo, captura todos los parametros que le pasemos, no importa cuantos sean 
     */
    guardarEstudiante(id, funcion, ...data) {
        var action = data[0], codigo = data[1], nombre = data[2], apellido = data[3];
        var fecha = data[4], documento = data[5], email = data[6], telefono = data[7];
        var direccion = data[8], estado = data[9];
        //  alert(action);
        if (codigo === "") {
            document.getElementById("Codigo").focus();
        } else {
            if (nombre === "") {
                document.getElementById("Nombre").focus();
            } else {
                if (apellido === "") {
                    document.getElementById("Apellidos").focus();
                } else {
                    if (fecha === "") {
                        document.getElementById("FechaNacimiento").focus();
                    } else {
                        if (documento === "") {
                            document.getElementById("Documento").focus();
                        } else {
                            if (email === "") {
                                document.getElementById("Email").focus();
                            } else {
                                if (telefono === "") {
                                    document.getElementById("Telefono").focus();
                                } else {
                                    if (direccion === "") {
                                        document.getElementById("Direccion").focus();
                                    } else {
                                        $.post(
                                            action,
                                            { id, codigo, nombre, apellido, fecha, documento, email, telefono, direccion, estado, funcion },
                                            (response) => {


                                            }
                                        );
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

    }
    filtarEstudiantes(numPagina, valor, order, action) {
        /*
         operador terniario que nos permite validar si valor= vacio entonces valor =null sino valor
         */
        valor = (valor === "") ? "null" : valor;
        $.post(
            action,
            { valor, numPagina, order },
            (response) => {
                $("#resultSearch").html(response[0]);
                $("#paginado").html(response[1]);
            });

    }

    getEstudiante(id, funcion, action) {
        // alert(id + " " + funcion + " " + action);
        $.post(
            action,
            { id }, (response) => {
                console.log(response);
                if (funcion === 1) {
                    idEstudiante = response[0].id;
                    document.getElementById("Codigo").value = response[0].codigo;
                    document.getElementById("Nombre").value = response[0].nombres;
                    document.getElementById("Apellidos").value = response[0].apellidos;
                    document.getElementById("FechaNacimiento").value = response[0].fecha;
                    document.getElementById("Documento").value = response[0].documento;
                    document.getElementById("Email").value = response[0].email;
                    document.getElementById("Telefono").value = response[0].telefono;
                    document.getElementById("Direccion").value = response[0].direccion;
                    document.getElementById("Estado").checked = response[0].estado;
                }
                var action = 'Estudinates/guardarEstudiante';
                this.editarEstudiante(response, funcion, action)
            });
    }

    editarEstudiante(response, funcion, action) {
        $post(
            action,
            { response, funcion },
            (response) => {
                console.log(response);
            });
    }
}
