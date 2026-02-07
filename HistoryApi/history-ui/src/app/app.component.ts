import { Component } from '@angular/core';
import { HistoryTableComponent } from './page/historytable/history-table.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,                // обязательно
  imports: [CommonModule, HistoryTableComponent], // <- сюда добавляем компонент
  templateUrl: './app.component.html'
})
export class AppComponent
{
  title = 'history-ui';
}


