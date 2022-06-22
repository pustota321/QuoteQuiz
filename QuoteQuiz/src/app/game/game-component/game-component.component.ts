import { environment } from './../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthorDto, PlayerDto, Question } from 'src/app/dtos/Dtos';

@Component({
  selector: 'app-game-component',
  templateUrl: './game-component.component.html',
  styleUrls: ['./game-component.component.css']
})
export class GameComponent implements OnInit, OnDestroy {
  inBinaryMode : boolean = false;
  correctAnswers : number | undefined = +sessionStorage.getItem("score") || 0;
  question : Question;
  authors : AuthorDto[];
  binaryModeAuthor : AuthorDto;


  constructor(private http : HttpClient) {
    
  }

  private getRandomQuestion() {
    this.http.get<Question>(environment.baseURL + "/api/question/random").subscribe(response => {
      this.question = response;
      this.authors = this.question.authors.sort(() => Math.random() - 0.5); 
    });
  }

  private getRandomAuthor() : void{
    this.binaryModeAuthor = this.authors[Math.floor(Math.random()*this.authors.length)];
  }

  ngOnInit(): void {
    this.getRandomQuestion(); 
  }
  ngOnDestroy(): void {
    this.addScoreToPlayer();
  }

  changeMode(){
    this.getRandomAuthor();
    this.inBinaryMode = !this.inBinaryMode    
  }

  onYes(){
    if(this.binaryModeAuthor.isAnswer){
      this.correctAnswers++;
      sessionStorage.setItem('score', this.correctAnswers.toString())
    }
    this.addScoreToPlayer();
    this.getRandomQuestion();
    this.getRandomAuthor();
    
  }

  onNo(){
    if(!this.binaryModeAuthor.isAnswer){
      this.correctAnswers++;
      sessionStorage.setItem('score', this.correctAnswers.toString())
    }
    this.addScoreToPlayer();
    this.getRandomQuestion();
    this.getRandomAuthor();  
  } 
  onAuthorClick(isAnswer : boolean){
    if(isAnswer){
      this.correctAnswers++;
      sessionStorage.setItem('score', this.correctAnswers.toString())
    }
    this.addScoreToPlayer();
    this.getRandomQuestion();
    this.getRandomAuthor();
    
  }

  addScoreToPlayer(){
    const data: PlayerDto = {username : sessionStorage.getItem('username'), recordScore: +sessionStorage.getItem('score')}
    this.http.put(environment.baseURL + "/api/player/score", data ).subscribe((response) => {
            console.log(response);
            
    })
  }
}
