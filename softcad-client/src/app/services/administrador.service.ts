import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AdministradorService {

  id!: number;
  nome!: string;
  email!: string;
  foto!: any;

  constructor() { }

  setAdminPhoto(data: any, foto: any){
    this.id = data.id;
    this.nome = data.nome;
    this.email = data.email;
    this.foto = foto;
  }

  setAdministrador(data: any){
    this.id = data.id;
    this.nome = data.nome;
    this.email = data.email;
    this.foto = "../../../../assets/imagens/user.png";
  }

  getAdministrador(){
    return {
      id: this.id,
      nome: this.nome,
      email: this.email,
      foto: this.foto
    };
  }

  setPhoto(photo: any){
    if(photo != null){
      this.foto = photo;
    } else {
      this.foto = "../../../../assets/imagens/user.png";
    }
  }
  
  getPhoto(){
    return this.foto;
  }
}
