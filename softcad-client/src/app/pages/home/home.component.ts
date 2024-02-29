import { UsersRequestService } from '../../services/requests/users-request.service';
import { Component, OnInit } from '@angular/core';
import { DashboardRequestService } from '../../services/requests/dashboard-request.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})

export class HomeComponent implements OnInit{

  month: number = 0;
  status: number = 0;
  users: number = 0;
  boards: any = []; 
  

  constructor(
    private service: DashboardRequestService, 
    private userService: UsersRequestService,
  ){}


  ngOnInit(): void {
    this.updateBoards();
    this.getMonth();
    this.getStatus();
    this.getAll();

    this.userService.changed.subscribe(()=>{ 
      this.getMonth();
      this.getStatus();
      this.getAll();     
    })
  }

  getMonth() {
    this.service.getUsersByMonth().subscribe(response => {
      this.month = response;
      this.updateBoards();
    });
  }
  
  getStatus(){
    this.service.getUsersByStatus().subscribe(response => {
      this.status = response;
      this.updateBoards();
    });
  }
  
  getAll(){
    this.service.getUsersByAdmin().subscribe(response => {
      this.users = response;
      this.updateBoards();
    });
  }

  updateBoards(){
    this.boards = [
      {id: 1, amount: this.users, text: 'Total de cadastros', color: '#415996'},
      {id: 2, amount: this.month, text: 'Cadastros no último mês', color: '#3F9D2F'},
      {id: 3, amount: this.status, text: 'Cadastros com pendência de revisão', color: '#C15959'}
    ]
  }

}
