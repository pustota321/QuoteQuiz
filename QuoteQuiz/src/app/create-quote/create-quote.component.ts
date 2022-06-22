import { environment } from './../../environments/environment.prod';
import { HttpClient } from '@angular/common/http';
import { CreateQuoteDto, AuthorDto } from './../dtos/Dtos';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-create-quote',
  templateUrl: './create-quote.component.html',
  styleUrls: ['./create-quote.component.css']
})
export class CreateQuoteComponent implements OnInit {
  authors : AuthorDto[];
  authorFirstName : string;
  authorLastName : string;

  quoteForm = new FormGroup({
    content : new FormControl('', Validators.required)
  });
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get<AuthorDto[]>(environment.baseURL + "/api/author").subscribe(response => {
        this.authors = response;
    })
  }
  onAuthorChange(event){
    const authorInfo : string = event.target.value;
    const infoarray = authorInfo.split(' ')
    this.authorFirstName = infoarray[0];
    this.authorLastName = infoarray[1];
    
  }

  submit(){
    var data : CreateQuoteDto = {
      content : this.quoteForm.value['content'],
      authorFirstName: this.authorFirstName,
      authorLastName: this.authorLastName
    } 
    this.http.post(environment.baseURL + "/api/quote/create", data).subscribe(response => {
          window.location.reload();
    });
  }

}
