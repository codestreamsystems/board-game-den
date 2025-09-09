import { Routes } from '@angular/router';
import { BoardGamesComponent } from './components/board-games/board-games.component';

export const routes: Routes = [
  { path: '', component: BoardGamesComponent },
  { path: 'games', component: BoardGamesComponent },
  { path: '**', redirectTo: '' } // * route 404 pages
];