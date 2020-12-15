
class Inscripciones {
    constructor() {

    }
    filtrarDataInscripcion(valor, action, fun) {
        valor = (valor === "") ? "null" : valor;
        $.post(
            action, { valor }, (response) => {
                console.log(response);
                switch (fun) {
                    case 1:
                        $("#resultSearchEstudiante").html(response);
                        break;
                    case 2:
                        $("#resultSearchCurso").html(response);
                        break;
                }
            });
    }
    getData(id, action,fun) {
        $.post(action,
            { id },
            (response) => {
                console.log(response);

                document.getElementById("Estudiante").value = response[0].apellidos + " " + response[0].nombres;
                this.restablecer();
            });

    }
    restablecer() {

        document.getElementById("filtrar").value = "";
        $('#modalEstudiante').modal('hide');
    }
}
