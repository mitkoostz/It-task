import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { EventTableComponent } from './event-table/event-table.component';

const routes:Routes = [
  {path: '', component: EventTableComponent}

]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)

  ]
})
export class EventRoutingModule { }
