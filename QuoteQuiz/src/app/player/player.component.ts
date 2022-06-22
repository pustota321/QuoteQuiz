import { environment } from './../../environments/environment.prod';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { PlayerDto } from '../dtos/Dtos';


@Component({
  selector: 'app-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.css']
})
export class PlayerComponent implements OnInit {
  userForm = new FormGroup({
    username : new FormControl('')
  });


  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit(): void {
  }

  submitUsername(){
    const user : PlayerDto = {username: this.userForm.value.username}
    this.http.post<PlayerDto>(environment.baseURL + '/api/player', user).subscribe(response => {
      sessionStorage.setItem("username", response.username)
      sessionStorage.setItem("score", response.recordScore?.toString())
      this.router.navigate(["game"]);
   });
  }

}
