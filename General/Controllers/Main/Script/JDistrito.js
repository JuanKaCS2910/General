$(document).ready(function () {

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

                //aqui redireccionas
                if (data != null) {
                    if (data.Resultado = "OK") {
                        $("#dvActualizar").modal("hide");
                        //window.location.href = "http://localhost:2672/Distrito/Index";
                        CargarGrilla();
                    }
                }

            },
            error: function (request, status, error) {
                //$("#dvActualizar").modal("hide");
            },



        });
    });
    $("#ChangeRow").change(function () {

        CargarGrilla();

    });

    $("#btnCreate").click(function () {
        
        var EDistrito = {
            Nombre: $("#RegistroNombre").val(),
            DepartamentoId: 1
        };

        var Grilla = {
            page: 0,
            countrow: 0
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
                        //window.location = "/Distrito/Index/";
                        $("#success").modal('show');
                        $("#SuccessResult").text('El registro se grabo exitosamente');
                        //$.ajax({
                        //    url: "../Distrito/Index",
                        //    data: JSON.stringify(Grilla),
                        //    type: 'GET',
                        //    success: function (result) {
                        //        do the necessary updations
                        //    },
                        //    error: function (result) {

                        //    }
                        //});

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
                //debugger;
                //alert(request.responseText);
            },

        });

    });
});

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
                    CargarGrilla();
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