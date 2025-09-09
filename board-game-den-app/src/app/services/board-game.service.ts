import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { catchError, Observable, of, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { BoardGameProductsResponse } from '../models/board-game.models';

@Injectable({
    providedIn: 'root'
})
export class BoardGameService {
    private readonly apiUrl = `${environment.apiBaseUrl}/api/products`;
    private cachedProducts: BoardGameProductsResponse | null = null;
    private cacheTimestamp: Date | null = null;
    private readonly CACHE_DURATION_MINUTES = 5; //5 minutes for now

    constructor(private http: HttpClient) { }

    private getHeaders(): HttpHeaders {
        return new HttpHeaders({
            'X-API-Key': environment.apiKey,
            'Content-Type': 'application/json'
        });
    }

    //Cache the results for time stipulated above 
    getProducts(forceRefresh: boolean = false): Observable<BoardGameProductsResponse> {
        // Check if we should use cache
        if (!forceRefresh && this.isCacheValid()) {
            console.log('Loading from cache...');
            return of(this.cachedProducts!);
        }

        console.log('Loading from API...');
        return this.fetchFromApi();
    }

    // Force a refresh from Api
    refreshProducts(): Observable<BoardGameProductsResponse> {
        return this.getProducts(true);
    }

    private fetchFromApi(): Observable<BoardGameProductsResponse> {

        const headers = this.getHeaders();

        return this.http.get<BoardGameProductsResponse>(this.apiUrl,{ headers }).pipe(
            tap(response => {
                // Store in the cache when Api call succeeds
                this.cachedProducts = response;
                this.cacheTimestamp = new Date();
                console.log('Data cached at:', this.cacheTimestamp);
            }),
            catchError(error => {
                console.error('API call failed:', error);

                // If we have cached data, return it as fallback
                if (this.cachedProducts) {
                    console.log('API failed, returning cached data as fallback');
                    return of(this.cachedProducts);
                }

                throw error;
            })
        );
    }

    private isCacheValid(): boolean {
        if (!this.cachedProducts || !this.cacheTimestamp) {
            return false;
        }

        const now = new Date();
        const diffInMinutes = (now.getTime() - this.cacheTimestamp.getTime()) / (1000 * 60);

        return diffInMinutes < this.CACHE_DURATION_MINUTES;
    }

    // Clear the cache manually    
    clearCache(): void {
        this.cachedProducts = null;
        this.cacheTimestamp = null;
        console.log('Cache cleared');
    }
}