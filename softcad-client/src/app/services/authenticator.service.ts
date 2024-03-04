import { Injectable } from '@angular/core';

import * as jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthenticatorService {

  private token!: string;
  private userid!: number | null;

  setToken(data: any){
    //let { token } = data;
    this.token = data;

    let { id } = JSON.parse(JSON.stringify(jwt_decode.jwtDecode(this.token)));
    this.userid = id;

    localStorage.setItem('token', this.token);
  }

  getToken(){
    let token = localStorage.getItem('token');
    if (this.token != null) {
      return this.token;
    } else if (token != null){
      this.token = token;
      return this.token;
    }
    return null;
  }

  getIdAdmin(){
    let token = localStorage.getItem('token');
    if(this.userid != null){
      return this.userid;
    } else if( token != null){
      var { id } = JSON.parse(JSON.stringify(jwt_decode.jwtDecode(token)));
      this.userid = id;
      return this.userid;
    }
    return null;
  }

  userAuthenticated(){
    let token = localStorage.getItem('token');
    if(token != null){
      return true;
    }
    return false;
  }
}
