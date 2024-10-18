export class ColumnaFiltro{
  key: string | undefined;
  canFilter: boolean = false;
  canShow: boolean = true;
  filter: boolean = false;
  nombre: string | undefined;
  selected: boolean = false;
  show: boolean = true;
  export: boolean = true;
  type: 'time' | 'DateTime' | 'Date' | 'string' | 'int' | 'float' | 'boolean' | 'any' = 'any';
  constructor() {
  }
}
