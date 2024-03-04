import { Component, OnInit} from '@angular/core';
import { AdminsRequestService } from '../../../services/requests/admins-request.service';
import { SearchService } from '../../../services/search.service';
import { AdministradorService } from '../../../services/administrador.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-header',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent implements OnInit{
  admin: any;
  photoProfile: any;
  sidebarVisible = false;
  changed!: Subscription;

  constructor(
    private service: AdminsRequestService,
    private search: SearchService,
    private adminservice: AdministradorService
  ){}

  toggleSidebar() {
    this.sidebarVisible = !this.sidebarVisible;
  }
  
  ngOnInit(): void {
    this.getAdministrador();
  }

  getAdministrador(){
    this.service.getAdministrador().subscribe(dados => {
      var admin = dados;
      if(admin.photo != null){
        this.service.getPhotoProfile().subscribe(foto =>{
          const url = URL.createObjectURL(foto);
          this.adminservice.setAdminPhoto(admin, url);
          this.admin = this.adminservice.getAdministrador();
        })
      } else{
        this.adminservice.setAdministrador(admin);
        this.admin = this.adminservice.getAdministrador();
      }
    })
  }

  buscar(event: any): void {
    this.search.buscarTabela(event.target.value);
  }
}
