(function () {

    var historico = document.getElementById("hisearch");
    historico.addEventListener("click", SearchPerson, "");

    var documentypeId = document.getElementById("DocumentypeId");
    documentypeId.addEventListener("change", ChangeDocument, "");

    var grabar = document.getElementById("btnSave");
    grabar.addEventListener("click", SaveHistory, "");

    var nuevo = document.getElementById("btnNew");
    nuevo.addEventListener("click", NewHistory, "");

}());

function SaveHistory() {

    var personaId = document.getElementById('PersonaId');
    //
    var diagnostico = document.getElementById('Historicos_Diagnostico');
    var paquete = document.getElementById('Historicos_Paquetes');
    var costo = document.getElementById('Historicos_Costo');
    //Agente Térmico.
    var caliente = document.getElementById('Historicos_checkCaliente');
    var fria = document.getElementById('Historicos_checkFria');
    var contraste = document.getElementById('Historicos_checkContraste');
    //Agente Electrofísico
    var descElectroanalgesico = document.getElementById('Historicos_descElectroanalgesico');
    var checkElectroestimulacion = document.getElementById('Historicos_checkElectroestimulacion');
    var descElectroestimulacion = document.getElementById('Historicos_descElectroestimulacion');
    var checkMagnetoterapia = document.getElementById('Historicos_checkMagnetoterapia');
    var descMagnetoterapia = document.getElementById('Historicos_descMagnetoterapia');
    var checkUltrasonido = document.getElementById('Historicos_checkUltrasonido');
    var descUltrasonido = document.getElementById('Historicos_descUltrasonido');
    var checkTCombinada = document.getElementById('Historicos_checkTCombinada');
    var descTCombinada = document.getElementById('Historicos_descTCombinada');
    var checkLaserterapia = document.getElementById('Historicos_checkLaserterapia');
    var descLaserterapia = document.getElementById('Historicos_descLaserterapia');
    //Maniobras Terapéuticas
    var relajante = document.getElementById('Historicos_checkRelajante');
    var descontracturante = document.getElementById('Historicos_checkDescontracturante');
    var estiramiento = document.getElementById('Historicos_checkEstiramiento');
    var fortalecimiento = document.getElementById('Historicos_checkFortalecimiento');
    var rpg = document.getElementById('Historicos_checkRPG');
    var activacion = document.getElementById('Historicos_checkActivacion');
    var tape = document.getElementById('Historicos_checkTAPE');
    //
    var observaciones = document.getElementById('Historicos_Observaciones');

    var param = {
        PersonaId: personaId.value,
        Diagnostico: diagnostico.value,
        Paquetes: paquete.value,
        Costo: costo.value,
        checkCaliente: caliente.checked,
        checkFria: fria.checked,
        checkContraste: contraste.checked,
        descElectroanalgesico: descElectroanalgesico.value,
        checkElectroestimulacion: checkElectroestimulacion.checked,
        descElectroestimulacion: descElectroestimulacion.value,
        checkMagnetoterapia: checkMagnetoterapia.checked,
        descMagnetoterapia: descMagnetoterapia.value,
        checkUltrasonido: checkUltrasonido.checked,
        descUltrasonido: descUltrasonido.value,
        checkTCombinada: checkTCombinada.checked,
        descTCombinada: descTCombinada.value,
        checkLaserterapia: checkLaserterapia.checked,
        descLaserterapia: descLaserterapia.value,
        checkRelajante: relajante.checked,
        checkDescontracturante: descontracturante.checked,
        checkEstiramiento: estiramiento.checked,
        checkFortalecimiento: fortalecimiento.checked,
        checkRPG: rpg.checked,
        checkActivacion: activacion.checked,
        checkTAPE: tape.checked,
    };

    $.ajax({
        url: '../Historial/SaveHistorico',
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

function NewHistory() {

}

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
    $("#PersonaId").val("");
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
                    var personaId = data.Resultado[0].PersonaId;

                    $("#Paciente").val(apPaterno + " " + apMaterno + ", " + nombre);
                    $("#PersonaId").val(personaId);
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