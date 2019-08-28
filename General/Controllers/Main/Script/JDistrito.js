﻿$(document).ready(function () {

    $('.dataTables-example').DataTable({
        pageLength: 25,
        dom: '<"html5buttons"B>lTfgitp',
        buttons: [
            { extend: 'copy' },
            { extend: 'csv' },
            { extend: 'excel', title: 'ExampleFile' },
            { extend: 'pdf', title: 'ExampleFile' },

            {
                extend: 'print',
                customize: function (win) {
                    $(win.document.body).addClass('white-bg');
                    $(win.document.body).css('font-size', '10px');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
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

    $("#btnActualizar").click(function () {

        var registro = {
            DepartamentoId: $("#hdepartamentoId").val(),
            nombre: $("#ActualizarNombre").val(),
            DistritoId: $("#hdistritoId").val(),
        };

        $.ajax({
            url: '../Distrito/Actualizar',
            type: 'POST',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(registro),
            async: true,
            success: function (data) {
                debugger;
                //aqui redireccionas
                if (data != null) {
                    if (data.Resultado == "OK") {
                        $("#dvActualizar").modal("hide");
                        ViewGrilla();
                    }
                }
            },
            error: function (request, status, error) {
                //$("#dvActualizar").modal("hide");
            },



        });
    });

    $("#tblGrilla_length").change(function () {
        ViewGrilla()
    });

    $("#ChangeRow").change(function () {
        CargarGrilla();
    });

    $("#btnCreate").click(function () {

        var EDistrito = {
            Nombre: $("#RegistroNombre").val(),
            DepartamentoId: 1
        };

        $.ajax({
            url: '../Distrito/Grabar',
            type: 'POST',
            data: JSON.stringify(EDistrito),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                //aqui redireccionas
                if (data != null) {
                    if (data.Resultado == "OK") {
                        $("#success").modal('show');
                        $("#SuccessResult").text('El registro se grabo exitosamente');
                        ViewGrilla();
                    }
                    else {
                        $("#ErrorResult").text(data.Resultado);
                        $("#error").modal('show');
                    }
                }
            },
            error: function (request, status, error) {
                $("#error").modal('show');
                $("#ErrorResult").text();
            },

        });

    });
});

function ViewGrilla()
{
    var paginacion = {
        countrow: $("#tblGrilla_length option:selected").text()
    };

    $.ajax({
        url: '../Distrito/CargarGrilla',
        type: 'POST',
        data: JSON.stringify(paginacion),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            debugger;
            if (data != null) {
                if (data.Resultado.length > 1) {
                    var table = $('#tblGrilla');
                    table.find("tbody tr").remove();
                    data.Resultado.forEach(function (result) {
                        table.append("<tr><td class='col-xs-12 col-md-6'>" + result.Nombre + "</td>" +
                            "<td class='col-xs-12 col-md-3'>" +
                            " <input type='button' onclick='Edit(this)' id='btnEdit' data-assigned-id=" +
                            result.DistritoId + " class='btn btn-info' value='Modificar' />" +
                            " <input type='button' onclick='Delete(this)' id='btnDelete' data-assigned-id=" +
                            result.DistritoId + " class='btn btn-danger' value='Delete' /></td>" +
                            "<td class='col-xs-12 col-md-2'></td><td class='col-xs-12 col-md-1'></td></tr>");
                    });
                }
            }

        },
        error: function (request, status, error) {
            alert("dd");
        },
    });

}

function Delete(id) {
    var resultado = {
        distritoId: $(id).data('assigned-id')
    };

    $.ajax({
        url: '../Distrito/Delete',
        type: 'POST',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(resultado),
        success: function (data) {
            //aqui redireccionas
            if (data != null) {
                if (data.Resultado == "OK") {
                    $("#success").modal('show');
                    $("#SuccessResult").text('Se elimino satisfactoriamente el registro');
                    ViewGrilla();
                }
                else {
                    $("#ErrorResult").text(data.Resultado);
                    $("#error").modal('show');
                }
            }
        },
        error: function (request, status, error) {
            debugger;
            $("#error").modal('show');
            $("#ErrorResult").text();
            debugger;
            //alert(request.responseText);
        },

    });

}

function Edit(id) {

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
                    var departamento = data.Resultado[0].DepartamentoId;
                    var distrito = data.Resultado[0].DistritoId;
                    $("#dvActualizar").modal("show");
                    $("#ActualizarNombre").val(nombre);
                    $("#hdepartamentoId").val(departamento);
                    $("#hdistritoId").val(distrito);
                }
            }

        },
        error: function (request, status, error) {

        },


    });

}

function CargarGrilla() {
    var paginacion = {
        countrow: $("#ChangeRow").val()
    };

    $.ajax({
        url: '../Distrito/Index',
        type: 'POST',
        data: JSON.stringify(paginacion),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            alert(data);
        },
        error: function (request, status, error) {
            //debugger;
            //alert("request : " + request);
            //alert("status : " + status);
            //alert("error : " + error);
            //alert("error");
        },
    });
}