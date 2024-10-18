import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  Inject,
  OnInit,
  ViewChild,
  OnDestroy
} from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {animate, style, transition, trigger} from '@angular/animations';
import {BehaviorSubject, Subscription} from "rxjs";
import { NgbModalRef} from "@ng-bootstrap/ng-bootstrap";
import {SelectionModel} from "@angular/cdk/collections";
import {Empleado} from "../../shared/models/Empleado";
import {ColumnaFiltro} from "../../shared/models/Tabla";
import {EmpleadoService} from "../../shared/services/empleado.service";
import {Router} from "@angular/router";
import {HotToastService} from "@ngneat/hot-toast";

import Swal from 'sweetalert2';


@Component({
  selector: 'app-tbl-listado-usuario',
  templateUrl: './tbl-listado-empleado.component.html',
  styleUrls: ['./tbl-listado-empleado.component.scss'],
  animations: [
    trigger('slideFromBottom', [
      transition('void => *', [
        style({ opacity: 0, transform: 'translateY(15px)' }),
        animate('300ms {{delay}}ms ease-out', style({ transform: 'translateY(0%)', opacity: 1 }, ))
      ], { params: { delay: 10 } })
    ])
  ]
})
export class TblListadoEmpleadoComponent implements OnInit, AfterViewInit, OnDestroy {

  _loading = new  BehaviorSubject<boolean>(false);
  _collection = new BehaviorSubject<Empleado[]>([]);

  displayedColumns: string[];
  dataSource : MatTableDataSource<Empleado>;
  selection: SelectionModel<Empleado>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  loading: boolean;

  // subscriptions
  modalRef: NgbModalRef | undefined;
  subscriptions: Subscription[] = [];

  columnas: ColumnaFiltro[] = [
    {key:'select',canFilter: false, canShow: true, filter:false, nombre:'', selected: false, export: false, show: true, type: 'any'},
    // {key:'activo',canFilter: false, canShow: true, filter:false, nombre:'Activo', selected: false, export: true, show: true, type: 'any'},
    // {key:'idEmpleado',canFilter: true, canShow: true, filter:true, nombre:'Nombre', selected: false, export: true, show: true, type: 'Date'},
    {key:'nombres',canFilter: true, canShow: true, filter:true, nombre:'Nombres', selected: false, export: true, show: true, type: 'string'},
    {key:'apellidos',canFilter: true, canShow: true, filter:true, nombre:'Apellidos', selected: false, export: true, show: true, type: 'int'},
    {key:'fechaNacimiento',canFilter: true, canShow: true, filter:true, nombre:'F. Nac.', selected: false, export: true, show: true, type: 'float'},
    {key:'fechaIngreso',canFilter: true, canShow: true, filter:true, nombre:'F. Ing.', selected: false, export: true, show: true, type: 'float'},
    {key:'sueldo',canFilter: true, canShow: true, filter:true, nombre:'Sueldo', selected: false, export: true, show: true, type: 'string'},
    {key:'afp',canFilter: true, canShow: true, filter:true, nombre:'AFP', selected: false, export: true, show: true, type: 'string'},
    {key:'cargo',canFilter: true, canShow: true, filter:true, nombre:'Cargo', selected: false, export: true, show: true, type: 'string'},
    {key:'status',canFilter: true, canShow: true, filter:true, nombre:'Estado', selected: false, export: true, show: true, type: 'DateTime'},
  ];


  columnasFiltar: ColumnaFiltro[] = [];
  searchFocus: boolean = false;
  filtrarTodos: boolean = false;


  constructor(
    @Inject(ChangeDetectorRef) private readonly cdr: ChangeDetectorRef,
    private api: EmpleadoService,
    private router: Router,
    private toast: HotToastService,
  ) {
    this.loading = false;
    this.displayedColumns = this.columnas.map(x => x.key!);
    this.dataSource = new MatTableDataSource<Empleado>([]);
    this.selection = new SelectionModel<Empleado>(false, []);
  }

  ngOnInit(): void {
    this.columnasFiltar = this.columnas.filter(x => x.canFilter && x.filter && x.canShow && x.show);
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.collection();
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => s.unsubscribe());
    this.modalRef?.close();
  }


  /**********************************************************************************************************
   * Getters
   */
  get showEdit(): boolean{
    return this.selection.hasValue() && this.selection.selected.length === 1;
  }

  get count(): number{
    return this.dataSource.data.length;
  }

  get getDisplayedColumns(): string[]{
    return this.columnas.filter(c => c.show).map(x => x.key!);
  }

  get getCountColumnasFiltrar(): number{
    return this.columnasFiltar.filter(x => x.show && x.filter).length;
  }

  get getCountColumnasShow(): number{
    return this.columnasFiltar.filter(x => x.show).length;
  }

  /**************************************************************************************************************
   * Obtener lista de caja del cliente
   * @private
   *
   */
  private collection(): void{

    this.loading = true;
    this._loading.next(true);

    const sub = this.api.obtenerEmpleados().subscribe((res: any) => {
      this.dataSource.data = res.data;
      this._collection.next(res);

      this.loading = false;
      this._loading.next(false);
    }, (error: any) => {
      this.loading = false;
      this._loading.next(false);
    });
    this.subscriptions.push(sub);
  }

  // Events
  evtOnReload(): void{
    this.collection();
    this.selection.clear();
  }

  evtInput(event: Event): void{
    const filterValue = (event.target as HTMLInputElement).value;
    this.searchFocus = true;
  }

  evtOutput(event: Event): void{
    const filterValue = (event.target as HTMLInputElement).value;
    this.searchFocus = !!filterValue;
  }

  evtApplyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    if(this.columnas.filter(x => x.filter && x.show).length){
      this.dataSource.filter = filterValue.trim().toLowerCase();
    }
  }

  evtToggleFilter(key: string, event: Event): void{
    const filterValue = (event.target as HTMLInputElement).checked;
    const column = this.columnasFiltar.find(x => x.key === key);
    if(column){
      column.selected = filterValue;
    }
    this.filtrarTodos = this.columnasFiltar.length === this.columnasFiltar.filter(x => x.selected).length;

    this.dataSource._updateChangeSubscription();
  }

  evtToggleAllFilter(event: Event): void{
    const filterValue = (event.target as HTMLInputElement).checked;
    this.filtrarTodos = filterValue;
    this.columnasFiltar.forEach(x =>{
      x.selected = filterValue;
    });
    this.dataSource._updateChangeSubscription();
  }


  evtOnAdd(): void{
    this.router.navigate(['/empleado/nuevo'])
  }

  evtOnUpdate(): void{
    const data = this.selection.selected[0]!;
    this.router.navigate([`/empleado/editar/${data.id}`])
  }

  evtOnDelete(): void{
    const data = this.selection.selected[0]!;

    Swal.fire({
      title: '¿Estás seguro?',
      text: "¡No podrás recuperar este empleado después de eliminarlo!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: '¡Sí, eliminarlo!'
    }).then((result) => {
      if (result.isConfirmed) {
        const sub = this.api.eliminarEmpleado(data.id).subscribe(
          () => {
            this.toast.success('El empleado se elimino con exito');
            this.evtOnReload();
          },
          (error) => {
            console.error('Error al eliminar empleado:', error);
            this.toast.error('No se pudo eliminar el empleado seleccionado');
          }
        );
        this.subscriptions.push(sub);
      }
    });
  }

}
