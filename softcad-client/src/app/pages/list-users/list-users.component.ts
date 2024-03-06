import { Component } from '@angular/core';

@Component({
  selector: 'app-users-list',
  templateUrl: './list-users.component.html',
  styleUrl: './list-users.component.scss'
})
export class UsersListComponent {
  
  print() {
    window.print();
  }

}
