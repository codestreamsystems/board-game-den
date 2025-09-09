import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgGridAngular } from 'ag-grid-angular';
import { ColDef, GridReadyEvent, GridApi, ModuleRegistry, AllCommunityModule } from 'ag-grid-community';
import { BoardGameService } from '../../services/board-game.service';
import { BoardGameModel, BoardGameProductsResponse } from '../../models/board-game.models';

// Register AG Grid modules
ModuleRegistry.registerModules([AllCommunityModule]);

@Component({
  selector: 'app-board-games',
  standalone: true,
  imports: [CommonModule, AgGridAngular],
  templateUrl: './board-games.component.html',
  styleUrl: './board-games.component.css'
})
export class BoardGamesComponent implements OnInit {
  private gridApi!: GridApi;
  private allRowData: BoardGameModel[] = []; // Store original data

  // Setup the AG Grid properties
  rowData: BoardGameModel[] = [];
  loading = false;
  error: string | null = null;
  totalCount = 0;
  fetchedAt: Date | null = null;

  // Complete column definitions
  colDefs: ColDef[] = [
    { 
      field: 'thumbnail', 
      headerName: 'Image', 
      width: 120,
      sortable: false,
      filter: false,
      resizable: false,
      cellRenderer: (params: any) => {
        if (params.value) {
          return `
            <div class="flex items-center justify-center h-full py-2">
              <img 
                src="${params.value}" 
                alt="${params.data.name}" 
                class="w-16 h-12 object-cover rounded-md shadow-sm border border-gray-200" 
                onerror="this.src='${params.data.thumbnail2 || params.data.mainImage}'"
              />
            </div>
          `;
        }
        return '<div class="flex items-center justify-center h-full text-gray-400 text-sm">No Image</div>';
      }
    },
    { 
      field: 'name', 
      headerName: 'Product Title', 
      flex: 1,
      sortable: true,
      filter: true,
      cellRenderer: (params: any) => {
        return `
          <div class="flex items-center h-full">
            <div class="font-medium text-gray-900 truncate pr-2">${params.value}</div>
          </div>
        `;
      }
    },
    { 
      field: 'salePrice', 
      headerName: 'Sale Price', 
      width: 140,
      sortable: true,
      filter: 'agNumberColumnFilter',
      cellRenderer: (params: any) => {
        return `
          <div class="flex items-center justify-end h-full">
            <span class="text-gray-700 font-mono text-sm">$${params.value.toFixed(2)}</span>
          </div>
        `;
      }
    },
    { 
      field: 'ourPrice', 
      headerName: 'Our Sale Price', 
      width: 160,
      sortable: true,
      filter: 'agNumberColumnFilter',
      cellRenderer: (params: any) => {
        return `
          <div class="flex items-center justify-end h-full">
            <div class="bg-green-50 border border-green-200 rounded-md px-3 py-1">
              <span class="text-green-800 font-semibold font-mono text-sm">$${params.value.toFixed(2)}</span>
            </div>
          </div>
        `;
      }
    }
  ];

  defaultColDef: ColDef = {
    resizable: true,
  };

  constructor(private boardGameService: BoardGameService) { }

  ngOnInit(): void {
    this.loadBoardGames();
  }

  onGridReady(params: GridReadyEvent): void {
    this.gridApi = params.api;
    this.gridApi.sizeColumnsToFit();
  }

  loadBoardGames(): void {
    this.loading = true;
    this.error = null;

    this.boardGameService.getProducts().subscribe({
      next: (response: BoardGameProductsResponse) => {
        this.allRowData = response.products; // ✅ Store original data here!
        this.rowData = response.products;     // ✅ Display data
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
      // Filter the original data
      this.rowData = this.allRowData.filter(game => 
        game.name.toLowerCase().includes(searchTerm.toLowerCase())
      );
    } else {
      // Show all data when search is empty
      this.rowData = [...this.allRowData];
    }
  }
}