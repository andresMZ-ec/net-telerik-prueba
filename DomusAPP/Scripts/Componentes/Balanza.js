var intervaloBalanza;
var clickCount = 0;
var actualizacionEnProceso = false;

// Configuración
var INTERVALO_ACTUALIZACION = 10000; // 2 segundos
var TIMEOUT_REQUEST = 5000; // 5 segundos timeout

function StopCapture() {
    if (intervaloBalanza) {
        clearInterval(intervaloBalanza);
        intervaloBalanza = null;
    }
}

function GetBalanceWeight(tboxUpdaterID, IP, PORT, intervalTime) {
    if (tboxUpdaterID) {
        const data = {
            "ipAddress": IP,
            "port": PORT,
            "inputId": tboxUpdaterID
        }


        intervaloBalanza = setInterval(function () {
            GetWeightFromBalance(data);
        }, intervalTime);
    } else {
        console.error("Campo para actualizar no ha sido proporcionado");
    }
}

function GetWeightFromBalance(parameters) {
    const { inputId, data } = parameters

    $.ajax({
        type: "POST",
        url: "/Handlers/BalanzaService.asmx/ObtenerPesoBalanza",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(data),
        timeout: TIMEOUT_REQUEST,
        success: (response) => {
            try {
                const result = response.d;
                const input = $find(inputId);

                if (result.Status = 200) {
                    if (input == null)
                        throw new Error("No se encontró el control con ID:" + inputId);

                    const peso = result.Peso.toFixed(2);
                    input.set_value(peso);
                } else {
                    throw new Error(result.Mensaje);
                }
            } catch (error) {
                mostrarError(error);
            }
        },
        error: (xhr, status, error) => {
            console.error('Error AJAX:', {
                status: status,
                error: error,
                responseText: xhr.responseText,
                statusCode: xhr.status
            });
            mostrarError('Error de conexión: ' + status);
        }
    })
}

function iniciarActualizacionBalanza() {
    // Realizar primera lectura inmediatamente
    //actualizarPesoBalanza();

    //// Configurar intervalo de actualización
    //intervaloBalanza = setInterval(function () {
    //    actualizarPesoBalanza();
    //}, INTERVALO_ACTUALIZACION);

}

function detenerActualizacionBalanza() {
    if (intervaloBalanza) {
        clearInterval(intervaloBalanza);
        intervaloBalanza = null;
        console.log('Actualización de balanza detenida');
    }
}

function actualizarPesoBalanza(data) {

    if (!(data instanceof FormData)) {
        console.error("La data no es de tipo FormData")
    }

    $.ajax({
        type: "POST",
        url: "/Handlers/BalanzaService.asmx/ObtenerPesoBalanza",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: data,
        timeout: TIMEOUT_REQUEST,
        success: function (response) {
            var resultado = response.d;

            if (resultado.Exitoso) {
                console.log($find('<%= txtPeso.ClientID %>'))
                // Actualizar UI - Solo el valor del input, no el control completo
                var $input = $find("<%= txtPeso.ClientID %>").get_textBoxValue();
                var nuevoValor = resultado.Peso.toFixed(2);

                // Actualizar solo si el valor cambió
                if ($input !== nuevoValor) {
                    $find("<%= txtPeso.ClientID %>").set_value(nuevoValor);
                }

                // Actualizar elementos adicionales
                $('#lblUnidad').text(resultado.Unidad);
                $('#lblFechaHora').text('Última actualización: ' + resultado.FechaHora);

                // Actualizar indicador de estado
                $('#lblEstado').text('Conectado');
                $('#spanIndicador').css('background-color', 'green');
            } else {
                mostrarError(resultado.MensajeError);
            }
        },
        error: function (xhr, status, error) {
            console.error('Error al obtener peso:', error);
            mostrarError('Error de conexión: ' + status);
        },
        complete: function () {
            actualizacionEnProceso = false;
        }
    });
}

function mostrarError(mensaje) {
    $('#lblEstado').text('Error: ' + mensaje);
    $('#spanIndicador').css('background-color', 'red');
    console.error(mensaje);
}

// Función para pausar/reanudar manualmente (opcional)
function pausarActualizacion() {
    detenerActualizacionBalanza();
}

function reanudarActualizacion() {
    if (!intervaloBalanza) {
        iniciarActualizacionBalanza();
    }
}