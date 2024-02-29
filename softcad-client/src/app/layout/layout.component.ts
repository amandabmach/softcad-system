import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.scss'
})
export class LayoutComponent implements OnInit{

  isPage: boolean = false; 

  constructor(
    private router: Router
  ){ }

  ngOnInit(){
    this.router.events.subscribe(() =>{
      if(this.router.url === '/home'){
        this.isPage = true;
      } else{
        this.isPage = false;
      }
    });
  }
}
