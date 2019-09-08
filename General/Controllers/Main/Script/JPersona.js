$(document).ready(function () {

    var grabar = document.getElementById("btnSave");
    grabar.addEventListener("click", SavePerson, "");

    var edit = document.getElementById("btnEdit");
    edit.addEventListener("click", EditPerson, "");

    var eliminar = document.getElementById("btnDelete");
    eliminar.addEventListener("click", DeletePerson, "");

    var nuevo = document.getElementById("btnNew");
    nuevo.addEventListener("click", NewPerson, "");
    
    $('#date_1 .input-group.date').datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        autoclose: true,
        locale: 'es'
    });
    
    $('.dataTables-example').DataTable({
        pageLength: 25,
        buttons: [
            { extend: 'pdf', title: 'ExampleFile' }
        ],
        "language": {
            "decimal": "",
            "emptyTable": "No se encontraron registros.",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ registros",
            "infoEmpty": "Mostrando 0 a 0 de 0 registros",
            "infoFiltered": "(filtrado de _MAX_ total registros)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar:&nbsp;  _MENU_  registros",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar: ",
            "zeroRecords": "No se encontraron registros",
            "paginate": {
                "first": "Primera",
                "last": "Última",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            "aria": {
                "sortAscending": ": activar para ordernar columna de forma ascendente",
                "sortDescending": ": activar para ordenar de forma descendente"
            }
        }

    });

    $("#btnFiltro").click(function () {
        BusquedaGrilla();
    });

    $("#btnDistrito").click(function () {
        var filtro = $('#tblFiltro_filter');
        filtro.remove();

        var filtro_info = $('#tblFiltro_info');
        filtro_info.remove();

        $("#modalDistrito").modal('show');
        BusquedaGrilla();
    });

});

function LimpiarCampos() {
    document.getElementById("Person_Apellidopaterno").value = "";
    document.getElementById("Person_Apellidomaterno").value = "";
    document.getElementById("Person_Nombre").value = "";
    document.getElementById("SexoId").value = "";
    document.getElementById("DocumentypeId").value = "";
    document.getElementById("Person_Nrodocumento").value = "";
    document.getElementById("FechaNacimiento").value = "";
    document.getElementById("hdistritoId").value = "";
    document.getElementById("Namedistrito").value = "";
    document.getElementById("Person_Direccion").value = "";
    document.getElementById("Person_Nrotelefono").value = "";
    document.getElementById("Person_Ocupacion").value = "";
}

function NewPerson() {
    LimpiarCampos();
}

function SavePerson() {

    var apPaternoGrabar = document.getElementById("Person_Apellidopaterno");
    var apMaternoGrabar = document.getElementById("Person_Apellidomaterno");
    var nombre = document.getElementById("Person_Nombre");
    var sexo = document.getElementById("SexoId");
    var documento = document.getElementById("DocumentypeId");
    var nroDocumento = document.getElementById("Person_Nrodocumento");
    var fecNacimiento = document.getElementById("FechaNacimiento");
    var distrito = document.getElementById("hdistritoId");
    var direccion = document.getElementById("Person_Direccion");
    var nroTelefono = document.getElementById("Person_Nrotelefono");
    var ocupacion = document.getElementById("Person_Ocupacion");

    var campos = "";

    if (apPaternoGrabar.value == "") {
        campos = "Apellido Paterno <span style='color:red'>obligatorio </span>" + "<br/>"
    }
    if (apMaternoGrabar.value == "") {
        campos = campos + "Apellido Materno <span style='color:red'>obligatorio </span> " + "<br/>"
    }
    if (nombre.value == "") {
        campos = campos + "Nombre <span style='color:red'>obligatorio </span> " + "<br/>"
    }
    if (sexo.value == "") {
        campos = campos + "Sexo <span style='color:red'>obligatorio </span> " + "<br/>"
    }
    if (documento.value == "") {
        campos = campos + "Tipo Documento <span style='color:red'>obligatorio </span> " + "<br/>"
    }
    if (nroDocumento.value == "") {
        campos = campos + "Nro. de Documento <span style='color:red'>obligatorio </span> " + "<br/>"
    }
    if (distrito.value == "") {
        campos = campos + "Distrito <span style='color:red'>obligatorio </span> " + "<br/>"
    }
    if (direccion.value == "") {
        campos = campos + "Dirección <span style='color:red'>obligatorio </span> " + "<br/>"
    }

    var arrayCampo = campos.split("<br/>");

    if (arrayCampo != "") {
        var ul = $('#ulCamposObligatorios');
        ul.find("li").remove();
        arrayCampo.forEach(function (result) {
            if (result != "") {
                ul.append("<li> <label class='control-label input-style'>" + result + "</label></li>");
            }
        });
        $("#modalObligatorios").modal('show');
    }
    else {
        var param = {
            TipodocumentoId: documento.value,
            Nrodocumento: nroDocumento.value,
            Nombre: nombre.value,
            Apellidopaterno: apPaternoGrabar.value,
            Apellidomaterno: apMaternoGrabar.value,
            Nrotelefono: nroTelefono.value,
            Fecnacimiento: fecNacimiento.value,
            DistritoId: distrito.value,
            Direccion: direccion.value,
            Ocupacion: ocupacion.value,
            SexoId: sexo.value,
            Usuariocreacion: "",
            Fechacreacion: fecNacimiento.value
        };

        $.ajax({
            url: '../Persona/SavePerson',
            type: 'POST',
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    if (data.Resultado == "OK") {
                        $("#success").modal('show');
                        $("#SuccessResult").text('El registro se grabo exitosamente');
                    }
                    else {
                        $("#ErrorResult").text(data.Resultado);
                        $("#error").modal('show');
                    }
                }

            },
            error: function (request, status, error) {
                alert("dd");
            },
        });
    }

}

function EditPerson() {
    alert("Editar");
}

function DeletePerson() {
    alert("Eliminar");
}

function BusquedaGrilla() {
    var parametros = {
        FDistrito: $("#Descripcionfiltro").val(),
        rows: $("#tblFiltro_length option:selected").text()
    };

    $.ajax({
        url: '../Persona/CargarFiltro',
        type: 'POST',
        data: JSON.stringify(parametros),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.Resultado.length > 0) {
                    var table = $('#tblFiltro');
                    table.find("tbody tr").remove();
                    //class="col-xs-12 col-md-1"
                    data.Resultado.forEach(function (result) {//glyphicon glyphicon-search
                        table.append("<tr><td class='col-xs-12 col-md-1'>" +
                            "<button type='button' onclick='SeleccionarDistrito(this)' id='btnEdit' class='btn btn-primary' data-assigned-id=" +
                            result.DistritoId + "> <i class='glyphicon glyphicon-search' ></i></button > " +
                            "</td><td class='col-xs-12 col-md-11'>" + result.Nombre + "</td></tr>");
                    });
                }
            }

        },
        error: function (request, status, error) {
            alert("dd");
        },
    });
}

function SeleccionarDistrito(id) {

    var resultado = {
        distritoId: $(id).data('assigned-id')
    };

    $.ajax({
        url: '../Distrito/Edit',
        type: 'POST',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(resultado),
        success: function (data) {
            //aqui redireccionas
            if (data != null) {
                if (data.Resultado.length > 0) {
                    var nombre = data.Resultado[0].Nombre;
                    var distrito = data.Resultado[0].DistritoId;
                    $("#modalDistrito").modal("hide");
                    $("#Namedistrito").val(nombre);
                    $("#hdistritoId").val(distrito);
                }
            }
        },
        error: function (request, status, error) {
        },
    });

}
