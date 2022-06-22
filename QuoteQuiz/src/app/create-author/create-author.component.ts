import { AuthorDto } from './../dtos/Dtos';
import { environment } from './../../environments/environment.prod';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-create-author',
  templateUrl: './create-author.component.html',
  styleUrls: ['./create-author.component.css']
})
export class CreateAuthorComponent implements OnInit {
  authorForm = new FormGroup({
    firstName : new FormControl('', Validators.required),
    lastName : new FormControl('', Validators.required)
  });
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }
  submit(){
    const data : AuthorDto = {
      firstName: this.authorForm.value['firstName'],
      lastName: this.authorForm.value['lastName']
    }
    this.http.post(environment.baseURL +"/api/author", data).subscribe()
  }
}
