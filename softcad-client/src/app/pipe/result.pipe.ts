import { Pipe, PipeTransform } from '@angular/core';
import { Log } from '../models/log';

@Pipe({
  name: 'result'
})
export class ResultPipe implements PipeTransform {

  transform(items: Log[], result: string): Log[] {
    if (!items) return [];
    if (!result) return items;
    if(result == "Selecionar") return items;

    result = result.toUpperCase();

    return items.filter(item => { 
      return item.result.toUpperCase().includes(result);
    });
  }
}

