(function () {
    var fila = document.getElementById("cantTotal");

    var grabar = document.getElementById("btnSave");
    grabar.addEventListener("click", SavePerson, "");

    //var edit = document.getElementById("btnUpdate");
    //edit.addEventListener("click", EditPerson, "");

    var nuevo = document.getElementById("btnNew");
    nuevo.addEventListener("click", NewPerson, "");

    var countRow = document.getElementById("tblPersonGrid_length");
    countRow.addEventListener("change", ViewGrid, "");

    $('#date_1 .input-group.date').datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        autoclose: true,
        locale: 'es'
    });
    /*
    $('.dataTables-examplePerson').DataTable({
        dom: '<"html5buttons"B>lTfgitp',
        //======================================....
        buttons: [
            {
                extend: 'excel',
                text: 'Exportar a Excel',
                title: 'Consulta Resultados Docentes',
                exportOptions: {
                    columns: ':visible'
                }
            }
        ],
        deferRender: true,
        responsive: true,
        "language": {
            "decimal": "",
            "emptyTable": "No se encontraron registros.",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ registros",
            "infoEmpty": "Mostrando 0 a 0 de 0 registros",
            "infoFiltered": "(filtrado de _MAX_ total registros)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar  _MENU_  registros",
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
    */
    $('.dataTables-example').DataTable({
        pageLength: 10,
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

    var rowGrid = $("#tblPersonGrid_length option:selected").text();

    var mostrar = "Mostrando 1 a " + rowGrid + " de " + fila.value + " registros";
    $("#tblPersonGrid_info").html(mostrar);
}());

//$(document).ready(function () {

    

//});

function parseJsonRow(jsonRowString) {
    if (!jsonRowString) {
        return "";
    }
    return jsonRowString;
}

function parseJsonDate(jsonDateString) {
    if (!jsonDateString) {
        return "";
    }
    //var completedDate = new Date(parseInt(jsonDateString.replace("/Date(", "").replace(")/")));
    //var result = completedDate.toDateString();
    //return result;
    var nowDate = new Date(parseInt(jsonDateString.substr(6)));
    var result = nowDate.format("dd/mm/yyyy");
    return result;
    //return new Date(parseInt(jsonDateString.replace('/Date(', '')));
}

function LimpiarCampos() {
    document.getElementById("Person_PersonaId").value = "0";
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
    var persona = document.getElementById("Person_PersonaId");
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
            Fechacreacion: fecNacimiento.value,
            personaId: persona.value
        };

        if (persona.value == "0") {

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
                            ViewGrid();
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
        else {
            
            $.ajax({
                url: '../Persona/EditPerson',
                type: 'POST',
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data != null) {
                        if (data.Resultado == "OK") {
                            $("#success").modal('show');
                            $("#SuccessResult").text('El registro se actualizó exitosamente');
                            ViewGrid();
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

}

function ViewGrid()
{

    var paginacion = {
        countrow: $("#tblPersonGrid_length option:selected").text()
    };

    $.ajax({
        url: '../Persona/CargarGrilla',
        type: 'POST',
        data: JSON.stringify(paginacion),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.Resultado.PersonaGrilla.length > 0) {

                    var mostrar = "Mostrando 1 a " + data.Resultado.PersonaGrilla.length + " de " + data.Resultado.cantTotal  +" registros";
                    $("#tblPersonGrid_info").html(mostrar);

                    var table = $('#tblPersonGrid');
                    table.find("tbody tr").remove();
                    data.Resultado.PersonaGrilla.forEach(function (result) {
            
                        table.append("<tr><td class='col-xs-12 col-md-1'>" +
                            "<div><table><tr><td class='col-xs-12 col-md-6'> " +
                            "<a class='fa fa-search' onclick='PersonSelect(this)' id ='btnEdit' "+
                            " style='color: #6A5ACD' data-assigned-id=" + result.PersonaId +
                            " </a></td> " +
                            "<td class='col-xs-12 col-md-6'> " +
                            "<a class='fa fa-minus-circle' onclick='DeletePerson(this)' id ='btnElimPerson' " +
                            " style='color:red' data-assigned-id=" + result.PersonaId +
                            " </a></td></tr></table></div></td>" +
                            "<td class='col-xs-12 col-md-2'>" + result.Apellidopaterno + "</td>" +
                            "<td class='col-xs-12 col-md-2'>" + result.Nombre + "</td>" +
                            "<td class='col-xs-12 col-md-2'>" + result.Nrodocumento + "</td>" +
                            "<td class='col-xs-12 col-md-1'>" + parseJsonRow(result.Nrotelefono) + "</td>" +
                            "<td class='col-xs-12 col-md-2'>" + parseJsonRow(result.Direccion) + "</td>" +
                            "<td class='col-xs-12 col-md-2'>" + parseJsonDate(result.Fecnacimiento) + "</td></tr>");
                    });

                    var page = $('#hiPaginado');
                    page.find("div").remove();
                    var html = "";

                    for (var i = 1; i < data.Resultado.cantPage + 1; i++)
                    {
                        if (i == 1) {
                            html = "<div class='pagination-container'><ul class='pagination'><li class='active'><a>" + i + "</a></li>"
                        }
                        else {
                            html = html + "<li class=''><a href='/Persona/Index?page=" + i + "'>" + i + "</a></li>"
                           
                        } 
                    }
                    //html = html + "<li class='PagedList-skipToNext'><a href='/Persona/Index?page=" + i + "' rel='next' >»" + i + "</a></li>"
                    html = html + "</ul></div>";
                    page.append(html);

                    LimpiarCampos();

                    var tab1 = document.getElementById("tab-1");
                    var tab2 = document.getElementById("tab-2");
                    var ltab1 = document.getElementById("litab1");
                    var ltab2 = document.getElementById("litab2");

                    tab2.classList.remove("active");
                    tab1.classList.add("active");
                    ltab2.classList.remove("active");
                    ltab1.classList.add("active");

                }
            }

        },
        error: function (request, status, error) {
            alert("dd");
        },
    });
}

/*
function EditPerson() {
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
    var persona = document.getElementById("Person_PersonaId");

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
            Fechacreacion: fecNacimiento.value,
            personaId : persona.value
        };

        $.ajax({
            url: '../Persona/EditPerson',
            type: 'POST',
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    if (data.Resultado == "OK") {
                        $("#success").modal('show');
                        $("#SuccessResult").text('El registro se actualizó exitosamente');
                        ViewGrid();
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
*/
function DeletePerson(id)
{
    var resultado = {
        personaId: $(id).data('assigned-id')
    };

    $.ajax({
        url: '../Persona/DeletePerson',
        type: 'POST',
        data: JSON.stringify(resultado),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.Resultado == "OK") {
                    $("#SuccessResult").text('El registro se eliminó exitosamente');
                    $("#success").modal('show');
                    ViewGrid();
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

function PersonSelect(id)
{
    var resultado = {
        personaId: $(id).data('assigned-id')
    };

    var tab1 = document.getElementById("tab-1");
    var tab2 = document.getElementById("tab-2");
    var ltab1 = document.getElementById("litab1");
    var ltab2 = document.getElementById("litab2");
    

    tab1.classList.remove("active");
    tab2.classList.add("active");
    ltab1.classList.remove("active");
    ltab2.classList.add("active");

    $.ajax({
        url: '../Persona/CargarPerson',
        type: 'POST',
        data: JSON.stringify(resultado),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.Resultado.length > 0) {
                    var apMaterno = data.Resultado[0].Apellidomaterno;
                    var apPaterno = data.Resultado[0].Apellidopaterno;
                    var direccion = data.Resultado[0].Direccion;
                    var distritoId = data.Resultado[0].DistritoId;
                    var fecNacimiento = parseJsonDate(data.Resultado[0].Fecnacimiento);
                    var nombre = data.Resultado[0].Nombre;
                    var nroDocumento = data.Resultado[0].Nrodocumento;
                    var nroTelefono = data.Resultado[0].Nrotelefono;
                    var ocupacion = data.Resultado[0].Ocupacion;
                    var personaId = data.Resultado[0].PersonaId;
                    var sexoId = data.Resultado[0].SexoId;
                    var tipodocumentoId = data.Resultado[0].TipodocumentoId;
                    var nombreDistrito = data.Resultado[0].NombreDistrito;
                    var userCreate = data.Resultado[0].Usuariocreacion;
                    var userDate = parseJsonDate(data.Resultado[0].Fechacreacion);
                    var userModify = data.Resultado[0].Usuariomodificacion;
                    var userDateModify = parseJsonDate(data.Resultado[0].Fechamodificacion);

                    $("#Person_Nombre").val(nombre);
                    $("#Person_Apellidopaterno").val(apPaterno);
                    $("#Person_Apellidomaterno").val(apMaterno);
                    $("#Person_Direccion").val(direccion);
                    $("#FechaNacimiento").val(fecNacimiento);
                    $("#hdistritoId").val(distritoId);
                    $("#Person_Nrotelefono").val(nroTelefono);
                    $("#Person_Ocupacion").val(ocupacion);
                    $("#Person_Nrodocumento").val(nroDocumento);
                    $("#Person_PersonaId").val(personaId);

                    $("#SexoId").val(sexoId);
                    $("#DocumentypeId").val(tipodocumentoId);
                    $("#Namedistrito").val(nombreDistrito);

                    $("#Person_Usuariocreacion").val(userCreate);
                    $("#Person_Fechacreacion").val(userDate);
                    $("#Person_Usuariomodificacion").val(userModify);
                    $("#Person_Fechamodificacion").val(userDateModify);

                    
                }
            }

        },
        error: function (request, status, error) {
            alert("dd");
        },
    });

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
