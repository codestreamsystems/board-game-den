import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BoardGameService } from '../../services/board-game.service';
import { BoardGameModel, BoardGameProductsResponse } from '../../models/board-game.models';
import { AgGridAngular } from 'ag-grid-angular';
import { ColDef, GridReadyEvent, GridApi } from 'ag-grid-community';

@Component({
  selector: 'app-board-games',
  standalone: true,
  imports: [CommonModule, AgGridAngular],
  templateUrl: './board-games.component.html',
  styleUrl: './board-games.component.css'
})

export class BoardGamesComponent implements OnInit {
  private gridApi!: GridApi;
  private allRowData: BoardGameModel[] = []; //Org data

  // Setup the AG Grid properties
  rowData: BoardGameModel[] = [];
  loading = false;
  error: string | null = null;
  totalCount = 0;
  fetchedAt: Date | null = null;

  constructor(private boardGameService: BoardGameService) { }

  ngOnInit(): void {
    this.loadBoardGames();
  }

  onGridReady(params: GridReadyEvent): void {
    this.gridApi = params.api;
    this.gridApi.sizeColumnsToFit();
  }

  colDefs: ColDef[] = [
    {
      field: 'name',
      headerName: 'Title',
      flex: 1,
      sortable: true,
      filter: true,
      cellStyle: { 'font-weight': '500' }
    }
  ];

  defaultColDef: ColDef = {
    resizable: true,
  };


  loadBoardGames(): void {
    this.loading = true;
    this.error = null;

    this.boardGameService.getProducts().subscribe({
      next: (response: BoardGameProductsResponse) => {
        this.rowData = response.products;
        this.totalCount = response.totalCount;
        this.fetchedAt = new Date(response.fetchedAt);
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading board games:', error);
        this.error = 'Failed to load board games. Please try again.';
        this.loading = false;
      }
    });
  }

  onSearch(searchTerm: string): void {
  if (searchTerm.trim()) {
    // Filter the orig data
    this.rowData = this.allRowData.filter(game => 
      game.name.toLowerCase().includes(searchTerm.toLowerCase())
    );
  } else {
    // Show all the data when search is txt is empty
    this.rowData = [...this.allRowData];
  }
}
}
