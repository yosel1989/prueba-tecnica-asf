import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmpleadoListadoRoutingModule } from './empleado-listado-routing.module';
import {EmpleadoListadoComponent} from "./empleado-listado.component";
import {TblListadoEmpleadoModule} from "../../components/tbl-listado-empleado/tbl-listado-empleado.module";


@NgModule({
  declarations: [
    EmpleadoListadoComponent
  ],
  imports: [
    CommonModule,
    EmpleadoListadoRoutingModule,
    TblListadoEmpleadoModule
  ],
  providers:[],
  exports: [EmpleadoListadoComponent],
  bootstrap: [EmpleadoListadoComponent]
})
export class EmpleadoListadoModule { }
