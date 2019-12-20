﻿(function () {

    var historico = document.getElementById("hisearch");
    historico.addEventListener("click", SearchPerson, "");

    var documentypeId = document.getElementById("DocumentypeId");
    documentypeId.addEventListener("change", ChangeDocument, "");

    var grabar = document.getElementById("btnSave");
    grabar.addEventListener("click", SaveHistory, "");

    var nuevo = document.getElementById("btnNew");
    nuevo.addEventListener("click", NewHistory, "");

    var filtro = document.getElementById("btnFiltro");
    filtro.addEventListener("click", FoundGrid, "");

}());

//Utilitarios.
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

//Primer Tab
function FoundGrid() {

    var Fdocumento = document.getElementById("FiltroPerson_Nrodocumento");
    var Ftipodocumento = document.getElementById("FiltroDocumentypeId");
    var Fappaterno = document.getElementById("FiltroPerson_Apellidopaterno");

    var paginacion = {
        countrow: $("#tblPersonGrid_length option:selected").text()
    };

    var filtro = {
        TipodocumentoId: Ftipodocumento.value,
        Nrodocumento: Fdocumento.value,
        Apellidopaterno: Fappaterno.value,
        countrow: $("#tblPersonGrid_length option:selected").text()
    };

    $.ajax({
        url: '../Historial/CargarBusquedaGrilla',
        type: 'POST',
        data: JSON.stringify(filtro),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {

                if (data.Resultado.PersonaGrilla.length == 0) {
                    $("#tblPersonGrid_info").html("");
                    var table = $('#tblPersonGrid');
                    table.find("tbody tr").remove();
                    var page = $('#hiPaginado');
                    page.find("div").remove();
                }

                if (data.Resultado.PersonaGrilla.length > 0) {

                    var mostrar = "Mostrando 1 a " + data.Resultado.PersonaGrilla.length + " de " + data.Resultado.cantTotal + " registros";
                    $("#tblPersonGrid_info").html(mostrar);

                    var table = $('#tblPersonGrid');
                    table.find("tbody tr").remove();
                    data.Resultado.PersonaGrilla.forEach(function (result) {

                        table.append("<tr><td class='col-xs-12 col-md-1'>" +
                            "<div><table><tr><td class='col-xs-12 col-md-12'> " +
                            "<a class='fa fa-search' onclick='PersonSelect(this)' id ='btnEditPerson' " +
                            " style='color: #6A5ACD' data-assigned-id=" + result.PersonaId +
                            " </a></td> </tr></table></div></td>" +
                            "<td class='col-xs-12 col-md-4'>" + result.Apellidopaterno + " " + result.Apellidomaterno + " , " + result.Nombre + "</td>" +
                            "<td class='col-xs-12 col-md-2'>" + result.Nrodocumento + "</td>" +
                            "<td class='col-xs-12 col-md-1'>" + parseJsonRow(result.Nrotelefono) + "</td>" +
                            "<td class='col-xs-12 col-md-4'>" + parseJsonRow(result.Direccion) + "</td></tr>");
                    });
                    //"<td class='col-xs-12 col-md-2'>" + result.Nombre + "</td>" +
                    var page = $('#hiPaginado');
                    page.find("div").remove();
                    var html = "";
                    html = "<div class='pagination-container'><ul class='pagination'>";
                    for (var i = 1; i < data.Resultado.cantPage + 1; i++) {
                        if (i == data.Resultado.pageView) {
                            html = html + "<li class='active'><a>" + i + "</a></li>"
                        }
                        else {
                            html = html + "<li class=''><a href='javaScript:FoundGrid(" + i + "," + $("#tblPersonGrid_length").val() + ")'>" + i + "</a></li>"
                        }
                    }
                    html = html + "</ul></div>";
                    page.append(html);


                }
            }

        },
        error: function (request, status, error) {
            alert("dd");
        },
    });

}

function PersonSelect(id) {
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
                    var nombre = data.Resultado[0].Nombre;
                    var nroDocumento = data.Resultado[0].Nrodocumento;
                    var tipodocumentoId = data.Resultado[0].TipodocumentoId;
                    var personaId = data.Resultado[0].PersonaId;

                    $("#TDocumentoHistorico").val(tipodocumentoId);
                    $("#NroDocumentoHistorico").val(nroDocumento);
                    $("#NombreCompletoHistorico").val(apPaterno + ' ' + apMaterno + ' ' + nombre);

                    ViewGridHistorico(personaId);
                }
            }

        },
        error: function (request, status, error) {
            alert("dd");
        },
    });

}

//Segundo Tab.
function ViewGridHistorico(personaId) {

    var paginacion = {
        countrow: $("#tblHistoricoGrid_length option:selected").text(),
        PersonaId: personaId
    };

    $.ajax({
        url: '../Historial/CargarGrilla',
        type: 'POST',
        data: JSON.stringify(paginacion),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.Resultado.HistoricoGrilla.length > 0) {

                    var mostrar = "Mostrando 1 a " + data.Resultado.HistoricoGrilla.length + " de " + data.Resultado.cantTotal + " registros";
                    $("#tblHistoricoGrid_info").html(mostrar);

                    var table = $('#tblHistoricoGrid');
                    table.find("tbody tr").remove();
                    data.Resultado.HistoricoGrilla.forEach(function (result) {

                        table.append("<tr><td class='col-xs-12 col-md-1'>" +
                            "<div><table><tr><td class='col-xs-12 col-md-6'> " +
                            "<a class='fa fa-search' onclick='HistoricoSelect(this)' id ='btnEditHistorico' " +
                            " style='color: #6A5ACD' data-assigned-id=" + result.HistoricoId +
                            " </a></td> " +
                            "<td class='col-xs-12 col-md-6'> " +
                            "<a class='fa fa-minus-circle' onclick='DeleteHistorico(this)' id ='btnElimHistorico' " +
                            " style='color:red' data-assigned-id=" + result.HistoricoId +
                            " </a></td></tr></table></div></td>" +
                            "<td class='col-xs-12 col-md-3'>" + parseJsonRow(result.Diagnostico) + "</td>" +
                            "<td class='col-xs-12 col-md-3'>" + parseJsonRow(result.observaciones) + "</td>" +
                            "<td class='col-xs-12 col-md-3'>" + parseJsonRow(result.Otros) + "</td>" +
                            "<td class='col-xs-12 col-md-2'>" + parseJsonDate(result.Fechacreacion) + "</td></tr>");
                    });

                    var page = $('#hiPaginado');
                    page.find("div").remove();
                    var html = "";
                    html = "<div class='pagination-container'><ul class='pagination'>";
                    for (var i = 1; i < data.Resultado.cantPage + 1; i++) {
                        if (i == data.Resultado.pageView) {
                            html = html + "<li class='active'><a>" + i + "</a></li>"
                        }
                        else {
                            //html = html + "<li class=''><a href='/Historico/Index?page=" + i + "'>" + i + "</a></li>"
                            html = html + "<li class=''><a href='javaScript:ViewGridJson(" + i + "," + $("#tblPersonGrid_length").val() + ")'>" + i + "</a></li>"
                        }
                    }

                    html = html + "</ul></div>";
                    page.append(html);

                    //LimpiarCampos();

                    var tab1 = document.getElementById("tab-1");
                    var tab2 = document.getElementById("tab-2");
                    var ltab1 = document.getElementById("litab1");
                    var ltab2 = document.getElementById("litab2");

                    tab1.classList.remove("active");
                    tab2.classList.add("active");
                    ltab1.classList.remove("active");
                    ltab2.classList.add("active");

                }
                else {
                    var table = $('#tblHistoricoGrid');
                    table.find("tbody tr").remove();
                    $("#tblHistoricoGrid_info").html("");
                    var page = $('#hiPaginado');
                    page.find("div").remove();
                }
            }

        },
        error: function (request, status, error) {
            alert("Inconveniente al cargar Grilla");
        },
    });
}

function HistoricoSelect(id) {
    var resultado = {
        idHistorico: $(id).data('assigned-id')
    };

    var tab2 = document.getElementById("tab-2");
    var tab3 = document.getElementById("tab-3");
    var ltab2 = document.getElementById("litab2");
    var ltab3 = document.getElementById("litab3");

    tab2.classList.remove("active");
    tab3.classList.add("active");
    ltab2.classList.remove("active");
    ltab3.classList.add("active");

    $.ajax({
        url: '../Historial/SearchHistorico',
        type: 'POST',
        data: JSON.stringify(resultado),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.Resultado.Persona.length > 0) {

                    var apMaterno = data.Resultado.Persona[0].Apellidomaterno;
                    var apPaterno = data.Resultado.Persona[0].Apellidopaterno;
                    var nombre = data.Resultado.Persona[0].Nombre;
                    var nroDocumento = data.Resultado.Persona[0].Nrodocumento;
                    var tipodocumentoId = data.Resultado.Persona[0].TipodocumentoId;

                    $("#DocumentypeId").val(tipodocumentoId);
                    $("#Documento").val(nroDocumento);
                    $("#Paciente").val(apPaterno + ' ' + apMaterno + ' ' + nombre);

                    if (data.Resultado.Historico.length > 0) {
                        var diagnostico = data.Resultado.Historico[0].Diagnostico;
                        var observaciones = data.Resultado.Historico[0].Observaciones;
                        var otros = data.Resultado.Historico[0].Otros;
                        $("#Historicos_Diagnostico").val(diagnostico);
                        $("#Historicos_Observaciones").val(observaciones);
                    }

                    if (data.Resultado.AgenteTermico.length > 0) {
                        data.Resultado.AgenteTermico.forEach(function (result) {
                            var condicion = result.Condicion;
                            switch (result.SubTramiteId) {
                                case 1:
                                    document.getElementById("Historicos_checkCaliente").checked = condicion;
                                    break;
                                case 2:
                                    document.getElementById("Historicos_checkFria").checked = condicion;
                                    break;
                                case 3:
                                    document.getElementById("Historicos_checkContraste").checked = condicion;
                                    break;
                                default:
                            }

                        });
                    }

                    if (data.Resultado.AgenteElectofisico.length > 0) {
                        data.Resultado.AgenteElectofisico.forEach(function (result) {
                            var descripcionAE = parseJsonRow(result.Descripcion);
                            var condicion = result.Condicion;
                            switch (result.SubTramiteId) {
                                case 4:
                                    $("#Historicos_descElectroanalgesico").val(descripcionAE);
                                break;
                                case 5:
                                    $("#Historicos_descElectroestimulacion").val(descripcionAE);
                                    document.getElementById("Historicos_checkElectroestimulacion").checked = condicion;
                                    break;
                                case 6:
                                    $("#Historicos_descMagnetoterapia").val(descripcionAE);
                                    document.getElementById("Historicos_checkMagnetoterapia").checked = condicion;
                                    break;
                                case 7:
                                    $("#Historicos_descUltrasonido").val(descripcionAE);
                                    document.getElementById("Historicos_checkUltrasonido").checked = condicion;
                                    break;
                                case 8:
                                    $("#Historicos_descTCombinada").val(descripcionAE);
                                    document.getElementById("Historicos_checkTCombinada").checked = condicion;
                                    break;
                                case 9:
                                    $("#Historicos_descLaserterapia").val(descripcionAE);
                                    document.getElementById("Historicos_checkLaserterapia").checked = condicion;
                                    break;
                                default:
                            }
                            
                        });
                    }

                    if (data.Resultado.ManiobraTerapeutica.length > 0) {
                        data.Resultado.ManiobraTerapeutica.forEach(function (result) {
                            var condicion = result.Condicion;
                            switch (result.SubTramiteId) {
                                case 1:
                                    document.getElementById("Historicos_checkRelajante").checked = condicion;
                                    break;
                                case 2:
                                    document.getElementById("Historicos_checkDescontracturante").checked = condicion;
                                    break;
                                case 3:
                                    document.getElementById("Historicos_checkEstiramiento").checked = condicion;
                                    break;
                                case 4:
                                    document.getElementById("Historicos_checkFortalecimiento").checked = condicion;
                                    break;
                                case 5:
                                    document.getElementById("Historicos_checkRPG").checked = condicion;
                                    break;
                                case 6:
                                    document.getElementById("Historicos_checkActivacion").checked = condicion;
                                    break;
                                case 7:
                                    document.getElementById("Historicos_checkTAPE").checked = condicion;
                                    break;
                                default:
                            }

                        });
                    }

                }
            }

        },
        error: function (request, status, error) {
            alert("dd");
        },
    });

}

//Tercer Tab.
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

function ViewGridJson(page, countrow) {
    //var Fdocumento = document.getElementById("FiltroPerson_Nrodocumento");
    //var Ftipodocumento = document.getElementById("FiltroDocumentypeId");
    //var Fappaterno = document.getElementById("FiltroPerson_Apellidopaterno");

    var paginacion = {
        page: page,
        countrow: countrow,
        TipodocumentoId: Ftipodocumento.value,
        Nrodocumento: Fdocumento.value,
        Apellidopaterno: Fappaterno.value,
    };

    $.ajax({
        url: '../Historial/CargarGrilla',
        type: 'POST',
        data: JSON.stringify(paginacion),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.Resultado.HistoricoGrilla.length > 0) {

                    var mostrar = "Mostrando 1 a " + data.Resultado.HistoricoGrilla.length + " de " + data.Resultado.cantTotal + " registros";
                    $("#tblHistoricoGrid_info").html(mostrar);

                    var table = $('#tblHistoricoGrid');
                    table.find("tbody tr").remove();

                    data.Resultado.HistoricoGrilla.forEach(function (result) {

                        table.append("<tr><td class='col-xs-12 col-md-1'>" +
                            "<div><table><tr><td class='col-xs-12 col-md-6'> " +
                            "<a class='fa fa-search' onclick='HistoricoSelect(this)' id ='btnEditHistorico' " +
                            " style='color: #6A5ACD' data-assigned-id=" + result.historicoId +
                            " </a></td> " +
                            "<td class='col-xs-12 col-md-6'> " +
                            "<a class='fa fa-minus-circle' onclick='DeleteHistorico(this)' id ='btnElimHistorico' " +
                            " style='color:red' data-assigned-id=" + result.historicoId +
                            " </a></td></tr></table></div></td>" +
                            "<td class='col-xs-12 col-md-3'>" + parseJsonRow(result.Diagnostico) + "</td>" +
                            "<td class='col-xs-12 col-md-3'>" + parseJsonRow(result.observaciones) + "</td>" +
                            "<td class='col-xs-12 col-md-3'>" + parseJsonRow(result.Otros) + "</td>" +
                            "<td class='col-xs-12 col-md-2'>" + parseJsonDate(result.Fechacreacion) + "</td></tr>");
                    });

                    var page = $('#hiPaginado');
                    page.find("div").remove();
                    var html = "";
                    html = "<div class='pagination-container'><ul class='pagination'>";
                    for (var i = 1; i < data.Resultado.cantPage + 1; i++) {
                        if (i == data.Resultado.pageView) {
                            html = html + "<li class='active'><a>" + i + "</a></li>"
                        }
                        else {
                            //html = html + "<li class=''><a href='/Historico/Index?page=" + i + "'>" + i + "</a></li>"
                            html = html + "<li class=''><a href='javaScript:ViewGridJson(" + i + "," + $("#tblPersonGrid_length").val() + ")'>" + i + "</a></li>"
                        }
                    }

                    html = html + "</ul></div>";
                    page.append(html);

                    //LimpiarCampos();

                    var tab1 = document.getElementById("tab-1");
                    var tab2 = document.getElementById("tab-2");
                    var ltab1 = document.getElementById("litab1");
                    var ltab2 = document.getElementById("litab2");

                    tab2.classList.remove("active");
                    tab1.classList.add("active");
                    ltab2.classList.remove("active");
                    ltab1.classList.add("active");

                }
                else {
                    var table = $('#tblHistoricoGrid');
                    table.find("tbody tr").remove();
                    $("#tblHistoricoGrid_info").html("");
                    var page = $('#hiPaginado');
                    page.find("div").remove();
                }
            }

        },
        error: function (request, status, error) {
            alert("Inconveniente al cargar Grilla");
        },
    });

}

function ViewGrid() {

    var paginacion = {
        countrow: $("#tblHistoricoGrid_length option:selected").text()
    };

    $.ajax({
        url: '../Historial/CargarGrilla',
        type: 'POST',
        data: JSON.stringify(paginacion),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.Resultado.HistoricoGrilla.length > 0) {

                    var mostrar = "Mostrando 1 a " + data.Resultado.HistoricoGrilla.length + " de " + data.Resultado.cantTotal + " registros";
                    $("#tblHistoricoGrid_info").html(mostrar);

                    var table = $('#tblHistoricoGrid');
                    table.find("tbody tr").remove();
                    data.Resultado.HistoricoGrilla.forEach(function (result) {

                        table.append("<tr><td class='col-xs-12 col-md-1'>" +
                            "<div><table><tr><td class='col-xs-12 col-md-6'> " +
                            "<a class='fa fa-search' onclick='HistoricoSelect(this)' id ='btnEdit' " +
                            " style='color: #6A5ACD' data-assigned-id=" + result.HistoricoId +
                            " </a></td> " +
                            "<td class='col-xs-12 col-md-6'> " +
                            "<a class='fa fa-minus-circle' onclick='DeleteHistorico(this)' id ='btnElimHistorico' " +
                            " style='color:red' data-assigned-id=" + result.HistoricoId +
                            " </a></td></tr></table></div></td>" +
                            "<td class='col-xs-12 col-md-3'>" + result.NombreCompleto + "</td>" +
                            "<td class='col-xs-12 col-md-4'>" + result.Diagnostico + "</td>" +
                            "<td class='col-xs-12 col-md-3'>" + parseJsonRow(result.Otros) + "</td>" +
                            "<td class='col-xs-12 col-md-1'>" + parseJsonDate(result.Fechacreacion) + "</td></tr>");
                    });
                    
                    var page = $('#hiPaginado');
                    page.find("div").remove();
                    var html = "";
                    html = "<div class='pagination-container'><ul class='pagination'>";
                    for (var i = 1; i < data.Resultado.cantPage + 1; i++) {
                        if (i == data.Resultado.pageView) {
                            html = html + "<li class='active'><a>" + i + "</a></li>"
                        }
                        else {
                            //html = html + "<li class=''><a href='/Historico/Index?page=" + i + "'>" + i + "</a></li>"
                            html = html + "<li class=''><a href='javaScript:ViewGridJson(" + i + "," + $("#tblPersonGrid_length").val() + ")'>" + i + "</a></li>"
                        }
                    }

                    html = html + "</ul></div>";
                    page.append(html);

                    //LimpiarCampos();

                    var tab1 = document.getElementById("tab-1");
                    var tab2 = document.getElementById("tab-2");
                    var ltab1 = document.getElementById("litab1");
                    var ltab2 = document.getElementById("litab2");

                    tab2.classList.remove("active");
                    tab1.classList.add("active");
                    ltab2.classList.remove("active");
                    ltab1.classList.add("active");

                }
                else {
                    var table = $('#tblHistoricoGrid');
                    table.find("tbody tr").remove();
                    $("#tblHistoricoGrid_info").html("");
                    var page = $('#hiPaginado');
                    page.find("div").remove();
                }
            }

        },
        error: function (request, status, error) {
            alert("Inconveniente al cargar Grilla");
        },
    });
}

