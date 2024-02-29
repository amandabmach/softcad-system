import { Pipe, PipeTransform } from '@angular/core';
import { Log } from '../models/log';

@Pipe({
  name: 'user'
})
export class UserPipe implements PipeTransform {

  transform(items: Log[], user: string): Log[] {
    if (!items) return [];
    if (!user) return items;

    user = user.toUpperCase();

    return items.filter(item => { 
      return item.executorEmail.toUpperCase().includes(user);
    });
  }
}
