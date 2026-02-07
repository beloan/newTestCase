import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { HistoryService } from '../../services/history.service';
import { FormsModule } from '@angular/forms';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzResizableModule } from 'ng-zorro-antd/resizable';


import { HistoryDto } from '../../models/history.dto';

type SortOrder = 'ascend' | 'descend' | null;


@Component({
  selector: 'app-history-table',
  standalone: true,
  imports: [CommonModule, FormsModule, NzTableModule, NzPaginationModule, NzInputModule, NzResizableModule],
  templateUrl: './history-table.component.html',
})
export class HistoryTableComponent implements OnInit {
  listOfData: any[] = [];
  total = 0;
  pageIndex = 1;
  pageSize = 10;
  loading = false;

  type NzSortOrder = 'ascend' | 'descend' | null;

   sortMap: { [key: string]: NzSortOrder } = {
     id: null,
       text: null,
         user: null,
           dt: null,
             eventtype: null
   };

   sortField: string | null = null;
   sortOrder: 'asc' | 'desc' | null = null;



  constructor(private historyService: HistoryService) { }

  ngOnInit(): void {
    this.loadData();
  }


  ///sort(field: string, order: 'ascend' | 'descend' | null) {
    ///this.sortField = field;
    ///this.sortOrder = order;
    ///this.loadData();
  ///}

  loadData(): void {
    this.loading = true;
    this.historyService.getHistory(this.pageIndex, this.pageSize, this.filters)
      .subscribe({
        next: res => {
          console.log(res);
          this.listOfData = res.items || [];
          this.total = res.totalItems;
          this.loading = false;
        },
        error: err => {
          console.error('API ERROR', err);
          this.loading = false;
        }
      });
  }

  onPageIndexChange(page: number): void {
    this.pageIndex = page;
    this.loadData();
  }

  get totalPages(): number {
    return Math.ceil(this.total / this.pageSize);
  }


  onPageSizeChange(size: number): void {
    this.pageSize = size;
    this.pageIndex = 1;
    this.loadData();
  }
  filters = {
    text: '',
    userId: '',
    eventTypeId:null,
    dateFrom: null,
    dateTo: null
  };

}

