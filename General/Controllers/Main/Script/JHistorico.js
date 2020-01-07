(function () {

    var historico = document.getElementById("hisearch");
    historico.addEventListener("click", SearchPerson, "");

    var tab3 = document.getElementById("litab3")
    tab3.addEventListener("click", Tab3Click, "");

    var documentypeId = document.getElementById("DocumentypeId");
    documentypeId.addEventListener("change", ChangeDocument, "");

    var grabar = document.getElementById("btnSave");
    grabar.addEventListener("click", SaveHistory, "");

    var nuevo = document.getElementById("btnNew");
    nuevo.addEventListener("click", NewHistory, "");

    var filtro = document.getElementById("btnFiltro");
    filtro.addEventListener("click", FoundGrid, "");

    var changePerson = document.getElementById("tblPersonGrid_length");
    changePerson.addEventListener("change", FoundGrid, "");
    
    var changeHistorico = document.getElementById("tblHistoricoGrid_length");
    changeHistorico.addEventListener("change", ViewGridH, "");

    Habilitar(true);
}());

function Tab3Click() {
    LimpiarTab3(false);
}

function ViewGridH() {

    var personaId = document.getElementById('PersonaId');

    var paginacion = {
        countrow: $("#tblHistoricoGrid_length option:selected").text(),
        PersonaId: personaId.value
    };

    var urlT = document.getElementById("urlId").value + "Historial/CargarGrilla";
    //'../Historial/CargarGrilla'
    $.ajax({
        url: urlT,
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
                            "<td class='col-xs-12 col-md-3'>" + parseJsonRow(result.Observaciones) + "</td>" +
                            "<td class='col-xs-12 col-md-3'>" + parseJsonRow(result.Otros) + "</td>" +
                            "<td class='col-xs-12 col-md-2'>" + parseJsonDate(result.Fechacreacion) + "</td></tr>");
                    });

                    var page = $('#hiPaginadoHistorico');
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
                    var tab3 = document.getElementById("tab-3");
                    var ltab1 = document.getElementById("litab1");
                    var ltab2 = document.getElementById("litab2");
                    var ltab3 = document.getElementById("litab3");

                    tab1.classList.remove("active");
                    tab3.classList.remove("active");
                    tab2.classList.add("active");
                    ltab1.classList.remove("active");
                    ltab3.classList.remove("active");
                    ltab2.classList.add("active");
                }
                else {
                    var table = $('#tblHistoricoGrid');
                    table.find("tbody tr").remove();
                    $("#tblHistoricoGrid_info").html("");
                    var page = $('#hiPaginadoHistorico');
                    page.find("div").remove();
                }
            }

        },
        error: function (request, status, error) {
            alert("Inconveniente al cargar Grilla");
        },
    });
}

function ViewGridJson(page, countrow) {
    var Fdocumento = document.getElementById("NroDocumentoHistorico");
    var Ftipodocumento = document.getElementById("TDocumentoHistorico");
    var Fappaterno = document.getElementById("NombreCompletoHistorico");

    var paginacion = {
        page: page,
        countrow: countrow,
        TipodocumentoId: Ftipodocumento.value,
        Nrodocumento: Fdocumento.value,
        Apellidopaterno: Fappaterno.value,
    };

    var urlG = document.getElementById("urlId").value + "Historial/CargarGrilla";
    //../Historial/CargarGrilla
    $.ajax({
        url: urlG,
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
                            "<td class='col-xs-12 col-md-3'>" + parseJsonRow(result.Observaciones) + "</td>" +
                            "<td class='col-xs-12 col-md-3'>" + parseJsonRow(result.Otros) + "</td>" +
                            "<td class='col-xs-12 col-md-2'>" + parseJsonDate(result.Fechacreacion) + "</td></tr>");
                    });

                    var page = $('#hiPaginadoHistorico');
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
                    var tab3 = document.getElementById("tab-3");
                    var ltab1 = document.getElementById("litab1");
                    var ltab2 = document.getElementById("litab2");
                    var ltab3 = document.getElementById("litab3");

                    tab1.classList.remove("active");
                    tab3.classList.remove("active");
                    tab2.classList.add("active");
                    ltab1.classList.remove("active");
                    ltab3.classList.remove("active");
                    ltab2.classList.add("active");

                }
                else {
                    var table = $('#tblHistoricoGrid');
                    table.find("tbody tr").remove();
                    $("#tblHistoricoGrid_info").html("");
                    var page = $('#hiPaginadoHistorico');
                    page.find("div").remove();
                }
            }

        },
        error: function (request, status, error) {
            alert("Inconveniente al cargar Grilla");
        },
    });

}

//Persona


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

    var urlB = document.getElementById("urlId").value + "Historial/CargarBusquedaGrilla";
    //../Historial/CargarBusquedaGrilla
    $.ajax({
        url: urlB,
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
                            html = html + "<li class=''><a href='javaScript:FoundGridJson(" + i + "," + $("#tblPersonGrid_length").val() + ")'>" + i + "</a></li>"
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

function FoundGridJson(page, countrow) {

    var Fdocumento = document.getElementById("FiltroPerson_Nrodocumento");
    var Ftipodocumento = document.getElementById("FiltroDocumentypeId");
    var Fappaterno = document.getElementById("FiltroPerson_Apellidopaterno");

    var filtro = {
        TipodocumentoId: Ftipodocumento.value,
        Nrodocumento: Fdocumento.value,
        Apellidopaterno: Fappaterno.value,
        page: page,
        countrow: countrow,
    };

    var urlFG = document.getElementById("urlId").value + "Historial/CargarBusquedaGrilla";
    //../Historial/CargarBusquedaGrilla
    $.ajax({
        url: urlFG,
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
                            html = html + "<li class=''><a href='javaScript:FoundGridJson(" + i + "," + $("#tblPersonGrid_length").val() + ")'>" + i + "</a></li>"
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

    document.getElementById("PersonaId").value = "";

    var urlP = document.getElementById("urlId").value + "Persona/CargarPerson";
    //'../Persona/CargarPerson'
    $.ajax({
        url: urlP,
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

                    document.getElementById("PersonaId").value = personaId;
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

function DeleteHistorico(id) {
    var resultado = {
        idHistorico: $(id).data('assigned-id')
    };
    var personaId = document.getElementById('PersonaId');

    var urlD = document.getElementById("urlId").value + "Historial/DeleteHistorico";
    //../Historial/DeleteHistorico
    $.ajax({
        url: urlD,
        type: 'POST',
        data: JSON.stringify(resultado),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.Resultado == "OK") {
                    $("#success").modal('show');
                    $("#SuccessResult").text('El registro se elimino exitosamente');
                    ViewGridHistorico(personaId.value);
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

function ViewGridHistorico(personaId) {

    var paginacion = {
        countrow: $("#tblHistoricoGrid_length option:selected").text(),
        PersonaId: personaId
    };
    var urlCG = document.getElementById("urlId").value + "Historial/CargarGrilla";
    //../Historial/CargarGrilla
    $.ajax({
        url: urlCG,
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
                            "<td class='col-xs-12 col-md-3'>" + parseJsonRow(result.Observaciones) + "</td>" +
                            "<td class='col-xs-12 col-md-3'>" + parseJsonRow(result.Otros) + "</td>" +
                            "<td class='col-xs-12 col-md-2'>" + parseJsonDate(result.Fechacreacion) + "</td></tr>");
                    });

                    var page = $('#hiPaginadoHistorico');
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
                    var tab3 = document.getElementById("tab-3");
                    var ltab1 = document.getElementById("litab1");
                    var ltab2 = document.getElementById("litab2");
                    var ltab3 = document.getElementById("litab3");

                    tab1.classList.remove("active");
                    tab3.classList.remove("active");
                    tab2.classList.add("active");
                    ltab1.classList.remove("active");
                    ltab3.classList.remove("active");
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

function LimpiarTab3(condicion)
{
    document.getElementById('historicoId').value = "";
    document.getElementById('PersonaId').value = "";
    document.getElementById('Historicos_Paquetes').value = "";
    document.getElementById('Historicos_Costo').value = "";
    
    document.getElementById('DocumentypeId').value = "";
    document.getElementById('Documento').value = "";
    document.getElementById('Paciente').value = "";
    document.getElementById('Frecuencia').value = "";
    

    $("#Historicos_Diagnostico").val("");
    $("#Historicos_Observaciones").val("");
    $("#Historicos_Otros").val("");
    
    $("#Historicos_descElectroanalgesico").val("");
    $("#Historicos_descElectroestimulacion").val("");
    $("#Historicos_descMagnetoterapia").val("");
    $("#Historicos_descUltrasonido").val("");
    $("#Historicos_descTCombinada").val("");
    $("#Historicos_descLaserterapia").val("");
    //Agente Térmico
    document.getElementById("Historicos_checkCaliente").checked = condicion;
    document.getElementById("Historicos_checkFria").checked = condicion;
    document.getElementById("Historicos_checkContraste").checked = condicion;
    //Agente Electrofísico
    document.getElementById("Historicos_checkElectroestimulacion").checked = condicion;
    document.getElementById("Historicos_checkMagnetoterapia").checked = condicion;
    document.getElementById("Historicos_checkUltrasonido").checked = condicion;
    document.getElementById("Historicos_checkTCombinada").checked = condicion;
    document.getElementById("Historicos_checkLaserterapia").checked = condicion;
    //Maniobras Terapéuticas
    document.getElementById("Historicos_checkRelajante").checked = condicion;
    document.getElementById("Historicos_checkDescontracturante").checked = condicion;
    document.getElementById("Historicos_checkEstiramiento").checked = condicion;
    document.getElementById("Historicos_checkFortalecimiento").checked = condicion;
    document.getElementById("Historicos_checkRPG").checked = condicion;
    document.getElementById("Historicos_checkActivacion").checked = condicion;
    document.getElementById("Historicos_checkTAPE").checked = condicion;
    //Antecedentes.
    document.getElementById("Historicos_checkRCaida").checked = condicion;
    document.getElementById("Historicos_checkEEmbarazada").checked = condicion;
    document.getElementById("Historicos_checkTDiabetes").checked = condicion;
    document.getElementById("Historicos_checkDCancer").checked = condicion;
    document.getElementById("Historicos_checkTEnfCardiaca").checked = condicion;
    document.getElementById("Historicos_checkRQuemadura").checked = condicion;
    document.getElementById("Historicos_checkPVarices").checked = condicion;
    document.getElementById("Historicos_checkHTA").checked = condicion;
    document.getElementById("Historicos_checkMarcapaso").checked = condicion;
    document.getElementById("Historicos_checkEOsteosintesis").checked = condicion;

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

    LimpiarTab3(false);
    //Habilitar los campos cuando es para editar.
    Habilitar(false);

    var urlCG = document.getElementById("urlId").value + "Historial/SearchHistorico";
    //../Historial/SearchHistorico
    $.ajax({
        url: urlCG,
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
                    var personaId = data.Resultado.Persona[0].PersonaId;

                    $("#DocumentypeId").val(tipodocumentoId);
                    $("#Documento").val(nroDocumento);
                    $("#Paciente").val(apPaterno + ' ' + apMaterno + ' ' + nombre);
                    document.getElementById('PersonaId').value = personaId;

                    if (data.Resultado.Historico.length > 0) {
                        var diagnostico = data.Resultado.Historico[0].Diagnostico;
                        var observaciones = data.Resultado.Historico[0].Observaciones;
                        var otros = data.Resultado.Historico[0].Otros;
                        var paquete = data.Resultado.Historico[0].Paquetes;
                        var costo = data.Resultado.Historico[0].Costo;
                        var frecuencia = data.Resultado.Historico[0].Frecuencia;
                        var historiaID = data.Resultado.Historico[0].HistoricoId;

                        $("#Historicos_Diagnostico").val(diagnostico);
                        $("#Historicos_Observaciones").val(observaciones);
                        $("#Historicos_Otros").val(otros);
                        $("#Frecuencia").val(frecuencia);
                        document.getElementById('historicoId').value = historiaID;
                        document.getElementById('Historicos_Paquetes').value = paquete;
                        document.getElementById('Historicos_Costo').value = costo;
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
                                case 10:
                                    document.getElementById("Historicos_checkRelajante").checked = condicion;
                                    break;
                                case 11:
                                    document.getElementById("Historicos_checkDescontracturante").checked = condicion;
                                    break;
                                case 12:
                                    document.getElementById("Historicos_checkEstiramiento").checked = condicion;
                                    break;
                                case 13:
                                    document.getElementById("Historicos_checkFortalecimiento").checked = condicion;
                                    break;
                                case 14:
                                    document.getElementById("Historicos_checkRPG").checked = condicion;
                                    break;
                                case 15:
                                    document.getElementById("Historicos_checkActivacion").checked = condicion;
                                    break;
                                case 16:
                                    document.getElementById("Historicos_checkTAPE").checked = condicion;
                                    break;
                                default:
                            }

                        });
                    }

                    if (data.Resultado.Antecedentes.length > 0) {
                        data.Resultado.Antecedentes.forEach(function (result) {
                            var condicion = result.Condicion;
                            switch (result.SubTramiteId) {
                                case 19:
                                    document.getElementById("Historicos_checkRCaida").checked = condicion;
                                    break;
                                case 20:
                                    document.getElementById("Historicos_checkEEmbarazada").checked = condicion;
                                    break;
                                case 21:
                                    document.getElementById("Historicos_checkTDiabetes").checked = condicion;
                                    break;
                                case 22:
                                    document.getElementById("Historicos_checkDCancer").checked = condicion;
                                    break;
                                case 23:
                                    document.getElementById("Historicos_checkTEnfCardiaca").checked = condicion;
                                    break;
                                case 24:
                                    document.getElementById("Historicos_checkRQuemadura").checked = condicion;
                                    break;
                                case 25:
                                    document.getElementById("Historicos_checkPVarices").checked = condicion;
                                    break;
                                case 26:
                                    document.getElementById("Historicos_checkHTA").checked = condicion;
                                    break;
                                case 27:
                                    document.getElementById("Historicos_checkMarcapaso").checked = condicion;
                                    break;
                                case 28:
                                    document.getElementById("Historicos_checkEOsteosintesis").checked = condicion;
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
    var historiaID = document.getElementById('historicoId');
    
    //
    var diagnostico = document.getElementById('Historicos_Diagnostico');
    var paquete = document.getElementById('Historicos_Paquetes');
    var costo = document.getElementById('Historicos_Costo');
    var frecuencia = document.getElementById('Frecuencia');
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
    var otros = document.getElementById('Historicos_Otros');
    
    //Antecedentes.
    var caida = document.getElementById('Historicos_checkRCaida');
    var embarazada = document.getElementById('Historicos_checkEEmbarazada');
    var diabetes = document.getElementById('Historicos_checkTDiabetes');
    var cancer = document.getElementById('Historicos_checkDCancer');
    var cardiaca = document.getElementById('Historicos_checkTEnfCardiaca');
    var quemadura = document.getElementById('Historicos_checkRQuemadura');
    var varices = document.getElementById('Historicos_checkPVarices');
    var hta = document.getElementById('Historicos_checkHTA');
    var marcapaso = document.getElementById('Historicos_checkMarcapaso');
    var osteosintesis = document.getElementById('Historicos_checkEOsteosintesis');

    var param = {
        HistoricoId: historiaID.value,
        PersonaId: personaId.value,
        Diagnostico: diagnostico.value,
        Paquetes: paquete.value,
        Costo: costo.value,
        Observaciones: observaciones.value,
        Otros: otros.value,
        Frecuencia: frecuencia.value,
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
        checkRCaida: caida.checked,
        checkEEmbarazada: embarazada.checked,
        checkTDiabetes: diabetes.checked,
        checkDCancer: cancer.checked,
        checkTEnfCardiaca: cardiaca.checked,
        checkRQuemadura: quemadura.checked,
        checkPVarices: varices.checked,
        checkHTA: hta.checked,
        checkMarcapaso: marcapaso.checked,
        checkEOsteosintesis: osteosintesis.checked,
    };

    var urlA = document.getElementById("urlId").value + "Historial/SaveHistorico";
    var urlE = document.getElementById("urlId").value + "Historial/EditHistorico";
    //../Historial/SaveHistorico
    //../Historial/EditHistorico
    if (historiaID.value == "") {
        $.ajax({
            url: urlA,
            type: 'POST',
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    if (data.Resultado == "OK") {
                        $("#success").modal('show');
                        $("#SuccessResult").text('El registro se grabo exitosamente');
                        ViewGridHistorico(personaId.value);

                        $("#TDocumentoHistorico").val($("#DocumentypeId").val());
                        $("#NroDocumentoHistorico").val($("#Documento").val());
                        $("#NombreCompletoHistorico").val($("#Paciente").val());

                        document.getElementById("FiltroDocumentypeId").value = "";
                        FoundGrid();
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
            url: urlE,
            type: 'POST',
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    if (data.Resultado == "OK") {
                        $("#success").modal('show');
                        $("#SuccessResult").text('El registro se actualizó exitosamente');
                        ViewGridHistorico(personaId.value);

                        $("#TDocumentoHistorico").val($("#DocumentypeId").val());
                        $("#NroDocumentoHistorico").val($("#Documento").val());
                        $("#NombreCompletoHistorico").val($("#Paciente").val());

                        document.getElementById("FiltroDocumentypeId").value = "";
                        FoundGrid();
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

function NewHistory() {
    Habilitar(false);
    LimpiarTab3(false);
}

function ChangeDocument() {
    Habilitar(true);
}

function Habilitar(condicion) {

    document.getElementById('Frecuencia').disabled = condicion;

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
    document.getElementById('Historicos_Otros').disabled = condicion;
    document.getElementById('Historicos_Observaciones').disabled = condicion;
    //Antecedentes.
    document.getElementById('Historicos_checkRCaida').disabled = condicion;
    document.getElementById('Historicos_checkEEmbarazada').disabled = condicion;
    document.getElementById('Historicos_checkTDiabetes').disabled = condicion;
    document.getElementById('Historicos_checkDCancer').disabled = condicion;
    document.getElementById('Historicos_checkTEnfCardiaca').disabled = condicion;
    document.getElementById('Historicos_checkRQuemadura').disabled = condicion;
    document.getElementById('Historicos_checkPVarices').disabled = condicion;
    document.getElementById('Historicos_checkHTA').disabled = condicion;
    document.getElementById('Historicos_checkMarcapaso').disabled = condicion;
    document.getElementById('Historicos_checkEOsteosintesis').disabled = condicion;
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
    var urlS = document.getElementById("urlId").value + "Historial/SearchPerson";
    //../Historial/SearchPerson

    $.ajax({
        url: urlS,
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





