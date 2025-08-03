import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable, map } from "rxjs";
import * as XLSX from 'xlsx';
@Injectable({
    providedIn: 'root'
})
export class codigoService {
    env: string = environment.url + 'product/getbycode?code=';
    constructor(private http: HttpClient) {
    }

    getListarUsuario(codigo: string): Observable<{
        data: [],
        respuesta: boolean
    }> {
        let response: any = { data: null, respuesta: false }
        return this.http.get<any>(this.env + codigo).pipe(
            map((r) => {
                try {
                    response.data = r;
                    response.respuesta = true;
                    return response;
                } catch (error) {
                    response.respuesta = false;
                    return response;
                }

            })
        )
    }


    readExcel(codigo: string): Observable<{
        message: string,
        data: any,
        respuesta: boolean
    }> {
        // Ruta del archivo Excel
        const csvUrl = 'assets/csv/STADA.csv';
        let dataArray: any = []

        // Hacer la solicitud HTTP para obtener el archivo Excel
        return this.http.get(csvUrl, { responseType: 'text' }).pipe(
            map(
                (data) => {
                    let csvToRowArray = data.split("\n");
                    let obj = csvToRowArray.find(x => x.includes(codigo));
                    dataArray = obj?.split(";");
                    if (!dataArray?.length) {
                        return { message: "Codigo encontrado", data: null, respuesta: false };
                    }

                    return { message: "Codigo encontrado", data: dataArray, respuesta: true }
                }
            ));
    }

}
