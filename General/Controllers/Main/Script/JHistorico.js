(function () {

    var historico = document.getElementById("hisearch");
    historico.addEventListener("click", SearchPerson, "");

    var documentypeId = document.getElementById("DocumentypeId");
    documentypeId.addEventListener("change", ChangeDocument, "");


}());

function ChangeDocument() {
    Habilitar(true);
}

function Habilitar(condicion) {
    document.getElementById('Historicos_Diagnostico').disabled = condicion;
    document.getElementById('Historicos_Paquetes').disabled = condicion;
    document.getElementById('Historicos_Costo').disabled = condicion;
    //Agente Térmico
    document.getElementById('Historicos_checkCaliente').disabled = condicion;
    document.getElementById('Historicos_checkFria').disabled = condicion;
    document.getElementById('Historicos_checkContraste').disabled = condicion;
    //Agente Electrofísico
    document.getElementById('Historicos_descElectroanalgesico').disabled = condicion;
    document.getElementById('Historicos_checkElectroestimulacion').disabled = condicion;
    document.getElementById('Historicos_descElectroestimulacion').disabled = condicion;
    document.getElementById('Historicos_checkMagnetoterapia').disabled = condicion;
    document.getElementById('Historicos_descMagnetoterapia').disabled = condicion;
    document.getElementById('Historicos_checkUltrasonido').disabled = condicion;
    document.getElementById('Historicos_descUltrasonido').disabled = condicion;
    document.getElementById('Historicos_checkTCombinada').disabled = condicion;
    document.getElementById('Historicos_descTCombinada').disabled = condicion;
    document.getElementById('Historicos_checkLaserterapia').disabled = condicion;
    document.getElementById('Historicos_descLaserterapia').disabled = condicion;
    //Maniobras Terapéuticas
    document.getElementById('Historicos_checkRelajante').disabled = condicion;
    document.getElementById('Historicos_checkDescontracturante').disabled = condicion;
    document.getElementById('Historicos_checkEstiramiento').disabled = condicion;
    document.getElementById('Historicos_checkFortalecimiento').disabled = condicion;
    document.getElementById('Historicos_checkRPG').disabled = condicion;
    document.getElementById('Historicos_checkActivacion').disabled = condicion;
    document.getElementById('Historicos_checkTAPE').disabled = condicion;

    //
    document.getElementById('Historicos_Observaciones').disabled = condicion;
}

function SearchPerson() {
    $("#Paciente").val("");
    var Fdocumento = document.getElementById("Documento");
    var Ftipodocumento = document.getElementById("DocumentypeId");
    var filtro = {
        TipodocumentoId: Ftipodocumento.value,
        Nrodocumento: Fdocumento.value,
        countrow: 5
    };

    $.ajax({
        url: '../Historial/SearchPerson',
        type: 'POST',
        data: JSON.stringify(filtro),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.Resultado.length > 0) {
                    var nombre = data.Resultado[0].Nombre;
                    var apPaterno = data.Resultado[0].Apellidopaterno;
                    var apMaterno = data.Resultado[0].Apellidomaterno;

                    $("#Paciente").val(apPaterno + " " + apMaterno + ", " + nombre);
                    Habilitar(false);
                    
                }
                else {
                    Habilitar(true);
                    $("#FindPerson").modal('show');
                }
            }

        },
        error: function (request, status, error) {
            alert("error");
        },
    });

}