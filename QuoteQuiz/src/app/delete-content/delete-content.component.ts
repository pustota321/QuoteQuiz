import { environment } from './../../environments/environment.prod';
import { HttpClient } from '@angular/common/http';
import { AuthorDto, QuoteDto } from './../dtos/Dtos';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-delete-content',
  templateUrl: './delete-content.component.html',
  styleUrls: ['./delete-content.component.css']
})
export class DeleteContentComponent implements OnInit {
  authors : AuthorDto[];
  quotes : QuoteDto[];
  constructor(private http : HttpClient) { }

  ngOnInit(): void {
    this.http.get<AuthorDto[]>(environment.baseURL +"/api/author").subscribe(response => {
      this.authors = response;
    })

    this.http.get<QuoteDto[]>(environment.baseURL +"/api/quote").subscribe(response => {
      this.quotes = response;
    })
  }
  onAuthorRowClick(id : number){
    this.http.delete(`${environment.baseURL}/api/author/${id}`).subscribe(() => {
      window.location.reload();
    })
  }
  onQuoteRowClick(id : number){
    this.http.delete(`${environment.baseURL}/api/quote/${id}`).subscribe(() => {
      window.location.reload();
    })
  }
}
