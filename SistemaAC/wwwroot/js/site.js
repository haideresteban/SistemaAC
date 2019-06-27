$('#modalEditar').on('shown.bs.modal', function () {
    $('#myInput').focus();
});
$('#ModalAC').on('shown.bs.modal', function () {
    $('#Nombre').focus();
});

function getUsuario(id, action) {
    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (response) {
            mostrarUsuario(response);
        }

    });
}
var items;
var j = 0;
// variables por propiedad del usuario
var id;
var userName;
var email;
var phoneNumber;
var rol;
var selectRole;
// almacenamiento  de los  datos del registro, pero estos datos no seran modificados
var accessFailedCount;
var concurrencyStamp;
var emailConfirmed;
var lockoutEnabled;
var lockoutEnd;
var normalizedUserName;
var normalizedEmail;
var passwordHash;
var phoneNumberConfirmed;
var securityStamp;
var twoFactorEnabled;

function mostrarUsuario(response) {
    items = response;
    j = 0;
    for (var i = 0; i < 3; i++) {
        var x = document.getElementById('Select');
        x.remove(i);
    }
    $.each(items, function (index, val) {
        $('input[name=Id]').val(val.id);
        $('input[name=UserName]').val(val.userName);
        $('input[name=Email]').val(val.email);
        $('input[name=PhoneNumber]').val(val.phoneNumber);
        document.getElementById('Select').options[0] = new Option(val.role, val.roleId);

        //mostrar detalles del usuario
        $("#dEmail").text(val.email);
        $("#dUserName").text(val.userName);
        $("#dPhoneNumber").text(val.phoneNumber);
        $("#dRole").text(val.role);

        //mostrar detalles del usuario a eliminar
        $("#eUsuario").text(val.email);
        $("#EIdUsario").val(val.id);

    });
}
function getRoles(action) {
    $.ajax({
        type: "POST",
        url: action,
        data: {},
        success: function (response) {
            if (j === 0) {
                for (var i = 0; i < response.length; i++) {
                    document.getElementById('Select').options[i] = new Option(response[i].text, response[i].value);
                    document.getElementById('SelectNuevo').options[i] = new Option(response[i].text, response[i].value);

                }
                j = 1;
            }
        }
    });
}
function editarUsuario(action) {
    id = $('input[name=Id]')[0].value;
    email = $('input[name=Email]')[0].value;
    phoneNumber = $('input[name=PhoneNumber]')[0].value;
    role = document.getElementById('Select');
    selectRole = role.options[role.selectedIndex].text;

    $.each(items, function (index, val) {
        accessFailedCount = val.accessFailedCount;
        concurrencyStamp = val.concurrencyStamp;
        emailConfirmed = val.emailConfirmed;
        lockoutEnabled = val.lockoutEnabled;
        lockoutEnd = val.lockoutEnd;
        userName = val.userName;
        normalizedUserName = val.normalizedUserName;
        normalizedEmail = val.normalizedEmail;
        passwordHash = val.passwordHash;
        phoneNumberConfirmed = val.phoneNumberConfirmed;
        securityStamp = val.securityStamp;
        twoFactorEnabled = val.twoFactorEnabled;
    });
    $.ajax({
        type: "POST",
        url: action,
        data: {
            id, userName, email, phoneNumber, accessFailedCount,
            concurrencyStamp, emailConfirmed, lockoutEnabled, lockoutEnd,
            normalizedEmail, normalizedUserName, passwordHash, phoneNumberConfirmed,
            securityStamp, twoFactorEnabled, selectRole
        },
        success: function (response) {
            if (response === "Save") {
                window.location.href = "usuarios";
            } else {
                alert("No se pueden editar los datos del usuario.");
            }
        }

    });

}
function OcultarDetalleUsuario() {
    $("#modalDetalle").modal("hide");
}
function eliminarUsuario(action) {
    var id = $('input[name=EIdUsario]')[0].value;

    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (response) {
            if (response === "delete") {
                window.location.href = "Usuarios";
            } else {
                alert("No se puede eliminar el registro");
            }
        }
    });
}
function crearUsuario(action) {
    //Datos ingresados
    email = $('input[name=EmailNuevo]')[0].value;
    phoneNumber = $('input[name=PhoneNumberNuevo]')[0].value;
    passwordHash = $('input[name=PasswordHashNuevo]')[0].value;
    role = document.getElementById('SelectNuevo');
    selectRole = role.options[role.selectedIndex].text;
    //validar campos vacios
    if (email === "") {
        $('#EmailNuevo').focus();
        alert("Ingrese el email del usuario");
    } else {
        if (passwordHash === "") {
            $('#PasswordHashNuevo').focus();
            alert("Ingrese el password del usuario");
        } else {
            $.ajax({
                type: "POST",
                url: action,
                data: { email, phoneNumber, passwordHash, selectRole },
                success: function (response) {
                    if (response === "save") {
                        window.location.href = "Usuarios";
                    } else {
                        $('#mensajenuevo').html("No se puede agregar el usuario. </br> selkeciione un rol <br> Ingrese un Email correcto </br> el password debe tener de 6-100 caracteres, almenos una letra mayuscula, un caracter especial,una letra mayuscula y un numero ");
                    }
                }
            });
        }
    }
}
//accion pára que se ejecute  cada vez que se abre index
$().ready(() => {
    var URLactual = window.location;// nos da el dato de la url (nombre de los controladores)
    document.getElementById("filtrar").focus();
    switch (URLactual.pathname) {
        case "/Categorias":
            filtrarDatos(1, "nombre");
            break;
        case "/Cursos":
            getCategorias(0, 0);

            filtrarCurso(1, "nombre");
            break;
        case "/Estudiantes":
            filtarEstudiantes(1, "nombre");
            break;
    }

});
//para seleccionar el campo nombre cuando se emplea la modal 
$('#modalCS').on('shown.bs.modal', () => {
    $('#Nombre').focus();
});
$('#modalAS').on('shown.bs.modal', () => {
    $('#Codigo').focus();
});
/** 
 CODIGO DE CATEGORIAS
 */
var idCategoria, funcion = 0, idCurso;

var agregarCategoria = () => {
    var nombre = document.getElementById("Nombre").value;
    var descripcion = document.getElementById("Descripcion").value;
    var estados = document.getElementById('Estado');// trae todos los estados
    var estado = estados.options[estados.selectedIndex].value;// selecciona el que este seleccionado en la vista
    var action;
    if (funcion === 0) {
        action = 'Categorias/guardarCategoria';
    } else {
        action = 'Categorias/editarCategoria';
    }
    var categoria = new Categoria(nombre, descripcion, estado, action);
    categoria.agregarCategoria(idCategoria, funcion);
    funcion = 0;

};
var filtrarDatos = (numPagina, order) => {
    var valor = document.getElementById('filtrar').value;
    var action = 'Categorias/filtrarDatos';
    var categoria = new Categoria(valor, "", "", action);
    categoria.filtrarDatos(numPagina, order);

};
var editarEstado = (id, fun) => {
    idCategoria = id;
    funcion = fun;
    var action = 'Categorias/getCategorias';
    var categoria = new Categoria("", "", "", action);
    categoria.qetCategoria(id, funcion);

};

var editarCategoria = () => {
    //asignar un objeto y pasarle la funcion :)
    var action = 'Categorias/editarCategoria';
    var categoria = new Categoria("", "", "", action);
    categoria.editarCategoria(idCategoria, funcion);
};
//CODIGO CURSOS
var getCategorias = (id, fun) => {
    var action = 'Cursos/getCategorias';
    var cursos = new Cursos("", "", "", "", "", "", "", action);
    cursos.getCategorias(id, fun);

};
var agregarCurso = () => {
    if (funcion === 0) {
        var action = 'Cursos/agregarCurso';
    } else {
        var action = 'Cursos/editarCurso';
    }

    var nombre = document.getElementById("Nombre").value;
    var descripcion = document.getElementById("Descripcion").value;
    var creditos = document.getElementById("Creditos").value;
    var horas = document.getElementById("Horas").value;
    var costo = document.getElementById("Costo").value;
    //para el checkbox
    var estado = document.getElementById("Estado").checked;
    //para el listbox
    var categorias = document.getElementById('CategoriaCursos');
    var categoria = categorias.options[categorias.selectedIndex].value;
    var cursos = new Cursos(nombre, descripcion, creditos, horas, costo, estado, categoria, action);
    cursos.agregarCurso(idCurso, funcion);
    funcion = 0;

};
var filtrarCurso = (numPagina, order) => {
    var valor = document.getElementById("filtrar").value;
    var action = 'Cursos/filtrarCurso';
    var cursos = new Cursos(valor, "", "", "", "", "", "", action);
    cursos.filtrarCurso(numPagina, order);

};
var editarEstadoCurso = (id, fun) => {
    funcion = fun;
    idCurso = id;
    var action = 'Cursos/getCursos';
    var cursos = new Cursos("", "", "", "", "", "", "", action);
    cursos.getCursos(id, fun);

};
var editarEstadoCurso1 = () => {
    var action = 'Cursos/editarCurso';
    var cursos = new Cursos("", "", "", "", "", "", "", action);
    cursos.editarEstadoCurso(idCurso, funcion);
};
var restablecer = () => {
    var cursos = new Cursos("", "", "", "", "", "", "", "");
    cursos.restablecer();

};
/**            
 CODIGO ESTUDIANTES            
 */
var estudiante = new Estudiantes();
var agregarEstudiante = () => {
    if (funcion === 0) {
        var action = 'Estudiantes/guardarEstudiante';
    }
    var id = 0;
    var codigo = document.getElementById("Codigo").value;
    var nombre = document.getElementById("Nombre").value;
    var apellido = document.getElementById("Apellidos").value;
    var fecha = document.getElementById("FechaNacimiento").value;
    var documento = document.getElementById("Documento").value;
    var email = document.getElementById("Email").value;
    var telefono = document.getElementById("Telefono").value;
    var direccion = document.getElementById("Direccion").value;
    var estado = document.getElementById("Estado").checked;
    estudiante.guardarEstudiante(id, funcion, action, codigo, nombre, apellido, fecha, documento, email, telefono, direccion, estado);
};
var filtarEstudiantes = (numPagina, order) => {
    var valor = document.getElementById("filtrar").value;
    var action = 'Estudiantes/filtrarEstudiantes';
    estudiante.filtarEstudiantes(numPagina, valor, order, action);
};
var editarEstudiante = (id, funcion) => {
    var action = 'Estudiantes/getEstudiante';
    estudiante.getEstudiante(id, funcion, action);

};