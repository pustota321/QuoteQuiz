import { environment } from './../../environments/environment.prod';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { PlayerDto } from '../dtos/Dtos';

@Component({
  selector: 'app-score-board',
  templateUrl: './score-board.component.html',
  styleUrls: ['./score-board.component.css']
})
export class ScoreBoardComponent implements OnInit {
  players : PlayerDto[];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get<[]>(environment.baseURL + "/api/player/all").subscribe(response => {
      this.players = response;
      console.log(response);
      
    })
  }

}
