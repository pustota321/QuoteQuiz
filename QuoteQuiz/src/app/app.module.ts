import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { GameComponent } from './game/game-component/game-component.component';
import { PlayerComponent } from './player/player.component';
import { ScoreBoardComponent } from './score-board/score-board.component';
import { CreateQuoteComponent } from './create-quote/create-quote.component';
import { CreateAuthorComponent } from './create-author/create-author.component';
import { DeleteContentComponent } from './delete-content/delete-content.component';

@NgModule({
  declarations: [
    AppComponent,
    GameComponent,
    PlayerComponent,
    ScoreBoardComponent,
    CreateQuoteComponent,
    CreateAuthorComponent,
    DeleteContentComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, ReactiveFormsModule, 
    RouterModule.forRoot([
      {path: "", component: PlayerComponent},
      {path: "game", component: GameComponent},
      {path: "players", component: ScoreBoardComponent},
      {path: "create-quote", component:CreateQuoteComponent},
      {path: "create-author", component:CreateAuthorComponent},
      {path: "delete-content", component:DeleteContentComponent},
      {path:"**", component:PlayerComponent}
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
