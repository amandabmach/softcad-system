import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AdministradorService {

  id!: number;
  name!: string;
  email!: string;
  photo!: any;

  constructor() { }

  setAdminPhoto(data: any, foto: any){
    this.id = data.id;
    this.name = data.name;
    this.email = data.email;
    this.photo = foto;
  }

  setAdministrador(data: any){
    this.id = data.id;
    this.name = data.name;
    this.email = data.email;
    this.photo = "../../../../assets/image/user.png";
  }

  getAdministrador(){
    return {
      id: this.id,
      name: this.name,
      email: this.email,
      photo: this.photo
    };
  }

  setPhoto(photo: any){
    if(photo != null){
      this.photo = photo;
    } else {
      this.photo = "../../../../assets/image/user.png";
    }
  }
  
  getPhoto(){
    return this.photo;
  }
}
