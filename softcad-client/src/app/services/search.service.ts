import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  changedSearch = new Subject<void>();
  conteudo!: string;

  buscarTabela(content: string){
    this.conteudo = content;
    this.changedSearch.next();
  }
}
