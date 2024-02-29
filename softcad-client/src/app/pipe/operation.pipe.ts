import { Pipe, PipeTransform } from '@angular/core';
import { Log } from '../models/log';

@Pipe({
  name: 'operation'
})
export class OperationPipe implements PipeTransform {

  transform(items: Log[], operacao: string): Log[] {
    if (!items) return [];
    if (!operacao) return items;
    if(operacao == "Selecionar") return items;

    operacao = operacao.toUpperCase();
    
    return items.filter(item => { 
      return item.operation.toUpperCase().includes(operacao);
    });
  }
}

