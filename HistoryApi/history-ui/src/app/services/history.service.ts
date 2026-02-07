import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HistoryDto } from '../models/history.dto';
import { HttpParams } from '@angular/common/http';

interface HistoryResponse {
  totalItems: number;
  page: number;
  pageSize: number;
  items: HistoryDto[];
}

@Injectable({ providedIn: 'root' })
@Injectable({ providedIn: 'root' })
export class HistoryService {
  private apiUrl = 'http://localhost:5051/api/Histories';

  constructor(private http: HttpClient) { }

  getHistory(page: number, pageSize: number, filters: any, sortField?: string | null,
    sortOrder?: 'ascend' | 'descend' | null) {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());


    if (filters.text) params = params.set('filterText', filters.text);
    if (filters.userId) params = params.set('filterUser', filters.userId);
    if (filters.dateFrom) params = params.set('dateFrom', filters.dateFrom);
    if (filters.eventTypeId) {
      params = params.set('filterEventType', filters.eventTypeId.toString());
    }
    if (sortField) params = params.set('sortBy', sortField);
    if (sortOrder) params = params.set('desc', sortOrder === 'descend' ? 'true' : 'false');




    return this.http.get<any>(this.apiUrl, { params });
  }

}
