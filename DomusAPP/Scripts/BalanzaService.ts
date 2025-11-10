enum STATUS {
    OK = 200,
    NOTFOUND = 400,
    ERROR = 500,
}

interface BalanceRequest {
    inputId: string;
    ipAddress: string;
    port: number;
}

interface BalanceResponse {
    status: STATUS;
    message: string;
    response: number;
    date: Date;
}

const CaptureWeightBalance = (data: BalanceRequest): void => {
    if (data !== null) {
        const { inputId } = data;
        const input = document.getElementById(inputId)

        if (input !== null) {
            return;
        }
        console.error("No se pudo encontrar el control con el ID: " + inputId);
    }
    else {
        console.error("No se proporciono la información requerida para la extracción de datos")
    }
}

const GetWeightFromBalance = async (data: BalanceRequest) => {
    const { inputId } = data;
    const url = "/Handlers/BalanzaService.asmx/ObtenerPesoBalanza";

    try {
        const input = document.getElementById(inputId)

        const response = await fetch(url, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            body: JSON.stringify({ d: data })
        })

        if (!response.ok) {
            const errorText = await response.text()
            console.error(`Error HTTP ${response.status}: ${errorText}`);
            return;
        }

        const jsonResponse = await response.json();
        const result: BalanceResponse = jsonResponse.d;

        input.
    }
    catch (error) {
        console.error("Error al obtener peso de balanza:", error);
    }
}