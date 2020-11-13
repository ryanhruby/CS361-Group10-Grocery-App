import { Component, Inject, Injectable, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginService } from '../login.service';
import { DataService } from '../data.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

@Injectable({
  providedIn: 'root'
})
export class HomeComponent {

  // holds all the glists for the component
  public allUsers: User[];
  public login: number;
  public currentUser: User;
  
  constructor(private loginService: LoginService, private router: Router) {
    this.refreshTable();
  }

  //func to refresh table
  async refreshTable() {

    const result = await this.loginService.getAllUser().toPromise();
    this.allUsers = result;

  }

  verifyUser() {
    const emailForm = document.getElementById("email") as HTMLInputElement;
    const email = emailForm.value;
    const passwordForm = document.getElementById("password") as HTMLInputElement;
    const password = passwordForm.value;
    
    if (email.length > 0) {
      for (const user of this.allUsers) {
        if (email == user.email && password == user.password) {
          this.login = 1;
          this.router.navigate(['/user-menu']);
          this.currentUser = user;
          break;
        } else {
          this.login = 0;
        }
      }
    }
    
  }
  
 }

interface User {
  id: number;
  email: string;
  password: string;
}


