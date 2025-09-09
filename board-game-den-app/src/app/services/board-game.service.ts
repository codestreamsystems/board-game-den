import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { BoardGameProductsResponse } from '../models/board-game.models';

@Injectable({
  providedIn: 'root'
})
export class BoardGameService {
  private readonly apiUrl = `${environment.apiBaseUrl}/api/products`;

  constructor(private http: HttpClient) { }

  getProducts(): Observable<BoardGameProductsResponse> {
    return this.http.get<BoardGameProductsResponse>(this.apiUrl);
  }
}