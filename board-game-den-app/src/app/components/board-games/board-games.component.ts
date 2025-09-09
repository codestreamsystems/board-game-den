import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BoardGameService } from '../../services/board-game.service';
import { BoardGameModel, BoardGameProductsResponse } from '../../models/board-game.models';

@Component({
  selector: 'app-board-games',
  standalone: true,
  imports: [],
  templateUrl: './board-games.component.html',
  styleUrl: './board-games.component.css'
})
export class BoardGamesComponent {

}
