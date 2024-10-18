import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';

import {environment} from "../../../environments/environment";
import {Observable} from "rxjs";
import {Empleado} from "../models/Empleado";

@Injectable({
  providedIn: 'root'
})
export class EmpleadoService {

  url: string;
  headers: HttpHeaders;

  constructor(
    private http: HttpClient,
  ) {
    this.url = environment.url;
    this.headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8; multipart/form-data',
      'Cache-Control': 'no-cache',
      Pragma: 'no-cache',
      Expires: 'Sat, 01 Jan 2000 00:00:00 GMT'
    });
  }


  obtenerEmpleados(): Observable<any> {
    return this.http.get<any>(`${this.url}/empleado`);
  }

  crearEmpleado(empleado: Empleado): Observable<Empleado> {
    return this.http.post<Empleado>(`${this.url}/empleado`, empleado);
  }

  actualizarEmpleado(empleado: Empleado): Observable<void> {
    return this.http.put<void>(`${this.url}/empleado/${empleado.id}`, empleado);
  }

  eliminarEmpleado(id: number): Observable<void> {
    return this.http.delete<void>(`${this.url}/empleado/${id}`);
  }

  obtenerEmpleadoPorId(id: number): Observable<any> {
    return this.http.get<any>(`${this.url}/empleado/${id}`);
  }


}
