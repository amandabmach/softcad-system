import { Usuario } from '../../../models/usuario';
import { Component, OnInit} from '@angular/core';
import { Subscription } from 'rxjs';
import { UsersRequestService } from '../../../services/requests/users-request.service';
import { SearchService } from '../../../services/search.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrl: './table.component.scss'
})
export class TableComponent implements OnInit {

  usersChanged!: Subscription;
  visibility!: boolean;
  userSelected: any;
  users: Usuario[] = [];
  filteredData: any[] = [];
  isPage: boolean = false;
  
  constructor(
    private service: UsersRequestService,
    private search: SearchService, 
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getUsers(); 
    this.localRoute();

    this.usersChanged = this.service.changed.subscribe(() => {
      this.getUsers();
    });

    this.filteredData = this.users;

    this.search.changedSearch.subscribe(() => {
      this.filteredData = this.filterUsers();
    });
  }

  localRoute(){
    this.router.events.subscribe(() =>{
      if(this.router.url === '/home'){
        this.isPage = true;
      } else{
        this.isPage = false;
      }
    });
  }

  filterUsers() {
    const searchString = this.search.conteudo.toUpperCase();
    return this.users.filter(user => user.nome.toUpperCase().includes(searchString));
  }
  
  getUsers() {
    this.service.getUsersByAdmin().subscribe((dados: any[]) => {
      this.users = dados.map(user => {
        if (user.status === true) {
          return {...user, status: 'Ativo'};
        } else {
          return {...user, status: 'Inativo'}
        }
      });
      this.filteredData = this.users;
    });
  }

  userClicked(user: any){
    this.userSelected = user;
    this.visibility = true;
  }

  ngOnDestroy(): void {
    if (this.usersChanged) {
      this.usersChanged.unsubscribe();
    }
  }
 
}
