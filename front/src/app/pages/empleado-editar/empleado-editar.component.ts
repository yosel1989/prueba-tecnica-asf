import {AfterViewInit, Component, OnDestroy, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {EmpleadoService} from "../../shared/services/empleado.service";
import {Subscription} from "rxjs";
import {HotToastService} from "@ngneat/hot-toast";
import {ActivatedRoute, ParamMap, Router} from "@angular/router";
import {Empleado} from "../../shared/models/Empleado";
import {DatePipe} from "@angular/common";

@Component({
  selector: 'app-usuario-editar',
  templateUrl: './empleado-editar.component.html',
  styleUrls: ['./empleado-editar.component.scss']
})
export class EmpleadoEditarComponent implements OnInit, AfterViewInit, OnDestroy {

  formGroup: FormGroup;
    loadingSubmit: boolean;

  subscriptions: Subscription[] = [];

  empleado: Empleado | undefined;

  constructor(
    private formBuilder: FormBuilder,
    private userService: EmpleadoService,
    private toast: HotToastService,
    private router: Router,
    private route: ActivatedRoute,
    private datePipe: DatePipe
  ) {

    this.loadingSubmit = true;

    this.formGroup = this.formBuilder.group({
      nombres: new FormControl(null, Validators.required),
      apellidos: new FormControl(null, Validators.required),
      fechaNacimiento: new FormControl(null, Validators.required),
      fechaIngreso: new FormControl(null, Validators.required),
      idAFP: new FormControl('', Validators.required),
      idCargo: new FormControl('', Validators.required),
      sueldo: new FormControl(0, [Validators.required, Validators.min(0)]),
      status: new FormControl(1, Validators.required),
    });
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe( (params: ParamMap) => {
      const idUser = parseInt(params.get('id')!, 10);
      this.getUserById(idUser);
    });
  }

  ngAfterViewInit(): void {
  }

  ngOnDestroy(): void {

    this.subscriptions.forEach(s => s.unsubscribe());

  }

  // Getter
  get f(): any{
    return this.formGroup.controls;
  }

  get model(): any{
    return {
      id: this.empleado?.id ?? 0,
      nombres: this.f.nombres.value,
      apellidos: this.f.apellidos.value,
      fechaNacimiento: this.f.fechaNacimiento.value,
      fechaIngreso: this.f.fechaIngreso.value,
      idAFP: parseInt(this.f.idAFP.value, 10),
      idCargo: parseInt(this.f.idCargo.value, 10),
      sueldo: parseFloat(this.f.sueldo.value),
      status: !!parseInt(this.f.status.value, 10),
    }
  }

  // Events
  evtOnSubmit(): void{
    if(this.formGroup.invalid){
      this.toast.error('Debe llenar todos los campos');
      return;
    }

    this.loadingSubmit = true;
    const sub = this.userService.actualizarEmpleado(this.model).subscribe(()=>{
      this.toast.success('Se actualizo los datos del empleado con exito');
      this.loadingSubmit = false;
      this.router.navigate(['/empleado'])
    }, error => {
      this.toast.error('No se pudo actualizar los datos del empleado');
      this.loadingSubmit = false;
    });
    this.subscriptions.push(sub);

  }

  getUserById(id: number): void{
    const subs = this.userService.obtenerEmpleadoPorId(id).subscribe((res: any) => {
      this.empleado = res.data;
      this.formGroup.patchValue({
        nombres: this.empleado?.nombres,
        apellidos: this.empleado?.apellidos,
        fechaNacimiento: this.empleado ? this.datePipe.transform(new Date(this.empleado?.fechaNacimiento), 'yyyy-MM-dd') : null,
        fechaIngreso: this.empleado ? this.datePipe.transform(new Date(this.empleado?.fechaIngreso), 'yyyy-MM-dd') : null,
        idAFP: this.empleado?.idAFP,
        idCargo: this.empleado?.idCargo,
        sueldo: this.empleado?.sueldo,
        status: this.empleado?.status ? 1 : 0,
      });
    }, error => {
      this.toast.error('No se pudo obtener los datos del usuario');
    });
    this.subscriptions.push(subs);
  }


}
